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

		if (Screen.orientation != ScreenOrientation.LandscapeLeft) {
			Screen.orientation = ScreenOrientation.LandscapeLeft;
			Debug.Log ("ScreenOrientation.LandscapeLeft");
		} else {
			Debug.Log ("No Switch Screen");
		}
			
		yield return new WaitForSeconds(2f);

		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu");

		// Wait until the asynchronous scene fully loads
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
	}
}
