using UnityEngine;

public class TownInfoUpdater : MonoBehaviour
{
    public LabelWrite lblName;
    public LabelWrite lblPopulation;
    public LabelWrite lblIncome;

    public void PopulateFields(TownInfo data)
    {
        this.gameObject.SetActive(true);
        lblName.WriteLabel(data.TownName);
        lblPopulation.WriteLabel(data.Population);
        lblIncome.WriteLabel(data.Income);
    }
}
