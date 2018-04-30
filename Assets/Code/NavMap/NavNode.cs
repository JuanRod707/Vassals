using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class NavNode : MonoBehaviour
{
    public List<NavNode> Neighbours;
    public LineRenderer[] NavLines;
    public Coordinate Coord;
    public int Height;
    public bool Passable;

    private void Start()
    {
        //this.transform.Translate(Vector3.up * 0.6f);
    }

    public void SetAllNeighbours(IEnumerable<NavNode> nodes)
    {
        Neighbours = nodes.ToList();
    }

    public void AddNeighbour(NavNode node)
    {
        if (Neighbours == null)
        {
            Neighbours = new List<NavNode>();
        }

        Neighbours.Add(node);
    }

    public void DrawLines()
    {
        int index = 0;

        foreach (var l in NavLines)
        {
            if (index < Neighbours.Count)
            {
                l.SetPosition(0, transform.position + (Vector3.up * 0.6f));
                l.SetPosition(1, Neighbours[index].transform.position + (Vector3.up * 0.6f));
                index++;
            }
            else
            {
                l.enabled = false;
            }
        }
    }
}
