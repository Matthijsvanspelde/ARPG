using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TileSetData : ScriptableObject
{
    public List<GameObject> tiles = new List<GameObject>();
    public GameObject endTile;
    public GameObject startTile;
    public GameObject wallTop;
    public GameObject wallRight;
    public GameObject wallLeft;
    public GameObject wallBottom;
    public GameObject wallFull;
    public GameObject wallDiagonalCornerDownRight;
    public GameObject wallDiagonalCornerDownLeft;
    public GameObject wallDiagonalCornerUpRight;
    public GameObject wallDiagonalCornerUpLeft;
}
