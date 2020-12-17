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
        Debug.Log("Entered gotolocation");
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

        Debug.Log("going to location");
        exploderBehaviour.ContinuePath();

        return TaskStatus.RUNNING;
    }
}
