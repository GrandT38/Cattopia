using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detecter : MonoBehaviour
{

    public float playerInRange;
    private GameObject player;
    public Controller controller;
    // Start is called before the first frame update
    void Start()
    {
        //controller = FindObjectOfType<Controller>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            //playerInRange += Time.deltaTime;
            Debug.Log("1");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange += Time.deltaTime;
            if (playerInRange >= 2f)
            {
                controller.ChangeTo3();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = 0;
        }
    }
}
