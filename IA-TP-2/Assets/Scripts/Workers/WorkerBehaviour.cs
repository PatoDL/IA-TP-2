using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerBehaviour : MonoBehaviour
{
    public Grid grid;

    public float mineSearchRadius;
    public LayerMask mineLayer;

    public Transform nearestMine;
    protected Animator animator;

    public LayerMask obstacleMask;

    public bool canSearch;

    virtual protected void Start()
    {
        animator = GetComponent<Animator>();
    }

    List<MineBehaviour> GetNearMines()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward, mineSearchRadius, mineLayer);
        List<MineBehaviour> mineBehavioursList = new List<MineBehaviour>();

        

        for (int i = 0; i < colliders.Length; i++)
        {
            MineBehaviour mineBehaviour = colliders[i].GetComponent<MineBehaviour>();
            if (mineBehaviour != null && !Physics.Raycast(transform.position,
                (mineBehaviour.transform.position - transform.position).normalized,
                Vector3.Distance(transform.position, mineBehaviour.transform.position), obstacleMask))
            {
                mineBehavioursList.Add(mineBehaviour);
            }
        }

        return mineBehavioursList;
    }

    virtual protected void SearchNearestMine(MineBehaviour otherMine)
    {
        if (nearestMine == null || Vector3.Distance(transform.position, otherMine.transform.position) < Vector3.Distance(transform.position, nearestMine.position))
        {
            nearestMine = otherMine.transform;
        }
    }

    protected void Update()
    {
        //Debug.DrawRay(transform.position, transform.forward * 100);
        if (canSearch)
        {
            List<MineBehaviour> mineBehavioursList = GetNearMines();
            for (int i = 0; i < mineBehavioursList.Count; i++)
            {
                SearchNearestMine(mineBehavioursList[i]);
            }

            if(nearestMine != null)
                animator.SetTrigger("MineFound");
        }
    }
}
