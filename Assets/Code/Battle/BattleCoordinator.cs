using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleCoordinator : MonoBehaviour
{
    public ArmyMaster ArmyL;
    public ArmyMaster ArmyR;
    public float TurnDuration;
    public AmbienceSound Ambience;

    public RectTransform VictoryPanel;
    public LabelWrite VictoryMessage;

    private ArmyMaster CurrentArmyTurn;
    private float TurnDurationElapsed = 0f;
    private bool IsBattleOver;

    private ArmyMaster CurrentEnemyArmy
    {
        get { return CurrentArmyTurn == ArmyR ? ArmyL : ArmyR; }
    }

    private void Start()
    {
        CurrentArmyTurn = ArmyL;
    }

    private void SwitchTurn()
    {
        CurrentArmyTurn = CurrentArmyTurn == ArmyR ? ArmyL : ArmyR;
    }

    private void PerformAttack()    
    {
        CurrentArmyTurn.Attack(CurrentEnemyArmy.TotalUnits);
    }

    public bool AttackOnUnit(int index)
    {
        return CurrentEnemyArmy.IsUnitKilled(index);
    }

    void FixedUpdate()
    {
        TurnDurationElapsed += Time.deltaTime;

        if (TurnDurationElapsed > TurnDuration && !IsBattleOver)
        {
            PerformAttack();
            SwitchTurn();
            CurrentArmyTurn.UpdateUI();
            CurrentEnemyArmy.UpdateUI();
            TurnDurationElapsed = 0f;

            if (!CurrentArmyTurn.CanKeepFighting)
            {
                CurrentArmyTurn.Retreat();
            }
            if (!CurrentEnemyArmy.CanKeepFighting)
            {
                CurrentEnemyArmy.Retreat();
            }
        }

        if(!IsBattleOver & (!CurrentArmyTurn.CanKeepFighting || !CurrentEnemyArmy.CanKeepFighting))
        {
            var victor = CurrentArmyTurn.CanKeepFighting ? CurrentArmyTurn : CurrentEnemyArmy;

            victor.Cheer();
            IsBattleOver = true;
            Ambience.EndBattleVictory();
            VictoryPanel.gameObject.SetActive(true);
            VictoryMessage.WriteLabel(victor.ArmyName);
        }
    }

    public void ChangeBattleSpeed(float speed)
    {
        TurnDuration = speed;
    }
}
