using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeBehavior : MonoBehaviour
{

    public int humansAttacking;
    public int hp;
    public bool treeDying;

    // Start is called before the first frame update
    void Start()
    {
        treeDying = false;
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
    }


    IEnumerator Falling()
    {
        treeDying = true;
        yield return new WaitForSeconds(0.3f);
        hp--;
        treeDying = false;
    }
}
