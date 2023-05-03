using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenS : MonoBehaviour
{
    private float timer1;
    public LoadLevel LoadLevel;

    private int stopthetimer = 0;

    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        timer1 += Time.deltaTime;
        if (timer1 >= 11)
        {
            //loadingCanvas.SetActive(true);
            //SceneManager.LoadScene("SampleScene");
            LoadLevel.loadScene("SampleScene");

            timer1 = 0;
        }
    }
}
