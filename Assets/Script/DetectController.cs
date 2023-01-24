using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectController : MonoBehaviour
{
    public int moveMode = 0;
    public float speed = 1f;
    public float patrolDistance = 3;

    [SerializeField]
    private WayPointsGroup waypoints1;


    private float overlappingRange = 0.1f;

    private Transform currentTargetPosition;

    void Start()
    {
        if (moveMode == 2)
        {
            //Set the object position to waypoint in first
            currentTargetPosition = waypoints1.ToNextPoint(currentTargetPosition);
            this.transform.localPosition = currentTargetPosition.position;
        }

    }


    void Update()
    {
        if (moveMode == 2)
        {
            WayPointMove();
            transform.LookAt(currentTargetPosition);
        }
    }

    void WayPointMove()
    {
        this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, currentTargetPosition.position, speed * Time.deltaTime);
        if (Vector3.Distance(this.transform.localPosition, currentTargetPosition.position) < overlappingRange) //if overlapping
        {
            //Set the nect waypoint target
            currentTargetPosition = waypoints1.ToNextPoint(currentTargetPosition);
            // -->WayPoint --->ToNextPoint(currentTargetPosition)
        }
    }
}
