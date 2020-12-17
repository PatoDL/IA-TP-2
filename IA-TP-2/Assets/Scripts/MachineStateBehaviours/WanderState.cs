using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : MyStateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, animatorStateInfo, layerIndex);
        StartNewPath();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, animatorStateInfo, layerIndex);

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
                StartNewPath();
            }
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        
    }
}
