using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : Singleton<ScenesManager>
{
    public void LoadScene(string nameScene)
    {
        StartCoroutine(LoadSceneAsync(nameScene));
    }

    public void UnloadScene(string nameScene)
    {
        StartCoroutine(UnloadSceneAsync(nameScene));
    }

    private IEnumerator LoadSceneAsync(string nameScene)
    {
        //LoadingPanel.Instance.LoadingStart(ELoading.LoadData);

        yield return new WaitForSeconds(0.5f);
        var async = SceneManager.LoadSceneAsync(nameScene, LoadSceneMode.Single);

        while (!async.isDone)
        {
            Debug.Log("Loading scene: " + nameScene);
            yield return null;
        }

        var scn = SceneManager.GetSceneByName(nameScene);
        SceneManager.SetActiveScene(scn);

        //LoadingPanel.Instance.LoadingStop();
    }

    private IEnumerator UnloadSceneAsync(string nameScene)
    {
        //LoadingPanel.Instance.LoadingStart(ELoading.LoadData);

        var async = SceneManager.UnloadSceneAsync(nameScene);

        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            if (async != null)
            {
                if (async.isDone)
                {
                    var asyncUnload = Resources.UnloadUnusedAssets();
                    yield return asyncUnload;

                    Debug.Log("Unload scene: " + nameScene);
                    //LoadingPanel.Instance.LoadingStop();

                    yield break;
                }
            }

            yield return null;
        }
    }
}