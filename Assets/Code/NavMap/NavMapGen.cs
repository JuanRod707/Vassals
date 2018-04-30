using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NavMapGen : MonoBehaviour
{
    public GameObject NodePrefab;
    public int HeightDifTolerance;
    public int WaterLevel;

    private NavNode[,] NavigationNodes;

    public NavNode[,] Graph {
        get { return NavigationNodes; }
    }

    public void GenerateNavMap()
    {
        NavigationNodes = new NavNode[BoardManager.Size, BoardManager.Size];

        for (int x = 0; x < BoardManager.Size; x++)
        {
            for (int y = 0; y < BoardManager.Size; y++)
            {
                var coord = new Coordinate(x, y);
                var tile = BoardManager.FindTile(coord);
                var node = GameObject.Instantiate(NodePrefab, tile.transform.position, Quaternion.identity) as GameObject;
                node.transform.SetParent(this.transform);
                var nodeInfo = node.GetComponent<NavNode>();
                nodeInfo.Coord = coord;
                nodeInfo.Height = tile.Height;
                nodeInfo.Passable = IsTilePassable(tile);
                NavigationNodes[x, y] = nodeInfo;
            }
        }

        for (int x = 0; x < BoardManager.Size; x++)
        {
            for (int y = 0; y < BoardManager.Size; y++)
            {
                var node = NavigationNodes[x, y];
                if (node.Passable)
                {
                    var nbs = HexGridHelper.FindAllNeighbours(node.Coord);
                    foreach (var n in nbs)
                    {
                        if (BoardManager.IsCoordValid(n))
                        {
                            var candidate = NavigationNodes[n.XCoord, n.YCoord];
                            if (candidate.Passable &&
                                Mathf.Abs(node.Height - candidate.Height) <= HeightDifTolerance)
                            {
                                node.Neighbours.Add(candidate);
                            }
                        }
                    }

                    node.DrawLines();
                }
                else
                {
                    node.gameObject.SetActive(false);
                }
            }
        }
    }

    public NavNode GetNavNode(Coordinate coord)
    {
        return NavigationNodes[coord.XCoord, coord.YCoord];
    }

    private bool IsTilePassable(TileInfo tile)
    {
        return tile.Height > WaterLevel & tile.Feature != TileFeature.River & tile.Feature != TileFeature.Mountain;
    }
}
