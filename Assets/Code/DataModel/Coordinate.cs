using System;
using UnityEngine;
using System.Collections;

[Serializable]
public struct Coordinate
{
    public int XCoord;
    public int YCoord;

    public Coordinate(int x, int y)
    {
        XCoord = x;
        YCoord = y;
    }

    public override string ToString()
    {
        return string.Format("({0}, {1})", XCoord, YCoord);
    }

    public int DistanceTo(Coordinate coord)
    {
        var side1 = coord.XCoord - XCoord;
        var side2 = coord.YCoord - YCoord;
        return (int) Mathf.Sqrt(side1*side1 + side2*side2);
    }
}
