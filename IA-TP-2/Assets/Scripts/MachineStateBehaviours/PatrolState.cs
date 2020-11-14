using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

public class PatrolState : MyStateMachineBehaviour
{
    private WorkerBehaviour workerBehaviour;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        workerBehaviour = animator.GetComponent<WorkerBehaviour>();
        if (workerBehaviour != null)
            workerBehaviour.canSearch = true;
        StartNewPath();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        if (workerBehaviour != null)
            workerBehaviour.canSearch = false;
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
                StartNewPath();
            }
        }
    }
}
