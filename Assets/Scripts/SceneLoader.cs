using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad;
    AsyncOperation loadingOperation;
    [SerializeField]
    private Slider progressBar;
    private bool isLoading = false;
    [SerializeField]
    private GameObject loadingScreen;

    private void Start()
    {
        loadingScreen.SetActive(false);
    }

    void Update()
    {
        UpdateProgressBar();
    }

    private void UpdateProgressBar() 
    {
        if (isLoading)
        {
            progressBar.value = Mathf.Clamp01(loadingOperation.progress / 0.9f);
        }
    }

    public void LoadScene() 
    {
        loadingScreen.SetActive(true);
        loadingOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        isLoading = true;
    }
}
