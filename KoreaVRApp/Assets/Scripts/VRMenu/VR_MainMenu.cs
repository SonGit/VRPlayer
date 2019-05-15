using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

public class VR_MainMenu : MonoBehaviour
{
	public static VR_MainMenu instance;

	VR_BasicMenu[] menus;

	[SerializeField]
	VR_BasicMenu currentMenu;

	[SerializeField] private BasicButtonMenu btnLogin;
	[SerializeField] private BasicButtonMenu btnLogout;
	[SerializeField] private Text userNameText;
	[SerializeField]
	private GameObject loginAlert;
	[SerializeField]
	private GameObject deleteAlert;
	[SerializeField]
	private GameObject networkAlert;
	[SerializeField]
	private GameObject noVideoAlert;
	[SerializeField]
	private GameObject streamingAlert;

	private float _delayCount = 0;
	private float _delayTime = 2.5f;
	Video video;
	VR_InboxVideoUI vr_InboxVideoUI;
	VR_UserVideoUI vr_UserVideoUI;

	void Awake()
	{
		instance = this;
		menus = this.GetComponents<VR_BasicMenu> ();

	}


    // Start is called before the first frame update
    void Start()
    {
		Init ();
    }

	void Init(){

		HideLoginAlert ();
		HideDeleteAlert ();
		HideNetworkAlert ();
		HideStreamingAlert ();
	}

	void Update()
	{
		CheckDisableNetworkAlert ();
	}

	public void OpenStorageMenu()
	{
		HideLoginAlert ();
		HideDeleteAlert ();
		HideNetworkAlert ();
		HideNoVideoAlert ();
		HideStreamingAlert ();


		//Show
		foreach (VR_BasicMenu menu in menus) {
			if (menu is VR_StorageMenu) {
				currentMenu = menu;
				menu.Show ();

				if(VR_NavMenuManager.instance != null)
					VR_NavMenuManager.instance.OnClick_PhoneVideoMenu ();
				
			} else {
				menu.Hide ();
			}
		}
		//Show

		if (MainAllController.instance != null){
			bool isNoVideo = MainAllController.instance.CheckNovideos_LocalVideo ();
			if (!isNoVideo) {
				HideNoVideoAlert ();
			} else {
				ShowNoVideoAlert ();
			}
		}
	}

	/// <summary>
	/// Useful for init storage menu
	/// This is called when LocalVideoManager is ready.
	/// </summary>
	public void InitStorageMenu()
	{
		foreach (VR_BasicMenu menu in menus) {
			if (menu is VR_StorageMenu) {
				menu.FastRefresh ();
			}
		}
	}

	public void OpenUserVideoMenu()
	{
		HideDeleteAlert ();
		HideNoVideoAlert ();
		HideStreamingAlert ();

		bool isConnect = CheckNetworkConnection ();
		if (isConnect) {
			if (MainAllController.instance.HasUserLoggedIn ()) {

				//Show
				foreach (VR_BasicMenu menu in menus) {
					if (menu is VR_UserVideoMenu) {
						currentMenu = menu;
						menu.Show ();
						if (VR_MyvideoTitleManager.instance != null) {
							VR_MyvideoTitleManager.instance.VideoListTitle ();
						}

						if(VR_NavMenuManager.instance != null)
							VR_NavMenuManager.instance.OnClick_UserVideoMenu ();
					} else {
						menu.Hide ();
					}
				}
				//Show

				if (MainAllController.instance != null){
					bool isNoVideo = MainAllController.instance.CheckNovideos_UserVideo ();
					if (!isNoVideo) {
						HideNoVideoAlert ();
					} else {
						ShowNoVideoAlert ();
					}
				}
			} else {
				ShowLoginAlert ();
			}
		} else {
			ShowNetworkAlert ();
		}
	}

	public void OpenDownloadMenu()
	{
		HideDeleteAlert ();
		HideNoVideoAlert ();
		HideStreamingAlert ();

		bool isConnect = CheckNetworkConnection ();
		if (isConnect) {
			if (MainAllController.instance.HasUserLoggedIn ()) {
				
				//Show
				foreach (VR_BasicMenu menu in menus) {
					if (menu is VR_DownloadMenu) {
						currentMenu = menu;
						menu.Show ();

						if (VR_NavMenuManager.instance != null) {
							VR_NavMenuManager.instance.OnClick_DownloadMenu ();
						}
					} else {
						menu.Hide ();
					}
				}
				//Show

			}else {
				ShowLoginAlert ();
			}
		}else {
			ShowNetworkAlert ();
		}
	}

	public void OpenInboxMenu()
	{
		HideDeleteAlert ();
		HideNoVideoAlert ();
		HideStreamingAlert ();

		bool isConnect = CheckNetworkConnection ();
		if (isConnect) {
			if (MainAllController.instance.HasUserLoggedIn ()) {

				//Show
				foreach (VR_BasicMenu menu in menus) {
					if (menu is VR_InboxMenu) {
						currentMenu = menu;
						menu.Show ();

						if(VR_NavMenuManager.instance != null)
							VR_NavMenuManager.instance.OnClick_DownloadMenu ();
					} else {
						menu.Hide ();
					}
				}
				//Show

				if (MainAllController.instance != null){
					bool isNoVideo = MainAllController.instance.CheckNovideos_InboxVideo ();
					if (!isNoVideo) {
						HideNoVideoAlert ();
					} else {
						ShowNoVideoAlert ();
					}
				}

			}else {
				ShowLoginAlert ();
			}
		}else {
			ShowNetworkAlert ();
		}
	}

	public void OpenFavoriteMenu()
	{
		HideDeleteAlert ();
		HideNoVideoAlert ();
		HideStreamingAlert ();

		bool isConnect = CheckNetworkConnection ();
		if (isConnect) {
			if (MainAllController.instance.HasUserLoggedIn ()) {

				//Show
				foreach (VR_BasicMenu menu in menus) {
					if (menu is VR_FavoriteMenu) {
						currentMenu = menu;
						menu.Show ();
						if (VR_MyvideoTitleManager.instance != null) {
							VR_MyvideoTitleManager.instance.FavoriteTitle ();
						}

						if (VR_NavMenuManager.instance != null) {
							VR_NavMenuManager.instance.OnClick_FavoriteVideoMenu ();
						}
					} else {
						menu.Hide ();
					}
				}
				//Show

				if (MainAllController.instance != null){
					bool isNoVideo = MainAllController.instance.CheckNovideos_FavoriteVideo ();
					if (!isNoVideo) {
						HideNoVideoAlert ();
					} else {
						ShowNoVideoAlert ();
					}
				}

			}else {
				ShowLoginAlert ();
			}
		}else {
			ShowNetworkAlert ();
		}
	}

	public void GoToPage(int pageNo)
	{
//		Debug.Log ("asfssssss");
		if(currentMenu != null)
		currentMenu.ShowPage (pageNo);
	}



//	#region AutoLogin for Testing 
//	public void Login()
//	{
//		if (ScreenLoading.instance != null) {
//			ScreenLoading.instance.Play ();
//		}
//
//		if (Networking.instance != null) {
//			Networking.instance.LoginRequest ("mint1002", "minttest", OnGetLogin, ErrorGetLoginCallback);
//		}
//	}
//
//	private void OnGetLogin(LoginRespone response){
//		if (response.event_code == "201") {
//			MainAllController.instance.LoginUser("mint1002", response.auth_token);
//		}
//	}
//
//	public void ErrorGetLoginCallback(){
//
//	}
//	#endregion


	/// <summary>
	/// Logout/login the viewable.
	/// </summary>
	/// <param name="visible">If set to <c>true</c> visible.</param>
	public void LogoutViewable_VR(bool visible)
	{
		if (btnLogout != null)
			btnLogout.gameObject.SetActive (visible);
		else {
			Debug.LogError ("btnLogout null!");
		}
	}

	public void LoginViewable_VR(bool visible)
	{
		if (btnLogout != null)
			btnLogin.gameObject.SetActive (visible);
		else {
			Debug.LogError ("btnLogin null!");
		}
	}

	public void UserNameViewable_VR(string name)
	{
		if (userNameText != null)
			userNameText.text = name;
		else {
			Debug.LogError ("userNameText null!");
		}
	}

	#region LoginAlert
	public void ShowLoginAlert(){
		bool isConnect = CheckNetworkConnection ();
		if (isConnect) {
			if (loginAlert != null) {
				loginAlert.SetActive (true);
			} else {
				Debug.LogError ("Null................");
			}
		} else {
			ShowNetworkAlert ();
		}
	}

	public void HideLoginAlert(){
		if (loginAlert != null) {
			loginAlert.SetActive (false);
		}else{
			Debug.LogError ("Null................");
		}
	}

	public void ClickYesButton_LoginAlert(){
		if (MainAllController.instance != null){
			MainAllController.instance.OpenLoginMenuFromVR ();
		}

		HideLoginAlert ();
	}
	#endregion

	#region DeleteAlert
	public void ShowDeleteAlert(Video video, VR_InboxVideoUI vr_InboxVideoUI){
		if (deleteAlert != null ) {
			deleteAlert.SetActive (true);
			this.video = video;
			this.vr_InboxVideoUI = vr_InboxVideoUI;
		}else{
			Debug.LogError ("Null................");
		}
	}

	public void HideDeleteAlert(){
		if (deleteAlert != null) {
			deleteAlert.SetActive (false);
		}else{
			Debug.LogError ("Null................");
		}
	}
		
	public void ClickYesButton_DeleteAlert(){
		string path = Path.Combine (MainAllController.instance.user.GetPath(), video.videoInfo.id) ;

		if (Directory.Exists (path)) {
			
			try
			{
				Directory.Delete (path, true);
			} catch (Exception e) {
				Debug.Log ("DeleteVideo exception " + e.Message);
			}
				
			HideDeleteAlert ();

			VR_InboxMenu menu = UnityEngine.Object.FindObjectOfType<VR_InboxMenu> ();
			if (menu != null) {
				menu.RemoveUIPerma (vr_InboxVideoUI);
			}

			OpenInboxMenu ();

			MainAllController.instance.RefeshInboxVideo2D ();
		}
	}
	#endregion

	#region NetworkAlert
	public void ShowNetworkAlert(){
		if (networkAlert != null ) {
			networkAlert.SetActive (true);
		}else{
			Debug.LogError ("Null................");
		}
	}

	public void HideNetworkAlert(){
		if (networkAlert != null) {
			networkAlert.SetActive (false);
		}else{
			Debug.LogError ("Null................");
		}
	}
	#endregion

	#region NoVideoAlert
	public void ShowNoVideoAlert(){
		if (noVideoAlert != null ) {
			noVideoAlert.SetActive (true);
		}else{
			Debug.LogError ("Null................");
		}
	}

	public void HideNoVideoAlert(){
		if (noVideoAlert != null) {
			noVideoAlert.SetActive (false);
		}else{
			Debug.LogError ("Null................");
		}
	}
	#endregion

	#region StreamingAlert
	public void ShowStreamingAlert(VR_UserVideoUI vr_UserVideoUI){
		if (streamingAlert != null ) {
			streamingAlert.SetActive (true);
			this.vr_UserVideoUI = vr_UserVideoUI;
		}else{
			Debug.LogError ("Null................");
		}
	}

	public void HideStreamingAlert(){
		if (streamingAlert != null) {
			streamingAlert.SetActive (false);
		}else{
			Debug.LogError ("Null................");
		}
	}

	public void ClickYesButton_StreamingAlert(){
		if (this.vr_UserVideoUI != null){
			this.vr_UserVideoUI.OnClickStreaming3D ();
		}

		HideStreamingAlert ();
	}
	#endregion


	#region CheckNetworkConnection

	public bool CheckNetworkConnection (){
		if(Application.internetReachability == NetworkReachability.NotReachable)
		{
			return false;
		}

		return true;
	}

	private void CheckDisableNetworkAlert(){

		if (networkAlert.activeSelf && networkAlert != null) {
			_delayCount += Time.deltaTime;
			if (_delayCount >= _delayTime) {
				_delayCount = 0;
				HideNetworkAlert ();
			}
		}
	}

	#endregion


}
