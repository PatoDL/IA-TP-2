using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorerBehaviour : MonoBehaviour
{
    public Grid grid;
    public float mineSearchRadius;
    public LayerMask mineLayer;

    public Transform nearestMine;
    private Animator animator;
    public bool canSearch;

    public float searchChargeTime;
    private float searchChargeTimeHandler;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        searchChargeTimeHandler = searchChargeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSearch)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, mineSearchRadius, mineLayer);

            for(int i=0;i < colliders.Length; i++)
            {
                MineBehaviour mineBehaviour = colliders[i].GetComponent<MineBehaviour>();
                if (mineBehaviour != null && !mineBehaviour.alreadyExplored)
                {
                    if (nearestMine == null || Vector3.Distance(transform.position, colliders[i].transform.position) < Vector3.Distance(transform.position, nearestMine.position))
                    {
                        nearestMine = colliders[i].transform;
                        animator.SetTrigger("MineFound");
                    }
                }
            }
        }
        else
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
