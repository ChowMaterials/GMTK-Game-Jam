using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] float MovementSpeed;
    public Transform TreePreviewPrefab;
    private Transform TreePreview;
    private bool isHoldingTree;
<<<<<<< Updated upstream
=======
    private bool isFacingRight;

    private GameObject collidingWith;
    private bool empowering; // if the spirit is empowering, it can no longer move
    private SpriteRenderer charSprite;
    private Color defaultColor = new Color(16,159,173,255);
    private Color empoweringColor = new Color(255, 250, 26, 146);

    

>>>>>>> Stashed changes

    void Start()
    {
        charSprite = GetComponent<SpriteRenderer>();
        empowering = false;
        isHoldingTree = false;
    }

    
    void Update()
    {
        Movement();
        PlaceTree();
    }

    void Movement()
    {
        // can only move if not empowering
        if (!empowering)
        {
            var _x = Input.GetAxis("Horizontal") * Time.deltaTime;
            var _y = Input.GetAxis("Vertical") * Time.deltaTime;
            var _positionOffset = new Vector3(_x, _y, 0) * MovementSpeed;

<<<<<<< Updated upstream
        transform.position += _positionOffset;
=======
            transform.position += _positionOffset;
            DeterminFacingDirection(_x);
        }        
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

>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
            

=======
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
        if (Input.GetKeyDown(KeyCode.Q) && !empowering)
        {
            Debug.Log("Colliding with : " + collidingWith.gameObject.tag);
            if (collidingWith != null)
            {
                empowering = true;
                charSprite.color = empoweringColor;
                StartCoroutine(empoweringTimer());
            }
        }
    }

    void OnTriggerEnter2D(Collider2D myCollision)
    {
        
        if (myCollision.gameObject.tag == "tree" || myCollision.gameObject.tag == "animal")
        {
            collidingWith = myCollision.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D myCollision)
    {
        if (myCollision.gameObject.tag == "tree" || myCollision.gameObject.tag == "animal")
        {
            collidingWith = null;
>>>>>>> Stashed changes
        }

    }

    IEnumerator empoweringTimer()
    {
        yield return new WaitForSeconds(3); //empowers for 3 seconds
        empowering = false;
        charSprite.color = defaultColor;
    }

}
