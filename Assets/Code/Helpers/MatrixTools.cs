using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class MatrixTols : MonoBehaviour
{
    protected TileGenData[,] board;

    protected int size
    {
        get { return board.GetLength(0); }
    }

    protected void SetTileHeight(Coordinate coord, int height)
    {
        board[coord.XCoord, coord.YCoord].Height = height;
    }

    protected void RiseTile(Coordinate coord, int height)
    {
        board[coord.XCoord, coord.YCoord].Height += height;
    }

    protected Coordinate GetRandomAdjacent(Coordinate coord)
    {
        var rndX = Random.Range(-1, 1);
        var rndY = Random.Range(-1, 1);
        var newCoord = new Coordinate(coord.XCoord + rndX, coord.YCoord + rndY);

        while (!IsValidTile(newCoord))
        {
            rndX = Random.Range(-1, 1);
            rndY = Random.Range(-1, 1);
            newCoord = new Coordinate(coord.XCoord + rndX, coord.YCoord + rndY);
        }

        return new Coordinate(coord.XCoord + rndX, coord.YCoord + rndY);
    }

    protected bool IsValidTile(Coordinate coord)
    {
        return coord.XCoord >= 0 && coord.XCoord < size && coord.YCoord >= 0 && coord.YCoord < size;
    }

    protected Coordinate GetRandomTile()
    {
        var rndX = Random.Range(0, size - 1);
        var rndY = Random.Range(0, size - 1);
        return new Coordinate(rndX, rndY);
    }

    protected Coordinate GetRandomTileOfHeight(int height)
    {
        var result = new List<Coordinate>();

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (board[x, y].Height == height)
                {
                    result.Add(new Coordinate(x, y));
                }
            }
        }

        return result.PickOne();
    }

    protected TileGenData FindTile(Coordinate coord)
    {
        if (IsValidTile(coord))
        {
            return board[coord.XCoord, coord.YCoord];
        }

        return null;
    }

    protected IEnumerable<TileGenData> GetAllNeighbours(Coordinate coord)
    {
        var allAdjacent = HexGridHelper.FindAllNeighbours(coord);
        return allAdjacent.Select(x => FindTile(x));
    }
}
