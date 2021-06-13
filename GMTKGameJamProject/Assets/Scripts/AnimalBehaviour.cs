using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBehaviour : MonoBehaviour
{
    public static Vector3 DesiredPosition;
    private int Health;
    private float AttackCooldown;
    public bool isDead;
    public Transform BearVisual;
    public Sprite[] BearSprite;
    public ParticleSystem Particels;
    [SerializeField] float AttackSpeed;
    [SerializeField] float MovementSpeed;

    void Start()
    {
        Health = 100;
        AttackCooldown = 0;
    }

    
    void Update()
    {
        
        if(!isDead)
        {
            
            Move();
            isDead = false;
            AttackCooldown += Time.deltaTime;
        }
        else
        {
            Death();
        }
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
        if (_DistanceToTarget>1)
        {
            transform.position = Vector3.MoveTowards(transform.position, DesiredPosition, Time.deltaTime *MovementSpeed);
            BearHop(transform.position.x > DesiredPosition.x, _DistanceToTarget);
            
        }
    }
    void BearHop(bool _isFacingRight, float _DistanceToTarget)
    {
        var _bearSprite = BearVisual.gameObject.GetComponent<SpriteRenderer>();
        if(_isFacingRight)
        {
            _bearSprite.sprite = BearSprite[1];
        }
        else
        {
            _bearSprite.sprite = BearSprite[0];
        }
        BearVisual.localPosition = new Vector3(0, Mathf.Sin(_DistanceToTarget*15)*0.1f, 0);
    }

    
    void Attack(Collider2D _Enemy)
    {
        if(AttackCooldown>1/AttackSpeed)
        {
            _Enemy.gameObject.GetComponent<enemyBehavior>().TakeDamage(50);

           
        }


    }

    public void TakeDamage(int _Damage)
    {
        Health -= _Damage;
        Particels.Play();
        if(Health<0)
        {
            isDead = true;
            BearVisual.localRotation = Quaternion.Euler(new Vector3(0,0,90));
        }
    }
    void Death()
    {
        StartCoroutine(DespawnCooldown());
    }

    IEnumerator DespawnCooldown()
    {

        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
