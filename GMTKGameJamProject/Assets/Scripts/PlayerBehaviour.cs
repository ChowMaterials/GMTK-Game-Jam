using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] float MovementSpeed;
    public Sprite[] NimphSprites;
    public Transform TreeCollection;
    public Transform TreePreviewPrefab;
    public Transform TreePrefab;
    private Transform TreePreview;
    private bool isHoldingTree;
    private bool isFacingRight;

    private int seedsInStock;
    private float plantCooldown = 3f; //Can plant a tree every 3 seconds
    private Text seedUI;
    private Image seedWheel;
    public bool waitingToPlant;

    public GameObject collidingWith;
    private bool empowering; // if the spirit is empowering, it can no longer move
    private SpriteRenderer charSprite;
    public float distanceToTarget;

    private DistanceJoint2D treeConnexion;
    public GameObject redFlash;
    private bool waitForHit;
    public int hp = 5;
    public static bool isPlayerDead;
    private bool isOutOfBounds;



    void Start()
    {
        isOutOfBounds = false;
        isPlayerDead = false;
        seedsInStock = 10; // start the game with 10 seeds to build your forest
        waitingToPlant = false;

        treeConnexion = GetComponent<DistanceJoint2D>();
        redFlash.SetActive(false);
        waitForHit = true;

        seedUI = GameObject.Find("Seeds").GetComponent<Text>();
        seedUI.text = ": " + seedsInStock;
        seedWheel = GameObject.Find("seedCooldownWheel").GetComponent<Image>();
        seedWheel.fillAmount = 1;

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
        if (hp <= 0)
        {
            isPlayerDead = true;
            redFlash.SetActive(false);
            return;
        }

        PlaceTree();
        RallyAnimals();
        empower();

        if (seedWheel.fillAmount < 1)
            seedWheel.fillAmount += 0.3333f * 1f * Time.deltaTime;

        if (seedWheel.fillAmount == 1)
            seedWheel.color = Color.green;
        else
            seedWheel.color = Color.red;


        if (treeConnexion.connectedBody == null && !waitForHit)
            playerHurting();

        
    }

    void Movement()
    {
        // can only move if not empowering
        if (!empowering && !isPlayerDead)
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
            gameObject.GetComponent<SpriteRenderer>().sprite = NimphSprites[0];
        }
        if (_x < 0)
        {
            isFacingRight = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = NimphSprites[1];
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
        gameObject.GetComponent<LineRenderer>().enabled = false;
        if (_DesiredConnection != transform)
        {
            waitForHit = false;
            hp = 5;
            treeConnexion.connectedBody = _DesiredConnection.gameObject.GetComponent<Rigidbody2D>();
            if (_DesiredConnection.gameObject !=null)
            {
                gameObject.GetComponent<LineRenderer>().enabled = true;
                DrawConnectionToTree(_DesiredConnection.position);
            }
            
            
        }
        if (_DesiredConnection == transform)
        {
            treeConnexion.connectedBody = null;
        }
        
    }
    void DrawConnectionToTree(Vector3 _endPoint)
    {
        var _line = gameObject.GetComponent<LineRenderer>();
        _line.SetPosition(0, transform.position);
        _line.SetPosition(1, _endPoint);
    }

    bool canPlant()
    { 
        return !waitingToPlant;
    }

    IEnumerator waitToPlant()
    {
        waitingToPlant = true;
        yield return new WaitForSeconds(plantCooldown);
        waitingToPlant = false;
    }

    void PlaceTree()
    {

        var _treePlacement = new Vector3(1, 0, 0) + transform.position;
        if (!isFacingRight)
        {
            _treePlacement = new Vector3(-1, 0, 0) + transform.position;
        }

        if (Input.GetKey(KeyCode.E) && canPlant() && seedsInStock > 0)
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
                var _Color = new Color(1,1,1,0.5f);

                TreePreview.position = _treePlacement;
                if(isOutOfBounds)
                {
                    _Color = new Color(1,0.5f,0.5f,0.5f);
                }
                TreePreview.gameObject.GetComponent<SpriteRenderer>().color = _Color;
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            if (isHoldingTree && !isOutOfBounds)
            {
                var _newTreeOffset = _treePlacement + new Vector3(0, 0, 1);
                var _tree = Instantiate(TreePrefab, _newTreeOffset, Quaternion.identity);
                
                isHoldingTree = false;
                Destroy(TreePreview.gameObject);
                seedWheel.fillAmount = 0;
                StartCoroutine(waitToPlant());
                seedsInStock--;
                seedUI.text = ": " + seedsInStock;
            }
            else if(isHoldingTree && isOutOfBounds)
            {
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
                    empowering = true;
                    if(collidingWith.tag == "tree")
                    {
                        collidingWith.GetComponent<treeBehavior>().empowered = empowering;
                        StartCoroutine(empoweringTimer());
                    }
            }
        } 
    }
    

    void playerHurting()
    {
        waitForHit = true;
        StartCoroutine(waitASecond());
    }

    IEnumerator showAndHide(GameObject screenFlash, float duration)
    {
        hp--;
        redFlash.SetActive(true);
        yield return new WaitForSeconds(duration);
        redFlash.SetActive(false);
        waitForHit = false;
    }

    IEnumerator waitASecond()
    {
        yield return new WaitForSeconds(1);
        if (treeConnexion.connectedBody == null)
            StartCoroutine(showAndHide(redFlash, 0.5f));
        else
        {
            waitForHit = false;
            hp = 5;
        }
    }

    private void OnTriggerStay2D(Collider2D myCollision)
    {
        if ((myCollision.gameObject.tag == "tree" /*|| myCollision.gameObject.tag == "animal"*/) && (myCollision.GetType() == typeof(CircleCollider2D)))
        {
            collidingWith = myCollision.gameObject;
        }
        if(myCollision.gameObject.tag == "Bounds")
        {
            isOutOfBounds = true;
            
        }
    }

    void OnTriggerExit2D(Collider2D myCollision)
    {
        if ((myCollision.gameObject.tag == "tree" /*|| myCollision.gameObject.tag == "animal"*/) && (myCollision.GetType() == typeof(CircleCollider2D)))
        {
            collidingWith = null;
        }
        if (myCollision.gameObject.tag == "Bounds")
        {
            isOutOfBounds = false;
        }
    }

    IEnumerator empoweringTimer()
    {
        yield return new WaitForSeconds(1); //empowers for 3 seconds
        empowering = false;
        collidingWith.GetComponent<treeBehavior>().empowered = empowering;
    }


}