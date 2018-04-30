using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CitadelTileMaster : CastleTileMasterBase
{
    public GameObject DistrictPrefab;
    public GameObject MainBuilding;
    public GameObject Banner;
    public CityInfo CityInfo;

    public List<GameObject> Towns;

    // Use this for initialization
    void Start ()
	{
        this.transform.SetParent(GameObject.Find("PopTiles").transform);
        this.Territory = new List<TileInfo>();
        this.TileRef = this.GetComponent<MapReference>().TileRef;
        Debug.Log(string.Format("Placed city on {0}, {1}", TileRef.XCoord, TileRef.YCoord));
        CityInfo = this.GetComponent<CityInfo>();
        CleanTile();
        RandomizeGates();
	    ErectMainBuilding();
	    ErectHouses();
	    CreateVillages();
        CityInfo.GenerateBaseCity(villages);
    }

    protected override void ErectMainBuilding()
    {
        var HouseMaterial = GameObject.Find("Resources").GetComponent<BannerRandomizer>().GetRandomMaterial();
        this.HouseColor = HouseMaterial.color;
        mainBuilding = Instantiate(MainBuilding, this.transform.position, Quaternion.identity) as GameObject;
        var mat = this.Banner.GetComponent<MeshRenderer>().materials[0];
        this.Banner.GetComponent<MeshRenderer>().materials = new[] {mat, HouseMaterial};
        mainBuilding.transform.Rotate(new Vector3(0f, Random.Range(0f, 360f), 0f));
        mainBuilding.transform.SetParent(this.transform);
    }

    public void ExpandRandomDir(DistrictType districtType)
    {
        var neighbouring = HexGridHelper.FindAllNeighbours(this.TileRef.Coord).Select(x => BoardManager.FindTile(x)).Where(y => y != null && y.Feature == TileFeature.Village);
        if (neighbouring.Any())
        {
            var candidate = neighbouring.PickOne();
            var district = Instantiate(DistrictPrefab, candidate.transform.position, Quaternion.identity) as GameObject;
            district.GetComponent<DistrictTileMaster>().ErectMainBuilding(districtType);
            district.GetComponent<MapReference>().TileRef = candidate;
            candidate.CityRef = district;
        }
    }

    public void UpdateCityInfo(DistrictInfo dInfo)
    {
        CityInfo.UpdateInfo(dInfo);
    }

    public void AddTown(GameObject town)
    {
        if (Towns == null)
        {
            Towns = new List<GameObject>();
        }

        Towns.Add(town);
    }
}
