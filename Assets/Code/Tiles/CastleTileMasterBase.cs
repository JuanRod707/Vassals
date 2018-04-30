using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class CastleTileMasterBase : MonoBehaviour
{
    public GameObject Towers;
    public GameObject Walls;
    public GameObject Gates;

    public GameObject[] Houses;
    public GameObject Village;

    public Material CityStreetMat;
    public Material TownStreetMat;

    public int MaxHouses;
    public int MaxGates;
    public int WaterLevel;

    public List<TileInfo> Territory; 
    public Color HouseColor;
    public TileInfo TileRef;

    protected GameObject mainBuilding;
    protected int villages;
    
    protected virtual void ErectMainBuilding()
    {

    }

    protected virtual void CleanTile()
    {
        var tile = BoardManager.FindTile(TileRef.Coord);
        if (tile.Feature == TileFeature.Village)
        {
            Destroy(tile.Village);
            tile.Village = null;
        }

        tile.Feature = TileFeature.City;
    }

    protected void RandomizeGates()
    {
        Gates.GetComponent<EdgeManager>().HideAll();

        var gatesToGen = Random.Range(1, MaxGates);
        var rolledSides = new List<int>();

        for (int i = 0; i < gatesToGen; i++)
        {
            var side = Random.Range(0, 8);
            while (rolledSides.Contains(side) || side == 2 || side == 6)
            {
                side = Random.Range(0, 8);
            }

            var wall = Walls.GetComponent<EdgeManager>().GetEdge((Compass)side);
            var gate = Gates.GetComponent<EdgeManager>().GetEdge((Compass)side);

            wall.SetActive(false);
            gate.SetActive(true);
        }
    }

    protected void ErectHouses()
    {
        var buildings = new List<GameObject>();
        buildings.Add(mainBuilding);

        for (int i = 0; i < MaxHouses; i++)
        {
            var posInCircle = Random.insideUnitCircle;

            while (posInCircle.magnitude < 0.3f || posInCircle.magnitude > 0.7f)
            {
                posInCircle = Random.insideUnitCircle;
            }
            
            var pos = new Vector3(transform.position.x + posInCircle.x, transform.position.y,
                transform.position.z + posInCircle.y);

            var houseObj = Houses[Random.Range(0, Houses.Length)];
            var house = Instantiate(houseObj, pos, Quaternion.identity) as GameObject;
            
            var colliders =
                buildings.Where(
                    x => house.GetComponent<BoxCollider>().bounds.Intersects(x.GetComponent<BoxCollider>().bounds));

            int tries = 20;
            while (colliders.Any() && tries > 0)
            {
                while (posInCircle.magnitude < 0.3f || posInCircle.magnitude > 0.8f)
                {
                    posInCircle = Random.insideUnitCircle;
                }

                pos = new Vector3(transform.position.x + posInCircle.x, transform.position.y,
                transform.position.z + posInCircle.y);
                house.transform.position = pos;
                colliders =
                buildings.Where(
                    x => house.GetComponent<BoxCollider>().bounds.Intersects(x.GetComponent<BoxCollider>().bounds));
                tries--;
            }

            if (tries > 0)
            {
                buildings.Add(house);
                house.transform.Rotate(new Vector3(0f, Random.Range(0f, 360f), 0f));
                house.transform.SetParent(this.transform);
            }
            else
            {
                GameObject.Destroy(house);
            }
        }
    }

    public IEnumerable<Compass> GetNeighbouringCities()
    {
        var result = new List<Compass>();

        var dirsToCheck = new[]
        {Compass.West, Compass.NorthWest, Compass.NorthEast, Compass.East, Compass.SouthEast, Compass.SouthWest};

        foreach (var dir in dirsToCheck)
        {
            var nCoords = HexGridHelper.FindNeighbour(TileRef.Coord, dir);
            if (BoardManager.IsTileCity(nCoords))
            {
                result.Add(dir);
            }
        }

        return result;
    }

    public void TearDownWall(Compass side)
    {
        Debug.Log(string.Format("Tearing down wall on the {0}, my coordinates are {1}, {2}", side, TileRef.XCoord, TileRef.YCoord));
        Walls.GetComponent<EdgeManager>().GetEdge(side).SetActive(false);
        Gates.GetComponent<EdgeManager>().GetEdge(side).SetActive(false);
    }

    public void ReactToExpansion()
    {
        var neighbours = GetNeighbouringCities();

        var adjacentNeighbours = HexGridHelper.GetAdjacentSides(neighbours);

        foreach (var an in adjacentNeighbours)
        {
            Towers.GetComponent<VertexManager>().GetVertexFromTwoSides(an.Value1, an.Value2).SetActive(false);
        }

        foreach (var n in neighbours)
        {
            Walls.GetComponent<EdgeManager>().GetEdge(n).SetActive(false);
            Gates.GetComponent<EdgeManager>().GetEdge(n).SetActive(false);
        }
    }

    public void SetTerritoryColor()
    {
        foreach (var tile in Territory)
        {
            if (tile.Feature != TileFeature.Mountain && tile.Feature != TileFeature.River && tile.Feature != TileFeature.Forest && tile.Height > -5)
            {
                tile.GetComponent<TerritorySetter>().SetTerritoryColor(this.HouseColor);
            }
        }
    }

    public virtual void CreateVillages()
    {
        var neighbours = HexGridHelper.FindAllNeighbours(TileRef.Coord);
        TileRef.UpdateTileGroundMaterial(CityStreetMat);

        foreach (var n in neighbours)
        {
            var tile = BoardManager.FindTile(n);

            if (tile != null && tile.Feature == TileFeature.None && tile.Height <= TileRef.Height + 1 && tile.Height > WaterLevel)
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
}
