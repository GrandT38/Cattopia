using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldHints : MonoBehaviour
{

    public DialogTxt dialogue;
    public Text dialogTxt;
    //public Text hitTxt;

    private string[] mySentences;
    private int sentencesNo;

    private LevelManager levelm;

    public PlayerLead playerLead;

    // Start is called before the first frame update
    void Start()
    {
        levelm = FindObjectOfType<LevelManager>();
        mySentences = dialogue.dialoguerSentences;
        sentencesNo = 0;
        dialogTxt.text = mySentences[sentencesNo];
        
        if (!PlayerPrefs.HasKey("PlayerX"))
        {
            dialogTxt.text = mySentences[sentencesNo];
            sentencesNo = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
        {
            if (sentencesNo == 1)
            {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                {
                    sentencesNo = 2;
                }
            }
            else if(sentencesNo == 2)
            {
                if (Input.GetKey(KeyCode.Escape))
                {
                    sentencesNo = 3;
                }
            }
            else if (sentencesNo == 3)
            {
                if (Input.GetKey(KeyCode.H))
                {
                    sentencesNo = 0;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            sentencesNo = 0;
            if (playerLead.gameObject.activeInHierarchy)
            {
                playerLead.gameObject.SetActive(false);
            }
            else if (!playerLead.gameObject.activeInHierarchy)
            {
                playerLead.gameObject.SetActive(true);
            }
        }

        if(playerLead.gameProgress == 4)
        {
            sentencesNo = 5;
        }


        dialogTxt.text = mySentences[sentencesNo];
    }


    public void warningPlayer()
    {
        sentencesNo = 4;
        StartCoroutine(stopWarning());
    }

    IEnumerator stopWarning()
    {
        yield return new WaitForSeconds(5f);
        sentencesNo = 0;
    }
}
