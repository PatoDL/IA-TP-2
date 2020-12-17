using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using BBUnity.Actions;

[Action("MyActions/GoToLocation")]
public class GoToLocation : GOAction
{
    ExploderBehaviour exploderBehaviour;

    public override void OnStart()
    {
        exploderBehaviour = gameObject.GetComponent<ExploderBehaviour>();
        exploderBehaviour.StartNewPath(exploderBehaviour.NearestMine.transform.position);
    }

    public override TaskStatus OnUpdate()
    {
        if (exploderBehaviour.ReachedLocation)
        {
            exploderBehaviour.ReachedLocation = false;
            return TaskStatus.COMPLETED;
        }
            

        exploderBehaviour.ContinuePath();

        return TaskStatus.RUNNING;
    }
}
