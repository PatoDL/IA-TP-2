using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploderBehaviour : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private float speed = 5f;
    [SerializeField] private float nearRadius = 0.3f;
    [SerializeField] private float targetRadius = 2f;
    [SerializeField] private float mineRadius = 3f;
    [SerializeField] private float explosionRadius = 1f;
    [SerializeField] private Grid grid;
    #endregion

    #region PRIVATE_FIELDS
    private BehaviorExecutor behaviorExecutor;
    private Pathfinding pathfinding;
    private bool reachedLocation = false;
    private Vector3 target;
    private List<Node> path;
    private int pathIndex;
    private GameObject nearestMine;
    #endregion

    public System.Action<GameObject> OnMineFound = null;

    #region PROPERTIES
    public bool ReachedLocation { get => reachedLocation; set => reachedLocation = value; }
    public GameObject NearestMine { get => nearestMine; set => nearestMine = value; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        OnMineFound = SetMine;
        behaviorExecutor = GetComponent<BehaviorExecutor>();
        pathfinding = GetComponent<Pathfinding>();
    }

    private void SetMine(GameObject g)
    {
        NearestMine = g;
    }

    protected Vector3 SearchForPoint()
    {
        Vector2 aux = Random.insideUnitCircle * targetRadius;
        Vector3 pos = new Vector3(transform.position.x + aux.x, transform.position.y, transform.position.z + aux.y);
        Node n = grid.NodeFromWorldPoint(pos);
        if (n == null)
        {
            while (n == null)
            {
                aux = Random.insideUnitCircle * targetRadius;
                pos = new Vector3(transform.position.x + aux.x, transform.position.y, transform.position.z + aux.y);
                n = grid.NodeFromWorldPoint(pos);
            }
        }

        return n.worldPosition;
    }

    public void StartNewPath()
    {
        target = SearchForPoint();
        pathfinding.FindPath(transform.position, target, grid);
        path = grid.path;
        pathIndex = 0;
        target = path[pathIndex].worldPosition;
    }

    public void StartNewPath(Vector3 newTarget)
    {
        target = newTarget;
        pathfinding.FindPath(transform.position, target, grid);
        path = grid.path;
        pathIndex = 0;
        target = path[pathIndex].worldPosition;
    }

    public void ContinuePath()
    {
        transform.rotation = Quaternion.LookRotation(new Vector3(target.x - transform.position.x, 0f,
            target.z - transform.position.z), Vector3.up);

        transform.position += transform.forward * speed * Time.deltaTime;

        Vector2 pos = new Vector2(transform.position.x, transform.position.z);
        Vector2 tar = new Vector2(target.x, target.z);

        if (Vector2.Distance(pos, tar) < nearRadius)
        {
            if (pathIndex < path.Count - 1)
            {
                pathIndex++;
                target = path[pathIndex].worldPosition;
            }
            else
            {
                reachedLocation = true;
            }
        }
    }

    public void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, LayerMask.NameToLayer("MineLayer"));
    
        for (int i = 0; i < colliders.Length; i++)
        {
            MineBehaviour mb = colliders[i].GetComponent<MineBehaviour>();

            Destroy(mb.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    
}
