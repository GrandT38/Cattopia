using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Controller : MonoBehaviour
{
    public int moveMode = 0;


    public float  speed = 1f;
    private float angle = 0;
    private float a = 1f;

    private  float objectLocationZ;
    private  float objectLocationX;

    private  float positionZ;
    private  float positionX;

    private float stepCount = 0;
    public float  patrolDistance = 3;

    [SerializeField]
    private WayPointsGroup waypoints;


    private float overlappingRange = 0.1f;

    private Transform currentTargetPosition;

    Transform playerPos;
    NavMeshAgent navMeshAgnet;
    /*
    public float playerInRange;
    */
    private GameObject player;
    
    private Animator anim;

    public float attackCharge;
    private LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        //Used to record the initial position of this object
        positionX = this.transform.localPosition.x;                         //"this"  means the object who has this script
        positionZ = this.transform.localPosition.z;
        //Used to calculate the patrol distance of this object
        objectLocationZ = this.transform.localPosition.z;
        objectLocationX = this.transform.localPosition.x;

        if(moveMode == 2)
        {
            //Set the object position to waypoint in first
            currentTargetPosition = waypoints.ToNextPoint(currentTargetPosition);
            this.transform.localPosition = currentTargetPosition.position;
        }

        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgnet = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

    }


    void Update()
    {
        if (moveMode == 0)
        {
            anim.SetBool("isWalk", true);
            moveSquare();
            transform.position = new Vector3(positionX, this.transform.localPosition.y, positionZ);
        }
        else if (moveMode == 1)
        {
            angle = 90;

            moveCircle();
            transform.position = new Vector3(positionX * patrolDistance+ objectLocationX, this.transform.localPosition.y, positionZ * patrolDistance+ objectLocationZ);
            transform.LookAt(new Vector3(positionX +objectLocationX, this.transform.localPosition.y, positionZ + objectLocationZ) );
            //make the object look at the center of circle
            transform.Rotate(0, angle, 0);
            // and Then turn around 90(y-axis)  
            //sequence is important
            anim.SetBool("isWalk", true);
        }

        else if (moveMode == 2)
        {
            WayPointMove();
            transform.LookAt(currentTargetPosition);
            anim.SetBool("isWalk", true);
        }
        else if (moveMode ==3)
        {
            navMeshAgnet.SetDestination(playerPos.position);
            anim.SetBool("isAttack", false );
            anim.SetBool("isWalk", true);
        }
        else if (moveMode == 4)
        {
            navMeshAgnet.SetDestination(playerPos.position);
            anim.SetBool("isWalk", false);
            anim.SetBool("isAttack", true);
        }
    }


    void moveSquare()
    {
        if (stepCount == 0)     //move forward
        {
            angle = 0;
            positionZ += speed * Time.deltaTime;

            if (positionZ >= objectLocationZ + patrolDistance)
            {
                stepCount = 1;
            }

        }
        else if(stepCount == 1)     //turn direction to left
        {
            angle -= 90;
            transform.Rotate(0, angle, 0);
            stepCount = 2;
        }
        else if(stepCount == 2)
        {
            positionX -= speed * Time.deltaTime;
            if (positionX <= objectLocationX - patrolDistance)
            {
                stepCount = 3;
            }
        }
        else if(stepCount == 3)     
        {
            transform.Rotate(0, angle, 0);
            stepCount = 4;
        }
        else if (stepCount == 4)
        {
            positionZ -= speed * Time.deltaTime;
            if (positionZ <= objectLocationZ )
            {
                stepCount = 5;
            }
        }
        else if (stepCount == 5)        
        {
            transform.Rotate(0, angle, 0);
            stepCount = 6;
        }
        else if (stepCount == 6)
        {
            positionX += speed * Time.deltaTime;
            if (positionX >= objectLocationX )
            {
                stepCount = 7;
            }
        }
        else if (stepCount == 7)        
        {
            transform.Rotate(0, angle, 0);
            stepCount = 0;
        }
    }

    void moveCircle()
    {
        a += Time.deltaTime*speed;
        angle += Time.deltaTime;
        positionX = Mathf.Cos(a);
        positionZ = Mathf.Sin(a);
    }

    void WayPointMove()
    {
        this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, currentTargetPosition.position, speed * Time.deltaTime);
        if (Vector3.Distance(this.transform.localPosition, currentTargetPosition.position) < overlappingRange) //if overlapping
        {
            //Set the nect waypoint target
            currentTargetPosition = waypoints.ToNextPoint(currentTargetPosition);
            // -->WayPoint --->ToNextPoint(currentTargetPosition)
        }
    }

    public  void ChangeTo3()
    {
        moveMode = 3;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            moveMode = 4;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        attackCharge += Time.deltaTime;
        if (other.gameObject == player && attackCharge >= 2.25f )
        {
            AttackPlayer();
        }
        if(attackCharge >= 2.5)
        {
            attackCharge = 0;
        }

        
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            attackCharge = 0;
            moveMode = 3;
        }
    }

    void AttackPlayer()
    {
        levelManager.RespawnPlayer();
    }
}
