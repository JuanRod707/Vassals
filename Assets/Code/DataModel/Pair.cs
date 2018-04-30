using UnityEngine;
using System.Collections;

public struct Pair<T>
{
    public T Value1;
    public T Value2;

    public Pair(T v1, T v2)
    {
        Value1 = v1;
        Value2 = v2;
    }
}
