using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TileGenerator tileGenerator) 
    {
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction.cardinalDirections);
        foreach (var position in basicWallPositions)
        {
            tileGenerator.PlaceSingleWall(position);
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
