using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Networking.NetworkSystem;

public class RoadPlacer : MonoBehaviour
{
    public GameObject RoadPrefab;
    public int MaxDistance;

    private NavMapGen navMap;
    private NavNode[,] graph;
    private List<RoadNode> AllRoadNodes; 

    public void PathRoads()
    {
        AllRoadNodes = new List<RoadNode>();
        navMap = GameObject.Find("NavMap").GetComponent<NavMapGen>();
        graph = navMap.Graph;
        var cities = BoardManager.CityList.Select(x => x.GetComponent<CitadelTileMaster>());

        foreach (var c in cities)
        {
            var closeCities = cities.Where(x => x != c && c.TileRef.Coord.DistanceTo(x.TileRef.Coord) < MaxDistance);
            foreach (var n in closeCities)
            {
                var path = FindPathTo(c.TileRef.Coord, n.TileRef.Coord);
                for (int i = 0; i < path.Length; i++)
                {
                    var tile = BoardManager.FindTile(path[i].Coord);
                    if (!tile.hasRoad)
                    {
                        var road = GameObject.Instantiate(RoadPrefab, tile.transform, false) as GameObject;
                        var rNode = road.GetComponent<RoadNode>();
                        rNode.Neighbours = new List<RoadNode>();
                        rNode.TileRef = tile;

                        if (i > 0)
                        {
                            var lastRoad = FindRoadFromNode(path[i - 1]);
                            if(!lastRoad.Neighbours.Contains(rNode)) lastRoad.Neighbours.Add(rNode);
                            if (!rNode.Neighbours.Contains(lastRoad)) rNode.Neighbours.Add(lastRoad);
                        }

                        AllRoadNodes.Add(rNode);
                        tile.hasRoad = true;
                    }
                    else
                    {
                        var rNode = FindRoadFromNode(path[i]);
                        if (i > 0)
                        {
                            var lastRoad = FindRoadFromNode(path[i - 1]);
                            if (!lastRoad.Neighbours.Contains(rNode)) lastRoad.Neighbours.Add(rNode);
                            if (!rNode.Neighbours.Contains(lastRoad)) rNode.Neighbours.Add(lastRoad);
                        }
                    }
                }
            }
        }

        foreach (var r in AllRoadNodes)
        {
            r.ShowFragments();
        }

        this.GetComponent<MapGen>().AdvancePhase();
    }

    private NavNode[] FindPathTo(Coordinate origin, Coordinate finish)
    {
        var source = navMap.GetNavNode(origin);
        var target = navMap.GetNavNode(finish);
        return Pathfinder.FindBestPath(source, target, graph).ToArray();
    }

    private RoadNode FindRoadFromNode(NavNode node)
    {
        var tile = BoardManager.FindTile(node.Coord);
        return AllRoadNodes.First(x => x.TileRef == tile);
    }
}
