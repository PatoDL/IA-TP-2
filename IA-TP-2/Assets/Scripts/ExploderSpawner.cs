using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploderSpawner : MonoBehaviour
{
    public float spawnTime;
    private float spawnTimeHandler;
    public Collider floor;
    public ExploderBehaviour exploderPF;

    void Start()
    {
        spawnTimeHandler = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime -= Time.deltaTime;
        if (spawnTime <= 0)
        {
            SpawnExploder();
            spawnTime = spawnTimeHandler;
        }
    }

    void SpawnExploder()
    {
        float mineSize = exploderPF.transform.localScale.x;
        mineSize /= 2;
        float randomX = Random.Range(floor.bounds.min.x + mineSize, floor.bounds.max.x - mineSize);
        float randomZ = Random.Range(floor.bounds.min.z + mineSize, floor.bounds.max.z - mineSize);
        Vector3 gPos = new Vector3(randomX, floor.bounds.max.y + exploderPF.transform.localScale.y/2f, randomZ);

        ExploderBehaviour g = Instantiate(exploderPF);
        g.transform.position = gPos;
        g.transform.rotation = Quaternion.identity;
        g.Grid = floor.GetComponent<Grid>();
    }
}
