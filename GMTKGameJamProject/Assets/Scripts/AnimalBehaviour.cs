using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBehaviour : MonoBehaviour
{
    public static Vector3 DesiredPosition;
    void Start()
    {
        DesiredPosition = new Vector3(0,0,0);
    }

    
    void Update()
    {
        Move();
    }

    void Move()
    {
        var _DistanceToTarget = Vector3.Distance(transform.position, DesiredPosition);
        if (_DistanceToTarget>2)
        {
            transform.position = Vector3.Lerp(transform.position, DesiredPosition, Time.deltaTime *0.1f);

        }


    }



    void Attack()
    {



    }

}
