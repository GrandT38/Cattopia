using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLead : MonoBehaviour
{
    public GameObject LookAtP;
    private bool world = false;
    public int gameProgress;

    public GameObject Park;
    public GameObject Residential;
    public GameObject Clinic;
    public GameObject Market;
    public GameObject Cattopia;

    void Start()
    {
        if(SceneManager.GetActiveScene().name == "SampleScene")
        {
            world = true;
        }
        else
        {
            world = false;
        }
    }

    void Update()
    {
        if (!world)
        {
            transform.LookAt(LookAtP.transform.position);
        }
        else
        {
            switch (gameProgress)
            {
                case 0:
                    transform.LookAt(Park.transform.position);
                    break;

                case 1:
                    transform.LookAt(Residential.transform.position);
                    break;

                case 2:
                    transform.LookAt(Clinic.transform.position);
                    break;

                case 3:
                    transform.LookAt(Market.transform.position);
                    break;

                case 4:
                    transform.LookAt(Cattopia.transform.position);
                    break;
            }
        } 

    }

    public void ShowLeader()
    {
        gameObject.SetActive(true);
    }
}
