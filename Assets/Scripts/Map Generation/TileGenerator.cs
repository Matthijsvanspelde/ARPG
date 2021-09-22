using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class TileGenerator : MonoBehaviour
{    
    private float tileSize = 10;
    private NavMeshSurface navMeshSurface;
    private TileSetData tileSetData;
    private DungeonLevel dungeonLevel;

    private void Awake()
    {
        dungeonLevel = GetComponent<DungeonLevel>();
        tileSetData = dungeonLevel.GetTileSet();
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    public void SetTileSetData() 
    {
        dungeonLevel.level++;
        dungeonLevel.SetLevelText();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        tileSetData = dungeonLevel.GetTileSet();
    }

    public void BakeNavMesh() 
    {
        NavMesh.RemoveAllNavMeshData();
        navMeshSurface.RemoveData();
        navMeshSurface.BuildNavMesh();
    }

    public void PlaceFloorTiles(IEnumerable<Vector2Int> floorPositions) 
    {
        PlaceTiles(floorPositions);
    }

    private void PlaceTiles(IEnumerable<Vector2Int> floorPositions)
    {
        int totalWeight = 0;
        foreach (var item in tileSetData.tiles)
        {
            totalWeight += item.GetComponent<TileWeight>().SpawnChange;
        }
        
        foreach (var position in floorPositions)
        {
            if (position == floorPositions.FirstOrDefault())
            {
                PlaceTransitionTile(position, tileSetData.startTile);
            }
            else if (position == floorPositions.LastOrDefault())
            {
                PlaceTransitionTile(position, tileSetData.endTile);
            }
            else
            {
                PlaceSingleTile(position, totalWeight);
            }
                      
        }
        
    }

    internal void PlaceSingleWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        GameObject wall = null;
        var rotation = Quaternion.Euler(new Vector3(0,0,0));
        if (WallTypesHelper.wallTop.Contains(typeAsInt))
        {
            wall = tileSetData.wallTop;
            rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (WallTypesHelper.wallRight.Contains(typeAsInt))
        {
            wall = tileSetData.wallRight;
            rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
        else if (WallTypesHelper.wallLeft.Contains(typeAsInt))
        {
            wall = tileSetData.wallLeft;
            rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
        else if (WallTypesHelper.wallBottom.Contains(typeAsInt))
        {
            wall = tileSetData.wallBottom;
            rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        }
        else if (WallTypesHelper.wallFull.Contains(typeAsInt))
        {
            wall = tileSetData.wallFull;
            rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        }
        if (wall != null)
        {
            GameObject wallToInstantiate = Instantiate(wall, wall.transform.position + new Vector3(position.x * tileSize, 0, position.y * tileSize), rotation);
            wallToInstantiate.transform.SetParent(transform);
        }       
    }

    private void PlaceTransitionTile(Vector2Int position, GameObject tile) 
    {
        GameObject instantiatedTile = Instantiate(tile);
        instantiatedTile.transform.position = new Vector3(position.x * tileSize, 0, position.y * tileSize);
        instantiatedTile.transform.SetParent(transform);
    }

    private void PlaceSingleTile(Vector2Int position, int totalWeight)
    {
        var tileIndex = RandomWeighted(totalWeight);
        GameObject tile = Instantiate(tileSetData.tiles[tileIndex]);
        tile.transform.position = new Vector3(position.x * tileSize, 0, position.y * tileSize);
        tile.transform.SetParent(transform);
    }

    internal void PlaceSingleCornerWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        GameObject wall = null;
        var rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt))
        {
            wall = tileSetData.wallDiagonalCornerDownLeft;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt))
        {
            wall = tileSetData.wallDiagonalCornerDownRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
        {
            wall = tileSetData.wallDiagonalCornerUpRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt))
        {
            wall = tileSetData.wallDiagonalCornerUpLeft;
        }
        else if (WallTypesHelper.wallFullEightDirections.Contains(typeAsInt))
        {
            wall = tileSetData.wallFull;
        }
        else if (WallTypesHelper.wallBottmEightDirections.Contains(typeAsInt))
        {
            wall = tileSetData.wallBottom;
        }

        if (wall != null)
        {
            GameObject wallToInstantiate = Instantiate(wall, wall.transform.position + new Vector3(position.x * tileSize, 0, position.y * tileSize), rotation);
            wallToInstantiate.transform.SetParent(transform);
        }
    }

    private int RandomWeighted(int totalWeight)
    {
        int result = 0, total = 0;
        int randVal = UnityEngine.Random.Range(0, totalWeight + 1);
        for (result = 0; result < tileSetData.tiles.Count; result++)
        {
            total += tileSetData.tiles[result].GetComponent<TileWeight>().SpawnChange;
            if (total >= randVal) break;
        }
        return result;
    }
}
