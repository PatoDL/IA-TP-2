using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;

using BBUnity.Actions;

[Action("MyActions/Explode")]
public class Explode : GOAction
{
    private ExploderBehaviour exploderBehaviour;

    public override void OnStart()
    {
        Debug.Log("Entered explode");
        exploderBehaviour = gameObject.GetComponent<ExploderBehaviour>();
        exploderBehaviour.Explode();
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.NONE;
    }
}
