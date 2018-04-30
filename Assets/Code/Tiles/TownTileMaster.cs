using UnityEngine;
using System.Collections.Generic;

public class TownTileMaster : CastleTileMasterBase
{
    public Transform Hall;
    public Transform Wall;
    public GameObject LineCaller;
    public GameObject HouseCastleRef;
    public int MaxVillages;
    public TownInfo TownInfo;

    // Use this for initialization
    void Start()
    {
        this.transform.SetParent(GameObject.Find("PopTiles").transform);
        this.Territory = new List<TileInfo>();
        this.TileRef = this.GetComponent<MapReference>().TileRef;
        Debug.Log(string.Format("Placed town on {0}, {1}", TileRef.XCoord, TileRef.YCoord));
        TownInfo = this.GetComponent<TownInfo>();
        CleanTile();
        ErectMainBuilding();
        ErectHouses();
        CreateVillages();

        TownInfo.GenerateBaseTown(villages);
    }

    protected override void CleanTile()
    {
        var tile = BoardManager.FindTile(TileRef.Coord);
        if (tile.Feature == TileFeature.Village)
        {
            Destroy(this.gameObject);
        }

        tile.Feature = TileFeature.Town;
    }

    protected override void ErectMainBuilding()
    {
        Hall.transform.Rotate(new Vector3(0f, Random.Range(0f, 360f), 0f));
        Wall.transform.Rotate(new Vector3(0f, Random.Range(0f, 360f), 0f));
        mainBuilding = Hall.gameObject;
    }

    public override void CreateVillages()
    {
        TileRef.UpdateTileGroundMaterial(CityStreetMat);
        var neighbours = HexGridHelper.FindAllNeighbours(TileRef.Coord);

        foreach (var n in neighbours.PickSome(MaxVillages))
        {
            var tile = BoardManager.FindTile(n);

            if (tile != null && tile.Feature == TileFeature.None && tile.Height <= TileRef.Height && tile.Height > WaterLevel)
            {
                var village = Instantiate(Village, tile.transform.position, Quaternion.identity) as GameObject;
                tile.Feature = TileFeature.Village;
                tile.Village = village;
                tile.UpdateTileGroundMaterial(TownStreetMat);
                village.GetComponent<VillageTileManager>().TileRef = tile;
                villages++;
            }
        }
    }

    public void DrawLine(GameObject city)
    {
        var line = LineCaller.GetComponent<LineRenderer>();

        var origin = this.transform.position;
        origin.y += 1;
        var target = city.transform.position;
        target.y += 1;

        line.SetPosition(0, origin);
        line.SetPosition(1, target);
    }

    public void AssignHouse(GameObject city)
    {
        HouseCastleRef = city;
        city.GetComponent<CitadelTileMaster>().AddTown(this.gameObject);
        DrawLine(city);
        HouseColor = city.GetComponent<CitadelTileMaster>().HouseColor;
    }
}
