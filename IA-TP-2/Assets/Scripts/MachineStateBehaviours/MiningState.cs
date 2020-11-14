using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningState : MyStateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    private MinerBehaviour minerBehaviour;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        minerBehaviour = animator.GetComponent<MinerBehaviour>();
        if (minerBehaviour != null && minerBehaviour.nearestMine != null)
        {
            StartNewPath(minerBehaviour.nearestMine.position);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
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
            else if (minerBehaviour.nearestMine != null)
            {
                minerBehaviour.isMining = true;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        minerBehaviour.isMining = false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
