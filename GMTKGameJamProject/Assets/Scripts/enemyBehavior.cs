using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    public bool isAttacking;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        target = GetClosestTree(GameObject.FindGameObjectsWithTag("tree"));
        Debug.Log(target);
    }

    // Update is called once per frame
    void Update()
    {
        
        //transform.position = Vector3.Lerp(transform.position, new Vector3 (0,0,0), Time.deltaTime * 0.1f);

        if (!isAttacking)
        {
            
            if (target != null)
            {
                isAttacking = false;
                Move(target.position);
            }
        }
    }



    Transform GetClosestTree(GameObject[] trees)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject potentialTarget in trees)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr && Random.Range(1,11) > 1 && potentialTarget.GetComponent<treeBehavior>().humansAttacking < 3)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }

        if (bestTarget != null)
            bestTarget.GetComponent<treeBehavior>().humansAttacking++;

        return bestTarget;
    }



    void Move(Vector3 DesiredPosition)
    {
        var _DistanceToTarget = Vector3.Distance(transform.position, DesiredPosition);
        if (_DistanceToTarget > 2)
        {
            Debug.Log(DesiredPosition);
            transform.position = Vector3.Lerp(transform.position, DesiredPosition, Time.deltaTime * 0.1f);
        }
        


    }

}
