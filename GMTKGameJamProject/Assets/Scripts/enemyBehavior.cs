using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    public float strikeCooldown;
    public bool canAttack;
    public bool foundTarget;


    private Vector3 desiredPosition;
    private Transform target;
    

    // Start is called before the first frame update
    void Start()
    {
        strikeCooldown = 1;
        canAttack = true;
        init();

    }

    // Update is called once per frame
    void Update()
    {

        if (!foundTarget)
        {
            if (target != null)
            {
                foundTarget = true;
            }
            else
            {
                init();
            }
        }
        else
        {
            //Debug.Log(Vector3.Distance(transform.position, target.position));
            Move(desiredPosition);
            if (!(transform.hasChanged) && canAttack)
            {
                Debug.Log("Yaaargh !");
                canAttack = false;
                target.GetComponent<treeBehavior>().hp--;
                StartCoroutine("waitForattack");
                if (target.GetComponent<treeBehavior>().hp <= 0)
                {
                    Destroy(target);
                    target = null;
                }
                    
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
        {
            desiredPosition = bestTarget.position;
            bestTarget.GetComponent<treeBehavior>().humansAttacking++;
            if (bestTarget.GetComponent<treeBehavior>().humansAttacking >= 3)
            {
                bestTarget = null;
            }
        }
            
        


        return bestTarget;
    }



    void Move(Vector3 DesiredPosition)
    {
        var _DistanceToTarget = Vector3.Distance(transform.position, DesiredPosition);
        if (_DistanceToTarget > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, DesiredPosition, Time.deltaTime * 1f);
        }


    }

    public void init()
    {
        foundTarget = false;
        target = GetClosestTree(GameObject.FindGameObjectsWithTag("tree"));
    }

    public 

    IEnumerator waitForAttack()
    {
        yield return new WaitForSeconds(strikeCooldown);
        canAttack = true;
    }

    public void TakeDamage(int _Damage)
    {
        
    }
}
