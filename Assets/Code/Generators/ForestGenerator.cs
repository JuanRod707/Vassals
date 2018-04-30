using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class ForestGenerator : MatrixTols, IFeatureGenerator
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
    public int ForestsCores;
    public int MaxForestRadius;
    public int WaterLevel;

    public void ExecuteFeatureGenerator(TileGenData[,] board)
    {
        this.board = board;

        for (int m = 0; m < ForestsCores; m++)
        {
            var radius = Random.Range(1, MaxForestRadius);
            var fCore = GetRandomTile();
            var tile = FindTile(fCore);

            while (tile.Height <= WaterLevel | (tile.Feature != TileFeature.None & tile.Feature != TileFeature.Forest))
            {
                fCore = GetRandomTile();
                tile = FindTile(fCore);
            }

            tile.Feature = TileFeature.Forest;
            ExpandForest(fCore, radius);
        }
    }

    private void ExpandForest(Coordinate coord, int maxSize)
    {
        if (maxSize > 0)
        {
            var candidates = HexGridHelper.FindAllNeighbours(coord);

            foreach (var c in candidates.Where(x => IsValidTile(x)))
            {
                var tile = FindTile(c);
                if (tile.Feature == TileFeature.None && tile.Height > WaterLevel)
                {
                    board[c.XCoord, c.YCoord].Feature = TileFeature.Forest;
                    ExpandForest(c, maxSize - 1);
                }
            }
        }
    }
}