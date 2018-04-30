using System.Collections.Generic;
using UnityEngine;

internal interface IFeatureGenerator
{
    bool IsEnabled { get; }

    int Order { get; }

    void ExecuteFeatureGenerator(TileGenData[,] board);
}