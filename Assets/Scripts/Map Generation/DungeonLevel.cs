using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DungeonLevel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text levelText;
    [SerializeField]
    private List<TileSetData> tileSets = new List<TileSetData>();
    public int level = 1;

    private void Awake()
    {
        SetLevelText();
    }

    public TileSetData GetTileSet() 
    {
        TileSetData tileSetData = null;
        if (tileSets.Count >= level)
        {
            tileSetData = tileSets[level - 1];
            
        }
        return tileSetData;
    }

    public void SetLevelText()
    {
        levelText.text = "Floor: " + level;
    }

}
