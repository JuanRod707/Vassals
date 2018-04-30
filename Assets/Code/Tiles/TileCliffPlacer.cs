using UnityEngine;
using System.Collections;

public class TileCliffPlacer : MonoBehaviour
{
    public EdgeManager Cliffs;

	// Use this for initialization
	public void DrawCliffs(TileGenData[,] mainBoard)
	{
        this.Cliffs.HideAll();
        var tile = this.GetComponent<TileInfo>();
        var dirsToCheck = new[] { Compass.West, Compass.NorthWest, Compass.NorthEast, Compass.East, Compass.SouthEast, Compass.SouthWest };

	    foreach (var d in dirsToCheck)
	    {
	        var n = HexGridHelper.FindNeighbour(tile.Coord, d);
	        if (BoardManager.IsCoordValid(n))
	        {
	            var adj = mainBoard[n.XCoord,n.YCoord];
	            if (adj.Height < tile.Height - 1)
	            {
	                switch (d)
	                {
	                    case Compass.East:
                            this.Cliffs.EEdge.SetActive(true);
	                        break;
	                    case Compass.SouthEast:
                            this.Cliffs.SEEdge.SetActive(true);
                            break;
	                    case Compass.SouthWest:
                            this.Cliffs.SWEdge.SetActive(true);
                            break;
	                    case Compass.West:
                            this.Cliffs.WEdge.SetActive(true);
                            break;
	                    case Compass.NorthWest:
                            this.Cliffs.NWEdge.SetActive(true);
                            break;
	                    case Compass.NorthEast:
                            this.Cliffs.NEEdge.SetActive(true);
                            break;
	                }
	            }
	        }
	    }
        
	}
}
