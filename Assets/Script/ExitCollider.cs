using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitCollider : MonoBehaviour
{
    private GameObject player;
    private LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        levelManager = FindObjectOfType<LevelManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitShow()
    {
        gameObject.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            Time.timeScale = 0;
            if (SceneManager.GetActiveScene().name == "ParkScene")
            {
                PlayerPrefs.SetInt("parkS", 1);
            }
            else if (SceneManager.GetActiveScene().name == "MarketSceneNight")
            {
                PlayerPrefs.SetInt("MarketS", 1);
            }
            else if (SceneManager.GetActiveScene().name == "MarketSceneNight2")
            {
                PlayerPrefs.SetInt("MarketS", 2);
            }
            else if (SceneManager.GetActiveScene().name == "ClinicNight")
            {
                PlayerPrefs.SetInt("ClinicS", 1);
            }
            else if (SceneManager.GetActiveScene().name == "Residential Scene Night")
            {
                PlayerPrefs.SetInt("ResidentialS", 1);
            }

            levelManager.LoadToWorld();
            
        }
    }

}
