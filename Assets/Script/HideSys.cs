using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSys : MonoBehaviour
{
    private LevelManager levelmanager;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        levelmanager = FindObjectOfType<LevelManager>();
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
           levelmanager.PlayerHide = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            levelmanager.PlayerHide = false;
        }
    }
}
