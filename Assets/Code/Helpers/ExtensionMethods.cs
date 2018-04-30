using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

internal static class ExtensionMethods
{
    public static bool AreSidesAdjacent(this Compass obj, Compass side)
    {
        switch (obj)
        {
            case Compass.West:
                return side == Compass.NorthWest || side == Compass.SouthWest;
            case Compass.SouthWest:
                return side == Compass.SouthEast || side == Compass.West;
            case Compass.SouthEast:
                return side == Compass.SouthWest || side == Compass.East;
            case Compass.East:
                return side == Compass.NorthEast || side == Compass.SouthEast;
            case Compass.NorthEast:
                return side == Compass.NorthWest || side == Compass.East;
            case Compass.NorthWest:
                return side == Compass.NorthEast || side == Compass.West;
        }

        return false;
    }

    public static T PickOne<T>(this IEnumerable<T> col)
    {
        return col.ToArray()[Random.Range(0, col.Count())];
    }

    public static T[] PickSome<T>(this IEnumerable<T> col, int amount)
    {
        var result = new List<T>();

        if (amount > col.Count())
        {
            return col.ToArray();
        }

        for (int i = 0; i < amount; i++)
        {
            col = col.Where(x => !result.Contains(x));
            result.Add(col.ToArray()[Random.Range(0, col.Count())]);
        }

        return result.ToArray();
    }
}
