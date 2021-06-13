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
    public Sprite[] empoweredStage;
    public Transform TreeCollection;

    private CircleCollider2D empowerementZone;
    public bool empowered;



    void Start()
    {
        empowered = false;
        GetComponent<SpriteRenderer>().sprite = GrowStage[GrowSageIndedx];
        empowerementZone = GetComponent<CircleCollider2D>();
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
            GameObject.Find("TreeDies").GetComponent<AudioSource>().Play();
            Destroy(gameObject,1);
            
        }
        if (empowered)
        {
            hp += 3;
            if (hp > 10)
                hp = 10;
            humansAttacking = 0;
            if (GrowSageIndedx == 4)
                GetComponent<SpriteRenderer>().sprite = empoweredStage[GrowSageIndedx-1];
            
            else
                GetComponent<SpriteRenderer>().sprite = empoweredStage[GrowSageIndedx];
        }
        else
        {
            if (GrowSageIndedx == 4)
                GetComponent<SpriteRenderer>().sprite = GrowStage[GrowSageIndedx - 1];
            else
                GetComponent<SpriteRenderer>().sprite = GrowStage[GrowSageIndedx];
        }
        GrowCycle();

        //The Circle Collider grows with the tree
        empowerementZone.radius = 0.08f + 0.05f * (GrowSageIndedx + 1);
        GetComponent<BoxCollider2D>().size = new Vector2(0.1f + 0.2f * (GrowSageIndedx),0.1f + 0.2f * (GrowSageIndedx));

    }

    void GrowCycle()
    {
        if(!treeGrowing)
        {
            if (GrowSageIndedx<4)
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
