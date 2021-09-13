using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorFirst : RandomWalkGenerator
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f, 1f)]
    private float roomPercent = 0.8f;

    protected override void RunProceduralGeneration() 
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        CreateCorridors(floorPositions);

        tileGenerator.PlaceFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tileGenerator);
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions)
    {
        var currentPosition = startPosition;

        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGeneration.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[corridor.Count - 1];
            floorPositions.UnionWith(corridor);
        }
    }
}
