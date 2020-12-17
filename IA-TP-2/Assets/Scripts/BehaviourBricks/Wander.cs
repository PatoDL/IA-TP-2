using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using BBUnity.Actions;

[Action("MyActions/Wander")]
public class Wander : GOAction
{
    private ExploderBehaviour exploderBehaviour;

    public override void OnStart()
    {
        exploderBehaviour = gameObject.GetComponent<ExploderBehaviour>();
        exploderBehaviour.StartNewPath();
    }

    public override TaskStatus OnUpdate()
    {
        if(exploderBehaviour.ReachedLocation)
        {
            exploderBehaviour.ReachedLocation = false;
            return TaskStatus.COMPLETED;
        }
            
        
        exploderBehaviour.ContinuePath();
        return TaskStatus.RUNNING;
    }
}
