using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class TileGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject tile;
    [SerializeField]
    private GameObject wall;
    private float tileSize;


    private void Awake()
    {
        tileSize = tile.GetComponent<Renderer>().bounds.size.x;
    }

    private void Start()
    {
        
    }

    public void PlaceFloorTiles(IEnumerable<Vector2Int> floorPositions) 
    {
        PlaceTiles(floorPositions);
    }

    internal void PlaceSingleWall(Vector2Int position)
    {
        Instantiate(wall, transform.position + new Vector3(position.x * tileSize, 0, position.y * tileSize), Quaternion.identity);
    }

    private void PlaceTiles(IEnumerable<Vector2Int> floorPositions)
    {
        foreach (var position in floorPositions)
        {
            PlaceSingleTile(position);
        }
    }

    private void PlaceSingleTile(Vector2Int position)
    {
        GameObject idkman = Instantiate(tile);
        Debug.Log(tileSize);
        idkman.transform.position = transform.position + new Vector3(position.x * tileSize, 0, position.y * tileSize);
    }
}
