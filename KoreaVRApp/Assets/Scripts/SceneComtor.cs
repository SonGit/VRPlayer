using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneComtor : MonoBehaviour
{

	private User user;
	
	private static SceneComtor _instance;

	public static SceneComtor instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<SceneComtor>();

				if (_instance == null)
				{
					GameObject container = new GameObject("SceneComtor");
					_instance = container.AddComponent<SceneComtor>();
				}
			}

			return _instance;
		}
	}

	void Awake()
	{
		//This object will persists on all scenes
		DontDestroyOnLoad(this.gameObject);
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape)){
			SceneComtor.instance.GoTo2D ();
		}
	}

    // Start is called before the first frame update
    public void Start()
    {
        
    }

	public void SetUser(User user)
	{
		this.user = user;
	}

	public User GetUser()
	{
		return user;
	}

	public void GoTo2D(BasicMenu lastMenu = null)
	{
		StartCoroutine (BackTo2DScene(lastMenu));
	}

	IEnumerator BackTo2DScene(BasicMenu lastMenu= null)
	{
		Camera.main.enabled = false;
		// The Application loads the Scene in the background as the current Scene runs.
		// This is particularly good for creating loading screens.
		// You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
		// a sceneBuildIndex of 1 as shown in Build Settings.

		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu");

		// Wait until the asynchronous scene fully loads
		while (!asyncLoad.isDone)
		{
			yield return null;
		}

		if (MainAllController.instance != null) {

			// Return to user to the equivalent menu vased on last menu
			if (lastMenu != null) {
				if (lastMenu is StorageMenu) {
					MainAllController.instance.AccessMenu_OnMyStorage ();
				}

				if (lastMenu is UserVideoMenu) {
					MainAllController.instance.AccessMenu_OnMyVideo ();
				}

				if (lastMenu is FavoriteVideoMenu) {
					MainAllController.instance.AccessMenu_OnFavoriteMenu ();
				}

				if (lastMenu is UserDetailMenu) {
					MainAllController.instance.UserVideo_OnUserVideoDetail ();
				}

				if (lastMenu is DownloadMenu) {
					MainAllController.instance.GoToDownloadMenu ();
				}

				if (lastMenu is InboxMenu) {
					MainAllController.instance.GoToInbox ();
				}
			} else {
				MainAllController.instance.AccessMenu_OnMyStorage ();
			}

		} else {

			Debug.Log ("MainAllController is null!");

		}
	}

	public void GoTo3D(BasicMenu lastMenu = null)
	{
		// Remember this scene
		lastMenuName = lastMenu.name;
		StartCoroutine (GoTo3D_async());
	}

	public string lastMenuName = string.Empty;

	IEnumerator GoTo3D_async()
	{
		Camera.main.enabled = false;

		// The Application loads the Scene in the background as the current Scene runs.
		// This is particularly good for creating loading screens.
		// You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
		// a sceneBuildIndex of 1 as shown in Build Settings.

		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("VRScene");

		// Wait until the asynchronous scene fully loads
		while (!asyncLoad.isDone)
		{
			yield return null;
		}

		yield return new WaitForEndOfFrame ();

		Debug.Log ("lastMenuName " + lastMenuName);

		//lastMenu = cacheMenu;

		SceneVR sceneVR = UnityEngine.Object.FindObjectOfType<SceneVR> ();

		if (sceneVR != null) {

			switch (lastMenuName) {

			case "StorageMenu":
				sceneVR.ShowStorageMenu ();
				break;
			case "VideoListMenu":
				sceneVR.ShowUserVideoMenu ();
				break;
			}

		} else {

			Debug.Log ("sceneVR is null!");

		}
	}

	public void Play3DFromURL(Video video)
	{
		StartCoroutine (Play3DFromURL_async(video));
	}

	IEnumerator Play3DFromURL_async(Video video)
	{
		Camera.main.enabled = false;
		// The Application loads the Scene in the background as the current Scene runs.
		// This is particularly good for creating loading screens.
		// You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
		// a sceneBuildIndex of 1 as shown in Build Settings.

		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("VRScene");

		// Wait until the asynchronous scene fully loads
		while (!asyncLoad.isDone)
		{
			yield return null;
		}

		SceneVR sceneVR = UnityEngine.Object.FindObjectOfType<SceneVR> ();

		if (sceneVR != null) {

			sceneVR.PlayFromURL (video);

		} else {

			Debug.Log ("sceneVR is null!");

		}
	}

	public void Streaming3D(Video video,string url)
	{
		StartCoroutine (Streaming3D_async(video,url));
	}

	IEnumerator Streaming3D_async(Video video,string url)
	{
		Camera.main.enabled = false;
		// The Application loads the Scene in the background as the current Scene runs.
		// This is particularly good for creating loading screens.
		// You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
		// a sceneBuildIndex of 1 as shown in Build Settings.

		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("VRScene");

		// Wait until the asynchronous scene fully loads
		while (!asyncLoad.isDone)
		{
			yield return null;
		}

		SceneVR sceneVR = UnityEngine.Object.FindObjectOfType<SceneVR> ();

		if (sceneVR != null) {

			sceneVR.Streaming (video,url);

		} else {

			Debug.Log ("sceneVR is null!");

		}
	}
}
