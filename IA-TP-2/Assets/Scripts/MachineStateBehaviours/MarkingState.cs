using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkingState : MyStateMachineBehaviour
{
    private ExplorerBehaviour explorerBehaviour;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        explorerBehaviour = animator.GetComponent<ExplorerBehaviour>();
        if (explorerBehaviour != null && explorerBehaviour.nearestMine != null)
        {
            StartNewPath(explorerBehaviour.nearestMine.position);
            explorerBehaviour.marking = true;
            explorerBehaviour.canSearch = false;
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

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
            else if(explorerBehaviour.nearestMine != null)
            {
                explorerBehaviour.nearestMine.GetComponent<MineBehaviour>().ExploreMine();
                explorerBehaviour.marking = false;
                explorerBehaviour.nearestMine = null;
                animator.SetTrigger("MineMarked");
            }
        }
    }
}
