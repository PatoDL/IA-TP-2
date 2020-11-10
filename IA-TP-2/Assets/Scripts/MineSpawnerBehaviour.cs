using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSpawnerBehaviour : MonoBehaviour
{
    public float SpawnTime;
    private float SpawnTimeHandler;
    public Collider Floor;
    public GameObject MinePF;
    public LayerMask MineLayer;
    public int minAmountOfUnexploredMines;
    private List<MineBehaviour> Mines = new List<MineBehaviour>();



    // Start is called before the first frame update
    void Start()
    {
        SpawnTimeHandler = SpawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTime -= Time.deltaTime;
        if (SpawnTime <= 0)
        {
            SpawnMine();
            SpawnTime = SpawnTimeHandler;
        }
    }

    private void SpawnMine()
    {
        float mineSize = MinePF.transform.localScale.x;
        mineSize /= 2;
        float randomX = Random.Range(Floor.bounds.min.x + mineSize, Floor.bounds.max.x - mineSize);
        float randomZ = Random.Range(Floor.bounds.min.z + mineSize, Floor.bounds.max.z - mineSize);
        Vector3 gPos = new Vector3(randomX, Floor.bounds.max.y, randomZ);

        Collider[] nearColliders = Physics.OverlapSphere(gPos, mineSize, MineLayer);

        if (nearColliders.Length == 0)
        {
            GameObject g = Instantiate(MinePF);
            g.transform.position = gPos;
            g.transform.rotation = Quaternion.identity;

            MineBehaviour mineBehaviour = g.GetComponent<MineBehaviour>();

            Mines.Add(mineBehaviour);
            mineBehaviour.ExploreMine = CheckExploredMines;
        }
    }

    private void CheckExploredMines()
    {
        int exploredMines = 0;
        for (int i = 0; i < Mines.Count; i++)
        {
            if (Mines[i].alreadyExplored)
                exploredMines++;
        }

        while (exploredMines < minAmountOfUnexploredMines)
            SpawnMine();
    }
}
