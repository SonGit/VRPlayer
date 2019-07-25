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
	private GameObject vrProgressBar;

	private VR_RecenterPanel vr_RecenterPanel;

	[SerializeField]
	private VR_ButtonScreenLock buttonScreenLock;

	private BasicMenu lastMenu;

    // Start is called before the first frame update
    void Start()
    {
		vr_RecenterPanel = UnityEngine.Object.FindObjectOfType<VR_RecenterPanel>();

      //  Show();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

	void ShowMainMenu()
	{
		vrMainMenu.gameObject.SetActive (true);

		if (vr_RecenterPanel != null && !MainAllController.instance.IsShowRecenterPanel){
			vrMainMenu.gameObject.SetActive (false);
			vr_RecenterPanel.Show (OnDoneShowVR_MainMenu);
		}
	}

	private void SetupAllSetting(){
		vrMainMenu.gameObject.SetActive (true);

		vrSetting.HideSetting ();

		buttonScreenLock.OnClickLockBtn (true);
		vrPlayer.gameObject.SetActive (false);
		ShowProgressBar ();

		// HasUserLoggedIn ???
		if (MainAllController.instance != null && MainAllController.instance.HasUserLoggedIn ()) {
			ClickLoginBnt ();

			vrMainMenu.UserNameViewable_VR (MainAllController.instance.GetUserNameInput());
		} else {
			ClickLogoutBnt ();
		}
	}

	public void ShowStorageMenu()
	{
		SetupAllSetting ();
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

		vrMainMenu.UserNameViewable_VR ("");

		if (MainAllController.instance != null) {
			vrMainMenu.UserNameViewable_VR (MainAllController.instance.GetUserNameInput());
		}

		vrMainMenu.OpenStorageMenu ();
	}

	public void ShowUserVideoMenu()
	{
		bool isConnect = VR_MainMenu.instance.CheckNetworkConnection ();
		if (isConnect) {
			SetupAllSetting ();
			VR_MainMenu.instance.OpenUserVideoMenu ();
			vrPlayer.gameObject.SetActive (false);
			vrSetting.HideSetting ();
		} else {
			ShowStorageMenu ();
		}
	}

	public void ShowDownloadMenu()
	{
		bool isConnect = VR_MainMenu.instance.CheckNetworkConnection ();
		if (isConnect) {
			SetupAllSetting ();
			VR_MainMenu.instance.OpenDownloadMenu ();
			vrPlayer.gameObject.SetActive (false);
			vrSetting.HideSetting ();
		}else {
			ShowStorageMenu ();
		}
	}

	public void ShowInboxMenu()
	{
		bool isConnect = VR_MainMenu.instance.CheckNetworkConnection ();
		if (isConnect) {
			SetupAllSetting ();
			VR_MainMenu.instance.OpenInboxMenu ();
			vrPlayer.gameObject.SetActive (false);
			vrSetting.HideSetting ();
		}else {
			ShowStorageMenu ();
		}
	}

	public void ShowFavoriteVideoMenu()
	{
		bool isConnect = VR_MainMenu.instance.CheckNetworkConnection ();
		if (isConnect) {
			SetupAllSetting ();
			VR_MainMenu.instance.OpenFavoriteMenu ();
			vrPlayer.gameObject.SetActive (false);
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

	/// <summary>
	/// Shows the VR menu after click RecenterPanel
	/// </summary>
	private void ShowVR_Menu(){

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

			if (lastMenu is DownloadMenu) {
				ShowInboxMenu ();
			}

			if (lastMenu is InboxMenu) {
				ShowInboxMenu ();
			}
		}
	}

	#region Listvideo Button in vr_setting
	public void ShowCurrentVR_Menu(){

		if (VR_MainMenu.instance.currentMenu != null) {
			if (VR_MainMenu.instance.currentMenu is VR_StorageMenu) {
				ShowStorageMenu ();
			}

			if (VR_MainMenu.instance.currentMenu is VR_UserVideoMenu) {
				ShowUserVideoMenu ();
			}

			if (VR_MainMenu.instance.currentMenu is VR_FavoriteMenu) {
				ShowFavoriteVideoMenu ();
			}

			if (VR_MainMenu.instance.currentMenu is VR_InboxMenu) {
				ShowInboxMenu ();
			}
		} else {
			ShowStorageMenu ();
		}
	}

	#endregion


	Video currentVideo;
	string currentUrl;

	public void PlayFromURL(Video video)
	{
		vrSetting.HideSetting ();
		vrMainMenu.gameObject.SetActive (false);
		ResetAllMode ();

		string url = string.Empty;

		if (video is LocalVideo) {
			vrPlayer.InitSubtitle ();
            //MainAllController.instance.IsShowRecenterPanel = false;

        } else {
//			if (currentVideo != null && currentVideo.videoInfo.id == video.videoInfo.id) {
//				Debug.Log ("Not Setup Subtitle");
//			} else {
//				vrPlayer.InitSubtitle ();
//				vrPlayer.SetupSubtitle (video);
//				Debug.Log ("Setup Subtitle");
//			}

			vrPlayer.InitSubtitle ();
			vrPlayer.SetupSubtitle (video);
			Debug.Log ("Setup Subtitle");
		}

		currentVideo = video;

        //vr_RecenterPanel.Show(OnDoneRecenter);

        if (vr_RecenterPanel != null && !MainAllController.instance.IsShowRecenterPanel) {
			ShowProgressBar ();
        	vr_RecenterPanel.Show (OnDoneRecenter);
        } else {
        	OnDoneRecenter ();
        }
    }

	public void Streaming(Video video, string url)
	{
		vrSetting.HideSetting ();
		vrMainMenu.gameObject.SetActive (false);
		ResetAllMode ();

		if (video is LocalVideo) {

		} else {
//			if (currentVideo != null && currentVideo.videoInfo.id == video.videoInfo.id) {
//				Debug.Log ("Not Setup Subtitle");
//			} else {
//				vrPlayer.InitSubtitle ();
//				vrPlayer.SetupSubtitle (video);
//				Debug.Log ("Setup Subtitle");
//			}

			vrPlayer.InitSubtitle ();
			vrPlayer.SetupSubtitle (video);
			Debug.Log ("Setup Subtitle");
		}

		currentVideo = video;
		currentUrl = url;

		if (vr_RecenterPanel != null && !MainAllController.instance.IsShowRecenterPanel) {
			ShowProgressBar ();
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

	void OnDoneShowVR_MainMenu()
	{
		if (vrMainMenu != null){
			vrMainMenu.gameObject.SetActive (true);
		}

		if (vrSetting != null){
			vrSetting.HideSetting ();
		}

		ShowVR_Menu ();
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
		if (vrProgressBar != null) {
			vrProgressBar.SetActive(false);
		} else {
			Debug.LogError ("NULL...............");
		}
	}

	public void ShowProgressBar()
	{
		if (vrProgressBar != null) {
			vrProgressBar.SetActive(true);
		}else {
			Debug.LogError ("NULL...............");
		}
	}

	public override void Show(BasicMenu lastMenu = null)
	{

		Screen.fullScreen = true;

		base.Show (lastMenu);
		StartCoroutine(SwitchToVR());

		ShowMainMenu ();
		SetupAllSetting ();

		VR_Recenterer.instance.Recenter ();

		// Return to user to the equivalent menu vased on last menu
		if (lastMenu != null) 
		{
			this.lastMenu = lastMenu;
		}
		else
        {
            ShowStorageMenu();
        }
	}
		


	IEnumerator LoadDevice(string newDevice)
	{
//		int targetWidth = MainAllController.instance.maxWidth;
//		int targetHeight = MainAllController.instance.maxHeight;
//
//		yield return new WaitForEndOfFrame();
//		UnityEngine.XR.XRSettings.LoadDeviceByName(newDevice);
//		yield return null;
//		UnityEngine.XR.XRSettings.enabled = true;
//
//		Screen.orientation = ScreenOrientation.LandscapeLeft;
//		yield return new WaitForSeconds (.25f);
//		Screen.SetResolution (targetHeight,targetWidth,true);
//		yield return new WaitForSeconds (.25f);
//
//		VR_Recenterer.instance.Recenter ();
//
//		QualitySettings.vSyncCount = 0;
//		Application.targetFrameRate = 60;
//
//		Debug.Log ("VR ++++Current Resolution  " + Screen.currentResolution.width + "  " + Screen.currentResolution.height);
//
		yield return new WaitForSeconds (.25f);
	}

	IEnumerator SwitchToVR() {

        if (MainAllController.instance != null) {
			MainAllController.instance.ShowScreenSwitchSceneMode ();
		}

		GvrViewer.Instance.VRModeEnabled = true;

		yield return new WaitForSeconds (0.25f);


        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        //#if !UNITY_EDITOR
        // Disable auto rotation, except for landscape left.
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        yield return new WaitForSeconds(1.4f);
        Debug.Log("SwitchToVR DONE ROTATING!");

		if (MainAllController.instance != null) {
			MainAllController.instance.ShowVR_CloseButton ();
		}

		if (MainAllController.instance != null) {
			MainAllController.instance.HideScreenSwitchSceneMode ();
		}

        if(VRCrosshair != null)
        {
            VRCrosshair.SetActive(true);
        }else
        {
            Debug.Log("VRCrosshair is null!");
        }

        yield return new WaitForSeconds(1f);

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

	private void ResetAllMode(){
		vrPlayer.CinemaMode ();
		vrPlayer.PackingNone ();

		if (VR_ModeManager.instance != null){
			VR_ModeManager.instance.Init ();
		}

		if (VR_SettingsManager.instance != null){
			VR_SettingsManager.instance.Init ();
		}
	}
}
