using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] float MovementSpeed;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        var _x = Input.GetAxis("Horizontal")*Time.deltaTime;
        var _y = Input.GetAxis("Vertical") * Time.deltaTime;
        var _positionOffset = new Vector3(_x, _y, 0) * MovementSpeed;

        transform.position += _positionOffset;

    }
}
