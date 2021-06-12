using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBehaviour : MonoBehaviour
{
    public static Vector3 DesiredPosition;
    private int Health;
    private float AttackCooldown;
    [SerializeField] float AttackSpeed;
    [SerializeField] float MovementSpeed;

    void Start()
    {
        Health = 100;
        DesiredPosition = new Vector3(0,0,0);
        AttackCooldown = 0;
    }

    
    void Update()
    {
        Move();
        AttackCooldown += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Enemy")
        {
            Attack(other);
            
        }

    }


    void Move()
    {
        var _DistanceToTarget = Vector3.Distance(transform.position, DesiredPosition);
        if (_DistanceToTarget>2)
        {
            transform.position = Vector3.Lerp(transform.position, DesiredPosition, Time.deltaTime *MovementSpeed);

        }
    }



    void Attack(Collider2D _Enemy)
    {
        if(AttackCooldown>1/AttackSpeed)
        {
            _Enemy.gameObject.GetComponent<enemyBehavior>().TakeDamage(10);

           
        }


    }

    public void TakeDamage(int _Damage)
    {
        Health -= _Damage;
        if(Health<0)
        {
            Death();
        }
    }
    void Death()
    {
        Debug.Log("Dead");
    }
}
