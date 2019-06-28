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
		
		if(pendingDelete)
		{
			Pause();
			DeleteDownloader();

			StartCoroutine(DeleteProcess());
			pendingDelete = false;
		}

		SetDownloadProgressUI ();
		SetUI ();
	}
		
	public void Download ()
	{
		//Find downlaoder object
		// if found, kick start the download
		GameObject videoDownloaderObj = GameObject.Find ("VideoDownLoader" + "-" + video.videoInfo.id);

		if (videoDownloaderObj != null) {
			
			videoDownloader = videoDownloaderObj.GetComponent<VideoDownloader>();
			videoDownloader.Download (video);
		
		}

		//If not, create a downlaoder object and start downloading 
		else
		{

			videoDownloader = ((GameObject)Instantiate (videoDownloaderPrefab)).GetComponent<VideoDownloader> ();
			videoDownloader.name = "VideoDownLoader" + "-" + video.videoInfo.id;
			videoDownloader.Download (video);

		}

		SetDownloadStateUI (DownloadState.Downloading);
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
		Download ();
	}

	public void Cancel()
	{
		Delete();
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

				//SetDownloadStateUI (DownloadState.Downloading);

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
//                Destroy();
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
		this.video_length.text = MakeRegistrationDateString() + " | " +((video.videoInfo.size / 1024) / 1024) + " MB"; 

		video_image.texture = null;
        videoDownloader = null;

        CheckThumbnail ();
	}
		
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

	void SetUI()
	{
		//Find downlaoder object
		if (videoDownloader != null) {

			SetDownloadStateUI (videoDownloader.downloadState);

		} else {
			
			SetDownloadStateUI (DownloadState.Pause);

		}
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

	void CheckThumbnail()
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

//        string _path = Path.Combine(MainAllController.instance.user.GetPath(), video.videoInfo.id);
//
//        if (Directory.Exists(_path))
//        {
//            Directory.Delete(_path, true);
//
//            Pause();
//
//            DeleteDownloader();
//        }

        //DownloadMenu menu = Object.FindObjectOfType<DownloadMenu> ();
        //if (menu != null) {
        //menu.RemoveUI (this);
        //}

    }


    protected override void OnAlertDeleteComplete_IOS(int buttonIndex)
    {

        if (buttonIndex == 1)
        {
            OnAlertDeleteComplete();
        }

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

	public override void RefreshCellView()
	{
		Setup(DownloadMenu.instance.getVideoAtIndex(dataIndex));
	}
}
