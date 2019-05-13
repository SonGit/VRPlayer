using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CielaSpike;
using EasyMobile;
using System.Net;
using UnityEngine.Networking;
using System.IO;
using TMPro;


public class UserVideoUI : VideoUI
{
	[SerializeField] protected RawImage video_image = null;
	[SerializeField] protected Text video_name = null;
	[SerializeField] protected Text video_length = null;
	[SerializeField] protected Text video_size = null;
	[SerializeField] protected Text video_desc = null;
	//[SerializeField] protected RawImage bookmarkIcon = null;

	public GameObject videoDownloaderPrefab = null;
	private VideoDownloader videoDownloader;
	private UserDetailMenu userDetailMenu;

	[Header("Favorite")]
	[SerializeField] protected GameObject favoriteBtn;
	[SerializeField] protected GameObject unfavoriteBtn;

	private string path;

	UserVideoMenu.UserVideoDownloadCallback callback;

	// Use this for initialization
	void Start () 
	{
		userDetailMenu = Object.FindObjectOfType<UserDetailMenu> ();
		if (thumbnailTexture == null) {
			thumbnailTexture = new Texture2D(4, 4, TextureFormat.DXT1, false);
		}
	}

	public RawImage GetVideoImage()
	{
		if (video_image != null){
			return video_image;
		}
		return null;
	}

	#region Go Detail page
	public void GoDetailVideo(){
		if(MainAllController.instance != null){
			MainAllController.instance.PlayButtonSound ();
		}

		if (MainAllController.instance != null){
			MainAllController.instance.UserVideo_OnUserVideoDetail ();
		}

		SetDetailVideoInfo ();
	}

	public void SetDetailVideoInfo(){
		if (userDetailMenu != null){
			userDetailMenu.Setup(video,this);
		}
	}
	#endregion


	public void Download ()
	{
		if (MainAllController.instance != null) {
			
			MainAllController.instance.PlayButtonSound ();

			if (video.isDownloaded ()) {
				NativeUI.Alert("","You have downloaded this video!");
				return;
			}

			GameObject videoDownloaderObj = GameObject.Find ("VideoDownLoader" + "-" + video.videoInfo.id);

			if (videoDownloaderObj != null) {

				Debug.Log ("Already in Download Menu!!!");
				GoToDownloadMenu ();

			} else {
				string authToken = MainAllController.instance.user.token;
				if (authToken != null){

					ScreenLoading.instance.Play ();

					Networking.instance.GetVideoLinkRequest (video.videoInfo.id, authToken, OnGetLink,OnFailedGetStreamingLink);
				}
			}
		}

	}

	void OnFailedGetStreamingLink()
	{
		ScreenLoading.instance.Stop ();
	}

	public void OnGetLink(GetLinkVideoResponse getLinkVideoResponse){

		try
		{
			// Get filepath to video folder
			if (MainAllController.instance != null) {
				path = MainAllController.instance.user.GetPath ();
			}

			string filepath = MainAllController.instance.user.GetPathToFile (video.videoInfo.id,video.videoInfo.video_name);

			// If filepath point to nothing, create directory to hold video
			if (!File.Exists (filepath)) {
				Directory.CreateDirectory(Path.Combine(path,video.videoInfo.id));
				// Immiedetly create a placeholder file, so UserVideoMenu can display correctly

				using(FileStream saveFileStream = new FileStream (filepath,
					FileMode.Create, FileAccess.ReadWrite, 
					FileShare.ReadWrite))
				{

				}
			}


			// If file is already exists and has been completely downlaoded
			if (GetDownloadProgress () == 100) {

				DeleteDownloader ();

				GoToDownloadMenu ();

			} else {

				// If not, create a downloader
				// downlaoder only get kickstart via DownloadVideoUI
				if (videoDownloader == null) {
					Debug.Log("CREATE +++++++++++++++++++++++" + video.videoInfo.id);
					videoDownloader = ((GameObject)Instantiate (videoDownloaderPrefab)).GetComponent<VideoDownloader> ();
					videoDownloader.name = "VideoDownLoader" + "-" + video.videoInfo.id;
				}

				GoToDownloadMenu ();

				// Remove self from menu
				DestroySelf ();
			}

		} catch (System.Exception e)
		{
			Debug.LogError ("OnGetLink Exception " + e.Message);
		
		} finally {
			
			ScreenLoading.instance.Stop ();

		}
			
	}

	/// <summary>
	/// Gracefully goes to Download Menu once download has started
	/// </summary>
	void GoToDownloadMenu()
	{
		ScreenLoading.instance.Stop ();
		MainAllController.instance.GoToDownloadMenu ();
	}

	#region setup info video
	public override void Setup(Video video)
	{
		base.Setup (video);
		video_name.text = video.videoInfo.video_name;
		//video_length.text = (video.videoInfo.length).ToString();
		this.video_length.text = "00:00:00" + " | " +((video.videoInfo.size / 1024) / 1024) + " MB"; ;
		video_size.text = ((video.videoInfo.size / 1024) / 1024) + " MB";
		video_desc.text = video.videoInfo.description;

		OnEnable ();

		bool isVideoFavorited = MainAllController.instance.user.IsVideoFavorited (video);
		//bookmarkIcon.enabled = isVideoFavorited;
		SetupFavoriteBtns();
	}
	#endregion


	void DestroySelf()
	{
		if (video is FavoriteVideo) {
			FavoriteVideoMenu menu = Object.FindObjectOfType<FavoriteVideoMenu> ();

			if (menu != null) {
				menu.RemoveUI (this);
			}
		} else {
			UserVideoMenu menu = Object.FindObjectOfType<UserVideoMenu> ();

			if (menu != null) {
				menu.RemoveUI (this);
			}
		}

		if (this is VR_UserVideoUI) {
			VR_UserVideoMenu.instance.RemoveUIPerma (this);
		}

	}

	void DeleteDownloader()
	{
		if (videoDownloader != null) {
			Destroy (videoDownloader.gameObject);
			videoDownloader = null;
		}
		//DownloadSubtitle ();
	}

	/// <summary>
	/// Downloads the subtitle.
	/// </summary>
	void DownloadSubtitle()
	{
		if (video is UserVideo) {
			(video as UserVideo).DownloadSubtitle ();
		} else {
			Debug.LogError ("video is not UserVideo");
		}
	}

	#region Object Pool implementation

	public override void OnDestroy ()
	{
		video_image.texture = null;
		gameObject.SetActive (false);
	}

	public override void OnLive ()
	{
		gameObject.SetActive (true);
	}

	#endregion

	public override void OnLoadedThumbnail()
	{
		base.OnLoadedThumbnail ();
		video_image.texture = thumbnailTexture;
		StopLoadingScreen ();
	}

	protected void OnEnable()
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
		}

	}

	/// <summary>
	/// Call this event when user click on favorite button
	/// </summary>
	public virtual void OnClickFavoriteButton()
	{
		if (video != null) {
			ScreenLoading.instance.Play ();

			if(MainAllController.instance != null){
				MainAllController.instance.PlayButtonSound ();
			}

			Networking.instance.FavoriteVideoRequest (video.videoInfo.id,MainAllController.instance.user.token,OnCompleteFavorite,OnFailedFavorite);
		} else {
			Debug.LogError ("Video is null!");
		}
	}

	public virtual void OnCompleteFavorite(FavoriteVideoResponse callback)
	{
		if(MainAllController.instance != null){
			MainAllController.instance.UpdateFavorite ();
		}
		SetupFavoriteBtns ();
		if (favoriteBtn != null && unfavoriteBtn != null) {
			favoriteBtn.SetActive (false);
			unfavoriteBtn.SetActive (true);
		}
	}

	public virtual void OnFailedFavorite()
	{
		ScreenLoading.instance.Stop ();
	}

	/// <summary>
	/// Call this event when user click on unfavorite button
	/// </summary>
	public virtual void OnClickUnfavoriteButton()
	{
		if (video != null) {
			ScreenLoading.instance.Play ();

			if(MainAllController.instance != null){
				MainAllController.instance.PlayButtonSound ();
			}

			Networking.instance.UnfavoriteVideoRequest (video.videoInfo.id,MainAllController.instance.user.token,OnCompleteUnfavorite,OnFailedFavorite);
		} else {
			Debug.LogError ("Video is null!");
		}
	}

	public virtual void OnCompleteUnfavorite(UnfavoriteVideoResponse callback)
	{
		if(MainAllController.instance != null){
			MainAllController.instance.UpdateFavorite ();
		}

		SetupFavoriteBtns();
		if (favoriteBtn != null && unfavoriteBtn != null) {
			favoriteBtn.SetActive (true);
			unfavoriteBtn.SetActive (false);
		}
	}

	public virtual void  SetupFavoriteBtns()
	{
		bool isVideoFavorited = MainAllController.instance.user.IsVideoFavorited (video);
		if (isVideoFavorited) {
			if (favoriteBtn != null && unfavoriteBtn != null) {
				favoriteBtn.SetActive (false);
				unfavoriteBtn.SetActive (true);
			}
		} else {
			if (favoriteBtn != null && unfavoriteBtn != null) {
				favoriteBtn.SetActive (true);
				unfavoriteBtn.SetActive (false);
			}
		}
	}
}
