using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeBehavior : MonoBehaviour
{

    public int humansAttacking;
    public bool alive;
    public int hp;

    // Start is called before the first frame update
    void Start()
    {
        humansAttacking = 0;
        alive = true;
        hp = 10;
    }

}
