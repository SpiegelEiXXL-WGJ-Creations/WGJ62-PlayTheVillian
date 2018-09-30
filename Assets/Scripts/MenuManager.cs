using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private string levelLoading;
    public GameObject rootObject;
    public GameObject LoadingScreen;

    // Use this for initialization
    void Start()
    {

    }

    public void goToNextScene(string newScene)
    {
        goToNextScene(newScene, true);
    }

    public void goToNextScene(string newScene, bool loadingScreen = true)
    {
        if (loadingScreen)
        {
            rootObject.SetActive(false);
            LoadingScreen.SetActive(true);
            foreach (RectTransform o in LoadingScreen.GetComponentsInChildren<RectTransform>())
            {
                o.gameObject.SetActive(true);
            }
            Camera.main.gameObject.SetActive(true);
        }
        levelLoading = newScene;
        StartCoroutine(LoadNewScene());
    }
    IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(1);
        AsyncOperation o = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(levelLoading, UnityEngine.SceneManagement.LoadSceneMode.Single);
        o.completed += Handle_Completed;
        while (!o.isDone)
        {
            yield return null;
        }
    }

    void Handle_Completed(AsyncOperation obj)
    {
        obj.allowSceneActivation = true;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
