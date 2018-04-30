using UnityEngine;
using System.Collections;

public class TownInfo : MonoBehaviour
{
    public string TownName;
    public int Population;
    public int Income;
    
    public int SinglePopIncome;
    public int MaxPopPerTile;

    public void GenerateBaseTown(int villages)
    {
        this.TownName = NameGenerator.GenerateTownName();
        this.Population = CalculatePopulation(villages);
        this.Income = CalculateIncome();
    }

    private int CalculatePopulation(int villages)
    {
        var pop = 0;
        for (int i = 0; i <= villages; i++)
        {
            pop += Random.Range((MaxPopPerTile/4)*3, MaxPopPerTile);
        }

        return pop;
    }

    private int CalculateIncome()
    {
        return Population*SinglePopIncome;
    }

    public void UpdateInfo(DistrictInfo dInfo)
    {
        Population -= Random.Range((MaxPopPerTile / 4) * 3, MaxPopPerTile);
        Population += dInfo.Population;
        Income = CalculateIncome();

        GameObject.Find("Sidebar").GetComponent<SidebarController>().UpdateTownInfo(this);
    }
}
