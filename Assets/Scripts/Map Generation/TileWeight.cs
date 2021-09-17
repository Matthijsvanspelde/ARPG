using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileWeight : MonoBehaviour
{
    [SerializeField]
    private int spawnChance = 1;

    public int SpawnChange { get => spawnChance; private set => spawnChance = value; }
}

