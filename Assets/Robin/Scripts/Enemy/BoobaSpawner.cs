using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoobaSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public int numberEnemies;
    }

    public GameObject booba;
    List<GameObject> total = new List<GameObject>();

    public Wave[] waves;

    int numberWaves;
    int currentWave;

    int currentSpawned;

    float startTime;
    public float spawnInterval;

    bool canSpawn;
    bool nextWaveReady;

    void Start()
    {
        numberWaves = waves.Length;
        numberWaves -= 1;

        currentWave = 0;
    }

    void Update()
    {
        if(currentWave > numberWaves)
        {
            print("No next wave");
            canSpawn = false;
        }

        if(canSpawn)
        {
            SpawnWave();
        }
    }

    void SpawnWave()
    {

        if(canSpawn && Time.time - startTime > spawnInterval)
        {
            if(currentSpawned < waves[currentWave].numberEnemies)
            {
                GameObject inst = Instantiate(booba, transform.position, Quaternion.identity);
                inst.transform.SetParent(transform);

                total.Add(inst);
                nextWaveReady = true; ;
            }            

            currentSpawned++;
            startTime = Time.time;
        }

        for(int i = total.Count - 1; i > -1; i--)
        {
            if(total[i] == null)
            {
                total.RemoveAt(i);
            }
        }

        if(total.Count == 0)
        {
            NextWave();
        }

    }

    void NextWave()
    {
        if(canSpawn && nextWaveReady)
        {
            print("Next Wave");

            nextWaveReady = false;

            currentSpawned = 0;
            currentWave++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            print("You shall not Pass");

            canSpawn = true;
        }
    }
}
