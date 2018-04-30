using UnityEngine;
using System.Collections;

public class DistrictInfo : MonoBehaviour
{
    public int Population;
    public int MaxPopPerTile;

    public void GenerateBaseDistrict(int villages)
    {
        this.Population = CalculatePopulation(villages);
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
}
