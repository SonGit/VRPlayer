using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine (LoadAsyncSceneMainMenu ());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	IEnumerator LoadAsyncSceneMainMenu (){

		yield return new WaitForSeconds(0.5f);

		Screen.orientation = ScreenOrientation.LandscapeLeft;

		yield return new WaitForSeconds(0.5f);

		Debug.Log ("LoadSceneAsync: MainMenu");

		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu");

		// Wait until the asynchronous scene fully loads
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
	}
}
