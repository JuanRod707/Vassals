using UnityEngine;
using System.Collections;

public class TileInfo : MonoBehaviour
{
    public GameObject CityRef;
    public GameObject Village;
    public MeshRenderer Model;

    public int XCoord;
    public int YCoord;
    public int Height;
    public bool isPassable;
    public bool hasRoad;

    public TileFeature Feature = TileFeature.None;

    public Coordinate Coord {
        get { return new Coordinate(XCoord, YCoord); }
    }

    public void UpdateTileGroundMaterial(Material mat)
    {
        var stoneMat = this.Model.materials[0];
        this.Model.materials = new[] {stoneMat, mat};
    }

    public void UpdateCliffs(TileGenData[,] mainBoard)
    {
        var cliffs = this.GetComponent<TileCliffPlacer>();
        if (cliffs != null)
        {
            cliffs.DrawCliffs(mainBoard);
        }
    }
}
