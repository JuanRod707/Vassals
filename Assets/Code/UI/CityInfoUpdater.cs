using UnityEngine;

public class CityInfoUpdater : MonoBehaviour
{
    public LabelWrite lblName;
    public LabelWrite lblPopulation;
    public LabelWrite lblLord;
    public LabelWrite lblIncome;
    public LabelWrite lblUnrest;

    public void PopulateFields(CityInfo data)
    {
        this.gameObject.SetActive(true);
        lblName.WriteLabel(data.CityName);
        lblLord.WriteLabel(data.LordName);
        lblPopulation.WriteLabel(data.Population);
        lblIncome.WriteLabel(data.Income);
        lblUnrest.WriteLabel(data.Unrest);
    }
}
