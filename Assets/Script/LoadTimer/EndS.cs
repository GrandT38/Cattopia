using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndS : MonoBehaviour
{
    private float timer2;
    public LoadLevel LoadLevel;

    private int stopthetimer = 0;

    public GameObject GameOverPanel;
    private DataLoader dataL;

    void Start()
    {
        dataL = FindObjectOfType<DataLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        timer2 += Time.deltaTime;

        if (timer2 >= 16.5f)
        {
            GameOverPanel.SetActive(true);
            
        }
        if (timer2 >= 24)
        {
            PlayerPrefs.DeleteAll();
            timer2 = 0;
            dataL.PlayerX = 0;
            dataL.PlayerY = 0;
            dataL.PlayerZ = 0;
            dataL.Saved = false;
            dataL.TimerOfWorld = 12;
            LoadLevel.loadScene("MainMenu");


        }
    }
}
