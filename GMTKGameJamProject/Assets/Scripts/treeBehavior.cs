using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeBehavior : MonoBehaviour
{

    public int humansAttacking;
    public int hp;
    public bool treeDying;
    public bool treeGrowing;
    public int GrowSageIndedx;
    public Sprite[] GrowStage;
    public Transform TreeCollection;
    public bool empowered;



    void Start()
    {
        empowered = false;
        GetComponent<SpriteRenderer>().sprite = GrowStage[GrowSageIndedx];
        treeDying = false;
        treeGrowing = false;
        humansAttacking = 0;
        hp = 10;
        TreeCollection = GameObject.FindWithTag("TreeCollection").GetComponent<Transform>();
    }

    private void Update()
    {
        
        if (hp <= 3)
        {
            if (!treeDying)
                StartCoroutine(Falling());
        }

        if (hp <= 0)
        {
            Destroy(gameObject);
        }

        if (empowered)
        {
            hp = 10;
            humansAttacking = 0;
        }
        GrowCycle();
    }

    void GrowCycle()
    {
        if(!treeGrowing)
        {
            if (GrowSageIndedx<5)
            {
                StartCoroutine(Growing());
            } 
            else
            {
                transform.parent = TreeCollection;
            }
        }
    }
    IEnumerator Growing()
    {
        treeGrowing = true;
        yield return new WaitForSeconds(3);
        GetComponent<SpriteRenderer>().sprite = GrowStage[GrowSageIndedx];
        GrowSageIndedx++;
        treeGrowing = false;
    }


    IEnumerator Falling()
    {
        treeDying = true;
        yield return new WaitForSeconds(0.3f);
        hp--;
        treeDying = false;
    }



}
