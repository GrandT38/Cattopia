using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] private GameObject loadingCanvas;
    [SerializeField] private GameObject guiCanvas;

    public void loadScene(string sceneToLoad)
    {
        if (guiCanvas != null)
        {
            guiCanvas.SetActive(false);
        }
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
