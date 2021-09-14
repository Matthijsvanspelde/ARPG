using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class TileGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject tile;
    [SerializeField]
    private GameObject wallTop;
    [SerializeField]
    private GameObject wallRight;
    [SerializeField]
    private GameObject wallLeft;
    [SerializeField]
    private GameObject wallBottom;
    [SerializeField]
    private GameObject wallFull;
    [SerializeField]
    private GameObject wallDiagonalCornerDownRight;
    [SerializeField]
    private GameObject wallDiagonalCornerDownLeft;
    [SerializeField]
    private GameObject wallDiagonalCornerUpRight;
    [SerializeField]
    private GameObject wallDiagonalCornerUpLeft;
    private float tileSize;
    private NavMeshSurface navMeshSurface;

    private void Awake()
    {
        tileSize = tile.GetComponent<Renderer>().bounds.size.x;
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    public void BakeNavMesh() 
    {
        navMeshSurface.BuildNavMesh();
    }

    public void PlaceFloorTiles(IEnumerable<Vector2Int> floorPositions) 
    {
        PlaceTiles(floorPositions);
    }

    private void PlaceTiles(IEnumerable<Vector2Int> floorPositions)
    {
        foreach (var position in floorPositions)
        {
            PlaceSingleTile(position);
        }
    }

    internal void PlaceSingleWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        GameObject wall = null;
        var rotation = Quaternion.Euler(new Vector3(0,0,0));
        if (WallTypesHelper.wallTop.Contains(typeAsInt))
        {
            wall = wallTop;
            rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (WallTypesHelper.wallRight.Contains(typeAsInt))
        {
            wall = wallRight;
            rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
        else if (WallTypesHelper.wallLeft.Contains(typeAsInt))
        {
            wall = wallLeft;
            rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
        else if (WallTypesHelper.wallBottom.Contains(typeAsInt))
        {
            wall = wallBottom;
            rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        }
        else if (WallTypesHelper.wallFull.Contains(typeAsInt))
        {
            wall = wallFull;
            rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        }
        if (wall != null)
        {
            GameObject wallToInstantiate = Instantiate(wall, wall.transform.position + new Vector3(position.x * tileSize, 0, position.y * tileSize), rotation);
            wallToInstantiate.transform.SetParent(transform);
        }       
    }

    

    private void PlaceSingleTile(Vector2Int position)
    {
        GameObject tile = Instantiate(this.tile);
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
            wall = wallDiagonalCornerDownLeft;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt))
        {
            wall = wallDiagonalCornerDownRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
        {
            wall = wallDiagonalCornerUpRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt))
        {
            wall = wallDiagonalCornerUpLeft;
        }
        else if (WallTypesHelper.wallFullEightDirections.Contains(typeAsInt))
        {
            wall = wallFull;
        }
        else if (WallTypesHelper.wallBottmEightDirections.Contains(typeAsInt))
        {
            wall = wallBottom;
        }

        if (wall != null)
        {
            GameObject wallToInstantiate = Instantiate(wall, wall.transform.position + new Vector3(position.x * tileSize, 0, position.y * tileSize), rotation);
            wallToInstantiate.transform.SetParent(transform);
        }
    }
}
