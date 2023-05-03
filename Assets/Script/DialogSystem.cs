using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogSystem : MonoBehaviour
{
    public GameObject DialogPanel;

    public DialogTxt dialogue;
    public GameObject canvas;
    public Text dialogTxt;

    private string[] mySentences;
    private int sentencesNo = 0;
    private bool talking = false;
    private LevelManager levelManager;
    public string  GoWhichScene;

    public AudioSource meow, male;
    public bool CatCat;
    public bool human;
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        mySentences = dialogue.dialoguerSentences;
    }

    void Update()
    {
        if (talking && Input.GetKeyDown(KeyCode.E) && sentencesNo < mySentences.Length)
        {
            dialogTxt.text = mySentences[sentencesNo];
            sentencesNo += 1;
            if (GoWhichScene == "0")
            {
                PlayerPrefs.SetInt("Mission1", 1);
                male.Play();
            }
            else if (GoWhichScene == "1")
            {
                PlayerPrefs.SetInt("Mission2", 1);
                meow.Play();
            }
            else if (CatCat)
            {
                meow.Play();
            }
            else if(human)
            {
                male.Play();
            }
        }
        else if (talking && Input.GetKeyDown(KeyCode.E) && sentencesNo == mySentences.Length)
        {
            if (GoWhichScene == "ParkScene")
            {
                levelManager.LoadParkScene();
            }
            else if (GoWhichScene == "ParkScene2")
            {
                levelManager.LoadPark2();
            }

            else if (GoWhichScene == "MarketScene")
            {
                levelManager.LoadMarketScene();
            }

            else if (GoWhichScene == "Back")
            {
                levelManager.LoadToWorld();
            }

            else if(GoWhichScene == "Clinic")
            {
                levelManager.LoadClinicScene();
            }
            else if (GoWhichScene == "Residential")
            {
                levelManager.LoadResidentialScene();
            }
            /*
            if(talking /*&& Input.GetKeyDown(KeyCode.E) && sentencesNo < mySentences.Length)
            {
                dialogTxt.text = mySentences[sentencesNo];
            }
            */
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            sentencesNo = 0;
            canvas.SetActive(true);
            talking = true;
            dialogTxt.text = "Press E to talk.";
        }

        if (other.tag == "Player" && GoWhichScene == "MarketScene")
        {
            dialogTxt.text = "Press E to enter.";
        }
        else if (other.tag == "Player" && GoWhichScene == "Back")
        {
            dialogTxt.text = "Press E to leave.";
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canvas.SetActive(false);
            talking = false;
        }
    }

}
