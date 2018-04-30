using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using UnityEngine;

internal class RiverGenerator : MatrixTols, IFeatureGenerator
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
    public bool AffectTerrain;
    public int HeightOfRivers;
    public int MaxRivers;
    public int MaxRiverlength;
    public int MaxTries;

    private Coordinate invalid = new Coordinate(-1, -1);

    public void ExecuteFeatureGenerator(TileGenData[,] board)
    {
        this.board = board;

        for (int i = 0; i < MaxRivers; i++)
        {
            var length = Random.Range(MaxRiverlength/2, MaxRiverlength);
            var river = SpawnNewRiver();
            for (int r = 0; r < length; r++)
            {
                if (!river.Equals(invalid))
                {
                    river = CalculateNextRiverSegment(river);
                    if (!river.Equals(invalid))
                    {
                        ExtendRiver(river);
                    }
                }
            }
        }
    }

    private bool IsRiverCandidate(Coordinate coord)
    {
        if (IsValidTile(coord))
        {
            var adj = HexGridHelper.FindAllNeighbours(coord).Select(x => FindTile(x)).Where(x => x != null);
            var cond1 = FindTile(coord).Height == HeightOfRivers;
            var cond2 = adj.Any(x => x.Height < HeightOfRivers) && adj.Count(x => x.Height < HeightOfRivers) < 3;
            var cond3 = adj.Any(x => x.Height == HeightOfRivers);
            var cond4 = adj.Any(x => x.Feature == TileFeature.River);
            return cond1 & cond2 & cond3 & !cond4;
        }
        return false;
    }

    private bool IsPossibleExtension(Coordinate coord)
    {
        var adj = HexGridHelper.FindAllNeighbours(coord);
        var cond1 = adj.Where(y => FindTile(y) != null).Count(x => FindTile(x).Feature == TileFeature.River) < 2;
        var cond2 = adj.Where(y => FindTile(y) != null).Count(x => FindTile(x).Height < HeightOfRivers) < 1;

        return cond1 & cond2;
    }

    private Compass GetRiverOrigin(Coordinate coord)
    {
        if (IsValidTile(coord))
        {
            var adj = HexGridHelper.FindAllNeighbours(coord).Where(x => FindTile(x) != null && FindTile(x).Height < HeightOfRivers);
            if (adj.Any())
            {
                return HexGridHelper.FindDirectionToNeighbour(coord, adj.First());    
            }

            adj = HexGridHelper.FindAllNeighbours(coord).Where(x => FindTile(x) != null && FindTile(x).Feature == TileFeature.River);
            return HexGridHelper.FindDirectionToNeighbour(coord, adj.First());
        }
        
        return Compass.East;
    }

    private Coordinate SpawnNewRiver()
    {
        var tries = MaxTries;
        //var tileCoord = GetRandomTile();
        var tileCoord = GetRandomTileOfHeight(HeightOfRivers);
        while (!IsRiverCandidate(tileCoord) && tries > 0)
        {
            tileCoord = GetRandomTile();
            tries--;
        }

        if (tries > 0)
        {
            var tile = FindTile(tileCoord);
            tile.Feature = TileFeature.River;
            
            if (AffectTerrain)
            {
                tile.Height = HeightOfRivers;
            }

            tile.RiverConnections = new List<Compass>();
            tile.RiverConnections.Add(GetRiverOrigin(tileCoord));
            Debug.Log(tileCoord + " now is " + tile.Feature);
            return tileCoord;
        }

        return invalid;
    }

    private void ExtendRiver(Coordinate coord)
    {
        if (IsValidTile(coord))
        {
            var tile = FindTile(coord);
            tile.Feature = TileFeature.River;
            if (AffectTerrain)
            {
                tile.Height = HeightOfRivers;
            }
            tile.RiverConnections = new List<Compass>();
            tile.RiverConnections.Add(GetRiverOrigin(coord));
        }
    }

    private Coordinate CalculateNextRiverSegment(Coordinate coord)
    {
        if (IsValidTile(coord))
        {
            var sourceDir = FindTile(coord).RiverConnections.First();
            var possibleDirs = HexGridHelper.GetOpposite3(sourceDir);
            var candidates = HexGridHelper.FindNeighbours(coord, possibleDirs);
                //.Where(x => FindTile(x).Height == HeightOfRivers);

            if (candidates.Any())
            {
                var segment = candidates.PickOne();
                var tries = 10;
                while (!IsPossibleExtension(segment) && candidates.Any() && tries > 0)
                {
                    candidates = candidates.Where(x => !x.Equals(segment));
                    segment = candidates.PickOne();
                    tries--;
                }

                if (tries > 0 && candidates.Any() && FindTile(segment) != null && FindTile(segment).Feature == TileFeature.None)
                {
                    var dir = HexGridHelper.FindDirectionToNeighbour(coord, segment);
                    FindTile(coord).RiverConnections.Add(dir);
                    return segment;
                }
            }
        }

        return invalid;
    }
}