using System.Collections.Generic;
using UnityEngine;

internal class MatrixGenerator : MonoBehaviour
{
    public int BoardSize;
    public int MaxHeightHills;
    public int MaxHeightMountain;
    public int MaxHills;
    public int MaxMountains;

    private int[,] board;

    public int[,] GenerateMatrix()
    {
        InitializeBoard();
        LocateTopPoint(MaxHeightHills, 40);
        LocateTopPoint(MaxHeightMountain, 15);
        SmoothPass();
        //SmoothPass();
        //SmoothPass();
        return board;
    }

    private void InitializeBoard()
    {
        board = new int[BoardSize, BoardSize];
    }

    private void LocateTopPoint(int maxHeight, int extendChance)
    {
        for (int i = 0; i < MaxHills; i++)
        {
            var rndX = Random.Range(0, BoardSize - 1);
            var rndY = Random.Range(0, BoardSize - 1);

            while (board[rndX, rndY] > 0)
            {
                rndX = Random.Range(0, BoardSize - 1);
                rndY = Random.Range(0, BoardSize - 1);
            }

            var minHeight = maxHeight / 2;
            RiseTile(rndX, rndY, Random.Range(minHeight, maxHeight));

            var isToExtend = Random.Range(0, 100) <= extendChance;
            if (isToExtend)
            {
                var adj = GetRandomAdjacent(rndX, rndY);
                RiseTile(adj.XCoord, adj.YCoord, Random.Range(minHeight, maxHeight));
            }
        }
    }

    private void SmoothElevation(int x, int y)
    {
        var currentHeight = board[x, y];
        //Raise if applicable
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (!(i == -1 && (j == 1 | j == -1)) & IsValidTile(x + i, y + j))
                {
                    if (IsRaisableTile(x + i, y + j, currentHeight))
                    {
                        RiseTile(x + i, y + j, currentHeight - 1);
                    }
                }
            }
        }
    }

    private void RiseTile(int x, int y, int height)
    {
        board[x, y] = height;
    }

    private Coordinate GetRandomAdjacent(int x, int y)
    {
        var rndX = Random.Range(-1, 1);
        var rndY = Random.Range(-1, 1);

        while (!IsValidTile(rndX, rndY) || board[rndX, rndY] > 0)
        {
            rndX = Random.Range(-1, 1);
            rndY = Random.Range(-1, 1);
        }

        return new Coordinate(x + rndX, y + rndY);
    }

    private bool IsValidTile(int x, int y)
    {
        return x >= 0 && x < BoardSize && y >= 0 && y < BoardSize;
    }

    private bool IsRaisableTile(int x, int y, int height)
    {
        return board[x, y] < height - 1;
    }

    private void SmoothPass()
    {
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                SmoothElevation(i, j);
            }
        }
    }
}
