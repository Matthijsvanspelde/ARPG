using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonLevel : MonoBehaviour
{
    [SerializeField]
    private List<TileSetData> tileSets = new List<TileSetData>();
    public int level = 1;

    public TileSetData GetTileSet() 
    {
        TileSetData tileSetData = null;
        if (tileSets.Count >= level)
        {
            tileSetData = tileSets[level - 1];
            
        }
        return tileSetData;
    }

}
