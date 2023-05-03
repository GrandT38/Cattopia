using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialHit : MonoBehaviour
{
    public DialogTxt dialogue;
    public Text dialogTxt;
    public Text hitTxt;

    private string[] mySentences;
    private int sentencesNo;

    private LevelManager levelm;

    // Start is called before the first frame update
    void Start()
    {
        levelm = FindObjectOfType<LevelManager>();
        mySentences = dialogue.dialoguerSentences;
        sentencesNo = 0;
        dialogTxt.text = mySentences[sentencesNo];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && sentencesNo == 0)
        {
            sentencesNo = 1;
        }
        else if (levelm.Point == 1 && sentencesNo == 1)
        {
            sentencesNo = 2;
        }
        else if (levelm.warning1)
        {
            if (!levelm.playerExposed)
            {
                sentencesNo = 3;
            }
            else
            {
                sentencesNo = 4;
            }
        }
        if (levelm.Point == 3)
        {
            sentencesNo = 5;
            hitTxt.gameObject.SetActive(true);
        }

        dialogTxt.text = mySentences[sentencesNo];
    }
}