using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForTunnel : MonoBehaviour
{
    private GameObject player;
    private LevelManager lm;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lm = FindObjectOfType<LevelManager>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            lm.LoadCattopia();

        }
    }
}
