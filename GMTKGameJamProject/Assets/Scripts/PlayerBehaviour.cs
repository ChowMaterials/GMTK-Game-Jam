using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] float MovementSpeed;
    public Transform TreePreviewPrefab;
    public Transform TreePrefab;
    private Transform TreePreview;
    private bool isHoldingTree;


    void Start()
    {
        isHoldingTree = false;
    }

    
    void Update()
    {
        Movement();
        PlaceTree();
    }

    void Movement()
    {
        var _x = Input.GetAxis("Horizontal")*Time.deltaTime;
        var _y = Input.GetAxis("Vertical") * Time.deltaTime;
        var _positionOffset = new Vector3(_x, _y, 0) * MovementSpeed;

        transform.position += _positionOffset;

    }

    void PlaceTree()
    {
        var _treePlacement = new Vector3(1, 0, 0) + transform.position;
        
        if (Input.GetKey(KeyCode.E))
        {
            if(!isHoldingTree)
            {
                TreePreview = Instantiate(TreePreviewPrefab, _treePlacement, Quaternion.identity);
                isHoldingTree = true;
            }
        }
        if (Input.GetKey(KeyCode.E))
        {
            if(isHoldingTree)
            {
                TreePreview.position = _treePlacement;
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            if(isHoldingTree)
            {
                var _newTreeOffset = _treePlacement + new Vector3(0,0,1);
                Instantiate(TreePrefab, _newTreeOffset, Quaternion.identity);
                isHoldingTree = false;
                Destroy(TreePreview.gameObject);

            }
        }

    }


}
