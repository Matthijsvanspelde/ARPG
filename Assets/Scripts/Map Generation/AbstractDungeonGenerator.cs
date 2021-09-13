using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected TileGenerator tileGenerator = null;
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    private void Start()
    {
        GenerateDungeon();
    }

    public void GenerateDungeon() 
    {
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration();
}
