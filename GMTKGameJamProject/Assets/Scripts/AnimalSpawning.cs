using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawning : MonoBehaviour
{
    public Transform Animals;
    public Transform Bear;

    void Start()
    {
        
    }

    
    void Update()
    {
        SpawnAnimals();
    }

    void SpawnAnimals()
    {
        if(Animals.childCount<transform.childCount)
        {
            var _SpawnPosition = transform.GetChild(transform.childCount-1).position;
            var _newAnimal = Instantiate(Bear, _SpawnPosition, Quaternion.identity);
            _newAnimal.parent = Animals;

        }

    }

    
}
