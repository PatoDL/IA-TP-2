using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorerBehaviour : WorkerBehaviour
{
    public float searchChargeTime;
    private float searchChargeTimeHandler;

    public bool marking;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        marking = false;
        searchChargeTimeHandler = searchChargeTime;
    }

    protected override void SearchNearestMine(MineBehaviour otherMine)
    {
        if(!otherMine.alreadyExplored)
            base.SearchNearestMine(otherMine);
    }

    // Update is called once per frame
    new void Update() 
    {
        base.Update();
        if(!canSearch && !marking)
        {
            searchChargeTime -= Time.deltaTime;
            if (searchChargeTime <= 0)
            {
                searchChargeTime = searchChargeTimeHandler;
                canSearch = true;
            }
        }
    }
}
