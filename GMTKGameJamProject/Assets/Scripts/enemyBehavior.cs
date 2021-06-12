using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    public float strikeCooldown;
    public bool canAttack;
    public bool foundTreeToCut;
    public float speed = 1f;
    public int health = 100;
    private Vector3 desiredPosition;
    private Vector3 previousPosition;
    public Transform target;
    

    void Start()
    {
        speed = 1; // go high for testing purposes, but it should probably be set to 1 or 1,5f
        strikeCooldown = 1; //number of seconds to wait between attacks
        init();

    }

    void Update()
    {
        //if no tree to cut was found yet
        if (!foundTreeToCut)
        {
            //if the current target is not null and not the factory, it has to be a tree
            if (target != null && target != transform.parent)
            {
                foundTreeToCut = true;
            }
            else
            {
                //else let's look again
                init();
                if (target == transform.parent)
                    Move(transform.parent.position);
            }
        }
        //if we found a tree to cut
        else
        {
            previousPosition = transform.position;
            Move(desiredPosition);
            //if we're no longer walking to the tree and we can attack we hit the tree once
            if (Vector3.Distance(previousPosition,transform.position) == 0  && canAttack)
            {
                canAttack = false;
                target.GetComponent<treeBehavior>().hp--;
                StartCoroutine(waitForAttack());

                
            }
            if ((target.GetComponent<treeBehavior>().hp <= 3 && target.GetComponent<treeBehavior>().humansAttacking == 3)
                || (target.GetComponent<treeBehavior>().hp <= 2 && target.GetComponent<treeBehavior>().humansAttacking == 2)
                || (target.GetComponent<treeBehavior>().hp <= 1)
                )
            {
                init();
            }
            /*//else if we're carrying wood we go back to the factory
            else if(carryingWood)
            {
                target
            }*/
        }

        
    }



    Transform GetClosestTree(GameObject[] trees)
    {
        Transform bestTarget = transform.parent;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject potentialTarget in trees)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            //&& Random.Range(1,11) > 1 
            if ((dSqrToTarget < closestDistanceSqr && potentialTarget.GetComponent<treeBehavior>().humansAttacking < 3 && potentialTarget.GetComponent<treeBehavior>().hp > 1))
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }

        

        if (bestTarget != null && bestTarget != transform.parent)
        {
            desiredPosition = bestTarget.position;
            bestTarget.GetComponent<treeBehavior>().humansAttacking++;
            if (bestTarget.GetComponent<treeBehavior>().humansAttacking >= 4)
            {
                bestTarget = transform.parent;
            }
        }
            
        


        return bestTarget;
    }

    void Move(Vector3 DesiredPosition)
    {
        var _DistanceToTarget = Vector3.Distance(transform.position, DesiredPosition);
        if (_DistanceToTarget > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, DesiredPosition, Time.deltaTime * speed);
        }


    }

    public void init()
    {
        foundTreeToCut = false;
        canAttack = true;
        target = GetClosestTree(GameObject.FindGameObjectsWithTag("tree"));
    }

    IEnumerator waitForAttack()
    {
        yield return new WaitForSeconds(strikeCooldown);
        canAttack = true;
    }


    public void TakeDamage(int _Damage)
    {
        health -= _Damage;
        
        if(health<=0)
        {
            Death();
        }
    }
    void Death()
    {

    }
}
