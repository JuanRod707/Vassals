using UnityEngine;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
    public static TileInfo[,] MasterBoard;
    public static IList<GameObject> CityList;
    public static IList<GameObject> TownList;

    private void Start()
    {
        CityList = new List<GameObject>();
        TownList = new List<GameObject>();
    }

    public static int Size 
    {
        get { return MasterBoard.GetLength(0); }
    }

    public void InitializeBoard(int size)
    {
        MasterBoard = new TileInfo[size, size];
    }

    public static bool IsTileCity(Coordinate coords)
    {
        if (!IsCoordValid(coords))
        {
            return false;
        }

        return MasterBoard[coords.XCoord, coords.YCoord].Feature == TileFeature.City;
    }

    public static TileInfo FindTile(Coordinate coords)
    {
        if (!IsCoordValid(coords))
        {
            return null;
        }

        return MasterBoard[coords.XCoord, coords.YCoord];
    }

    public static bool IsBorderTile(Coordinate coords)
    {
        return coords.XCoord == 0 || coords.XCoord == Size || coords.YCoord == 0 || coords.YCoord == Size;
    }

    public static bool IsCoordValid(Coordinate coords)
    {
        var x = coords.XCoord;
        var y = coords.YCoord;
        return x >= 0 && x < Size && y >= 0 && y < Size;
    }
}
