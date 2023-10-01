using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LoadingScreen : MonoBehaviour
{
    public GameObject LoadingScreenCanvas;
    public int WaitTime;

    public void LoadScene(int sceneID)
    {
        LoadingScreenCanvas.SetActive(true);
        StartCoroutine(Wait(WaitTime, sceneID));
    }

    IEnumerator Wait(int Seconds, int sceneID)
    {

        yield return new WaitForSeconds(Seconds);
        StartCoroutine(LoadSceneAsync(sceneID));
    }


    IEnumerator LoadSceneAsync(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        LoadingScreenCanvas.SetActive(true);
        
        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
