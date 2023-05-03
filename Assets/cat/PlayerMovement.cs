using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private Vector3 moveDirection;
    private Vector3 velocity;
    private Animator animator;

    [SerializeField] public bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask floorMask;
    [SerializeField] private float gravity;

    [SerializeField] private float jumpHeight;

    private CharacterController characterController;

    private GameObject mainCamera;
    public float turnBackSpeed;

    public Slider EnduranceSlider;
    private  float currentEndurance = 5f;
    private float coldDownTime;
    public GameObject Fillbar;      //EnduranceSlider >>Fill Area >> Fill

    public bool TouchByNPC = false;
    public bool StartTouch = false;

    private LevelManager levelManager;
    private GameObject player;
    public  Vector3 positionSave;

    public bool PlayerIsRun = false;
    private bool PlayerCanMoveOrNot = false;
    
    private DataLoader dataLoader;
    public Vector3 currentPosition;
    void Awake()
    {

    }
    
    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        dataLoader = FindObjectOfType<DataLoader>();
        levelManager = FindObjectOfType<LevelManager>();
        EnduranceSlider = FindObjectOfType<Slider>();
        Fillbar = GameObject.Find("Fill");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        Vector3 currentPosition = this.gameObject.transform.position;
        /*
        if (PlayerPrefs.GetInt("PlayerX") == -66 && PlayerPrefs.GetInt("PlayerZ") == 28)
        {
            //this.transform.position = new Vector3(PlayerPrefs.GetInt("PlayerX"), 0.18f, PlayerPrefs.GetInt("PlayerZ"));
            //Debug.Log("PlayerX");
            this.gameObject.transform.position = positionSave;
            Debug.Log(PlayerPrefs.GetInt("PlayerX"));
        }
        else if (PlayerPrefs.GetInt("PlayerX") == 32 && PlayerPrefs.GetInt("PlayerZ") == 78)
        {
            this.gameObject.transform.position = positionSave;
            Debug.Log(PlayerPrefs.GetInt("PlayerX"));
        }
        */
        //StartCoroutine(MovePlayerToNewPosition(positionSave));
        player = GameObject.Find("Player");
        if (dataLoader.Saved)
        {
            //this.transform.position = new Vector3(PlayerPrefs.GetInt("PlayerX"), 0.18f, PlayerPrefs.GetInt("PlayerZ"));
            //Debug.Log("PlayerX");
            //positionSave = new Vector3(-66, 0.18f, 28);
            if (SceneManager.GetActiveScene().name != "SampleScene")
            {
                transform.position = currentPosition;
            }
            else
            {
                this.gameObject.transform.position = new Vector3(dataLoader.PlayerX, dataLoader.PlayerY, dataLoader.PlayerZ);
            }
            //Debug.Log("865320.");

        }
        /*
        else if (dataLoader.PlayerX == 32 && dataLoader.PlayerZ == 78)
        {
            positionSave = new Vector3(32, 0, 78);
            this.gameObject.transform.position = positionSave;
            player.gameObject.transform.position = positionSave;

        }*/
        else if /*(levelManager.IsSaved()==true)*/(PlayerPrefs.HasKey("PlayerX") )
        {

            if (SceneManager.GetActiveScene().name != "SampleScene")
            {
                transform.position = currentPosition;
            }
            else if (SceneManager.GetActiveScene().name == "SampleScene")
            {
                transform.position = new Vector3(PlayerPrefs.GetInt("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetInt("PlayerZ"));
            }
        }
        else
        {
            //positionSave = new Vector3(-64, 0.19f, 20);
            this.gameObject.transform.position = new Vector3(-64, 0.19f, 20);
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.G))
        {
            this.transform.position = new Vector3(-66, 0.18f, 28);
            Debug.Log("G");
        }

        if (PlayerCanMoveOrNot)
        {
            Move();
        }
        else if (!PlayerCanMoveOrNot)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                PlayerCanMoveOrNot = true;
            }
        }


        EnduranceSlider.value = currentEndurance;
        
        if(currentEndurance <= 1 || coldDownTime ==5)       //EnduranceSlider >>Fill Area >> Fill
        {
            Fillbar.GetComponent<Image>().color = Color.red;
        }
        else if (currentEndurance >= 3 && coldDownTime == 0)
        {
            Fillbar.GetComponent<Image>().color = Color.green;
        }
        else if (currentEndurance >= 1 && coldDownTime ==0)
        {
            Fillbar.GetComponent<Image>().color = Color.yellow;
        }

        // to ensure
        /*
        if (player.gameObject.transform.position != positionSave && abc <=11)
        {
            player.gameObject.transform.position = positionSave;
            abc++;
        }
        else
        {
        }
        */

    }


    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, floorMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");
        if (!StartTouch)
        {
            moveDirection = new Vector3(moveX, 0, moveZ);
            moveDirection = transform.TransformDirection(moveDirection);

            /*
            Debug.Log("X:"+moveX);
            Debug.Log("Z:"+moveZ);
            */
            if (moveX != 0 || moveZ != 0)
            {

                float playerCamera = mainCamera.transform.rotation.eulerAngles.y;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, playerCamera, 0), turnBackSpeed * Time.deltaTime);
            }
            if (moveX != 0)
            {
                walkSpeed = 1.25f;
                runSpeed = 3;
            }
            else
            {
                walkSpeed = 2.5f;
                runSpeed = 6;
            }
        }
        /*
        if(moveX > 0)
        {
            float playerCamera = mainCamera.transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -playerCamera, 0), turnBackSpeed * Time.deltaTime);
        }
        */

        if (isGrounded)
        {
            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift) && !StartTouch)
            {
                Walk();

            }

            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift) && coldDownTime == 0)
            {
                if (currentEndurance > 0 && coldDownTime == 0)
                {
                    Run();
                    currentEndurance -= Time.deltaTime;
                    //EnduranceSlider.value = currentEndurance;
                }

                else if (currentEndurance <= 0)
                {
                    StartCoroutine(EnduranceRecovery());
                    //Walk();
                    coldDownTime = 5;
                }


            }

            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift) && coldDownTime == 5 && !StartTouch)
            {
                Walk();
            }
            else if (Input.GetKeyDown(KeyCode.E) && TouchByNPC)
            {

                Sit();
                StartTouch = true;

            }

            else if (moveDirection == Vector3.zero && !StartTouch)
            {
                Idle();
            }


            moveDirection *= moveSpeed;

        }



        characterController.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void Idle()
    {
        if(currentEndurance <= 5)
        {
            currentEndurance += Time.deltaTime;
        }
        PlayerIsRun = false;
        animator.SetFloat("Speed", 0);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        animator.SetFloat("Speed", 0.6666667f);
        PlayerIsRun = true;
    }

    private void Walk()
    {
        if(currentEndurance <= 5)
        {
            currentEndurance += Time.deltaTime * 0.5f;
        }
        PlayerIsRun = false;
        moveSpeed = walkSpeed;
        animator.SetFloat("Speed", 0.3333333f);
    }

    private void Sit()
    {
        animator.SetTrigger("Sit");
        PlayerIsRun = false;
    }


    IEnumerator EnduranceRecovery()
    {
        yield return new WaitForSeconds(8f);
        coldDownTime = 0;
    }
    /*
    IEnumerator MovePlayerToNewPosition(Vector3 position)
    {
        yield return new WaitForSeconds(0.3f);
        player.gameObject.transform.position = position;
        Debug.Log("42104");
        //StartCoroutine(MovePlayerToNewPosition(positionSave));
    }
    */


    void OnTriggerStay(Collider other)
    {
        if (other.tag == "CatLoverTouch")
        {
            TouchByNPC = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "CatLoverTouch")
        {
            TouchByNPC = false;
            //StartTouch = false;
        }
    }
}
