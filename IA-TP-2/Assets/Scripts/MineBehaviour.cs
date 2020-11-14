using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehaviour : MonoBehaviour
{
    public bool alreadyExplored;

    public delegate void OnMineExplore();

    public OnMineExplore ExploreMine;

    public int goldAmount;

    // Start is called before the first frame update
    void Start()
    {
        //alreadyExplored = false;
    }

    public void Explore()
    {
        alreadyExplored = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
