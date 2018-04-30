using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Pathfinder : MonoBehaviour {

    public static IEnumerable<NavNode> FindBestPath(NavNode source, NavNode target, NavNode[,] graph)
    {
        var dist = new Dictionary<NavNode, int>();
        var prev = new Dictionary<NavNode, NavNode>();
        var unvisited = new List<NavNode>();

        dist[source] = 0;
        prev[source] = null;

        foreach (var v in graph)
        {
            if (v != source)
            {
                dist[v] = int.MaxValue;
                prev[v] = null;
            }

            unvisited.Add(v);
        }

        while (unvisited.Any())
        {
            NavNode u = null;

            foreach (var un in unvisited)
            {
                if (u == null || dist[un] < dist[u])
                {
                    u = un;
                }
            }
            //var u = unvisited.First(x => dist[x] == unvisited.Min(y => dist[y]));
            //var u = unvisited.OrderBy(x => dist[x]).First();
            if (u == target)
            {
                break;
            }

            unvisited.Remove(u);

            foreach (var n in u.Neighbours)
            {
                var alt = dist[u] + n.Coord.DistanceTo(u.Coord);
                if (alt < dist[n])
                {
                    dist[n] = alt;
                    prev[n] = u;
                }
            }
        }

        if (prev[target] != null)
        {
            var path = new List<NavNode>();
            var node = target;
            while (node != null)
            {
                path.Add(node);
                node = prev[node];
            }

            path.Reverse();
            return path;
        }

        return null;
    }
}
