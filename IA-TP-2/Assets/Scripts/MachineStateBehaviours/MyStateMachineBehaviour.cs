using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyStateMachineBehaviour : StateMachineBehaviour
{
    public float targetRadius = 50f;
    public Vector3 target;
    public float speed = 5f;
    protected Transform transform;
    public float nearRadius = 0.3f;
    protected Pathfinding pathfinding;
    protected int pathIndex = 0;
    protected List<Node> path;
    protected Grid grid;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        transform = animator.GetComponent<Transform>();
        pathfinding = animator.GetComponent<Pathfinding>();
        grid = animator.GetComponent<WorkerBehaviour>().grid;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        pathIndex = 0;
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

    protected void StartNewPath()
    {
        target = SearchForPoint();
        pathfinding.FindPath(transform.position, target, grid);
        path = grid.path;
        if (path == null)
        {
            Debug.Log("nulo");
        }
        pathIndex = 0;
        target = path[pathIndex].worldPosition;
    }

    protected void StartNewPath(Vector3 newTarget)
    {
        target = newTarget;
        pathfinding.FindPath(transform.position, target, grid);
        path = grid.path;
        pathIndex = 0;
        target = path[pathIndex].worldPosition;
    }
}
