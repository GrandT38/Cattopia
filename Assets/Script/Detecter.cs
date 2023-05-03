using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detecter : MonoBehaviour
{

    public float playerInRange;
    private GameObject player;
    private PlayerMovement playerMovement;
    public Controller controller;
    private LevelManager levelmanager;

    //int playerExposed = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        levelmanager = FindObjectOfType<LevelManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        //playerExposed = levelmanager.playerExposed;
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
            if (levelmanager.playerExposed)
            {
                playerInRange += 2;
            }
            if (playerMovement.PlayerIsRun )
            {
                controller.ChangeTo3();
                levelmanager.PlayerExposed();
                TurnOffDetecter();
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player && !levelmanager.PlayerHide)
        {
            playerInRange += Time.deltaTime;
            if (playerInRange > 0 && playerInRange <= 1.5f)
            {
                levelmanager.EnemyVigilant();

            }

            else if (playerInRange >= 1.5f)
            {
                controller.ChangeTo3();
                levelmanager.PlayerExposed();
                TurnOffDetecter();
            }
            if(playerMovement.PlayerIsRun )
            {
                controller.ChangeTo3();
                levelmanager.PlayerExposed();
                TurnOffDetecter();
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = 0;
        }
        if (playerInRange == 0f)
        {
            levelmanager.PlayerStealth();
        }
    }

    public void TurnOffDetecter()
    {
        gameObject.SetActive(false);
    }

}
