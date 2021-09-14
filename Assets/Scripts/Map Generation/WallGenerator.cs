using System;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TileGenerator tileGenerator)
    {
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction.cardinalDirections);
        var cornerWallPositions = FindWallsInDirections(floorPositions, Direction.diagonalDirections);
        CreateBasicWalls(tileGenerator, basicWallPositions, floorPositions);
        CreateCornerWalls(tileGenerator, cornerWallPositions, floorPositions);
    }

    private static void CreateCornerWalls(TileGenerator tileGenerator, HashSet<Vector2Int> cornerWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in cornerWallPositions)
        {
            string neighboursBinaryType = "";
            foreach (var direction in Direction.eightDirections)
            {
                var neighbourPosition = position + direction;
                if (floorPositions.Contains(neighbourPosition))
                {
                    neighboursBinaryType += "1";
                }
                else
                {
                    neighboursBinaryType += "0";
                }
            }
            tileGenerator.PlaceSingleCornerWall(position, neighboursBinaryType);
        }
    }

    private static void CreateBasicWalls(TileGenerator tileGenerator, HashSet<Vector2Int> basicWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in basicWallPositions)
        {
            string neighboursBinaryType = "";
            foreach (var direction in Direction.cardinalDirections)
            {
                var neighbourPosition = position + direction;
                if (floorPositions.Contains(neighbourPosition))
                {
                    neighboursBinaryType += "1";
                }
                else
                {
                    neighboursBinaryType += "0";
                }
            }
            tileGenerator.PlaceSingleWall(position, neighboursBinaryType);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directions)
    {
        HashSet<Vector2Int> wallPostions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions)
        {
            foreach (var direction in directions)
            {
                var neighbourPosition = position + direction;
                if (!floorPositions.Contains(neighbourPosition))
                {
                    wallPostions.Add(neighbourPosition);
                }
            }
        }
        return wallPostions;
    }
}
