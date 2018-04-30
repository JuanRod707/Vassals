using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoadNode : MonoBehaviour
{
    public TileInfo TileRef;
    public List<RoadNode> Neighbours;

    private EdgeManager roadFragments;

    public void ShowFragments()
    {
        roadFragments = this.GetComponent<EdgeManager>();
        roadFragments.HideAll();

        foreach (var n in Neighbours)
        {
            roadFragments.GetEdge(HexGridHelper.FindDirectionToNeighbour(this.TileRef.Coord, n.TileRef.Coord))
                .SetActive(true);
        }
    }
}
