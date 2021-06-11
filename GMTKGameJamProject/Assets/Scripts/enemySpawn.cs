using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{

    public GameObject enemyPrefab;
    public float spawnRadius;
    public float spawnTimer;

    private bool intoCouroutine;
    private GameObject enemySpawned;
    private Vector3 randomPos;

    // Start is called before the first frame update
    void Start()
    {
        intoCouroutine = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount < 10 && !intoCouroutine)
        {
            intoCouroutine = true;
            StartCoroutine(EnemySpawn());            
        }

    }

    
    IEnumerator EnemySpawn()
    {
        randomPos = Random.insideUnitSphere * spawnRadius;
        randomPos.z = -1;
        enemySpawned = Instantiate(enemyPrefab, transform.position + randomPos, Quaternion.identity);
        enemySpawned.transform.parent = gameObject.transform;
        yield return new WaitForSeconds(spawnTimer);
        intoCouroutine = false;
    }
}
