using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class TerritoryDelimiter : MonoBehaviour
{
    private int xCoord = 0;
    private int zCoord = 0;

    private Coordinate packedCoord
    {
        get { return new Coordinate(xCoord, zCoord); }
    }

    public void SetNextTile()
    {
        for (int i = 0; i < BoardManager.Size; i++)
        {
            if (zCoord < BoardManager.Size)
            {
                var closestCiv = FindClosestCiv(packedCoord).GetComponent<CastleTileMasterBase>();
                var tile = BoardManager.FindTile(packedCoord);
                closestCiv.Territory.Add(tile);
            }

            xCoord++;
        }

        
        if (xCoord >= BoardManager.Size)
        {
            xCoord = 0;
            zCoord++;
        }

        if (zCoord >= BoardManager.Size)
        {
            var civs = BoardManager.CityList.Union(BoardManager.TownList);
            foreach (var c in civs)
            {
                c.GetComponent<CastleTileMasterBase>().SetTerritoryColor();
            }

            this.GetComponent<MapGen>().AdvancePhase();
        }
    }

    private GameObject FindClosestCiv(Coordinate coord)
    {
        var civs = BoardManager.CityList.Union(BoardManager.TownList);
        return civs.OrderBy(x => x.GetComponent<MapReference>().TileRef.Coord.DistanceTo(coord)).First();
    }
}
