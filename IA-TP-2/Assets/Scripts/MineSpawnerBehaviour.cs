using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class MineSpawnerBehaviour : MonoBehaviour
{
    public float spawnTime;
    private float spawnTimeHandler;
    public Collider floor;
    public GameObject minePF;
    public LayerMask mineLayer;
    public int minAmountOfUnexploredMines;
    private List<MineBehaviour> mines = new List<MineBehaviour>();

    public float unexploredMines = 0;

    public bool shouldSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimeHandler = spawnTime;
        if (shouldSpawn)
        {
            while (mines.Count < minAmountOfUnexploredMines)
            {
                SpawnMine();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime -= Time.deltaTime;
        if (spawnTime <= 0)
        {
            SpawnMine();
            spawnTime = spawnTimeHandler;
        }

        unexploredMines = 0;
        for (int i = 0; i < mines.Count; i++)
        {
            if (!mines[i].alreadyExplored)
                unexploredMines++;
        }
    }

    public void RemoveMine(GameObject g)
    {
        mines.Remove(g.GetComponent<MineBehaviour>());
    }

    private void SpawnMine()
    {
        if (shouldSpawn)
        {
            float mineSize = minePF.transform.localScale.x;
            mineSize /= 2;
            float randomX = Random.Range(floor.bounds.min.x + mineSize, floor.bounds.max.x - mineSize);
            float randomZ = Random.Range(floor.bounds.min.z + mineSize, floor.bounds.max.z - mineSize);
            Vector3 gPos = new Vector3(randomX, floor.bounds.max.y, randomZ);

            Collider[] nearColliders = Physics.OverlapSphere(gPos, mineSize, mineLayer);

            if (nearColliders.Length == 0)
            {
                GameObject g = Instantiate(minePF);
                g.transform.position = gPos;
                g.transform.rotation = Quaternion.identity;

                MineBehaviour mineBehaviour = g.GetComponent<MineBehaviour>();
                mineBehaviour.mineSpawner = this;
                mines.Add(mineBehaviour);
            }
        }
    }

    public void CheckExploredMines()
    {
        if (shouldSpawn)
        {
            int unexploredMines = 0;
            while (unexploredMines < minAmountOfUnexploredMines)
            {
                for (int i = 0; i < mines.Count; i++)
                {
                    if (!mines[i].alreadyExplored)
                        unexploredMines++;
                }

                if (unexploredMines < minAmountOfUnexploredMines)
                    SpawnMine();
            }
        }
    }
}
