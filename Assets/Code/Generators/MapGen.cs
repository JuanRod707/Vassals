using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using System.Threading;
using Random = UnityEngine.Random;

public class MapGen : MonoBehaviour
{
    public TileGenData[,] MainBoard;
    public GameObject WaitMessage;

    public GameObject NormalTile;
    public GameObject MountainTile;
    public GameObject ForestTile;

    public GameObject StraightRiverTile;
    public GameObject CurveRiverTile;
    public GameObject EndRiverTile;
    
    public float TileRadius;
    public float HeightFactor;
    public int CapitalInitialExpansions;

    private int currentCapitalExp;
    private float rowFactor;
    private float columnFactor;
    private float oddOffset;

    private int xCoord = 0;
    private int zCoord = 0;
    private MapGenPhase phase = MapGenPhase.CityPlacement;
    private BoardManager boardManager;

    private float TileRotation;

	// Use this for initialization
	void Start ()
	{
	    rowFactor = (TileRadius*2)*(3f/4f);
	    columnFactor = ((float)Math.Sqrt(3)/2)*(TileRadius*2);
	    oddOffset = ((float) Math.Sqrt(3)/2)*(TileRadius*2) / 2;
	    MainBoard = this.GetComponent<NoiseMatrixGenerator>().GenerateMatrix();
	    boardManager = this.GetComponent<BoardManager>();
        boardManager.InitializeBoard(MainBoard.GetLength(0));
        phase = MapGenPhase.TilePlacement;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    switch(phase)
	    {
            case MapGenPhase.TilePlacement:
	            TilePlaceMentRoutine();
	            break;
            case MapGenPhase.CityPlacement:
	            CityPlacementRoutine();
                break;
            case MapGenPhase.TownPlacement:
	            TownPlacementRoutine();
                break;
            case MapGenPhase.TerritoryDefinition:
	            DefineTerritory();
	            break;
            case MapGenPhase.NavMapDefinition:
                GenerateNavigationMap();
                break;
            case MapGenPhase.RoadPlacement:
                RoadPlacementRoutine();
                break;
            case MapGenPhase.CapitalDefinition:
                FindAndExpandCapital();
                break;
        }
	}

    private void TilePlaceMentRoutine()
    {
        for (int i = 0; i < this.MainBoard.GetLength(0); i++)
        {
            if (zCoord < this.MainBoard.GetLength(0))
            {
                Vector3 tilePos;

                if (zCoord%2 != 0)
                {
                    tilePos = new Vector3(xCoord*columnFactor + oddOffset, MainBoard[xCoord, zCoord].Height*HeightFactor,
                        zCoord*rowFactor);
                }
                else
                {
                    tilePos = new Vector3(xCoord*columnFactor, MainBoard[xCoord, zCoord].Height*HeightFactor, zCoord*rowFactor);
                }

                GameObject tileToPlace;
                var tileData = MainBoard[xCoord, zCoord];

                switch (tileData.Feature)
                {
                    case TileFeature.None:
                        tileToPlace = NormalTile;
                        TileRotation = 0f;
                        break;
                    case TileFeature.Forest:
                        tileToPlace = ForestTile;
                        TileRotation = 0f;
                        break;
                    case TileFeature.Mountain:
                        tileToPlace = MountainTile;
                        TileRotation = 0f;
                        break;
                    case TileFeature.River:
                        tileToPlace = CalculateRiverTile(new Coordinate(xCoord, zCoord));
                        break;
                    default:
                        tileToPlace = NormalTile;
                        TileRotation = 0f;
                        break;
                }

                var placedTile = GameObject.Instantiate(tileToPlace, tilePos, Quaternion.identity) as GameObject;
                placedTile.transform.Rotate(new Vector3(0f, TileRotation, 0f));

                var tInfo = placedTile.GetComponent<TileInfo>();
                tInfo.XCoord = xCoord;
                tInfo.YCoord = zCoord;
                tInfo.Feature = tileData.Feature;
                tInfo.Height = tileData.Height;
                tInfo.UpdateCliffs(this.MainBoard);

                if (tileData.Height <= 0 || tileData.Feature == TileFeature.Mountain)
                {
                    tInfo.isPassable = false;
                }

                BoardManager.MasterBoard[xCoord, zCoord] = tInfo;

                placedTile.transform.SetParent(this.transform);
                xCoord++;
            }
        }

        if (xCoord >= this.MainBoard.GetLength(0))
        {
            xCoord = 0;
            zCoord++;
        }

        if (zCoord >= this.MainBoard.GetLength(0))
        {
            AdvancePhase();
        }

    }

    private GameObject CalculateRiverTile(Coordinate coord)
    {
        var tileData = MainBoard[coord.XCoord, coord.YCoord];

        if (tileData.RiverConnections.Count == 1 || (tileData.RiverConnections.Count == 2 && HexGridHelper.AreOpposite(tileData.RiverConnections[0], tileData.RiverConnections[1])))
        {
            switch (tileData.RiverConnections.First())
            {
                case Compass.East:
                    TileRotation = 0f;
                    break;
                case Compass.SouthEast:
                    TileRotation = 60f;
                    break;
                case Compass.SouthWest:
                    TileRotation = 120f;
                    break;
                case Compass.West:
                    TileRotation = 180f;
                    break;
                case Compass.NorthWest:
                    TileRotation = 240f;
                    break;
                case Compass.NorthEast:
                    TileRotation = 300f;
                    break;
            }

            return tileData.RiverConnections.Count == 1 ? EndRiverTile : StraightRiverTile;
        }
        else if (tileData.RiverConnections.Count == 2 && !HexGridHelper.AreOpposite(tileData.RiverConnections[0], tileData.RiverConnections[1]))
        {
            switch (tileData.RiverConnections.First())
            {
                case Compass.East:
                    TileRotation = 0f;
                    if (tileData.RiverConnections[1] == Compass.NorthWest)
                    {
                        TileRotation += 240;
                    }
                    
                    break;
                case Compass.SouthEast:
                    TileRotation = 60f;
                    if (tileData.RiverConnections[1] == Compass.NorthEast)
                    {
                        TileRotation += 240;
                    }
                    break;
                case Compass.SouthWest:
                    TileRotation = 120f;
                    if (tileData.RiverConnections[1] == Compass.East)
                    {
                        TileRotation += 240;
                    }
                    break;
                case Compass.West:
                    TileRotation = 180f;
                    if (tileData.RiverConnections[1] == Compass.SouthEast)
                    {
                        TileRotation += 240;
                    }
                    break;
                case Compass.NorthWest:
                    TileRotation = 240f;
                    if (tileData.RiverConnections[1] == Compass.SouthWest)
                    {
                        TileRotation += 240;
                    }
                    break;
                case Compass.NorthEast:
                    TileRotation = 300f;
                    if (tileData.RiverConnections[1] == Compass.West)
                    {
                        TileRotation += 240;
                    }
                    break;
            }

            return CurveRiverTile;
        }

        TileRotation = 0f;
        return StraightRiverTile;
    }

    private void CityPlacementRoutine()
    {
        this.GetComponent<CityPlacer>().PlaceNextCity();
    }

    private void TownPlacementRoutine()
    {
        this.GetComponent<TownPlacer>().PlaceNextTown();
    }

    private void DefineTerritory()
    {
        this.GetComponent<TerritoryDelimiter>().SetNextTile();
    }

    private void RoadPlacementRoutine()
    {
        this.GetComponent<RoadPlacer>().PathRoads();
    }

    private void FindAndExpandCapital()
    {
        var capital = BoardManager.CityList.OrderByDescending(x => x.GetComponent<CitadelTileMaster>().Towns.Count).First();
        var citadel = capital.GetComponent<CitadelTileMaster>();
        citadel.ExpandRandomDir((DistrictType)currentCapitalExp);
        currentCapitalExp ++;

        if (currentCapitalExp >= CapitalInitialExpansions)
        {
            AdvancePhase();
        }
    }

    private void GenerateNavigationMap()
    {
        GameObject.Find("NavMap").GetComponent<NavMapGen>().GenerateNavMap();
        AdvancePhase();
    }

    public void AdvancePhase()
    {
        phase++;
        Debug.Log(phase);

        if(phase == MapGenPhase.Finished)
        {
            this.WaitMessage.SetActive(false);
        }
    }
}