using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    public float strikeCooldown;
    public bool canAttack;
    public bool canHitBear;
    public bool foundTreeToCut;
    public bool isAttacking;
    public bool isDead;
    public float speed = 1f;
    public int health = 100;
    private Vector3 desiredPosition;
    private Vector3 previousPosition;
    public Transform target;
    public Transform HumanVisual;
    public bool carryingWood;
    private Collider2D Bear;
    public ParticleSystem Particel;


    void Start()
    {
        isDead = false;
        isAttacking = false;
        carryingWood = false;
        canHitBear = true;
        speed = 1; // go high for testing purposes, but it should probably be set to 1 or 1,5f
        strikeCooldown = 2; //number of seconds to wait between attacks
        init();

    }

    void Update()
    {
        if(isDead)
        {
            Death();
            return;
        }

        if (!isAttacking)
        {
            //if no tree to cut was found yet and we're not carrying wood
            if (!foundTreeToCut && !carryingWood)
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
                if (carryingWood)
                {
                    previousPosition = transform.position;
                    target = transform.parent;
                    Move(target.position);
                    if (Vector3.Distance(previousPosition, transform.position) <=1)
                    {
                        carryingWood = false;
                        init();
                    }
                }
                else
                {
                    previousPosition = transform.position;
                    Move(desiredPosition);
                    //if we're no longer walking to the tree and we can attack we hit the tree once
                    if (Vector3.Distance(previousPosition, transform.position) == 0 && canAttack)
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
                        carryingWood = true;
                    }
                }
            }
        }
        else
        {
            //Attack code here
            if (canHitBear)
            {
                try
                {
                    StartCoroutine(AttackCooldown(Bear));
                }
                catch { return; }
            }
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
        HumanVisual.localPosition = new Vector3(0, Mathf.Sin(_DistanceToTarget * 15) * 0.1f, 0);

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
        Particel.Play();

        if (health<=0)
        {
            if(foundTreeToCut)
            {
                target.GetComponent<treeBehavior>().humansAttacking--;
            }
            HumanVisual.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
            Death();
        }
    }
    void Death()
    {
        StartCoroutine(DeathDespawn());
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "animal")
        {
            other.gameObject.GetComponent<AnimalBehaviour>().TakeDamage(10);
            Bear = other;
            StartAttacking();
        }
    }
    void OnTriggerExit2D()
    {
        isAttacking = false;
    }

    void StartAttacking()
    {
        isAttacking = true;
    }
    IEnumerator AttackCooldown(Collider2D other)
    {
        canHitBear = false;
        yield return new WaitForSeconds(1);
        if (other != null) 
        {
            other.gameObject.GetComponent<AnimalBehaviour>().TakeDamage(10);
        }
        
        canHitBear = true;
    }
    IEnumerator DeathDespawn()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

}
