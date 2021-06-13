using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnFactory : MonoBehaviour
{
    public GameObject factory;
    private GameObject newFactory;
    public float spawnCooldown = 21;
    public bool canSpawn;
    private float xPos;
    private float yPos;
    private int totalFactoriesSpawned;

    // Start is called before the first frame update
    void Start()
    {
        canSpawn = false;
        totalFactoriesSpawned = 0;
        StartCoroutine(firstSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn && transform.childCount < 5)
        {
            xPos = Random.Range(-15.5f, 16f);
            yPos = Random.Range(-9f, 8f);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(xPos, yPos), -Vector2.up);
            if (hit.collider != null)
            {
                if (hit.collider.tag != "animal" && hit.collider.tag != "factory" && hit.collider.tag != "animal" && hit.collider.tag != "tree" && hit.collider.tag != "Player")
                {
                    newFactory = Instantiate(factory, new Vector2(xPos, yPos), transform.rotation);
                    newFactory.transform.parent = transform;
                    newFactory.GetComponent<enemySpawn>().spawnTimer -= 0.2f * totalFactoriesSpawned;
                    newFactory.GetComponent<enemySpawn>().maxHumans += totalFactoriesSpawned;
                    canSpawn = false;
                    totalFactoriesSpawned++;
                    StartCoroutine(waitToSpawn());
                }                
            }
            else
            {
                newFactory = Instantiate(factory, new Vector2(xPos, yPos), transform.rotation);
                newFactory.transform.parent = transform;
                canSpawn = false;
                totalFactoriesSpawned++;
                StartCoroutine(waitToSpawn());
            }
            if (!canSpawn && spawnCooldown > 5)
            {
                spawnCooldown--;
            }
        }
    }

    IEnumerator firstSpawn()
    {
        //spawns the first factory 10 seconds into the game
        yield return new WaitForSeconds(10);
        canSpawn = true;
    }

    IEnumerator waitToSpawn()
    {
        yield return new WaitForSeconds(spawnCooldown);
        canSpawn = true;
    }
}
