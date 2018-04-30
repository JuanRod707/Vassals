using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TreasuryControl : MonoBehaviour
{
    public int Income;
    public int Tax;
    public int Expenses;
    public int Treasury;

    public LabelWrite lblIncome;
    public LabelWrite lblTax;
    public LabelWrite lblExpenses;
    public LabelWrite lblTreasury;

    public Slider slider;
    public RecruitmentControl RecruitPanel;

    private int TaxValue;

    void Start ()
    {
        RecalculateExpenses();
        RecruitPanel.UpdateLowerLayout(Expenses);
    }

    public void RecalculateExpenses()
    {
        var floatExpense = Income * slider.value;
        Expenses = (int)floatExpense;
        TaxValue = (int) ((Income*Tax)/100);
        Treasury = Income - Expenses - TaxValue;
        UpdateLayout();
        RecruitPanel.UpdateLowerLayout(Expenses);
    }

    public void UpdateLayout()
    {
        lblIncome.WriteLabel(Income);
        lblTax.WriteLabel(Tax, TaxValue);
        lblExpenses.WriteLabel(Expenses);
        var netTreasury = Treasury >= 0 ? string.Format("+{0}", Treasury) : Treasury.ToString();
        lblTreasury.WriteLabel(netTreasury);
    }
}

