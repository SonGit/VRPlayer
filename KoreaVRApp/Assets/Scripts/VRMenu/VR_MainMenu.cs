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

	public VR_BasicMenu currentMenu;

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
	[SerializeField]
	private GameObject sensorAlert;
	[SerializeField]
	private GameObject UsablecapacityAlert;
	[SerializeField]
	private GameObject purchaseAlert;
	[SerializeField]
	private GameObject vr_LoadingObj;

	private float _delayCount = 0;
	private float _delayTime = 2.5f;
	Video video;
	VideoUI videoUI;

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
		HideSensorAlert ();
		HideUsablecapacityAlert ();
		HideLoadingUI ();
		HidePurchaseAlert ();
	}

	void Update()
	{
		CheckDisableNetworkAlert ();
		CheckDisableUsablecapacityAlert ();
		CheckDisablePurchaseAlert ();
	}

	public void OpenStorageMenu()
	{
		HideLoginAlert ();
		HideDeleteAlert ();
		HideNetworkAlert ();
		HideNoVideoAlert ();
		HideStreamingAlert ();
		HideSensorAlert ();
		HideUsablecapacityAlert ();
		HideLoadingUI ();
		HidePurchaseAlert ();

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
		HideSensorAlert ();
		HideLoadingUI ();
		HidePurchaseAlert ();

		bool isConnect = CheckNetworkConnection ();
		if (isConnect) {
            if (MainAllController.instance != null)
            {
                if (MainAllController.instance.HasUserLoggedIn())
                {

                    //Show
                    foreach (VR_BasicMenu menu in menus)
                    {
                        if (menu is VR_UserVideoMenu)
                        {
                            currentMenu = menu;
                            menu.Show();
                            if (VR_MyvideoTitleManager.instance != null)
                            {
                                VR_MyvideoTitleManager.instance.VideoListTitle();
                            }

                            if (VR_NavMenuManager.instance != null)
                                VR_NavMenuManager.instance.OnClick_UserVideoMenu();
                        }
                        else
                        {
                            menu.Hide();
                        }
                    }
                    //Show
                }
                else
                {
                    ShowLoginAlert();
                }
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
		HideSensorAlert ();
		HideLoadingUI ();
		HidePurchaseAlert ();

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
		HideSensorAlert ();
		HideLoadingUI ();
		HidePurchaseAlert ();

		bool isConnect = CheckNetworkConnection ();
		if (isConnect) {

            if(MainAllController.instance != null)
            {
                if (MainAllController.instance.HasUserLoggedIn())
                {

                    //Show
                    foreach (VR_BasicMenu menu in menus)
                    {
                        if (menu is VR_InboxMenu)
                        {
                            currentMenu = menu;
                            menu.Show();

                            if (VR_NavMenuManager.instance != null)
                                VR_NavMenuManager.instance.OnClick_DownloadMenu();
                        }
                        else
                        {
                            menu.Hide();
                        }
                    }
                    //Show
                }
                else
                {
                    ShowLoginAlert();
                }
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
		HideSensorAlert ();
		HideLoadingUI ();
		HidePurchaseAlert ();

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
			}else {
				ShowLoginAlert ();
			}
		}else {
			ShowNetworkAlert ();
		}
	}

	public void GoToPage(int pageNo)
	{
		if (currentMenu != null) {
			currentMenu.ShowPage (pageNo);
			currentMenu.FastRefresh ();
		}
	}


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

    #region LogoutAlert

    public void VRLogoutBnt_Onclick()
    {
        bool isConnect = CheckNetworkConnection();
        if (isConnect)
        {
            if (MainAllController.instance != null)
            {
                MainAllController.instance.SceneVR_OnLogout();
            }
        }
        else
        {
            ShowNetworkAlert();
        }
    }

    #endregion

    #region DeleteAlert
    public void ShowDeleteAlert(Video video, VideoUI videoUI){
		if (deleteAlert != null ) {
			deleteAlert.SetActive (true);
			this.video = video;
			this.videoUI = videoUI;
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

//			VR_InboxMenu menu = UnityEngine.Object.FindObjectOfType<VR_InboxMenu> ();
//			if (menu != null) {
//				menu.RemoveUIPerma (vr_InboxVideoUI);
//			}

			OpenInboxMenu ();

			MainAllController.instance.InitInboxVideoMenu2D ();
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

	#region UsablecapacityAlert
	public void ShowUsablecapacityAlert(){
		if (UsablecapacityAlert != null ) {
			UsablecapacityAlert.SetActive (true);
		}else{
			Debug.LogError ("Null................");
		}
	}

	public void HideUsablecapacityAlert(){
		if (UsablecapacityAlert != null) {
			UsablecapacityAlert.SetActive (false);
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
	public void ShowStreamingAlert(VideoUI videoUI){
		if (streamingAlert != null ) {
			streamingAlert.SetActive (true);
			this.videoUI = videoUI;
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
		if (this.videoUI != null){
			this.videoUI.OnClickStreaming3D ();
		}

		HideStreamingAlert ();
	}
	#endregion

	#region SensorAlert
	public void ShowSensorAlert(){
		if (sensorAlert != null ) {
			sensorAlert.SetActive (true);
		}else{
			Debug.LogError ("Null................");
		}
	}

	public void HideSensorAlert(){
		if (sensorAlert != null) {
			sensorAlert.SetActive (false);
		}else{
			Debug.LogError ("Null................");
		}
	}

	public void ClickYesButton_SensorAlert(){
		if (MainAllController.instance != null){
			MainAllController.instance.GoToSensorMenu ();
		}

		HideSensorAlert ();
	}
	#endregion

	#region LoadingUI

	public void ShowLoadingUI(){
		if (vr_LoadingObj != null ) {
			vr_LoadingObj.SetActive (true);
		}else{
			Debug.LogError ("Null................");
		}
	}

	public void HideLoadingUI(){
		if (vr_LoadingObj != null) {
			vr_LoadingObj.SetActive (false);
		}else{
			Debug.LogError ("Null................");
		}
	}
		
	#endregion

	#region PurchaseAlert

	public void ShowPurchaseAlert(){
		if (purchaseAlert != null ) {
			purchaseAlert.SetActive (true);
		}else{
			Debug.LogError ("Null................");
		}
	}

	public void HidePurchaseAlert(){
		if (purchaseAlert != null) {
			purchaseAlert.SetActive (false);
		}else{
			Debug.LogError ("Null................");
		}
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

	private void CheckDisableUsablecapacityAlert(){

		if (UsablecapacityAlert.activeSelf && UsablecapacityAlert != null) {
			_delayCount += Time.deltaTime;
			if (_delayCount >= _delayTime) {
				_delayCount = 0;
				HideUsablecapacityAlert ();
			}
		}
	}

	private void CheckDisablePurchaseAlert(){

		if (purchaseAlert.activeSelf && purchaseAlert != null) {
			_delayCount += Time.deltaTime;
			if (_delayCount >= _delayTime) {
				_delayCount = 0;
				HidePurchaseAlert ();
			}
		}
	}

	#endregion

	#region CheckNoVideos

	public void VR_CheckNovideos(){
		bool isNoVideo = currentMenu.VR_CheckNoVideos ();
		if (!isNoVideo)
		{
			HideNoVideoAlert();
		}
		else
		{
			ShowNoVideoAlert();
		}
	}
		
	#endregion
}
