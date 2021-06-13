using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{

    public GameObject enemyPrefab;
    public float spawnRadius;
    public float spawnTimer = 3;
    public int maxHumans;

    private bool intoCouroutine;
    private GameObject enemySpawned;
    private Vector3 randomPos;

    // Start is called before the first frame update
    void Start()
    {
        maxHumans = 8;
        intoCouroutine = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer <= 0)
            spawnTimer = 0.1f;
        if (maxHumans >= 20)
            maxHumans = 20;
        if (transform.childCount < maxHumans && !intoCouroutine)
        {
            intoCouroutine = true;
            StartCoroutine(EnemySpawn());            
        }

    }

    
    IEnumerator EnemySpawn()
    {
        randomPos = Random.insideUnitSphere * spawnRadius;
        enemySpawned = Instantiate(enemyPrefab, transform.position + new Vector3 (randomPos.x, randomPos.y, 2), Quaternion.identity);
        enemySpawned.transform.parent = gameObject.transform;
        yield return new WaitForSeconds(spawnTimer);
        intoCouroutine = false;
    }
}
