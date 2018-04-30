using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class CityPlacer : MonoBehaviour
{
    public GameObject CityPrefab;
    public int HousesCount;
    public int CityDistance;
    public int Tries;

    private BoardHelper helper;
    private List<TileInfo> existingCities;
    private int currentCityDistance;

    private int placedCities = 0;

    // Use this for initialization
    void Start()
    {
        helper = this.GetComponent<BoardHelper>();
        existingCities = new List<TileInfo>();
        currentCityDistance = CityDistance;
    }

    public void PlaceNextCity()
    {
        var target = new Coordinate(Random.Range(0, helper.Size), Random.Range(0, helper.Size));
        var tries = Tries;

        while (!IsTilePossibleCity(target))
        {
            target = new Coordinate(Random.Range(0, helper.Size), Random.Range(0, helper.Size));
            tries--;

            if (tries <= 0)
            {
                currentCityDistance--;
                tries = Tries;
            }
        }

        currentCityDistance = CityDistance;
        CreateBasicCity(target);
        placedCities++;

        if (placedCities == HousesCount)
        {
            this.GetComponent<MapGen>().AdvancePhase();
        }
    }

    public void PlaceAllCities()
    {
        for (int i = 0; i < HousesCount; i++)
        {
            var target = new Coordinate(Random.Range(0, helper.Size), Random.Range(0, helper.Size));
            var tries = Tries;

            while (!IsTilePossibleCity(target))
            {
                target = new Coordinate(Random.Range(0, helper.Size), Random.Range(0, helper.Size));
                tries--;

                if (tries <= 0)
                {
                    currentCityDistance--;
                    tries = Tries;
                }
            }

            currentCityDistance = CityDistance;
            CreateBasicCity(target);
        }
    }

    private void CreateBasicCity(Coordinate coord)
    {
        var tileInfo = helper.FindTile(coord);
        var city = Instantiate(CityPrefab, tileInfo.transform.position, Quaternion.identity) as GameObject;
        city.GetComponent<MapReference>().TileRef = tileInfo;
        tileInfo.CityRef = city;
        existingCities.Add(tileInfo);
        BoardManager.CityList.Add(city);
    }

    private void CreateBasicCityExpansion(Coordinate coord)
    {
        var tileInfo = helper.FindTile(coord);
        var city = Instantiate(CityPrefab, tileInfo.transform.position, Quaternion.identity) as GameObject;
        city.GetComponent<MapReference>().TileRef = tileInfo;
        tileInfo.CityRef = city;
    }

    private void CreateCapital(Coordinate coord)
    {
        var tileInfo = helper.FindTile(coord);
        var city = Instantiate(CityPrefab, tileInfo.transform.position, Quaternion.identity) as GameObject;
        city.GetComponent<MapReference>().TileRef = tileInfo;
        tileInfo.CityRef = city;
        
        var neighbours = HexGridHelper.FindAllNeighbours(coord);
        foreach (var n in neighbours)
        {
            if (IsTilePossibleCity(n))
            {
                CreateBasicCityExpansion(n);
            }
        }

        existingCities.Add(tileInfo);
    }

    private bool IsTilePossibleCity(Coordinate coord)
    {
        var tile = helper.FindTile(coord);
        return tile != null && !BoardManager.IsBorderTile(coord) && tile.Height > helper.WaterLevel && tile.Feature == TileFeature.None &&
               existingCities.All(
                   x =>
                       Mathf.Abs(x.XCoord - tile.XCoord) > currentCityDistance &&
                       Mathf.Abs(x.YCoord - tile.YCoord) > currentCityDistance);
    }
}
