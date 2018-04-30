using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileGenData
{
    public int Height;
    public TileFeature Feature = TileFeature.None;
    public IList<Compass> RiverConnections;
}
