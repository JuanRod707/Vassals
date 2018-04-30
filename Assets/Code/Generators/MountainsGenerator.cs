using System.Collections.Generic;
using UnityEngine;

internal class MountainsGenerator : MatrixTols, IFeatureGenerator
{
    public bool IsEnabled
    {
        get { return IsFeatureEnabled; }
    }

    public int Order
    {
        get { return ExecutionOrder; }
    }

    public int ExecutionOrder;
    public bool IsFeatureEnabled;
    public int MaxMountains;
    public int MountainSize;
    public int MountainHeight;

    public void ExecuteFeatureGenerator(TileGenData[,] board)
    {
        this.board = board;

        for (int m = 0; m < MaxMountains; m++)
        {
            var size = Random.Range(2, MountainSize);
            var mtn = GetRandomTile();

            while (FindTile(mtn).Feature == TileFeature.Mountain)
            {
                mtn = GetRandomTile();
            }

            RiseTile(mtn, MountainHeight);
            var height = board[mtn.XCoord, mtn.YCoord].Height;

            board[mtn.XCoord, mtn.YCoord].Feature = TileFeature.Mountain;

            for (int a = 1; a <= size; a++)
            {
                var adj = GetRandomAdjacent(mtn);
                SetTileHeight(adj, height - a);
                board[adj.XCoord, adj.YCoord].Feature = TileFeature.Mountain;
            }
        }
    }
}