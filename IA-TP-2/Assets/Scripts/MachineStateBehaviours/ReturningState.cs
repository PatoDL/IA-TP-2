using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturningState : MyStateMachineBehaviour
{
    private MinerBehaviour minerBehaviour;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        minerBehaviour = animator.GetComponent<MinerBehaviour>();
        if (minerBehaviour != null && minerBehaviour.headQuartersBehaviour != null)
        {
            StartNewPath(minerBehaviour.headQuartersBehaviour.transform.position);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
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
            else if (minerBehaviour.headQuartersBehaviour != null)
            {
                minerBehaviour.headQuartersBehaviour.OnMinerReturn(minerBehaviour);
                animator.SetTrigger("ContinueWorking");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("MineMined",false);
        animator.SetBool("PocketFull", false);
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
