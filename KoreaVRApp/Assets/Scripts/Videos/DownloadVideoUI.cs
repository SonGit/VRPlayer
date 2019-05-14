using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CielaSpike;
using System.IO;
using EasyMobile;
using System.Net;
using UnityEngine.Networking;
using UnityEditor;
using EasyMobile.Demo;


public enum DownloadState {
	none,
	NotYetDownload,
	Downloading,
	Pause,
	Complete,
	Fail,
	Request_timed_out,
	Cancel
}

public class DownloadVideoUI : VideoUI
{
	[SerializeField] private RawImage video_image = null;
	[SerializeField] private Text video_name = null;
	[SerializeField] private Text video_length = null;
	[SerializeField] private Text video_size = null;
	[SerializeField] private Slider video_DownloadProgressSlider = null;
	[SerializeField] private Text video_DownloadProgressText = null;
	[SerializeField] private Text video_DownloadSpeedText = null;
	[SerializeField] private Button video_Download = null;
	[SerializeField] private Button video_Pause = null;
	[SerializeField] private Button video_Resume = null;
	[SerializeField] private Button video_Cancel = null;
	[SerializeField] private GameObject videoDownloaderPrefab = null;

	private string fullpathfile;
	private string path;
	private FileInfo fileInfo;

	private VideoDownloader videoDownloader;

	// Use this for initialization
	void Start () 
	{
		if (video_Download != null){
			video_Download.onClick.AddListener(() =>
				{
					if(MainAllController.instance != null){
						MainAllController.instance.PlayButtonSound ();
					}
				});
		}

		if (video_Pause != null){
			video_Pause.onClick.AddListener(() =>
				{
					if(MainAllController.instance != null){
						MainAllController.instance.PlayButtonSound ();
					}
				});
		}

		if (video_Cancel != null){
			video_Cancel.onClick.AddListener(() =>
				{
					if(MainAllController.instance != null){
						MainAllController.instance.PlayButtonSound ();
					}
				});
		}

		if (video_Resume != null){
			video_Resume.onClick.AddListener(() =>
				{
					if(MainAllController.instance != null){
						MainAllController.instance.PlayButtonSound ();
					}
				});
		}
	}

	void Update (){
		SetDownloadProgressUI ();
	}
		
	public void Download ()
	{
		ScreenLoading.instance.Play ();

		if (MainAllController.instance != null) {
			string authToken = MainAllController.instance.user.token;
			if (authToken != null){
				Networking.instance.GetVideoLinkRequest (video.videoInfo.id, authToken, OnGetLink,OnFailedGetStreamingLink);
			}
		}
	}

	void OnFailedGetStreamingLink()
	{
		ScreenLoading.instance.Stop ();
	}

	public void Pause()
	{
		Debug.LogError (" ++++Pause()!");

		if (videoDownloader != null) {
			videoDownloader.Pause ();
			SetDownloadStateUI (DownloadState.Pause);
		} else {

		}

	}

	public void Resume()
	{
		ScreenLoading.instance.Play ();
		Debug.LogError (" ++++Resume()!");

		if (MainAllController.instance != null && Networking.instance != null) {
			string authToken = MainAllController.instance.user.token;
			if (authToken != null){
				Networking.instance.GetVideoLinkRequest (video.videoInfo.id, authToken, OnGetLink,OnFailedGetStreamingLink);
			}
		}

	}

	public void Cancel()
	{
		#if UNITY_ANDROID || UNITY_IOS
		GetAlertDelete ();
		#endif

		#if UNITY_EDITOR
		Pause ();

		string path = Path.Combine (MainAllController.instance.user.GetPath(), video.videoInfo.id) ;

		if (Directory.Exists (path)) {
			Directory.Delete (path,true);
		}

		DownloadMenu menu = Object.FindObjectOfType<DownloadMenu> ();
		if (menu != null) {
			menu.RemoveUI (this);
		}
		#endif 
	}


	private void SetDownloadProgressUI (){
		if (video_DownloadProgressSlider != null && video_DownloadProgressText != null) {

			if (videoDownloader != null && videoDownloader.downloadState == DownloadState.Downloading) {
				float progress = (float)videoDownloader.downloadProgress;;
				video_DownloadProgressSlider.value = progress;
				video_DownloadProgressText.text = progress.ToString("0.0") + " %";

				if (videoDownloader.downloadSpeed >= 1024f) {
					video_DownloadSpeedText.text = (videoDownloader.downloadSpeed / 1024f).ToString ("0.0") + "  MB/s";
				} else {
					video_DownloadSpeedText.text = videoDownloader.downloadSpeed.ToString ("0.0") + "  KB/s";
				}

			}

		}
	}

	private void CheckDownloadComplete()
	{
		if (videoDownloader != null) {
			if (videoDownloader.downloadState == DownloadState.Complete || videoDownloader.downloadProgress == 100) {
				DeleteDownloader ();
				SetDownloadStateUI (DownloadState.Complete);

				// Kick event that this video has been downlaoded
				MainAllController.instance.Downloaded (video);

                // Destroy the this object too
                video = null;
                Destroy();
			}
		}
	}

	void DeleteDownloader()
	{
		if (videoDownloader != null) {
			Destroy (videoDownloader.gameObject);
			videoDownloader = null;
		}
	}

	public override void Setup(Video video)
	{
		base.Setup (video);
		video_name.text = video.videoInfo.video_name;
		this.video_length.text = "00:00:00" + " | " +((video.videoInfo.size / 1024) / 1024) + " MB"; ;

		OnEnable ();
	}

	public void OnGetLink(GetLinkVideoResponse getLinkVideoResponse){

		try
		{
			if(!CheckIfEnoughSpace())
			{
				return;
			}

			if (MainAllController.instance != null) {
				path = MainAllController.instance.user.GetPath ();
			}

			string filepath = MainAllController.instance.user.GetPathToFile (video.videoInfo.id,video.videoInfo.video_name);

			// Create a directory that houses the video file
			if (!File.Exists (filepath)) {
				Directory.CreateDirectory(Path.Combine(path,video.videoInfo.id));
			}

			if (videoDownloader != null && videoDownloader.downloadState != DownloadState.Downloading) {

				// If download state is not downloading, kick start download sequence
				videoDownloader.DownLoad (getLinkVideoResponse.link,filepath,OnGetDownloadCallback,OnSuccessDownloadCallback,OnFailDownloadCallback);

			} else {
				
				// THis is to doubly sure that there will be no 2 videoDownloader for a download
				GameObject videoDownloaderObj = GameObject.Find ("VideoDownLoader" + "-" + video.videoInfo.id);

				if (videoDownloaderObj != null) {
					videoDownloader = videoDownloaderObj.GetComponent<VideoDownloader>();
					videoDownloader.DownLoad (getLinkVideoResponse.link,filepath,OnGetDownloadCallback,OnSuccessDownloadCallback,OnFailDownloadCallback);

				}
				else
				{
					// If the file is partially downloaded, but have no downloader, attempt to resume
					videoDownloader = ((GameObject)Instantiate (videoDownloaderPrefab)).GetComponent<VideoDownloader> ();
					videoDownloader.name = "VideoDownLoader" + "-" + video.videoInfo.id;
					videoDownloader.DownLoad (getLinkVideoResponse.link,filepath,OnGetDownloadCallback,OnSuccessDownloadCallback,OnFailDownloadCallback);
				}


			}

			// Set UI state to downloading
			SetDownloadStateUI (DownloadState.Downloading);
		} catch (System.Exception e)
		{

		}
		finally {
			ScreenLoading.instance.Stop ();
		}


	}

	#region API DownloadVideo
	public void OnGetDownloadCallback(float progress, float downloadSpeed){

	}

	public void OnSuccessDownloadCallback()
	{
		Notifications_DownloadCompleted();
		CheckDownloadComplete ();
	}

	public void OnFailDownloadCallback()
	{
		
	}

	public void ErrorGetDownloadCallback(){
		NativeUI.AlertPopup alert = NativeUI.Alert("Check your Connection!", "");
	}
	#endregion


	/// <summary>
	/// If total size is retrieved, calculate progress and set state to Pause
	/// </summary>
	/// <param name="totalSize">Total size.</param>
	void ResumeProgress(long totalSize)
	{
		fullpathfile = MainAllController.instance.user.GetPathToFile(video.videoInfo.id,video.videoInfo.video_name);

		if (File.Exists (fullpathfile)) {
			fileInfo = new FileInfo (fullpathfile);

			float progress = (float)((double)fileInfo.Length * 100 / (double)totalSize);
			video_DownloadProgressSlider.value = progress;
			video_size.text = ((totalSize / 1024) / 1024) + " MB";

			if (progress == 100) {
		
			} else {
				
				if (videoDownloader == null)
					Pause ();

				SetDownloadStateUI (DownloadState.Pause);
			}


		} else {
			SetDownloadStateUI (DownloadState.NotYetDownload);
		}

	}

	void ErrorResumeProgress()
	{
		StopLoadingScreen ();
	}

	#region DownloadStateUI
	private void SetDownloadStateUI (DownloadState downloadState){
		switch (downloadState) 
		{
			case DownloadState.none:
				break;

			case DownloadState.NotYetDownload:
				SetActive (video_Download.gameObject, true);
				SetActive (video_Pause.gameObject, false);
				SetActive (video_Resume.gameObject, false);
				SetActive (video_Cancel.gameObject, false);
				SetActive (video_DownloadProgressSlider.gameObject, false);
				SetActive (video_DownloadProgressText.gameObject, false);
				SetActive (video_DownloadSpeedText.gameObject, false);
				break;
			case DownloadState.Downloading:
				SetActive (video_Download.gameObject, false);
				SetActive (video_Pause.gameObject, true);
				SetActive (video_Resume.gameObject, false);
				SetActive (video_Cancel.gameObject, true);
				SetActive (video_DownloadProgressSlider.gameObject, true);
				SetActive (video_DownloadProgressText.gameObject, true);
				SetActive (video_DownloadSpeedText.gameObject, true);
				break;
			case DownloadState.Pause:
				SetActive (video_Download.gameObject, false);
				SetActive (video_Pause.gameObject, false);
				SetActive (video_Resume.gameObject, true);
				SetActive (video_Cancel.gameObject, true);
				SetActive (video_DownloadProgressSlider.gameObject, true);
				SetActive (video_DownloadProgressText.gameObject, true);
				SetActive (video_DownloadSpeedText.gameObject, true);
				break;
			case DownloadState.Complete:
				SetActive (video_Download.gameObject, false);
				SetActive (video_Pause.gameObject, false);
				SetActive (video_Resume.gameObject, false);
				SetActive (video_Cancel.gameObject, false);
				SetActive (video_DownloadProgressSlider.gameObject, false);
				SetActive (video_DownloadProgressText.gameObject, false);
				SetActive (video_DownloadSpeedText.gameObject, false);
				break;
			case DownloadState.Fail:
				SetActive (video_Download.gameObject, false);
				SetActive (video_Pause.gameObject, false);
				SetActive (video_Resume.gameObject, true);
				SetActive (video_Cancel.gameObject, true);
				SetActive (video_DownloadProgressSlider.gameObject, false);
				SetActive (video_DownloadProgressText.gameObject, false);
				SetActive (video_DownloadSpeedText.gameObject, false);
				break;
			case DownloadState.Cancel:
				SetActive (video_Download.gameObject, true);
				SetActive (video_Pause.gameObject, false);
				SetActive (video_Resume.gameObject, false);
				SetActive (video_Cancel.gameObject, false);
				SetActive (video_DownloadProgressSlider.gameObject, false);
				SetActive (video_DownloadProgressText.gameObject, false);
				SetActive (video_DownloadSpeedText.gameObject, false);
				break;
			default:
				break;
		}
	}
	#endregion

	#region LoadingScreen
	public void PlayLoadingScreen()
	{
		if (rootLoading != null) {
			rootLoading.SetActive(true);
		}
	}

	public void StopLoadingScreen()
	{
		if (rootLoading != null) {
			rootLoading.SetActive(false);
		}
	}
	#endregion

	/// <summary>
	/// Sets the active.
	/// </summary>
	/// <param name="obj">Object.</param>
	/// <param name="value">If set to <c>true</c> value.</param>
	private void SetActive(GameObject obj, bool value)
	{
		if (obj != null) {
			obj.SetActive(value);
		}
	}

	public override void OnLoadedThumbnail()
	{
		base.OnLoadedThumbnail ();
		video_image.texture = thumbnailTexture;
		StopLoadingScreen ();
	}

	void OnEnable()
	{
		if (video != null) {

			if (thumbnailTexture == null) {
				thumbnailTexture = new Texture2D(4, 4, TextureFormat.DXT1, false);
			}

			if (video_image.texture == null || thumbnailTexture.name != video.videoInfo.id) {
				CheckAndDownloadThumbnail ();
			} else {
				StopLoadingScreen ();
			}

			if (videoDownloader == null) {
				GameObject videoDownloaderObj = GameObject.Find ("VideoDownLoader" + "-" + video.videoInfo.id);

				if (videoDownloaderObj != null) {
					videoDownloader = videoDownloaderObj.GetComponent<VideoDownloader> ();
					Download ();
				}

				ResumeProgress(video.videoInfo.size);
			}
		}

	}

	public void OnClickStreaming3D()
	{
		DownloadMenu.instance.Streaming3D (video);
	}

	public void OnClickStreaming2D()
	{
		DownloadMenu.instance.Streaming2D (video);
	}

	#region Object Pool implementation

	public override void OnDestroy ()
	{
		base.OnDestroy ();

		if (video != null) {
			// This is to doubly sure that there will be no leftover videoDownloader for a download
			GameObject videoDownloaderObj = GameObject.Find ("VideoDownLoader" + "-" + video.videoInfo.id);

			if (videoDownloaderObj != null) {
				Destroy (videoDownloaderObj.gameObject);
			}

			// Set downloader to null
			videoDownloader = null;
		}
	}

	public override void OnLive ()
	{
		base.OnLive ();
	}

	#endregion

	#region NativeUI AlertPopup	
	/// <summary>
	/// Gets the alert when not loggin.
	/// </summary>
	public override void GetAlertDelete ()
	{
		base.GetAlertDelete ();
	}

	public override void OnAlertDeleteComplete ()
	{
        base.OnAlertDeleteComplete();
        Pause ();

        string path = Path.Combine (MainAllController.instance.user.GetPath(), video.videoInfo.id) ;

        if (Directory.Exists (path)) {
        	Directory.Delete (path,true);
        }

        	DownloadMenu menu = Object.FindObjectOfType<DownloadMenu> ();
        	if (menu != null) {
        		menu.RemoveUI (this);
        	}

        // bool isFistButtonClicked = buttonIndex == 0;
        //bool isSecondButtonClicked = buttonIndex == 1;

        //if (isFistButtonClicked) {

        //} else {

        //}

        //if (isSecondButtonClicked) {
        //	Pause ();

        //	string path = Path.Combine (MainAllController.instance.user.GetPath(), video.videoInfo.id) ;

        //	if (Directory.Exists (path)) {
        //		Directory.Delete (path,true);
        //	}

        //	DownloadMenu menu = Object.FindObjectOfType<DownloadMenu> ();
        //	if (menu != null) {
        //		menu.RemoveUI (this);
        //	}
        //} else {

        //}
    }

    #endregion

    #region Notifications 

    /// <summary>
    /// Notificationses the downloaded.
    /// </summary>
    private void Notifications_DownloadCompleted(){
		NotificationsDemo notificationsDemo = Object.FindObjectOfType<NotificationsDemo>();
		SettingsMenu settingsMenu = Object.FindObjectOfType<SettingsMenu>();

		if (notificationsDemo != null && settingsMenu.GetNotificationText() == settingsMenu.GetkeyTrueNotification()){
			notificationsDemo.ScheduleLocalNotification ("" + video.videoInfo.video_name, "Download Completed!" );
		}
	}

	#endregion
}
