using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    public int Initiative;
    public UnitType UnitType;
    public int MoraleCost;
    public int StrengthValue;
    public int GoldCost;

    public Unit(UnitType type)
    {
        switch (type)
        {
            case UnitType.Archer:
                this.Initiative = 3;
                this.MoraleCost = 1;
                this.StrengthValue = 1;
                this.GoldCost = 30;
                break;
            case UnitType.Footman:
                this.Initiative = 1;
                this.MoraleCost = 2;
                this.StrengthValue = 3;
                this.GoldCost = 20;
                break;
            case UnitType.Knight:
                this.Initiative = 2;
                this.MoraleCost = 2;
                this.StrengthValue = 4;
                this.GoldCost = 80;
                break;
        }

        this.UnitType = type;
    }
}
