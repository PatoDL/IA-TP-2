using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;

[Condition("HasReachedMine")]
public class HasReachedMine : ConditionBase
{
    [InParam("ExploderBehaviour")]
    ExploderBehaviour exploderBehaviour;

    public override bool Check()
    {
        if(exploderBehaviour.ReachedLocation && exploderBehaviour.NearestMine)
            return true;

        return false;
    }
}
