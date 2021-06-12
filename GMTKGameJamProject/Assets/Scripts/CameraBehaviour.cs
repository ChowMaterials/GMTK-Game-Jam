using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform Player;

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {

        var _desiredPosition = Player.position + new Vector3(0,0,-10);
        transform.position = Vector3.Lerp(transform.position,_desiredPosition,Time.deltaTime);

    }
}
