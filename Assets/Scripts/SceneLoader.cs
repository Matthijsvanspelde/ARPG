using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
    [SerializeField]
    private Vector3 spawnPoint;

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
        if (!isLoading)
        {            
            loadingScreen.SetActive(true);
            loadingOperation = SceneManager.LoadSceneAsync(sceneToLoad);
            isLoading = true;
            GameObject player = GameObject.Find("Player");
            player.transform.position = spawnPoint;
            player.GetComponent<NavMeshAgent>().destination = spawnPoint;
        }       
    }
}
