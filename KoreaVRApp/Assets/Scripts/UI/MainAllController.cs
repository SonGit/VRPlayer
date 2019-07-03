using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using NavigationDrawer.UI;
using EasyMobile;
using CielaSpike;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using SimpleDiskUtils;
using EasyMobile.Demo;
using RenderHeads.Media.AVProVideo;

public class MainAllController : MonoBehaviour
{
	public static MainAllController instance;

	[Header("---- Menus ----")]

	private AccessMenu accessMenu = null;
	private StorageMenu storageMenu = null;
	private SettingsMenu settingsMenu = null;
	private LoginMenu loginMenu = null;
	private UserVideoMenu userVideoMenu = null;
	private FavoriteVideoMenu favoriteMenu = null;
	private DownloadMenu downloadMenu = null;
	private UserDetailMenu userDetailMenu = null;
	private InboxMenu inboxMenu = null;
	private VRPlayerMenu vrPlayerMenu = null;
	private MediaPlayerMenu mediaPlayerMenu = null;
	private AlertMenu alertMenu = null;
	private SensorMenu sensorMenu = null;
	private MyScrollView[] _myScrollViews;
	[SerializeField] 
	private GameObject vr_CloseButton;
	[SerializeField] 
	private GameObject ScreenSwitchSceneMode;

	private BasicMenu lastMenu = null;

	private BasicMenu _currentMenu = null;

	public BasicMenu currentMenu
	{
		get
		{
			return _currentMenu;
		}

		set {
			lastMenu = _currentMenu;
			_currentMenu = value;
		}
	}

	private MediaPlayer _mediaPlayer;

	public MediaPlayer mediaPlayer
	{
		get
		{
			return _mediaPlayer;
		}

		set {
			_mediaPlayer = value;
		}
	}


	public MyScrollView[] myScrollViews
	{
		get
		{
			return _myScrollViews;
		}

		set {
			_myScrollViews = value;
		}
	}


	[Header("---- User ----")]

	public User user;
	public FadeInOut fadeInOut;

	[Header("---- Scene Management ----")]
	[SerializeField]
	private AppScene[] scenes;
	[SerializeField]
	private AppScene currentScene;

	public event Action OnLoggedIn;
	public event Action OnLoggedOut;
	public event Action OnGetUserVideo;
	public event Action OnGetFavoriteVideo;
	public event Action<Video> OnDownloadedVideo;

	// This var purpose is to prevent user from spamming Refresh
	public bool Floodgate;
	private bool isGoVR;
	private bool isShowRecenterPanel;

	//Resolution
	public int maxWidth;
	public int maxHeight;

	void Awake()
	{
		instance = this;

		maxWidth = Screen.currentResolution.width;
		maxHeight = Screen.currentResolution.height;

		// Prevent screen jittering at start
		int targetWidth = MainAllController.instance.maxWidth / 2;
		int targetHeight = MainAllController.instance.maxHeight / 2;

		if (targetHeight > 1000) {
			//Screen.SetResolution (targetWidth,targetHeight,false);
		}
	}

	private void Start()
	{
		Init ();

		// Event open,close Menu
		if (accessMenu != null && storageMenu != null) {
			accessMenu.OnMyStorage += AccessMenu_OnMyStorage;
		} else {
			Debug.LogError ("Null");
		}

		if (accessMenu != null && settingsMenu != null) {
			accessMenu.OnSettings += AccessMenu_OnSettings;
		}else {
			Debug.LogError ("Null");
		}

		if (accessMenu != null && loginMenu != null) {
			accessMenu.OnLogin += AccessMenu_OnLogin;
		}else {
			Debug.LogError ("Null");
		}

		if (accessMenu != null && loginMenu != null) {
			accessMenu.OnLogout += AccessMenu_OnLogout;
		}else {
			Debug.LogError ("Null");
		}

		if (accessMenu != null && userVideoMenu != null) {
			accessMenu.OnMyVideo += AccessMenu_OnMyVideo;
		}else {
			Debug.LogError ("Null");
		}

		if (accessMenu != null) {
			accessMenu.OnClose += AccessMenu_OnClose;
		}else {
			Debug.LogError ("Null");
		}

		if (accessMenu != null && favoriteMenu != null) {
			accessMenu.OnFavorite += AccessMenu_OnFavoriteMenu;
		}else {
			Debug.LogError ("Null");
		}

		if (accessMenu != null && downloadMenu != null) {
			accessMenu.OnDownload += AccessMenu_OnDownloadMenu;
		}else {
			Debug.LogError ("Null");
		}

		if (accessMenu != null && downloadMenu != null) {
			accessMenu.OnInbox += AccessMenu_OnInboxMenu;
		}else {
			Debug.LogError ("Null");
		}

		if (storageMenu != null && accessMenu != null){
			storageMenu.OnBack += storageMenu_OnAccessMenu;
			storageMenu.OnVR += storageMenu_OnVRPlayerMenu;
		}else {
			Debug.LogError ("Null");
		}

		if (settingsMenu != null && accessMenu != null){
			settingsMenu.OnBack += settingsMenu_OnAccessMenu;
		}else {
			Debug.LogError ("Null");
		}

		if (loginMenu != null && accessMenu != null){
			loginMenu.OnBack += loginMenu_OnMyStorage;
		}else {
			Debug.LogError ("Null");
		}

		if (userVideoMenu != null && accessMenu != null){
			userVideoMenu.OnBack += myVideoMenu_OnAccessMenu;
			userVideoMenu.OnVR += userVideoMenu_OnVRPlayerMenu;
		}else {
			Debug.LogError ("Null");
		}

		if (favoriteMenu != null && accessMenu != null){
			favoriteMenu.OnBack += favoriteMenu_OnAccessMenu;
			favoriteMenu.OnVR += favoriteMenu_OnVRPlayerMenu;
		}else {
			Debug.LogError ("Null");
		}

		if (downloadMenu != null && accessMenu != null){
			downloadMenu.OnBack += downloadMenu_OnAccessMenu;
			downloadMenu.OnVR += downloadMenu_OnVRPlayerMenu;
		}else {
			Debug.LogError ("Null");
		}

		if (userDetailMenu != null && userVideoMenu != null){
			userDetailMenu.OnBack += userDetailMenu_OnUserVideoMenu;
			userDetailMenu.OnVR += userDetailMenu_OnVRPlayerMenu;
		}else {
			Debug.LogError ("Null");
		}

		if (accessMenu != null && inboxMenu != null){
			inboxMenu.OnBack += inboxMenu_OnAccessMenu;
			inboxMenu.OnVR += inboxMenu_OnVRPlayerMenu;
		}else {
			Debug.LogError ("Null");
		}

		if (accessMenu != null && settingsMenu != null) {
			settingsMenu.OnLogout += AccessMenu_OnLogout;
		}else {
			Debug.LogError ("Null");
		}

		if (vrPlayerMenu != null) {
			vrPlayerMenu.OnVR += VRPlayerMenu_OnVRPlayer;
			vrPlayerMenu.OnBack += VRPlayerMenu_OnBack;
		}else {
			Debug.LogError ("vrPlayerMenu Null");
		}

		if (mediaPlayerMenu != null) {
			mediaPlayerMenu.OnBack += MediaPlayerMenu_OnBack;
		}else {
			Debug.LogError ("mediaPlayerMenu Null");
		}

		if (sensorMenu != null) {
			sensorMenu.OnBack += SensorMenu_OnClose;
			sensorMenu.OnVR += SensorMenuMenu_OnSkip;
		}else {
			Debug.LogError ("sensorMenu Null");
		}
		// Event open,close Menu

		// Event click Button
		if (loginMenu != null) {
			loginMenu.OnLogin += LoginMenu_OnLogin;
		}else {
			Debug.LogError ("loginMenu Null");
		}

		if (settingsMenu != null) {
			settingsMenu.OnLogin += AccessMenu_OnLogin;
		}else {
			Debug.LogError ("settingsMenu Null");
		}

		if (vrPlayerMenu != null) {
			vrPlayerMenu.OnRunVRPlayer += VRPlayerMenu_OnRunVRPlayer;
		}else {
			Debug.LogError ("vrPlayerMenu Null");
		}
		// Event click Button

		//LoadAllData ();

	}

	private void Update (){

//		print ("RUNNINGGGGG");
		
		Application_BackButton ();



	}

	private void Init()
	{
		user = null;

		// Load all references
		accessMenu = UnityEngine.Object.FindObjectOfType<AccessMenu>();
		storageMenu = UnityEngine.Object.FindObjectOfType<StorageMenu>();
		settingsMenu = UnityEngine.Object.FindObjectOfType<SettingsMenu>();
		loginMenu = UnityEngine.Object.FindObjectOfType<LoginMenu>();
		userVideoMenu = UnityEngine.Object.FindObjectOfType<UserVideoMenu>();
		favoriteMenu = UnityEngine.Object.FindObjectOfType<FavoriteVideoMenu>();
		downloadMenu = UnityEngine.Object.FindObjectOfType<DownloadMenu>();
		userDetailMenu = UnityEngine.Object.FindObjectOfType<UserDetailMenu>();
		inboxMenu = UnityEngine.Object.FindObjectOfType<InboxMenu>();
		vrPlayerMenu = UnityEngine.Object.FindObjectOfType<VRPlayerMenu>();
		mediaPlayerMenu = UnityEngine.Object.FindObjectOfType<MediaPlayerMenu>();
		alertMenu = UnityEngine.Object.FindObjectOfType<AlertMenu>();
		scenes = UnityEngine.Object.FindObjectsOfType<AppScene> ();
		_mediaPlayer = UnityEngine.Object.FindObjectOfType<MediaPlayer> ();
		sensorMenu = UnityEngine.Object.FindObjectOfType<SensorMenu> ();
		_myScrollViews = UnityEngine.Object.FindObjectsOfType<MyScrollView> ();

		GoToScene2D ();

		// Start state
		if (storageMenu != null){
			storageMenu.SetActive(true);
			currentMenu = storageMenu;
		}

		if (accessMenu != null){
			accessMenu.SetActive(true);
		}

		// If user has logged out, return user to default tab
		OnLoggedIn += UpdateUserVideo;
		OnLoggedIn += UpdateFavorite;
		OnLoggedIn += LoginCompleted;
		OnLoggedOut += AccessMenu_OnMyStorage;
	}


	private void LoginCompleted(){
		
		if (currentMenu is LoginMenu || currentMenu is StorageMenu){
			accessMenu.Close ();
			if (accessMenu.checkerMyStorage != null){
				accessMenu.CheckerViewable (accessMenu.checkerMyStorage,true);
			}

			if (!(currentMenu is StorageMenu)) {
				currentMenu.SetActive (false);
				storageMenu.SetActive(true);
				currentMenu = storageMenu;
			}

			Viewable_Login();

			if (alertMenu != null){
				alertMenu.LoginAlert();
			}
		}

		// When login in sceneVR
		if (isGoVR && currentScene is Scene2D){
			OpenStorageMenuVR();
		}

		if (ScreenLoading.instance != null) {
			ScreenLoading.instance.Stop ();
		}

		if (VR_MainMenu.instance != null) {
			VR_MainMenu.instance.HideLoadingUI ();
		}
	}

	#region Get Videos
	/// <summary>
	/// Server does not provide a way to get total size of a video, so additional requests
	/// have to be made to get total size based on Content-Length header
	/// Request are made for each user video.
	/// </summary>
	public void UpdateUserVideo()
	{
		if (currentScene is Scene2D){
			if (ScreenLoading.instance != null) {
				ScreenLoading.instance.Play ();
			}
		}

//		if (currentScene is SceneVR) {
//			if (VR_MainMenu.instance != null) {
//				VR_MainMenu.instance.ShowLoadingUI ();
//			}
//		}
			
		Networking.instance.StopThread ();
		Networking.instance.GetUserVideoRequest (user.token, OnGetUserVideoList, OnErrorGetUserVideo);
		Debug.Log ("UpdatING UserVideo.....");

	}

	/// <summary>
	/// Same thing happens for favorite videos.
	/// </summary>
	public void UpdateFavorite()
	{
		if (currentScene is Scene2D){
			if (ScreenLoading.instance != null) {
				ScreenLoading.instance.Play ();
			}
		}

//		if (currentScene is SceneVR) {
//			if (VR_MainMenu.instance != null) {
//				VR_MainMenu.instance.ShowLoadingUI ();
//			}
//		}

		Networking.instance.StopThread ();
		Networking.instance.GetFavoriteVideoRequest (user.token, OnGetFavoriteVideoList, OnErrorFavoriteVideo);
		Debug.Log ("UpdatING FavoriteVideos.....");

	}
		

	/// <summary>
	/// Update without consulting server
	/// </summary>
	public void FastUpdateFavorite()
	{
		if (user != null) {

			if(OnGetFavoriteVideo != null)
			{
				OnGetFavoriteVideo();
			}

		} else {
			Debug.Log ("User Not Login!");
		}
	}

	void OnGetUserVideoList(Video_Info[] videoList)
	{
		try
		{
			if (currentScene is Scene2D){
				if (ScreenLoading.instance != null) {
					ScreenLoading.instance.Play ();
				}
			}

//			if (currentScene is SceneVR) {
//				if (VR_MainMenu.instance != null) {
//					VR_MainMenu.instance.ShowLoadingUI ();
//				}
//			}

			// Clear user videos list
			user.userVideos.Clear ();

			// Create new array of user video object
			for (int i = 0; i < videoList.Length; i++) {

                // Placeholder
                if(videoList[i].size == 0)
                {
                    videoList[i].size = 76500421;
                }

				UserVideo userVideo = new UserVideo (videoList[i]);
                print(videoList[i].size); 

                user.userVideos.Add (userVideo);
			}

			Debug.LogError ("Updated UserVideos completed!!!!!!!!");

			if (OnGetUserVideo != null)
				OnGetUserVideo ();
		} 

		catch(Exception e) 
		{

		} finally {
			if (ScreenLoading.instance != null) {
				ScreenLoading.instance.Stop ();
			}

			if (VR_MainMenu.instance != null) {
				VR_MainMenu.instance.HideLoadingUI ();
			}
        }

	}

	void OnGetFavoriteVideoList(Video_Info[] videoList)
	{
		try
		{
			if (currentScene is Scene2D){
				if (ScreenLoading.instance != null) {
					ScreenLoading.instance.Play ();
				}
			}

//			if (currentScene is SceneVR) {
//				if (VR_MainMenu.instance != null) {
//					VR_MainMenu.instance.ShowLoadingUI ();
//				}
//			}
			// Clear favorite videos list
			user.favoriteVideos.Clear ();

			// Create new array of favorite video object
			for (int i = 0; i < videoList.Length; i++) {

                // Placeholder
                if (videoList[i].size == 0)
                {
                    videoList[i].size = 76500421;
                }

                FavoriteVideo favoriteVideo = new FavoriteVideo (videoList[i]);
				user.favoriteVideos.Add (favoriteVideo);
			}

			Debug.LogError ("Updated FavoriteVideos completed!!!!!!!!");

			if (OnGetFavoriteVideo != null)
				OnGetFavoriteVideo ();
		} 

		catch(Exception e) 
		{

		} finally {
			if (ScreenLoading.instance != null) {
				ScreenLoading.instance.Stop ();
			}

			if (VR_MainMenu.instance != null) {
				VR_MainMenu.instance.HideLoadingUI ();
			}
        }
	}


	void OnErrorGetUserVideo()
	{
		//NativeUI.AlertPopup alert = NativeUI.Alert("Notification!", "Login isn't correct!");

		Debug.LogError ("+++++++++++++++++++++++++++++ERROR");

		if (ScreenLoading.instance != null) {
			ScreenLoading.instance.Stop ();
		}

		if (VR_MainMenu.instance != null) {
			VR_MainMenu.instance.HideLoadingUI ();
		}

		if (userVideoMenu != null){
			userVideoMenu.UpdateNetworkConnectionUI ();
		}

	}

	void OnErrorFavoriteVideo()
	{

		Debug.LogError ("+++++++++++++++++++++++++++++ERROR");

		if (ScreenLoading.instance != null) {
			ScreenLoading.instance.Stop ();
		}

		if (VR_MainMenu.instance != null) {
			VR_MainMenu.instance.HideLoadingUI ();
		}

		if (favoriteMenu != null){
			favoriteMenu.UpdateNetworkConnectionUI ();
		}

	}
		
	#endregion

	#region AccessMenu	
	private void AccessMenu_OnMyStorage()
	{
		accessMenu.Close ();

		if (accessMenu.checkerMyStorage != null){
			accessMenu.CheckerViewable (accessMenu.checkerMyStorage,true);
		}

		if (!(currentMenu is StorageMenu)) {
			currentMenu.SetActive (false);
			storageMenu.SetActive(true);
			currentMenu = storageMenu;

			storageMenu.FastRefresh ();
		}

		if (isGoVR && currentScene is Scene2D){
			OpenStorageMenuVR ();
		}
	}

	private void AccessMenu_OnSettings()
	{
		accessMenu.Close ();

		if (accessMenu.checkerSettings != null){
			accessMenu.CheckerViewable (accessMenu.checkerSettings,true);
		}

		if (!(currentMenu is SettingsMenu)) {
			currentMenu.SetActive (false);
			settingsMenu.SetActive(true);
			settingsMenu.InitViewable ();
			settingsMenu.DisableNetworkAlert ();
			currentMenu = settingsMenu;
		}
	}

	public void OpenLoginMenuFromVR()
	{
		if (!(currentScene is Scene2D)){
			GoToScene2D();
		}
		AccessMenu_OnLogin ();
		isGoVR = true;
	}

	private void AccessMenu_OnLogin()
	{
		accessMenu.Close ();

		if (!(currentMenu is LoginMenu)) {
			currentMenu.SetActive (false);
			loginMenu.SetActive(true);
			loginMenu.InitViewable ();
			loginMenu.UpdateNetworkConnectionUI ();
			currentMenu = loginMenu;
		}
	}

	private void AccessMenu_OnLogout()
	{
		loginMenu.Logout ();
	}

	public void SceneVR_OnLogout()
	{
		loginMenu.Logout ();
	}

	private void AccessMenu_OnMyVideo()
	{
		accessMenu.Close ();

		if (HasUserLoggedIn ()) {
			
			if (accessMenu.checkerMyVideo != null) {
				accessMenu.CheckerViewable (accessMenu.checkerMyVideo, true);
			}

			if (!(currentMenu is UserVideoMenu)) {
				currentMenu.SetActive (false);
				userVideoMenu.SetActive (true);
				currentMenu = userVideoMenu;

				userVideoMenu.Init ();
			}
		} else {
			GetAlertLoggin ();
		}
	}

	private void AccessMenu_OnFavoriteMenu()
	{
		accessMenu.Close ();
	
		if (HasUserLoggedIn()) {
			if (accessMenu.checkerFavorite != null){
				accessMenu.CheckerViewable (accessMenu.checkerFavorite,true);
			}

			if (!(currentMenu is FavoriteVideoMenu)) {
				currentMenu.SetActive (false);
				favoriteMenu.SetActive (true);
				currentMenu = favoriteMenu;
		
				favoriteMenu.Init();
			}
		}else {
			GetAlertLoggin ();
		}
	}

	public void GoToDownloadMenu()
	{
		AccessMenu_OnDownloadMenu ();
	}

	private void AccessMenu_OnDownloadMenu()
	{
		accessMenu.Close ();

		if (HasUserLoggedIn()) {
			if (accessMenu.checkerDownload != null){
				accessMenu.CheckerViewable (accessMenu.checkerDownload,true);
			}

			if (!(currentMenu is DownloadMenu)) {
				currentMenu.SetActive (false);
				downloadMenu.SetActive (true);
				currentMenu = downloadMenu;

				downloadMenu.Init ();
			}
		}else {
			GetAlertLoggin ();
		}
	}

	public void GoToInbox()
	{
		AccessMenu_OnInboxMenu ();
	}

	public void GoToFavoriteMenu()
	{
		AccessMenu_OnFavoriteMenu ();
	}

	public void GoToUserMenu()
	{
		AccessMenu_OnMyVideo ();
	}

	private void AccessMenu_OnInboxMenu()
	{
		accessMenu.Close ();

		if (HasUserLoggedIn()) {
			if (accessMenu.checkerInbox != null){
				accessMenu.CheckerViewable (accessMenu.checkerInbox,true);
			}

			if (!(currentMenu is InboxMenu)) {
				currentMenu.SetActive (false);
				inboxMenu.SetActive (true);
				currentMenu = inboxMenu;

				inboxMenu.Init ();
			}
		}else {
			GetAlertLoggin ();
		}
	}

	private void AccessMenu_OnClose()
	{
		accessMenu.Close ();
	}

	#endregion


	#region UserVideoMenu	
	public void UserVideo_OnUserVideoDetail()
	{
		accessMenu.Close ();

		if (currentMenu != userDetailMenu) {
			currentMenu.SetActive (false);
			userDetailMenu.SetActive (true);
			userDetailMenu.UpdateNetworkConnectionUI ();
			currentMenu = userDetailMenu;
		}
	}

	public void ReturnToLastMenuFromUserDetail()
	{
		userDetailMenu_OnUserVideoMenu ();
	}

	private void userDetailMenu_OnUserVideoMenu()
	{
		// Return to respective last accessed menu
		if (lastMenu is UserVideoMenu) {
			AccessMenu_OnMyVideo ();
			return;
		}

		if (lastMenu is FavoriteVideoMenu) {
			AccessMenu_OnFavoriteMenu ();
			return;
		}

		if (lastMenu is DownloadMenu) {
			AccessMenu_OnDownloadMenu ();
			return;
		}

		if (lastMenu is InboxMenu) {
			AccessMenu_OnInboxMenu ();
			return;
		}

		//If not, at least return to to something, better than getting stuck
		AccessMenu_OnMyVideo ();
	}

	private void userDetailMenu_OnVRPlayerMenu()
	{
		GoVRPplayerMenu ();
	}

	private void myVideoMenu_OnAccessMenu()
	{
		accessMenu.Open ();
	}

	private void userVideoMenu_OnVRPlayerMenu()
	{
		GoVRPplayerMenu ();
	}
	#endregion

	#region SettingMenu
	private void settingsMenu_OnAccessMenu()
	{
		accessMenu.Open();
	}
	#endregion

	#region storageMenu
	private void storageMenu_OnAccessMenu()
	{
		accessMenu.Open ();
	}

	private void storageMenu_OnVRPlayerMenu()
	{
		GoVRPplayerMenu ();
	}
		
	#endregion

	#region login/LogoutMenu
	private void loginMenu_OnMyStorage()
	{
		AccessMenu_OnMyStorage ();
	}

	private void LoginMenu_OnLogin()
	{
		loginMenu.Login ();
	}
		
	#endregion

	#region FavoriteMenu
	private void favoriteMenu_OnAccessMenu()
	{
		accessMenu.Open ();
	}

	private void favoriteMenu_OnVRPlayerMenu()
	{
		GoVRPplayerMenu ();
	}

	public void InitFavoriteMenu(){
		if (favoriteMenu != null){
			favoriteMenu.Init ();
		}
	}
	#endregion

	#region DownloadMenu
	private void downloadMenu_OnAccessMenu()
	{
		accessMenu.Open ();
	}

	private void downloadMenu_OnVRPlayerMenu()
	{
		GoVRPplayerMenu ();
	}

	public void InitDownloadMenu(){
		if (downloadMenu != null){
			downloadMenu.Init ();
		}
	}
	#endregion


	#region InboxMenu
	private void inboxMenu_OnAccessMenu()
	{
		accessMenu.Open ();
	}
	private void inboxMenu_OnVRPlayerMenu()
	{
		GoVRPplayerMenu ();
	}

	public void InitInboxVideoMenu2D(){
		if (inboxMenu != null){
			inboxMenu.Init ();
		}
	}

	#endregion

	#region VRPlayerMenu
	private void VRPlayerMenu_OnVRPlayer()
	{
		vrPlayerMenu.SetActive (false);
		vrPlayerMenu.IsShowVRPlayer = false;
	}

	private void VRPlayerMenu_OnBack()
	{
		vrPlayerMenu.SetActive (false);
		vrPlayerMenu.IsShowVRPlayer = false;
		currentMenu.SetActive (true);
	}

	private void GoVRPplayerMenu(){
		if (!(currentMenu is VRPlayerMenu)) {
            //currentMenu.SetActive (false);
        	vrPlayerMenu.SetActive (true);
			vrPlayerMenu.IsShowVRPlayer = true;
        }
	}

	private void VRPlayerMenu_OnRunVRPlayer(){
		vrPlayerMenu.SetActive (false);
		vrPlayerMenu.IsShowVRPlayer = false;

		if (isSensorNotComplete) {
			GoToSensorMenu ();
		} else {
			if (!(currentScene is SceneVR)){
				GoToSceneVR ();
			}	
		}
	}

	#endregion

	#region MediaPlayerMenu
	public void ModeVR_OnMediaPlayerMenu()
	{
		Play3D_2D ();

		accessMenu.Close ();

		if (!(currentMenu is MediaPlayerMenu)) {
			currentMenu.SetActive (false);
			mediaPlayerMenu.SetActive (true);
			accessMenu.SetHandleViewable (false);
			currentMenu = mediaPlayerMenu;
		}
	}

	private void MediaPlayerMenu_OnBack(){
		if (!(currentScene is Scene2D)){
			GoToScene2D();
		}
		currentMenu.SetActive (false);
		lastMenu.SetActive (true);
		currentMenu = lastMenu;
		accessMenu.SetHandleViewable (true);
		mediaPlayerMenu.CloseVideo ();
	}

	#endregion

	#region SensorMenu
	public bool isSensorNotComplete;
	public void GoToSensorMenu()
	{
		if (!(currentScene is SceneSensor)){
			GoTo2DSceneSensor ();
		}

		accessMenu.Close ();

		if (!(currentMenu is SensorMenu)) {
			sensorMenu.SetActive (true);
		}
	}

	private void SensorMenu_OnClose(){
		if (!(currentScene is Scene2D)){
			GoToScene2D();
		}

		isSensorNotComplete = true;
	}

	public void SensorMenuMenu_OnSkip(){
		
		VR_Recenterer.instance.Recenter ();

		if (!(currentScene is SceneVR)){
			GoToSceneVR ();
		}

		isSensorNotComplete = false;
		sensorMenu.SensorMenuInit ();
	}
		
	#endregion


	public void LoginUser (string username, string token)
	{
		user = new User (username, token);

		if (OnLoggedIn != null)
			OnLoggedIn ();
	}

	public void LogoutUser ()
	{
		try
		{
			if (currentMenu is LoginMenu) {
				currentMenu.SetActive (false);
				storageMenu.SetActive(true);
				currentMenu = storageMenu;
			}

			if (MainAllController.instance != null){
				if (user != null){
					MainAllController.instance.user.ClearAllInfo ();
				}
			}

			if (Networking.instance != null){
				Networking.instance.DeleteAllThread();
			}

			// clear usernameText & passwordText
			loginMenu.ClearAllInfo();

			Viewable_Logout();

			if (alertMenu != null){
				alertMenu.LogoutAlert();
			}

			if (OnLoggedOut != null)
				OnLoggedOut ();

			// When logout in sceneVR
			if (currentScene is SceneVR) {
				(currentScene as SceneVR).ClickLogoutBnt ();
			}
			// When logout in sceneVR
		}
		catch (System.Exception e)
		{
			Debug.LogError ("LogoutUser Exception " + e.Message);

		} finally {

			if (ScreenLoading.instance != null) {
				ScreenLoading.instance.Stop ();
			}

			if (VR_MainMenu.instance != null) {
				VR_MainMenu.instance.HideLoadingUI ();
			}

		}

	}

	public bool HasUserLoggedIn()
	{
		if (user == null)
			return false;
		if (user.username == String.Empty || user.token == String.Empty)
			return false;
		return true;
	}

	#region NativeUI AlertPopup	
	/// <summary>
	/// Gets the alert when not loggin.
	/// </summary>
	public void GetAlertLoggin(){
		AndroidDialog.instance.showLoginDialog ("Please try again after logging in!", OnAlertLogginComplete);
		//AndroidDialog.OnConfirm += OnAlertLogginComplete ;
//		NativeUI.AlertPopup alert = NativeUI.ShowTwoButtonAlert("Notification!", "Please try again after logging in.", "CANCEL", "CONFIRM");
//		if (alert != null)
//			alert.OnComplete += OnAlertLogginComplete;
	}

	void OnAlertLogginComplete(){
		AccessMenu_OnLogin ();
	//	AndroidDialog.OnConfirm -= OnAlertLogginComplete;
	}

	#endregion


	#region Mode Changing	

	public void Open2D()
	{
		if (!(currentScene is Scene2D)){
			GoToScene2D();
		}
	}

	public void OpenStorageMenuVR()
	{
		if (!(currentScene is SceneVR)) {
			GoToSceneVR ();
			if (currentScene is SceneVR) {
				(currentScene as SceneVR).ShowStorageMenu ();
			}
		} else {
			(currentScene as SceneVR).ShowStorageMenu ();
		}
	}


	public void Play3D(Video video)
	{
		this.video = video;
		isStreaming = false;

		if (currentScene is SceneVR) {
			(currentScene as SceneVR).PlayFromURL (video);
		} else {
			GoToSceneVR ();
			if (currentScene is SceneVR) {
				(currentScene as SceneVR).PlayFromURL (video);
			}
		}
	}

	public void Play2D(Video video)
	{
		this.video = video;
		isStreaming = false;

		ModeVR_OnMediaPlayerMenu ();
	}

	Video video;
	Video videoStreaming;
	VRPlayer vrPlayer;
	bool isStreaming;
	string urlStreaming;

	private void Play3D_2D()
	{
        GoTo2DMediaPlayer();

        if (isStreaming) {
			mediaPlayerMenu.Streaming (videoStreaming, urlStreaming);
		} else {
			mediaPlayerMenu.Play (video, this.vrPlayer);
		}
	}

	public void Play2D_3D()
	{
		mediaPlayerMenu.CloseVideo ();

		if (currentScene is SceneMediaPlayer) {
			currentMenu.SetActive (false);
			lastMenu.SetActive (true);
			currentMenu = lastMenu;
			accessMenu.SetHandleViewable (true);
			PlayButtonSound ();
		}

		if (isStreaming) {
			Streaming3D (videoStreaming,urlStreaming);
		} else {
			mediaPlayerMenu.Resume ();
			Play3D (video);
		}
	}

	public void LoadSubTitleVR(string url)
	{
		if (currentScene is SceneVR) {
			(currentScene as SceneVR).LoadSubTitleVR (url);
		}
	}


	public void DisableSubtitleVR()
	{
		if (currentScene is SceneVR) {
			(currentScene as SceneVR).DisableSubtitleVR ();
		}
	}

	public void LoadSubTitle2D(string url)
	{
		mediaPlayerMenu.LoadSubTitles (url);
	}

	public void DisableSubTitle2D()
	{
		mediaPlayerMenu.DisableSubtitles ();
	}

	public void Streaming3D(Video video,string url)
	{
		isStreaming = true;
		this.urlStreaming = url;
		this.videoStreaming = video;

		if (!(currentScene is SceneVR)){
			GoToSceneVR ();
		}
			
		if (currentScene is SceneVR) {
			(currentScene as SceneVR).Streaming (video,url);
		}
	}

	public void Streaming2D(Video video,string url)
	{
		isStreaming = true;
		this.urlStreaming = url;
		this.videoStreaming = video;

		ModeVR_OnMediaPlayerMenu ();
	}


	void GoToSceneVR()
	{
		isGoVR = true;

		foreach (AppScene scene in scenes) {
			if (scene is SceneVR) {

				scene.Show (currentMenu);

				currentScene = scene;


			} else {
				scene.Hide ();
			}
		}
	}

	void GoToScene2D()
	{
		isGoVR = false;
		IsShowRecenterPanel = false;
		HideVR_CloseButton ();

		if (ScreenLoading.instance != null) {
			ScreenLoading.instance.Stop ();
		}

		foreach (AppScene scene in scenes) {
			if (scene is Scene2D) {

				scene.Show (currentMenu);

				currentScene = scene;

				if (currentMenu != null) {
					currentMenu.SetActive (true);
					currentMenu.Init ();
				}

			} else {
				scene.Hide ();
			}
		}
	}

    void GoTo2DMediaPlayer()
    {
		IsShowRecenterPanel = false;
		HideVR_CloseButton ();
		HideScreenSwitchSceneMode ();

		if (ScreenLoading.instance != null) {
			ScreenLoading.instance.Stop ();
		}

        foreach (AppScene scene in scenes)
        {
            if (scene is SceneMediaPlayer)
            {

                scene.Show(currentMenu);

                currentScene = scene;

                if (currentMenu != null)
                    currentMenu.Init();
            }
            else
            {
                scene.Hide();
            }
        }
    }

	void GoTo2DSceneSensor()
	{
		IsShowRecenterPanel = false;
		HideVR_CloseButton ();
		HideScreenSwitchSceneMode ();

		if (ScreenLoading.instance != null) {
			ScreenLoading.instance.Stop ();
		}
			
		foreach (AppScene scene in scenes)
		{
			if (scene is SceneSensor)
			{

				scene.Show(currentMenu);

				currentScene = scene;

				if (currentMenu != null)
					currentMenu.Init();
			}
			else
			{
				scene.Hide();
			}
		}
	}

    #endregion


    #region UserName Data

    public void SubmitNewUserName(string username)
	{
		if (loginMenu != null) {
			loginMenu.SetUsernameInput (username);
			SaveUserNameProgress ();
		}
	}

	public string GetCurrentUserName ()
	{
		if (loginMenu != null) {
			return loginMenu.GetUsernameInput ();
		}

		return null;
	}

	public void LoadUserNameProgress ()
	{
		if (PlayerPrefs.HasKey ("Username")) {
			if (loginMenu != null) {
				string username = GetCurrentUserName ();
				username = PlayerPrefs.GetString ("Username");
				loginMenu.SetUsernameInput (username);
			}
		}
	}

	private void SaveUserNameProgress ()
	{
		if (loginMenu != null) {
			PlayerPrefs.SetString ("Username", loginMenu.GetUsernameInput ());
		}
	}

	#endregion


	#region Password Data
	public void SubmitNewPassword(string password)
	{
		if (loginMenu != null){
			loginMenu.SetPasswordInput(password);
			SavePasswordProgress ();
		}
	}

	public string GetCurrentPassword ()
	{
		if (loginMenu != null) {
			return loginMenu.GetPasswordInput ();
		}

		return null;
	}

	public void LoadPasswordProgress ()
	{
		if (PlayerPrefs.HasKey ("Password")) {
			string password = GetCurrentPassword ();
			password = PlayerPrefs.GetString ("Password");
			loginMenu.SetPasswordInput (password);
		}
	}

	private void SavePasswordProgress ()
	{
		PlayerPrefs.SetString ("Password", loginMenu.GetPasswordInput());
	}

	#endregion


	#region KeepLoggin Data

	public void SubmitKeepLogin(string visible)
	{
		if (settingsMenu != null) {
			settingsMenu.SetKeepLoginText (visible);
		}
		SaveKeepLoginProgress ();
	}

	public string GetCurrentKeepLogin ()
	{
		if (settingsMenu != null){
			return settingsMenu.GetKeepLoginText();
		}

		return null;
	}

	public void LoadKeepLoginProgress ()
	{
		if (PlayerPrefs.HasKey ("KeepLogin")) {
			if (settingsMenu != null) {
				string keepLoginText = GetCurrentKeepLogin ();
				keepLoginText = PlayerPrefs.GetString ("KeepLogin");
				settingsMenu.SetKeepLoginText (keepLoginText);
			}
		}
	}

	private void SaveKeepLoginProgress ()
	{
		if (settingsMenu != null) {
			PlayerPrefs.SetString ("KeepLogin", settingsMenu.GetKeepLoginText ());
		}
	}

	#endregion


	#region Notification Data

	public void SubmitNotification(string visible)
	{
		if (settingsMenu != null) {
			settingsMenu.SetNotificationText (visible);
		}
		SaveNotificationProgress ();
	}

	public string GetCurrentNotification ()
	{
		if (settingsMenu != null){
			return settingsMenu.GetNotificationText();
		}

		return null;
	}

	public void LoadNotificationProgress ()
	{
		if (PlayerPrefs.HasKey ("Notification")) {
			if (settingsMenu != null) {
				string notificationText = GetCurrentNotification ();
				notificationText = PlayerPrefs.GetString ("Notification");
				settingsMenu.SetNotificationText (notificationText);
			}
		}
	}

	private void SaveNotificationProgress ()
	{
		if (settingsMenu != null) {
			PlayerPrefs.SetString ("Notification", settingsMenu.GetNotificationText ());
		}
	}

	#endregion

	#region MobieNetwork Data

	public void SubmitMobieNetwork(string visible)
	{
		if (settingsMenu != null) {
			settingsMenu.SetMobieNetworkText (visible);
		}
		SaveMobieNetworkProgress ();
	}

	public string GetCurrentMobieNetwork ()
	{
		if (settingsMenu != null){
			return settingsMenu.GetMobieNetworkText();
		}

		return null;
	}

	public void LoadMobieNetworkProgress ()
	{
		if (PlayerPrefs.HasKey ("MobieNetwork")) {
			if (settingsMenu != null) {
				string mobieNetworkText = GetCurrentMobieNetwork ();
				mobieNetworkText = PlayerPrefs.GetString ("MobieNetwork");
				settingsMenu.SetMobieNetworkText (mobieNetworkText);
			}
		}
	}

	private void SaveMobieNetworkProgress ()
	{
		if (settingsMenu != null) {
			PlayerPrefs.SetString ("MobieNetwork", settingsMenu.GetMobieNetworkText ());
		}
	}

	#endregion


	#region ViewableButton

	private void Viewable_Login(){

		if (loginMenu.AutoLoginEnable()){
			settingsMenu.SetKeepLoginText (settingsMenu.GetkeyTrueKeepLogin());
		}else{
			settingsMenu.SetKeepLoginText (settingsMenu.GetkeyFalseKeepLogin());
		}

		string username = loginMenu.GetUsernameInput ();
		settingsMenu.Setusername (username);
	}

	private void Viewable_Logout(){

		if(settingsMenu != null){
			// Viewable button
			settingsMenu.KeepLogginViewable (false);
			// Viewable button

			// Username
			string username = loginMenu.GetUsernameInput ();
			settingsMenu.Setusername (username);
		}
	}

	#endregion


	#region SubmitAllData

	private void SubmitAllData(){
		SubmitNewUserName (loginMenu.GetUsernameInput());
		SubmitNewPassword (loginMenu.GetPasswordInput());
		SubmitKeepLogin(settingsMenu.GetKeepLoginText());
		SubmitNotification(settingsMenu.GetNotificationText());
		SubmitMobieNetwork (settingsMenu.GetMobieNetworkText());
	}

    #endregion


    #region LoadAllData

    public void LoadAllData(){
		LoadKeepLoginProgress ();
		LoadUserNameProgress ();
		LoadPasswordProgress ();
		LoadNotificationProgress ();
		LoadMobieNetworkProgress ();

		if (GetCurrentKeepLogin () == settingsMenu.GetkeyTrueKeepLogin()) {
			loginMenu.Login ();
		} else {
			loginMenu.ClearAllInfo ();
		}
	}

	#endregion


	public void Downloaded(Video video)
	{
		if(OnDownloadedVideo != null)
		OnDownloadedVideo (video);
	}


	#region ApplicationQuit

	private void OnApplicationQuit()
	{
		SubmitAllData ();
		Debug.Log("Application ending.........................................................");
	}

	private void Application_BackButton (){
		if (currentScene is Scene2D) {
			if (Input.GetKeyDown (KeyCode.Escape)) {

				if (ScreenLoading.instance != null) {
					ScreenLoading.instance.Stop ();
				}

				if (accessMenu.PanelLayer.gameObject.activeSelf) {
					accessMenu.Close ();
				} else {
					if (vrPlayerMenu.IsShowVRPlayer){
						VRPlayerMenu_OnBack ();
						return;
					}
						
					if (currentMenu is UserDetailMenu){
						userDetailMenu_OnUserVideoMenu ();
						return;
					}

					if (currentMenu is StorageMenu) 
					{
						if (alertMenu != null) {
							alertMenu.ExitAlert ();
						}
						return;
					} 

					if (currentMenu is MediaPlayerMenu) {
						return;
					}

					AccessMenu_OnMyStorage ();
				}
			}
		}
	}


	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			Debug.Log("Application pause.........................................................");
			SubmitAllData ();
		}
		else
		{
			Debug.Log("Application resumed.......................................................");
		}

//		EasyMobile.NativeUI.AlertPopup[] popups = GameObject.FindObjectsOfType<EasyMobile.NativeUI.AlertPopup> ();
//		foreach (EasyMobile.NativeUI.AlertPopup popup in popups) {
//			popup.Release ();
//		}
	}
	bool isPaused = false;
	void OnApplicationFocus(bool hasFocus)
	{
//		EasyMobile.NativeUI.AlertPopup[] popups = GameObject.FindObjectsOfType<EasyMobile.NativeUI.AlertPopup> ();
//		foreach (EasyMobile.NativeUI.AlertPopup popup in popups) {
//	
//			popup.Release ();
//		}
//		Debug.Log("Application resumed......................................................." + hasFocus);


	}

	#endregion


//	#region GetNovideos Info
//
//	public bool CheckNovideos_LocalVideo()
//	{
//		if (storageMenu != null){
//			storageMenu.FastRefresh ();
//			if (storageMenu.isNoVideo) {
//				return true;
//			}
//		}
//
//		return false;
//	}
//
//	public bool CheckNovideos_UserVideo()
//	{
//		if (userVideoMenu != null){
//			userVideoMenu.Init ();
//			if (userVideoMenu.isNoVideo) {
//				return true;
//			}
//		}
//
//		return false;
//	}
//
//	public bool CheckNovideos_FavoriteVideo()
//	{
//		if (favoriteMenu != null) {
//			favoriteMenu.Init ();
//			if (favoriteMenu.isNoVideo) {
//				return true;
//			}
//		}
//
//		return false;
//	}
//
//	public bool CheckNovideos_InboxVideo()
//	{
//		if (inboxMenu != null){
//			inboxMenu.Init ();
//			if (inboxMenu.isNoVideo) {
//				return true;
//			}
//		}
//
//		return false;
//	}
//
//	#endregion

	#region Orientation

//	private bool isLandscape;
//	private bool isPortrait;
//
//	private void CheckOrientation(){
//		if (currentScene is Scene2D){
//			if (currentMenu is MediaPlayerMenu) {
//				if (isLandscape){
//					Screen.orientation = ScreenOrientation.LandscapeLeft;
//					isLandscape = false;
//					Debug.Log ("LandscapeLeft..........................");
//				}
//			} else {
//				if(isPortrait){
//					Screen.orientation = ScreenOrientation.Portrait;
//					isPortrait = false;
//					Debug.Log ("Portrait..............................");
//				}
//
//			}
//		}
//	}
//
//	private void SetLandscapeRotation(){
//		isLandscape = true;
//		isPortrait = false;
//	}
//
//	private void SetPortraitRotation(){
//		isPortrait = true;
//		isLandscape = false;
//	}

	#endregion

	#region PlayButtonSound 

	public void PlayButtonSound()
	{
		SoundManager.Instance.PlaySound(SoundManager.Instance.button);
	}

	#endregion

	/// <summary>
	/// Gets or sets a value indicating whether this instance is show recenter panel.
	/// </summary>
	/// <value><c>true</c> if this instance is show recenter panel; otherwise, <c>false</c>.</value>
	public bool IsShowRecenterPanel
	{
		get { return isShowRecenterPanel; }
		set { isShowRecenterPanel = value; }
	}

	public string GetUserNameInput(){
		string username;

		if (loginMenu != null){
			username = loginMenu.GetUsernameInput ();
			return username;
		}

		return string.Empty;
	}

	public void HideVR_CloseButton(){
		if (vr_CloseButton != null) {
			vr_CloseButton.SetActive (false);
		} else {
			Debug.LogError ("NULL..................");
		}
	}

	public void ShowVR_CloseButton(){
		if (vr_CloseButton != null){
			vr_CloseButton.SetActive (true);
		}else {
			Debug.LogError ("NULL..................");
		}
	}

	public void VR_CloseButton_OnClick(){
		if(currentScene is SceneVR){
			GoToScene2D ();
		}
	}

	public void HideScreenSwitchSceneMode(){
		if (ScreenSwitchSceneMode != null) {
			ScreenSwitchSceneMode.SetActive (false);
		} else {
			Debug.LogError ("NULL..................");
		}
	}

	public void ShowScreenSwitchSceneMode(){
		if (ScreenSwitchSceneMode != null){
			ScreenSwitchSceneMode.SetActive (true);
		}else {
			Debug.LogError ("NULL..................");
		}
	}
}