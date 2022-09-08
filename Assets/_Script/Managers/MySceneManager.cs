using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void LoadSceneByString(string name)
    {
        Debug.Log("The scene: " + name + " was loaded.");
        DOTween.KillAll();
        SceneManager.LoadSceneAsync(name);
    }

    IEnumerator LoadingNextScene()
    {
        DOTween.KillAll();
        AsyncOperation AO = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        AO.allowSceneActivation = false;
        while (AO.progress < 0.9f)
        {
            yield return null;
        }
        AO.allowSceneActivation = true;
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadingNextScene());
    }
}
