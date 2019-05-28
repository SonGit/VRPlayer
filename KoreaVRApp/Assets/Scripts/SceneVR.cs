using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEditor;
using UnityEngine.SceneManagement;

public class SceneVR : AppScene
{
	public VRPlayer vrPlayer;
	[SerializeField]
	private VR_SettingMenu vrSetting;
	[SerializeField]
	private VR_MainMenu vrMainMenu;
	[SerializeField]
	private Canvas vrProgressBarCanvas;

	private VR_RecenterPanel vr_RecenterPanel;

	[SerializeField]
	private VR_ButtonScreenLock buttonScreenLock;

    // Start is called before the first frame update
    void Start()
    {
		vr_RecenterPanel = UnityEngine.Object.FindObjectOfType<VR_RecenterPanel>();

        Show();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

	void ShowMainMenu()
	{
		vrSetting.HideSetting ();
		vrMainMenu.gameObject.SetActive (true);

		//if (vr_RecenterPanel != null && !MainAllController.instance.IsShowRecenterPanel){
		//	vr_RecenterPanel.Show ();
		//}
			
		buttonScreenLock.OnClickLockBtn (true);
		vrPlayer.gameObject.SetActive (false);
		ShowProgressBar ();

		// HasUserLoggedIn ???
		if (MainAllController.instance != null && MainAllController.instance.HasUserLoggedIn ()) {
			ClickLoginBnt ();

			vrMainMenu.UserNameViewable_VR (MainAllController.instance.GetUserNameInput());
		} else {
			ClickLogoutBnt ();

			vrMainMenu.UserNameViewable_VR ("");
		}
	}

	public void ShowStorageMenu()
	{
		ShowMainMenu ();
		vrMainMenu.OpenStorageMenu ();
		//vrPlayer.gameObject.SetActive (false);

		//vrSetting.HideSetting ();
	}

	public void ClickLoginBnt(){
		vrMainMenu.LoginViewable_VR (false);
		vrMainMenu.LogoutViewable_VR (true);
	}

	public void ClickLogoutBnt(){
		vrMainMenu.LoginViewable_VR (true);
		vrMainMenu.LogoutViewable_VR (false);

		if (MainAllController.instance != null) {
			vrMainMenu.UserNameViewable_VR (MainAllController.instance.GetUserNameInput());
		}
	}

	public void ShowUserVideoMenu()
	{
		bool isConnect = VR_MainMenu.instance.CheckNetworkConnection ();
		if (isConnect) {
			ShowMainMenu ();
			VR_MainMenu.instance.OpenUserVideoMenu ();
			vrPlayer.gameObject.SetActive (false);
			ShowProgressBar ();
			vrSetting.HideSetting ();
		} else {
			ShowStorageMenu ();
		}
	}

	public void ShowDownloadMenu()
	{
		bool isConnect = VR_MainMenu.instance.CheckNetworkConnection ();
		if (isConnect) {
			ShowMainMenu ();
			VR_MainMenu.instance.OpenDownloadMenu ();
			vrPlayer.gameObject.SetActive (false);
			ShowProgressBar ();
			vrSetting.HideSetting ();
		}else {
			ShowStorageMenu ();
		}
	}

	public void ShowInboxMenu()
	{
		bool isConnect = VR_MainMenu.instance.CheckNetworkConnection ();
		if (isConnect) {
			ShowMainMenu ();
			VR_MainMenu.instance.OpenInboxMenu ();
			vrPlayer.gameObject.SetActive (false);
			ShowProgressBar ();
			vrSetting.HideSetting ();
		}else {
			ShowStorageMenu ();
		}
	}

	public void ShowFavoriteVideoMenu()
	{
		bool isConnect = VR_MainMenu.instance.CheckNetworkConnection ();
		if (isConnect) {
			ShowMainMenu ();
			VR_MainMenu.instance.OpenFavoriteMenu ();
			vrPlayer.gameObject.SetActive (false);
			ShowProgressBar ();
			vrSetting.HideSetting ();
		}else {
			ShowStorageMenu ();
		}
	}

	public void ShowSetting()
	{
		if (!vrMainMenu.gameObject.activeInHierarchy) {
			vrSetting.ShowSetting ();
		}

		ShowProgressBar ();
	}


	Video currentVideo;
	string currentUrl;

	public void PlayFromURL(Video video)
	{
		vrSetting.HideSetting ();
		vrMainMenu.gameObject.SetActive (false);

		string url = string.Empty;

		if (video is LocalVideo) {
			vrPlayer.InitSubtitle ();
            //MainAllController.instance.IsShowRecenterPanel = false;

        } else {
			if (currentVideo != null && currentVideo.videoInfo.id == video.videoInfo.id) {
				Debug.Log ("Not Setup Subtitle");
			} else {
				vrPlayer.InitSubtitle ();
				vrPlayer.SetupSubtitle (video);
				Debug.Log ("Setup Subtitle");
			}
		}

		currentVideo = video;

        vr_RecenterPanel.Show(OnDoneRecenter);

        //if (vr_RecenterPanel != null && !MainAllController.instance.IsShowRecenterPanel) {
        //	vr_RecenterPanel.Show (OnDoneRecenter);
        //} else {
        //	OnDoneRecenter ();
        //}
    }

	public void Streaming(Video video, string url)
	{
		vrSetting.HideSetting ();
		vrMainMenu.gameObject.SetActive (false);

		if (video is LocalVideo) {

		} else {
			if (currentVideo != null && currentVideo.videoInfo.id == video.videoInfo.id) {
				Debug.Log ("Not Setup Subtitle");
			} else {
				vrPlayer.InitSubtitle ();
				vrPlayer.SetupSubtitle (video);
				Debug.Log ("Setup Subtitle");
			}
		}

		currentVideo = video;
		currentUrl = url;

		if (vr_RecenterPanel != null && !MainAllController.instance.IsShowRecenterPanel) {
			vr_RecenterPanel.Show (OnDoneRecenter_Streaming);
		}else {
			OnDoneRecenter_Streaming ();
		}
	}

	public void LoadSubTitleVR(string url)
	{
		vrPlayer.LoadSubTitles (url);
	}

	public void DisableSubtitleVR()
	{
		vrPlayer.DisableSubtitles ();
		vrPlayer.ClearTextSubTitle ();
	}

	void OnDoneRecenter()
	{
		vrSetting.HideSetting ();

		ShowPlayer ();

		HideProgressBar ();

		if (currentVideo != null) {
			vrPlayer.Play (currentVideo);
		}
	}

	void OnDoneRecenter_Streaming()
	{
		vrSetting.HideSetting ();

		ShowPlayer ();

		HideProgressBar ();

		if (currentVideo != null) {
			vrPlayer.Stream (currentVideo,currentUrl);
		}
	}
		
	void HidePlayer()
	{
		vrPlayer.gameObject.SetActive (false);
	}

	void ShowPlayer()
	{
		vrPlayer.gameObject.SetActive (true);
	}

	public void HideProgressBar()
	{
		if (vrProgressBarCanvas != null) {
			vrProgressBarCanvas.enabled = false;
		} else {
			Debug.LogError ("NULL...............");
		}
	}

	public void ShowProgressBar()
	{
		if (vrProgressBarCanvas != null) {
			vrProgressBarCanvas.enabled = true;
		}else {
			Debug.LogError ("NULL...............");
		}
	}

	public override void Show(BasicMenu lastMenu = null)
	{

		Screen.fullScreen = true;

		base.Show (lastMenu);
		StartCoroutine(LoadDevice("cardboard"));

		// Return to user to the equivalent menu vased on last menu
		if (lastMenu != null) {
			if (lastMenu is StorageMenu) {
				ShowStorageMenu ();
			}

			if (lastMenu is UserVideoMenu) {
				ShowUserVideoMenu ();
			}

			if (lastMenu is FavoriteVideoMenu) {
				ShowFavoriteVideoMenu ();
			}

//			if (lastMenu is UserDetailMenu) {
//				ShowFavoriteVideoMenu ();
//			}

			if (lastMenu is DownloadMenu) {
				ShowInboxMenu ();
			}

			if (lastMenu is InboxMenu) {
				ShowInboxMenu ();
			}
		}else
        {
            ShowStorageMenu();
        }

		VR_Recenterer.instance.Recenter ();
	}

	IEnumerator LoadDevice(string newDevice)
	{
		//int targetWidth = MainAllController.instance.maxWidth;
		//int targetHeight = MainAllController.instance.maxHeight;

		yield return new WaitForEndOfFrame();
		UnityEngine.XR.XRSettings.LoadDeviceByName(newDevice);
		yield return null;
		UnityEngine.XR.XRSettings.enabled = true;

		Debug.Log ("++++ VR last " + Screen.currentResolution.width + "  " + Screen.currentResolution.height);
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		yield return new WaitForSeconds (.25f);
		//Screen.SetResolution (targetHeight,targetWidth,true);
		yield return new WaitForSeconds (.25f);
		Debug.Log ("++++ VR current " + Screen.currentResolution.width + "  " + Screen.currentResolution.height);

		Debug.Log ("++++ VR RES " + Screen.resolutions[0].width + "  " + Screen.resolutions[0].height);

		VR_Recenterer.instance.Recenter ();

		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
	}

	public override void Hide()
	{
		base.Hide ();

		vrPlayer.gameObject.SetActive (false);
		vrSetting.HideSetting ();

		if (vr_RecenterPanel != null){
			vr_RecenterPanel.Hide();
		}
	}
}
