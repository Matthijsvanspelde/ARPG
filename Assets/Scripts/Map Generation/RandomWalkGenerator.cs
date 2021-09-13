using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomWalkGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    private DungeonData dungeonData;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(dungeonData);
        tileGenerator.PlaceFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tileGenerator);
    }

    protected HashSet<Vector2Int> RunRandomWalk(DungeonData dungeonData)
    {
        var currentPosition = startPosition;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < dungeonData.iterations; i++)
        {
            var path = ProceduralGeneration.simpleRandomWalk(currentPosition, dungeonData.walkLength);
            floorPositions.UnionWith(path);
            if (dungeonData.startRandomlyEachIteration)
            {
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }
        return floorPositions;
    }
}
