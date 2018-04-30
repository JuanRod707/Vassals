using UnityEngine;
using System.Collections;
using System.Linq;

public class BoardHelper : MonoBehaviour
{
    public int Size {
        get { return this.GetComponent<NoiseMatrixGenerator>().BoardSize; }
    }

    public int WaterLevel;

    // Use this for initialization
    void Start()
    {

    }

    public TileInfo FindTile(Coordinate coord)
    {
        return this.transform.GetComponentsInChildren<TileInfo>()
                .FirstOrDefault(x => x.XCoord == coord.XCoord && x.YCoord == coord.YCoord);
    }
}
