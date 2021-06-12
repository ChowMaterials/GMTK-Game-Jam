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
    private bool empowering; // if the spirit is empowering, it can no longer move
    private SpriteRenderer charSprite;
    private Color defaultColor = new Color(16, 159, 173, 255);
    private Color empoweringColor = new Color(255, 250, 26, 146);

    public float distanceToTarget;




    void Start()
    {
        charSprite = GetComponent<SpriteRenderer>();
        empowering = false;
        isHoldingTree = false;
        isFacingRight = true;
    }

    void FixedUpdate()
    {
        Movement();
    }
    void Update()
    {
        
        PlaceTree();
        RallyAnimals();
        empower();
        
    }

    void Movement()
    {
        // can only move if not empowering
        if (!empowering)
        {
            var _x = Input.GetAxis("Horizontal") * Time.deltaTime;
            var _y = Input.GetAxis("Vertical") * Time.deltaTime;
            var _positionOffset = new Vector3(_x, _y, 0) * MovementSpeed;

            transform.position += _positionOffset;
            DeterminFacingDirection(_x);
            LockToNearestTree();
        }
    }
    void DeterminFacingDirection(float _x)
    {
        if (_x > 0)
        {
            isFacingRight = true;
        }
        if (_x < 0)
        {
            isFacingRight = false;
        }
    }
    void LockToNearestTree()
    {
        Transform[] _Trees = TreeCollection.GetComponentsInChildren<Transform>();
        var _ShortestDistance = 7f;
        var _DesiredConnection = transform;
        
        for (int i = 0; i<_Trees.Length; i++)
        {
            var _currentDistance = Vector3.Distance(_Trees[i].position, transform.position);
            if (_currentDistance<_ShortestDistance)
            {
                
                _DesiredConnection = _Trees[i];
                _ShortestDistance = _currentDistance;

            }
        }
        if(_DesiredConnection != transform)
        {
            transform.gameObject.GetComponent<DistanceJoint2D>().connectedBody = _DesiredConnection.gameObject.GetComponent<Rigidbody2D>();
            Debug.DrawLine(transform.position, _DesiredConnection.position);
            DrawConnectionToTree(_DesiredConnection.position);
        }
        
    }
    void DrawConnectionToTree(Vector3 _endPoint)
    {
        var _line = gameObject.GetComponent<LineRenderer>();
        _line.SetPosition(0, transform.position);
        _line.SetPosition(1, _endPoint);
    }


    void PlaceTree()
    {

        var _treePlacement = new Vector3(1, 0, 0) + transform.position;
        if (!isFacingRight)
        {
            _treePlacement = new Vector3(-1, 0, 0) + transform.position;
        }

        if (Input.GetKey(KeyCode.E))
        {
            if (!isHoldingTree)
            {
                TreePreview = Instantiate(TreePreviewPrefab, _treePlacement, Quaternion.identity);
                isHoldingTree = true;
            }
        }
        if (Input.GetKey(KeyCode.E))
        {
            if (isHoldingTree)
            {
                TreePreview.position = _treePlacement;
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            if (isHoldingTree)
            {
                var _newTreeOffset = _treePlacement + new Vector3(0, 0, 1);
                var _tree = Instantiate(TreePrefab, _newTreeOffset, Quaternion.identity);
                
                isHoldingTree = false;
                Destroy(TreePreview.gameObject);
            }
        }

    }



    void RallyAnimals()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AnimalBehaviour.DesiredPosition = transform.position;
        }

    }

    void empower()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !empowering)
        {
            if (collidingWith != null)
            {
                distanceToTarget = Vector3.Distance(transform.position, collidingWith.transform.position);
                if (distanceToTarget < 2f)
                {
                    Debug.Log("You're protected !");
                    empowering = true;
                    //collidingWith.GetComponent<treeBehavior>().empowered = empowering;
                    charSprite.color = empoweringColor;
                    StartCoroutine(empoweringTimer());
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D myCollision)
    {
        if ((myCollision.gameObject.tag == "tree" || myCollision.gameObject.tag == "animal") && (myCollision.GetType() == typeof(CircleCollider2D)))
        {
            collidingWith = myCollision.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D myCollision)
    {
        if ((myCollision.gameObject.tag == "tree" || myCollision.gameObject.tag == "animal") && (myCollision.GetType() == typeof(CircleCollider2D)))
        {
            collidingWith = null;
        }
    }

    IEnumerator empoweringTimer()
    {
        yield return new WaitForSeconds(3); //empowers for 3 seconds
        empowering = false;
      //  collidingWith.GetComponent<treeBehavior>().empowered = empowering;
        charSprite.color = defaultColor;
    }

}