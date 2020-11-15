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

    public float detectionConeMax;

    public HeadQuarterBehaviour headQuartersBehaviour;

    public Vector3 positionWhenMineFound = Vector3.zero;

    virtual protected void Start()
    {
        if (headQuartersBehaviour == null)
        {
            headQuartersBehaviour = GameObject.Find("HQ").GetComponent<HeadQuarterBehaviour>();
        }
        transform.parent = headQuartersBehaviour.transform;
        animator = GetComponent<Animator>();
    }

    List<MineBehaviour> GetNearMines()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward, mineSearchRadius, mineLayer);
        List<MineBehaviour> mineBehavioursList = new List<MineBehaviour>();

        

        for (int i = 0; i < colliders.Length; i++)
        {
            MineBehaviour mineBehaviour = colliders[i].GetComponent<MineBehaviour>();
            
            if (mineBehaviour != null)
            {
                bool isObstacleInBetween = Physics.Raycast(transform.position,
                    mineBehaviour.transform.position - transform.position, 100, obstacleMask);

                bool inDetectionCone = Vector3.Angle(transform.position + transform.forward,
                    mineBehaviour.transform.position - transform.position) <= detectionConeMax;

                if(!isObstacleInBetween && inDetectionCone)
                    mineBehavioursList.Add(mineBehaviour);
            }
        }

        return mineBehavioursList;
    }

    virtual protected void SearchNearestMine(MineBehaviour otherMine)
    {
        if (nearestMine == null || Vector3.Distance(transform.position, otherMine.transform.position) < Vector3.Distance(transform.position, nearestMine.position))
        {
            if (!headQuartersBehaviour.CheckIfMineIsAlreadyTaken(otherMine, this))
            {
                nearestMine = otherMine.transform;
            }
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

            if (nearestMine != null)
            {
                animator.SetTrigger("MineFound");
                positionWhenMineFound = transform.position;
            }
               
        }
    }
}
