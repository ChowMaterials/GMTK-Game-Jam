using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeBehavior : MonoBehaviour
{

    public int humansAttacking;
    public int hp;
    public bool treeDying;
    public bool treeGrowing;
    public int GrowSageeIndedx;
    public Sprite[] GrowStage;

    
    void Start()
    {
        treeDying = false;
        treeGrowing = false;
        humansAttacking = 0;
        hp = 10;
        
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
        GrowCycle();
    }

    void GrowCycle()
    {
        if(!treeGrowing && GrowSageeIndedx != 4)
        {
            StartCoroutine(Growing());
        }
        

    }
    IEnumerator Growing()
    {
        treeGrowing = true;
        yield return new WaitForSeconds(3);
        GetComponent<SpriteRenderer>().sprite = GrowStage[GrowSageeIndedx];
        GrowSageeIndedx++;
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
