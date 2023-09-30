using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NavNodeInterface))]
public class Spawn : MonoBehaviour
{
    public GameObject prefab;

    public NavBuilding building;
    private NavNodeInterface own;

    public bool isSpawning;
    public float cooldown;
    private float spawnCount;



    // Start is called before the first frame update
    void Start()
    {
        own = GetComponent<NavNodeInterface>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCount > cooldown && isSpawning) {
            var newEntity = Instantiate(prefab, transform.position, Quaternion.identity);
            var nav = newEntity.GetComponent<Navigator>();
            nav.goal = own.Other;
            nav.navManager = building;
            nav.currentNode = own;
            nav.isGoing = true;

            spawnCount = 0;
        }
        spawnCount += Time.deltaTime;
    }
}
