using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerBehaviour : WorkerBehaviour
{
    public int maxGoldAmount;
    public int goldCollected;

    public bool isMining;
    public int goldPerSecond;

    public float timer;
    float timerHandler;

    public Transform headQuartersTransform;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        timerHandler = timer;
    }

    protected override void SearchNearestMine(MineBehaviour otherMine)
    {
        if (otherMine.alreadyExplored)
            base.SearchNearestMine(otherMine);
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        if (isMining)
        {
            MineBehaviour mineBehaviour = nearestMine.GetComponent<MineBehaviour>();
            if (goldCollected < maxGoldAmount && mineBehaviour.goldAmount > 0f)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = timerHandler;
                    goldCollected += goldPerSecond;
                    mineBehaviour.goldAmount -= goldPerSecond;
                }
            }

            if (mineBehaviour.goldAmount <= 0f)
            {
                goldCollected += mineBehaviour.goldAmount;
                Destroy(nearestMine.gameObject);
                nearestMine = null;
                animator.SetBool("MineMined", true);
            }
            else if(goldCollected >= maxGoldAmount)
            {
                goldCollected = maxGoldAmount;
                animator.SetBool("PocketFull", true);
            }
            
        }
    }
}
