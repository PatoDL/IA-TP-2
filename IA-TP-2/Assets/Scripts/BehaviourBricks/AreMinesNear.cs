using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;

[Condition("AreMinesNear")]
public class AreMinesNear : ConditionBase
{
    [InParam("ExploderBehaviour")]
    ExploderBehaviour exploderBehaviour;

    public override bool Check()
    {
        if(exploderBehaviour.NearestMine != null)
            return true;

        return false;
    }
}
