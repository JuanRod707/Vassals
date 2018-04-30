using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class NoiseMatrixGenerator : MatrixTols
{
    public int BoardSize;
    public int Octaves;
    public float persistance;
    public float lacunarity;
    public float NoiseScale;

    public TileGenData[,] GenerateMatrix()
    {
        InitializeBoard();
        GenerateNoise();

        var features = GetComponents<IFeatureGenerator>();

        foreach (var fg in features.OrderBy(x => x.Order))
        {
            if (fg.IsEnabled)
            {
                fg.ExecuteFeatureGenerator(board);
            }
        }

        return board;
    }

    private void InitializeBoard()
    {
        board = new TileGenData[BoardSize, BoardSize];
    }

    private void GenerateNoise()
    {
        var seedX = Random.Range(0, 1000);
        var seedY = Random.Range(0, 1000);

        for (int x = 0; x < BoardSize; x++)
        {
            for (int y = 0; y < BoardSize; y++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;
                for (int o = 0; o < Octaves; o++)
                {
                    float sampleX = (seedX + x)/NoiseScale*frequency;
                    float sampleY = (seedY + y)/NoiseScale*frequency;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 -1;
                    noiseHeight += perlinValue*amplitude;
                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                var transformed = (int)(noiseHeight * 10);
                board[x, y] = new TileGenData();
                board[x, y].Height = transformed;
            }
        }
    }
}