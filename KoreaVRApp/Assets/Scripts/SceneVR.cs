using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEditor;

public class SceneVR : AppScene
{
	public VRPlayer vrPlayer;
	[SerializeField]
	private VR_SettingMenu vrSetting;
	[SerializeField]
	private VR_MainMenu vrMainMenu;

	private VR_RecenterPanel vr_RecenterPanel;

	[SerializeField]
	private VR_ButtonScreenLock buttonScreenLock;

    // Start is called before the first frame update
    void Start()
    {
		vr_RecenterPanel = UnityEngine.Object.FindObjectOfType<VR_RecenterPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void ShowMainMenu()
	{
		vrSetting.HideSetting ();
		vrMainMenu.gameObject.SetActive (true);

		if (vr_RecenterPanel != null && !MainAllController.instance.IsShowRecenterPanel){
			vr_RecenterPanel.Show ();
		}
			
		buttonScreenLock.OnClickLockBtn (true);
		vrPlayer.gameObject.SetActive (false);

		// HasUserLoggedIn ???
		if (MainAllController.instance != null && MainAllController.instance.HasUserLoggedIn ()) {
			ClickLoginBnt ();
		} else {
			ClickLogoutBnt ();
		}

		if (MainAllController.instance != null) {
			vrMainMenu.UserNameViewable_VR (MainAllController.instance.GetUserNameInput());
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

		//vrMainMenu.gameObject.SetActive (false);
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

		if (vr_RecenterPanel != null && !MainAllController.instance.IsShowRecenterPanel) {
			vr_RecenterPanel.Show (OnDoneRecenter);
		} else {
			OnDoneRecenter ();
		}
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

		if (currentVideo != null) {
			vrPlayer.Play (currentVideo);
		}

	}

	void OnDoneRecenter_Streaming()
	{
		vrSetting.HideSetting ();

		ShowPlayer ();

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

			if (lastMenu is DownloadMenu) {
				ShowDownloadMenu ();
			}
		}
	}

	IEnumerator LoadDevice(string newDevice)
	{
		yield return new WaitForEndOfFrame();
		UnityEngine.XR.XRSettings.LoadDeviceByName(newDevice);
		yield return null;
		UnityEngine.XR.XRSettings.enabled = true;

		VR_Recenterer.instance.Recenter ();
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
