using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DistrictTileMaster : CastleTileMasterBase
{
    public GameObject[] MainBuildings;
    public GameObject CitadelRef;
    private DistrictInfo DistrictInfo;

    // Use this for initialization
    void Start()
    {
        this.TileRef = this.GetComponent<MapReference>().TileRef;
        Debug.Log(string.Format("Placed expansion on {0}, {1}", TileRef.XCoord, TileRef.YCoord));
        DistrictInfo = this.GetComponent<DistrictInfo>();
        CleanTile();
        RandomizeGates();
        //ErectMainBuilding();
        ErectHouses();
        ExpandOnNeighbours();
        CreateVillages();
        DistrictInfo.GenerateBaseDistrict(villages);
        FindCitadel();
        CitadelRef.GetComponent<CitadelTileMaster>().UpdateCityInfo(DistrictInfo);
    }

    protected override void ErectMainBuilding()
    {
        mainBuilding =
            Instantiate(MainBuildings.PickOne(), this.transform.position,
                Quaternion.identity) as GameObject;
        mainBuilding.transform.Rotate(new Vector3(0f, Random.Range(0f, 360f), 0f));
        mainBuilding.transform.SetParent(this.transform);
    }

    public void ErectMainBuilding(DistrictType dType)
    {
        mainBuilding = Instantiate(MainBuildings[(int)dType], this.transform.position,
                Quaternion.identity) as GameObject;

        mainBuilding.transform.Rotate(new Vector3(0f, Random.Range(0f, 360f), 0f));
        mainBuilding.transform.SetParent(this.transform);
    }

    private void ExpandOnNeighbours()
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
            var towers = Towers.GetComponent<VertexManager>().GetVertexFromSide(n);
            towers[0].SetActive(false);
            towers[1].SetActive(false);

            BoardManager.FindTile(HexGridHelper.FindNeighbour(TileRef.Coord, n))
                .CityRef.GetComponent<CastleTileMasterBase>()
                .ReactToExpansion();
        }
    }

    public void ExpandRandomDir()
    {
        var candidate = HexGridHelper.FindAllNeighbours(this.TileRef.Coord).Select(x => BoardManager.FindTile(x)).Where(y => y.Feature == TileFeature.Village).PickOne();
        var district = Instantiate(this.gameObject, candidate.transform.position, Quaternion.identity) as GameObject;
        district.GetComponent<DistrictTileMaster>().TileRef = candidate;
        candidate.CityRef = district;
    }

    public void FindCitadel()
    {
        var neighbours = GetNeighbouringCities();
        CitadelRef =
            neighbours.Select(
                x =>
                    BoardManager.FindTile(HexGridHelper.FindNeighbour(TileRef.Coord, x))
                        .CityRef.GetComponent<CastleTileMasterBase>()).First(y => y is CitadelTileMaster).gameObject;
        this.transform.SetParent(CitadelRef.transform);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
