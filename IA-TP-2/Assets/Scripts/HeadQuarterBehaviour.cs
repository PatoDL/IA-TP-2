using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadQuarterBehaviour : MonoBehaviour
{
    public float gold;

    public GameObject minerPF;
    public GameObject explorerPF;

    private List<MinerBehaviour> minerList = new List<MinerBehaviour>();
    private List<ExplorerBehaviour> explorerList = new List<ExplorerBehaviour>();

    public float minerCost;
    public float explorerCost;

    public Grid grid;

    public void OnMinerReturn(MinerBehaviour minerBehaviour)
    {
        gold += minerBehaviour.goldCollected;
        minerBehaviour.goldCollected = 0;
    }

    void SpawnMiner()
    {
        GameObject g = Instantiate(minerPF, transform.position, Quaternion.identity);
        MinerBehaviour m = g.GetComponent<MinerBehaviour>();
        m.headQuartersBehaviour = this;
        m.grid = grid;
        minerList.Add(m);
    }

    void SpawnExplorer()
    {
        GameObject g = Instantiate(explorerPF, transform.position, Quaternion.identity);
        ExplorerBehaviour e = g.GetComponent<ExplorerBehaviour>();
        e.headQuartersBehaviour = this;
        e.grid = grid;
        explorerList.Add(e);
    }

    public bool CheckIfMineIsAlreadyTaken(MineBehaviour mine, WorkerBehaviour worker)
    {
        ExplorerBehaviour explorer = worker.GetComponent<ExplorerBehaviour>();
        if (explorer)
        {
            foreach (ExplorerBehaviour e in explorerList)
            {
                if (e.nearestMine == mine.transform)
                {
                    return true;
                }
            }
        }
        else
        {
            foreach (MinerBehaviour m in minerList)
            {
                if (m.nearestMine == mine.transform)
                {
                    return true;
                }
            }
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && gold > minerCost)
        {
            SpawnMiner();
            gold -= minerCost;
        }

        if (Input.GetKeyDown(KeyCode.E) && gold > explorerCost)
        {
            SpawnExplorer();
            gold -= explorerCost;
        }
    }
}
