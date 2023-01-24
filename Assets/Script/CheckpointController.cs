using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public bool checkPoint = false;
    public Vector3 respawnPosition;
    public GameObject player;
    //private GameObject Checkpoint;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        respawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.tag == "Checkpoint")
        {
            respawnPosition = collision.transform.position;
            //playerInRange += Time.deltaTime;
            Debug.Log("CheckPoint");
        }
    }
}
