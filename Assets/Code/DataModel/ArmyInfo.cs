using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class ArmyInfo : MonoBehaviour
{
    public string General;
    public int Morale;
    public int Spearmen;
    public int Archers;
    public int Knights;
    public int Strength;
    public LabelWrite Ribbon;

    // Use this for initialization
    private void Start()
    {
        GenerateArmy();
        UpdateRibbon();
    }

    private void GenerateArmy()
    {
        General = NameGenerator.GenerateGeneralName();
        Morale = Random.Range(75, 101);
        Spearmen = Random.Range(10, 181);
        Archers = Random.Range(10, 151);
        Knights = Random.Range(4, 61);
        Strength = CalculateArmyStrength();
    }

    private int CalculateArmyStrength()
    {
        var unit = new Unit(UnitType.Footman);
        var result = 0;
        result += Spearmen * unit.StrengthValue;

        unit = new Unit(UnitType.Archer);
        result += Archers * unit.StrengthValue;

        unit = new Unit(UnitType.Knight);
        result += Knights * unit.StrengthValue;

        return result;
    }

    public void UpdateRibbon()
    {
        this.Ribbon.WriteLabel(this.Strength);
    }
}
