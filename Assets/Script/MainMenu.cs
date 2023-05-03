using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject credit;
    public GameObject noContinue;
    public GameObject haveContinue;
    public GameObject noSavePanel;
    public GameObject haveSavePanel;
    private LevelManager levelManager;

    [SerializeField] private GameObject loadingCanvas;
    [SerializeField] private GameObject menuCanvas;

    void Start()
    {
        //Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        levelManager = FindObjectOfType<LevelManager>();

        if (!PlayerPrefs.HasKey("PlayerX"))
        {
            haveContinue.SetActive(false);
            noContinue.SetActive(true);
        }
        else
        {
            noContinue.SetActive(false);
            haveContinue.SetActive(true);
        }
    }

    public void NewGameButton(string sceneToLoad)
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
        {
            levelManager.ResetPlayerData();
            loadScene(sceneToLoad);
        }
        else
        {
            haveSavePanel.SetActive(true);
        }
    }

    public void ContinueButton(string sceneToLoad)
    {
        if (levelManager.IsSaved() == true)
        {
            loadScene(sceneToLoad);
        }
        else
        {
            noSavePanel.SetActive(true);
        }
    }

    public void Credit()
    {
        credit.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ConfirmNewGame(string sceneToLoad)
    {
        levelManager.ResetPlayerData();
        loadScene(sceneToLoad);
    }

    public void loadScene(string sceneToLoad)
    {
        menuCanvas.SetActive(false);
        loadingCanvas.SetActive(true);
        StartCoroutine(LoadAsyncScene(sceneToLoad));
    }

    IEnumerator LoadAsyncScene(string sceneToLoad)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            yield return null;
        }
    }
}