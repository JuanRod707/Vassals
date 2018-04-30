using UnityEngine;
using System.Collections;

public class VertexManager : MonoBehaviour
{
    public GameObject NWVertex;
    public GameObject NVertex;
    public GameObject NEVertex;
    public GameObject SEVertex;
    public GameObject SVertex;
    public GameObject SWVertex;

    public GameObject GetVertex(Compass point)
    {
        switch (point)
        {
            case Compass.NorthWest:
                return NWVertex;
            case Compass.North:
                return NVertex;
            case Compass.NorthEast:
                return NEVertex;
            case Compass.SouthEast:
                return SEVertex;
            case Compass.South:
                return SVertex;
            case Compass.SouthWest:
                return SWVertex;
            default:
                return null;
        }
    }

    public GameObject[] GetVertexFromSide(Compass point)
    {
        switch (point)
        {
            case Compass.NorthWest:
                return new[] {NWVertex, NVertex};
            case Compass.East:
                return new[] { NEVertex, SEVertex };
            case Compass.NorthEast:
                return new[] { NEVertex, NVertex };
            case Compass.SouthEast:
                return new[] { SEVertex, SVertex };
            case Compass.West:
                return new[] { NWVertex, SWVertex };
            case Compass.SouthWest:
                return new[] { SWVertex, SVertex };
            default:
                return null;
        }
    }

    public GameObject GetVertexFromTwoSides(Compass edge1, Compass edge2)
    {
        switch (edge1)
        {
            case Compass.NorthWest:
                if (edge2 == Compass.NorthEast)
                {
                    return NVertex;
                }
                return NWVertex;

            case Compass.West:
                if (edge2 == Compass.NorthWest)
                {
                    return NWVertex;
                }
                return SWVertex;

            case Compass.NorthEast:
                if (edge2 == Compass.NorthWest)
                {
                    return NVertex;
                }
                return NEVertex;

            case Compass.SouthEast:
                if (edge2 == Compass.East)
                {
                    return SEVertex;
                }
                return SVertex;

            case Compass.East:
                if (edge2 == Compass.NorthEast)
                {
                    return NEVertex;
                }
                return SEVertex;

            case Compass.SouthWest:
                if (edge2 == Compass.SouthEast)
                {
                    return SVertex;
                }
                return SWVertex;

            default:
                return null;
        }
    }

    public void HideAll()
    {
        NWVertex.SetActive(false);
        NVertex.SetActive(false);
        NEVertex.SetActive(false);
        SVertex.SetActive(false);
        SVertex.SetActive(false);
        SWVertex.SetActive(false);
    }

    public void ShowAll()
    {
        NVertex.SetActive(true);
        NWVertex.SetActive(true);
        NEVertex.SetActive(true);
        SVertex.SetActive(true);
        SEVertex.SetActive(true);
        SWVertex.SetActive(true);
    }
}
