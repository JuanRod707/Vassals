using UnityEngine;
using System.Collections;

public class EdgeManager : MonoBehaviour
{
    public GameObject WEdge;
    public GameObject NWEdge;
    public GameObject NEEdge;
    public GameObject EEdge;
    public GameObject SEEdge;
    public GameObject SWEdge;

    public GameObject GetEdge(Compass point)
    {
        switch (point)
        {
            case Compass.NorthWest:
                return NWEdge;
            case Compass.West:
                return WEdge;
            case Compass.NorthEast:
                return NEEdge;
            case Compass.SouthEast:
                return SEEdge;
            case Compass.East:
                return EEdge;
            case Compass.SouthWest:
                return SWEdge;
            default:
                return null;
        }
    }

    public void HideAll()
    {
        WEdge.SetActive(false);
        NWEdge.SetActive(false);
        NEEdge.SetActive(false);
        EEdge.SetActive(false);
        SEEdge.SetActive(false);
        SWEdge.SetActive(false);
    }
    
    public void ShowAll()
    {
        WEdge.SetActive(true);
        NWEdge.SetActive(true);
        NEEdge.SetActive(true);
        EEdge.SetActive(true);
        SEEdge.SetActive(true);
        SWEdge.SetActive(true);
    }
}
