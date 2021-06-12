using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] float MovementSpeed;
    public Transform TreeCollection;
    public Transform TreePreviewPrefab;
    public Transform TreePrefab;
    private Transform TreePreview;
    private bool isHoldingTree;
    private bool isFacingRight;
    private GameObject collidingWith;
    private int collisionCounter;


    void Start()
    {
        isHoldingTree = false;
        isFacingRight = true;
    }

    
    void Update()
    {
        Movement();
        PlaceTree();
        RallyAnimals();
        empower();
    }

    void Movement()
    {
        var _x = Input.GetAxis("Horizontal")*Time.deltaTime;
        var _y = Input.GetAxis("Vertical") * Time.deltaTime;
        var _positionOffset = new Vector3(_x, _y, 0) * MovementSpeed;

        transform.position += _positionOffset;
        DeterminFacingDirection(_x);
    }
    void DeterminFacingDirection(float _x)
    {
        if(_x > 0)
        {
            isFacingRight = true;
        }
        if(_x <0)
        {
            isFacingRight = false;
        }
    }



    void PlaceTree()
    {

        var _treePlacement = new Vector3(1, 0, 0) + transform.position;
        if(!isFacingRight)
        {
            _treePlacement = new Vector3(-1, 0, 0) + transform.position;
        }
        
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
                var _tree =Instantiate(TreePrefab, _newTreeOffset, Quaternion.identity);
                _tree.parent = TreeCollection;
                isHoldingTree = false;
                Destroy(TreePreview.gameObject);
            }
        }

    }



    void RallyAnimals()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AnimalBehaviour.DesiredPosition = transform.position;
        }

    }

    void empower()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Colliding with : " + collidingWith);
        }
    }

    void OnTriggerEnter2D(Collider2D myCollision)
    {
        Debug.Log("Colliding with : " + myCollision.gameObject.tag);
        if (myCollision.gameObject.tag == "tree" || myCollision.gameObject.tag == "animal")
        {
            
            collidingWith = myCollision.gameObject;
            collisionCounter++;
        }
    }

    void OnTriggerExit2D(Collider2D myCollision)
    {
        Debug.Log("dshaisdh");
        if (myCollision.gameObject.tag == "tree" || myCollision.gameObject.tag == "animal")
        {
            collidingWith = null;
            collisionCounter--;
            
        }
    }


}
