using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalSpawning : MonoBehaviour
{
    public Transform Animals;
    public Transform Bear;
    public Text animalUI;
    private bool canSpawn;

    void Start()
    {
        canSpawn = true;
        animalUI = GameObject.Find("Bears").GetComponent<Text>();
        animalUI.text = ": " + Animals.childCount;
    }

    
    void Update()
    {
        SpawnAnimals();
    }

    void SpawnAnimals()
    {
        
        if(Animals.childCount * 2 < transform.childCount && canSpawn)
        {
            StartCoroutine(spawnCooldown());
            var _SpawnPosition = transform.GetChild(transform.childCount-1).position;
            var _newAnimal = Instantiate(Bear, _SpawnPosition, Quaternion.identity);
            _newAnimal.parent = Animals;
            animalUI.text = ": " + Animals.childCount;
        }

    }

    IEnumerator spawnCooldown()
    {
        canSpawn = false;
        yield return new WaitForSeconds(2);
        canSpawn = true;
    }

    
}
