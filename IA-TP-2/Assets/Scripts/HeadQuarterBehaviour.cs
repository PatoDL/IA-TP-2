using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadQuarterBehaviour : MonoBehaviour
{
    public int gold;

    public void OnMinerReturn(MinerBehaviour minerBehaviour)
    {
        gold += minerBehaviour.goldCollected;
        minerBehaviour.goldCollected = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
