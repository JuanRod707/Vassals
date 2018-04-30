using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class TownPlacer : MonoBehaviour
{
    public GameObject TownPrefab;
    public int TownCount;
    public int TownDistance;
    public int Tries;

    private BoardHelper helper;
    private List<TileInfo> existingTowns;
    private int currentCityDistance;
    private int placedTowns = 0;

    // Use this for initialization
    void Start()
    {
        helper = this.GetComponent<BoardHelper>();
        existingTowns = new List<TileInfo>();
        currentCityDistance = TownDistance;
    }

    public void PlaceNextTown()
    {
        var target = new Coordinate(Random.Range(0, helper.Size), Random.Range(0, helper.Size));
        var tries = Tries;

        while (!IsTilePossibleTown(target))
        {
            target = new Coordinate(Random.Range(0, helper.Size), Random.Range(0, helper.Size));
            tries--;

            if (tries <= 0)
            {
                currentCityDistance--;
                tries = Tries;
            }
        }

        currentCityDistance = TownDistance;
        CreateBasicCity(target);
        placedTowns++;

        if (placedTowns == TownCount)
        {
            this.GetComponent<MapGen>().AdvancePhase();
        }
    }

    public void PlaceAllTowns()
    {
        for (int i = 0; i < TownCount; i++)
        {
            var target = new Coordinate(Random.Range(0, helper.Size), Random.Range(0, helper.Size));
            var tries = Tries;

            while (!IsTilePossibleTown(target))
            {
                target = new Coordinate(Random.Range(0, helper.Size), Random.Range(0, helper.Size));
                tries--;

                if (tries <= 0)
                {
                    currentCityDistance--;
                    tries = Tries;
                }
            }

            currentCityDistance = TownDistance;
            CreateBasicCity(target);
        }
    }

    private void CreateBasicCity(Coordinate coord)
    {
        var tileInfo = helper.FindTile(coord);
        var town = Instantiate(TownPrefab, tileInfo.transform.position, Quaternion.identity) as GameObject;
        var townMaster = town.GetComponent<TownTileMaster>();
        town.GetComponent<MapReference>().TileRef = tileInfo;
        tileInfo.CityRef = town;
        existingTowns.Add(tileInfo);

        townMaster.AssignHouse(FindClosestCity(coord));
        BoardManager.TownList.Add(town);        
    }

    private bool IsTilePossibleTown(Coordinate coord)
    {
        var tile = helper.FindTile(coord);
        var existingCities = BoardManager.CityList.Select(x => x.GetComponent<MapReference>().TileRef);
        return tile != null && !BoardManager.IsBorderTile(coord) && tile.Height > helper.WaterLevel && tile.Feature == TileFeature.None &&
               existingTowns.All(
                   x =>
                       Mathf.Abs(x.XCoord - tile.XCoord) > currentCityDistance &&
                       Mathf.Abs(x.YCoord - tile.YCoord) > currentCityDistance);
    }

    private GameObject FindClosestCity(Coordinate coord)
    {
        return
            BoardManager.CityList.OrderBy(x => x.GetComponent<MapReference>().TileRef.Coord.DistanceTo(coord)).First();
    }
}
