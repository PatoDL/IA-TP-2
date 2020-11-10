using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyStateMachineBehaviour : StateMachineBehaviour
{
    public float TargetRadius;
    public Vector3 Target;
    public float speed;
    protected Transform transform;
    public float nearRadius;
    protected Pathfinding pathfinding;
    protected int pathIndex = 0;
    protected List<Node> Path;
    protected Grid grid;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        transform = animator.GetComponent<Transform>();
        pathfinding = animator.GetComponent<Pathfinding>();
        grid = animator.GetComponent<ExplorerBehaviour>().grid;

       
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        pathIndex = 0;
    }

    protected Vector3 SearchForPoint()
    {
        Vector2 aux = Random.insideUnitCircle * TargetRadius;
        Vector3 pos = new Vector3(transform.position.x + aux.x, transform.position.y, transform.position.z + aux.y);
        Node n = grid.NodeFromWorldPoint(pos);
        if (n == null)
        {
            while (n == null)
            {
                aux = Random.insideUnitCircle * TargetRadius;
                pos = new Vector3(transform.position.x + aux.x, transform.position.y, transform.position.z + aux.y);
                n = grid.NodeFromWorldPoint(pos);
            }
        }

        return n.worldPosition;
    }

    protected void StartNewPath()
    { 
        Target = SearchForPoint();
        pathfinding.FindPath(transform.position, Target, grid);
        Path = grid.path;
        pathIndex = 0;
        Target = Path[pathIndex].worldPosition;
    }

    protected void StartNewPath(Vector3 newTarget)
    {
        Target = newTarget;
        pathfinding.FindPath(transform.position, Target, grid);
        Path = grid.path;
        pathIndex = 0;
        Target = Path[pathIndex].worldPosition;
    }
}
