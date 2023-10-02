using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NavNodeInterface))]
public class Spawn : MonoBehaviour
{
    [Serializable]
    public class SpawnSettings
    {
        public int weight;
        public GameObject prefab;
    }

    public List<SpawnSettings> Spawns;

    public NavBuilding building;
    private NavNodeInterface own;

    public bool isSpawning;
    public float cooldown;
    public float cooldownPull = 0.5f;

    private float spawnCount;

    private int totalSpawnWeight;

    private float averageSpawnMood;

    private GameObject getRandomSpawn()
    {
        int cnt = UnityEngine.Random.Range(0, totalSpawnWeight);
        int i = 0;
        while (cnt > 0) {
            cnt -= Spawns[i].weight;
            if (cnt <= 0)
                return Spawns[i].prefab;
            i++;
        }
        return Spawns[i].prefab;
    }

    // Start is called before the first frame update
    void Start()
    {
        own = GetComponent<NavNodeInterface>();
        spawnCount = cooldown;
        RecalculateWeights();
    }

    public void OnPlayerAction() {
        spawnCount += cooldown / averageSpawnMood * (1f - cooldownPull);
        Debug.Log("Action test");
    }

    public void RecalculateWeights() {
        totalSpawnWeight = 0;
        averageSpawnMood = 0;
        foreach (var s in Spawns)
        {
            totalSpawnWeight += s.weight;
            averageSpawnMood += (float) s.prefab.GetComponent<Entity>().mood * s.weight;
        }
        averageSpawnMood /= totalSpawnWeight;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCount > cooldown && isSpawning) {
            var newEntity = Instantiate(getRandomSpawn(), transform.position, Quaternion.identity);
            var nav = newEntity.GetComponent<Navigator>();
            nav.goal = own.Other;
            nav.navManager = building;
            nav.currentNode = own;
            nav.isGoing = true;

            spawnCount = 0;
        }
        spawnCount += cooldownPull * Time.deltaTime;
    }
}
