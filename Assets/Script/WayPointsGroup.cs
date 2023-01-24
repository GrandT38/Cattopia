using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointsGroup : MonoBehaviour
{
    public Transform ToNextPoint(Transform currentTargetPosition)
    {
        if (currentTargetPosition == null)
        {
            return transform.GetChild(0);    //send first child of waypoint position
        }

        if (currentTargetPosition.GetSiblingIndex() < transform.childCount - 1)
        {
            return transform.GetChild(currentTargetPosition.GetSiblingIndex() + 1);   //return the next child position
        }
        else
        {
            return transform.GetChild(0);       //back to first one
        }
    }

    //easy to see
    private void OnDrawGizmos()     //just in scene , cant see in game.
    {
        foreach (Transform target in transform)
        {
            //make  blue sphere just in scene , cant see in game.
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(target.position, 0.5f);
        }

        Gizmos.color = Color.blue;
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }

        Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
    }

}
