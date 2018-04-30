using UnityEngine;
using System.Collections;

public class CityInfo : MonoBehaviour
{
    public string CityName;
    public int Population;
    public string LordName;
    public int Income;
    public int Unrest;
    public LabelWrite Ribbon;

    public int SerfToRichRatio;
    public int SingleSerfIncome;
    public int SingleNobleIncome;
    public int MaxPopPerTile;

    public void GenerateBaseCity(int villages)
    {
        this.CityName = NameGenerator.GenerateCityName();
        Ribbon.WriteLabel(this.CityName);
        this.LordName = NameGenerator.GenerateLordName();
        this.Population = CalculatePopulation(villages);
        this.Income = CalculateIncome();
        this.Unrest = 6;
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
        var serfs = (Population/(SerfToRichRatio + 1))* SerfToRichRatio;
        var noblemen = Population - serfs;
        return serfs*SingleSerfIncome + noblemen*SingleNobleIncome;
    }

    public void UpdateInfo(DistrictInfo dInfo)
    {
        Population -= Random.Range((MaxPopPerTile / 4) * 3, MaxPopPerTile);
        Population += dInfo.Population;
        Income = CalculateIncome();
        Unrest += 6;

        GameObject.Find("Sidebar").GetComponent<SidebarController>().UpdateCityInfo(this);
    }
}
