using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCrateScript : MonoBehaviour
{
    // Start is called before the first frame update

    public float minTime = 0;
    public float maxTime = 5;

    public GameObject spawnee;

    private float spawnTime = 0;
    private float time = 0;
    void Start()
    {
        spawnTime = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > spawnTime)
        {
            Vector3 position = new Vector3(Random.Range(-10, 10), 0.5f, Random.Range(-10, 10));
            Instantiate(spawnee, position, Quaternion.identity);
            time = 0;
            spawnTime = Random.Range(minTime, maxTime);
        }
    }
}
