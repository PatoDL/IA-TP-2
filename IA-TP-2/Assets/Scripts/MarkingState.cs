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
        if(explorerBehaviour != null)
            StartNewPath(explorerBehaviour.nearestMine.position);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        transform.rotation = Quaternion.LookRotation(new Vector3(Target.x - transform.position.x, 0f,
            Target.z - transform.position.z), Vector3.up);

        transform.position += transform.forward * speed * Time.deltaTime;

        Vector2 pos = new Vector2(transform.position.x, transform.position.z);
        Vector2 tar = new Vector2(Target.x, Target.z);

        if (Vector2.Distance(pos, tar) < nearRadius)
        {
            if (pathIndex < Path.Count - 1)
            {
                pathIndex++;
                Target = Path[pathIndex].worldPosition;
            }
            else
            {
                explorerBehaviour.nearestMine.GetComponent<MineBehaviour>().Explore();
                animator.SetTrigger("MineMarked");
                explorerBehaviour.canSearch = false;
            }
        }
    }
}
