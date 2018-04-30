using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ArmyMaster : MonoBehaviour
{
    public BattleCoordinator Coordinator;
    public MoraleCounter MoraleCounter;
    public Text lblStrength;
    public Text lblKnights;
    public Text lblSpearmen;
    public Text lblArchers;
    public LabelWrite lblHouse;
    public int Morale;
    public int ArmyStrength;
    public string ArmyName;

    public int TotalUnits
    {
        get { return ActiveUnits.Count; }
    }
    public bool CanKeepFighting
    {
        get { return ActiveUnits.Count > 0 & Morale > 0; }
    }

    private IList<Unit> ActiveUnits;
    private int CurrentTurnUnitId;
    
    private void Start()
    {
        var gen = this.GetComponent<ArmyGenerator>();

        ActiveUnits = gen.SmartGenerateArmy(ArmyStrength);
        gen.SetUpArmySmart(ActiveUnits);
        UpdateUI();
        lblHouse.WriteLabel(this.ArmyName);
    }

    private void RemoveFromBattlefield(UnitType type)
    {
        var target = this.transform.GetComponentsInChildren<UnitCharge>().Where(x => x.TypeOfUnit == type).PickOne();
        Destroy(target.gameObject);
    }

    public bool IsUnitKilled(int id)
    {
        if (!ActiveUnits.Any())
        {
            return false;
        }

        if (id >= ActiveUnits.Count)
        {
            id = ActiveUnits.Count - 1;
        }

        var target = ActiveUnits[id];
        
        switch (target.UnitType)
        {
            case UnitType.Footman:
                var hit = Random.Range(0, 100);
                if (hit > 60)
                {
                    return false;
                }
                
                ActiveUnits.RemoveAt(id);
                Morale -= target.MoraleCost;
                MoraleCounter.UpdateMorale(Morale);
                RemoveFromBattlefield(target.UnitType);
                return true;

            case UnitType.Archer:
                ActiveUnits.RemoveAt(id);
                Morale -= target.MoraleCost;
                MoraleCounter.UpdateMorale(Morale);
                RemoveFromBattlefield(target.UnitType);
                return true;

            case UnitType.Knight:
                ActiveUnits.RemoveAt(id);
                Morale -= target.MoraleCost;
                MoraleCounter.UpdateMorale(Morale);
                RemoveFromBattlefield(target.UnitType);
                return true;
        }


        return false;
    }

    public void Attack(int maxIndex)
    {
        if (CurrentTurnUnitId >= ActiveUnits.Count)
        {
            CurrentTurnUnitId = 0;
        }

        var turn = ActiveUnits[CurrentTurnUnitId];
        var indx = Random.Range(0, maxIndex);
        bool attackResult = false;

        switch (turn.UnitType)
        {
            case UnitType.Footman:
                attackResult = Coordinator.AttackOnUnit(indx);
                break;

            case UnitType.Archer:
                attackResult = Coordinator.AttackOnUnit(indx);
                break;

            case UnitType.Knight:
                attackResult = Coordinator.AttackOnUnit(indx);
                
                if (attackResult)
                {
                    Debug.Log("Knight single Kill");
                    Morale++;
                    attackResult = Coordinator.AttackOnUnit(indx);
                }
                break;
        }

        if (attackResult)
        {
            Debug.Log(string.Format("Kill for {0} by {1}", ArmyName, turn.UnitType));
            Morale++;
        }

        MoraleCounter.UpdateMorale(Morale);

        CurrentTurnUnitId++;
    }

    public void UpdateUI()
    {
        MoraleCounter.UpdateMorale(Morale);
        lblArchers.text = ActiveUnits.Count(x => x.UnitType == UnitType.Archer).ToString();
        lblKnights.text = ActiveUnits.Count(x => x.UnitType == UnitType.Knight).ToString();
        lblSpearmen.text = ActiveUnits.Count(x => x.UnitType == UnitType.Footman).ToString();
        lblStrength.text = CalculateArmyStrength().ToString();
    }

    public void Retreat()
    {
        foreach (var u in this.GetComponentsInChildren<UnitCharge>())
        {
            u.Retreat();
        }
    }

    public void Cheer()
    {
        foreach (var u in this.GetComponentsInChildren<UnitCharge>())
        {
            u.Cheer();
        }
    }

    private int CalculateArmyStrength()
    {
        return ActiveUnits.Sum(u => u.StrengthValue);
    }
}
