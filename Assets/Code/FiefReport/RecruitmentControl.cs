using UnityEngine;
using System.Collections;

public class RecruitmentControl : MonoBehaviour
{
    public int InfantryPct;
    public int ArchersPct;
    public int CavalryPct;

    public LabelWrite lblInfantryPct;
    public LabelWrite lblArchersPct;
    public LabelWrite lblCavalryPct;

    public LabelWrite lblInfantryNum;
    public LabelWrite lblArchersNum;
    public LabelWrite lblCavalryNum;

    private int expense;

    void Start()
    {
        UpdateLayout();
    }

    public void UpdateLayout()
    {
        lblInfantryPct.WriteLabel(InfantryPct);
        lblArchersPct.WriteLabel(ArchersPct);
        lblCavalryPct.WriteLabel(CavalryPct);

        UpdateLowerLayout(expense);
    }

    public void UpdateLowerLayout(int exp)
    {
        this.expense = exp;

        var infBudget = (expense*InfantryPct)/100;
        var arcBudget = (expense * ArchersPct) / 100;
        var cavBudget = (expense * CavalryPct) / 100;

        var infNum = (int) (infBudget / new Unit(UnitType.Footman).GoldCost);
        var arcNum = (int)(arcBudget / new Unit(UnitType.Archer).GoldCost);
        var cavNum = (int)(cavBudget / new Unit(UnitType.Knight).GoldCost);

        lblInfantryNum.WriteLabel(infNum);
        lblArchersNum.WriteLabel(arcNum);
        lblCavalryNum.WriteLabel(cavNum);
    }

    public void AddInfantry()
    {
        if(InfantryPct < 100)
        {
            if (ArchersPct == CavalryPct)
            {
                InfantryPct += 10;
                ArchersPct -= 5;
                CavalryPct -= 5;
            }
            else
            {
                InfantryPct += 5;
                if (ArchersPct > CavalryPct)
                {
                    ArchersPct -= 5;
                }
                else
                {
                    CavalryPct -= 5;
                }
            }
        }

        UpdateLayout();
    }

    public void AddArchers()
    {
        if (ArchersPct < 100)
        {
            if (InfantryPct == CavalryPct)
            {
                ArchersPct += 10;
                InfantryPct -= 5;
                CavalryPct -= 5;
            }
            else
            {
                ArchersPct += 5;
                if (InfantryPct > CavalryPct)
                {
                    InfantryPct -= 5;
                }
                else
                {
                    CavalryPct -= 5;
                }
            }
        }

        UpdateLayout();
    }

    public void AddCavalry()
    {
        if (CavalryPct < 100)
        {
            if (ArchersPct == InfantryPct)
            {
                CavalryPct += 10;
                ArchersPct -= 5;
                InfantryPct -= 5;
            }
            else
            {
                CavalryPct += 5;
                if (ArchersPct > InfantryPct)
                {
                    ArchersPct -= 5;
                }
                else
                {
                    InfantryPct -= 5;
                }
            }
        }

        UpdateLayout();
    }
}
