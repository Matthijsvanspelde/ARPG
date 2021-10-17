using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class TileEndInteraction : MonoBehaviour
{
    private GameObject target;
    private NavMeshAgent agent;
    private TileGenerator tileGenerator;
    private AbstractDungeonGenerator dungeonGenerator;
    private bool hasGenerated = false;
    [SerializeField]
    private Animator animator;
    private TargetRange targetRange;

    private void Awake()
    {
        targetRange = GetComponent<TargetRange>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        WalkToEndTile();
        GoToNextLevel();
    }

    private void GoToNextLevel()
    {
        if (targetRange.IsAtTarget(target, 1.5f) && !hasGenerated)
        {
            animator.SetTrigger("Fade");
            tileGenerator = GameObject.Find("Tile Builder").GetComponent<TileGenerator>();
            tileGenerator.SetTileSetData();
            dungeonGenerator = GameObject.Find("Dungeon Generator").GetComponent<CorridorFirst>();
            dungeonGenerator.GenerateDungeon();
            gameObject.transform.position = new Vector3(0, transform.position.y, 0);
            hasGenerated = true;
        }
    }

    private void WalkToEndTile()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Stairs") && !EventSystem.current.IsPointerOverGameObject())
            {
                hasGenerated = false;
                target = hit.collider.gameObject;
                agent.destination = target.transform.position;
            }
            else
            {
                target = null;
            }
        }
    }
}
