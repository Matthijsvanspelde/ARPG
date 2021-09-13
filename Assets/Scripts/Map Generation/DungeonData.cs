using UnityEngine;

[CreateAssetMenu]
public class DungeonData : ScriptableObject
{
    public int iterations = 10;
    public int walkLength = 10;
    public bool startRandomlyEachIteration = true;
}
