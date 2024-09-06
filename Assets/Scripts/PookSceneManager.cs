using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PookSceneManager : MonoBehaviour
{
    public static PookSceneManager instance { get; private set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneIEnum(sceneIndex));
    }

    IEnumerator LoadSceneIEnum(int sceneIndex)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        asyncLoad.allowSceneActivation = false;
        LoadScreenManager.instance.loadScreenObj.SetActive(true);

        while (asyncLoad.progress < 0.9f)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            Debug.Log(progress);

            LoadScreenManager.instance.loadBar.anchoredPosition = 
                Vector2.Lerp(LoadScreenManager.instance.loadBarInitPos, new Vector2(LoadScreenManager.instance.loadBarInitPos.x, -250), progress);

            Debug.Log("Loading scene...");
            yield return null;
        }

        Debug.Log("Scene loaded");
        LoadScreenManager.instance.loadBar.anchoredPosition = new Vector2(LoadScreenManager.instance.loadBarInitPos.x, -250);
        LeanTween.alphaCanvas(LoadScreenManager.instance.loadScreenCanvasGroup, 0f, 0.2f).setEaseInCubic();

        yield return new WaitForSecondsRealtime(0.5f);
        asyncLoad.allowSceneActivation = true;


    }
}
