using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForResidential : MonoBehaviour
{
    private GameObject player;
    private WorldHints worldHints;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        worldHints = FindObjectOfType<WorldHints>();

    }

    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {


            worldHints.warningPlayer();

        }
    }
    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {


            worldHints.stopWarning();

        }
    }*/
}
