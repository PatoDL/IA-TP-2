using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehaviour : MonoBehaviour
{
    public bool alreadyExplored;

    public delegate void OnMineExplore();

    public OnMineExplore ExploreMine;

    public delegate void OnMineEnding(GameObject g);

    public OnMineEnding EndMining;

    public int goldAmount;

    public MineSpawnerBehaviour mineSpawner;

    // Start is called before the first frame update
    void Start()
    {
        if (mineSpawner == null)
        {
            mineSpawner = GameObject.Find("MineSpawner").GetComponent<MineSpawnerBehaviour>();
        }

        transform.parent = mineSpawner.transform;

        ExploreMine += Mark;
        ExploreMine += mineSpawner.CheckExploredMines;
        EndMining += mineSpawner.RemoveMine;
        EndMining += Destroy;
    }

    void OnDestroy()
    {
        ExploreMine -= Mark;
        ExploreMine -= mineSpawner.CheckExploredMines;
        EndMining -= mineSpawner.RemoveMine;
        EndMining -= Destroy;
    }

    public void StartMining()
    {
        transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.color = Color.red;
    }

    public void Mark()
    {
        alreadyExplored = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
