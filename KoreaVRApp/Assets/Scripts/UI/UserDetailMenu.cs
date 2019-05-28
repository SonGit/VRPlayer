using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.VR;
using System;
using System.Net;
using System.Text.RegularExpressions;

public class UserDetailMenu : BasicMenuNavigation
{
	[Header("---- Detail Video Info ----")]
	[SerializeField] private RawImage video_image = null;
	[SerializeField] private Text video_name = null;
	[SerializeField] private Text video_length = null;
	[SerializeField] private Text video_id = null;
	[SerializeField] private Text video_description = null;
	[SerializeField] private Text video_date = null;
	[SerializeField] private Text video_genre = null;
	[SerializeField] private Button BtnDownload;

	[SerializeField] private GameObject favoriteBtn;
	[SerializeField] private GameObject unfavoriteBtn;

	[SerializeField] private GameObject downloadedUI;
	[SerializeField] private GameObject haventDownloadedUI;

    [SerializeField] private ScrollRect scrollRect;

    private byte[] raw ;

	protected override void Start ()
	{
		base.Start ();
		ResetScrollView ();

		if (BtnDownload != null){
			BtnDownload.onClick.AddListener(() =>
				{
					if(MainAllController.instance != null){
						MainAllController.instance.PlayButtonSound ();
					}

					OnClickDownload();
				});
		}

		// Register events
		MainAllController.instance.OnDownloadedVideo += OnDownloadedVideo;
	}

	public Text GetVideoName()
	{
		if (video_name != null){
			return video_name;
		}
		return null;
	}

	public Text GetVideoID()
	{
		if (video_id != null){
			return video_id;
		}
		return null;
	}

	public Text GetVideoDescription()
	{
		if (video_description != null){
			return video_description;
		}
		return null;
	}

	public RawImage GetVideoImage()
	{
		if (video_image != null){
			return video_image;
		}
		return null;
	}

	public Text GetVideoLength()
	{
		if (video_length != null){
			return video_length;
		}
		return null;
	}

	public Text GetVideoDate()
	{
		if (video_date != null){
			return video_date;
		}
		return null;
	}

	public Text GetVideoGenre()
	{
		if (video_genre != null){
			return video_genre;
		}
		return null;
	}

	/// <summary>
	/// Resets the scroll view (RectTransform).
	/// </summary>
	public void ResetScrollView(){
        StartCoroutine(ResetScroll());
	}

    IEnumerator ResetScroll()
    {
        verticalGrid.enabled = false;
        yield return new WaitForEndOfFrame();
        verticalGrid.enabled = true;
        if (verticalGrid != null)
        {
            verticalGrid.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        }
        yield return null;
    }

	Video video;
	VideoUI currentShowUI;

	public void Setup(Video video, VideoUI currentShowUI)
	{
		GetVideoName().text = video.videoInfo.video_name;
		GetVideoID ().text = video.videoInfo.id;
		GetVideoDescription ().text = Regex.Unescape (video.videoInfo.description);
	
		if (currentShowUI is UserVideoUI){
			GetVideoImage ().texture = (currentShowUI as UserVideoUI).GetVideoImage().texture;
		}

		if (currentShowUI is DownloadVideoUI){
			GetVideoImage ().texture = (currentShowUI as DownloadVideoUI).GetVideoImage().texture;
		}

		GetVideoLength().text = (video.videoInfo.length).ToString();
		GetVideoDate().text = video.videoInfo.date;

		foreach (var tag in video.videoInfo.tag) {
			GetVideoGenre().text = tag;
		}
			
		this.video = video;
		this.currentShowUI = currentShowUI;

		if(GetVideoImage ().texture == null)
		this.StartCoroutine (DownloadThumbnail (video.videoInfo.thumbnail_link, video_image));

		SetupFavoriteBtns ();

		UiSwitch (currentShowUI);

        ResetScrollView();
    }

	#region Streaming 3D

	/// <summary>
	/// This is called when user clicked on 3D Streaming button
	/// </summary>
	public void OnClickStreaming3D()
	{
		Streaming3D (video.videoInfo.id);

		if(MainAllController.instance != null){
			MainAllController.instance.PlayButtonSound ();
		}
	}

	void Streaming3D(string id)
	{
		ScreenLoading.instance.Play ();
		Networking.instance.GetVideoLinkRequest (id, MainAllController.instance.user.token ,OnGetStreamingLink,OnFailedGetStreamingLink);
	}
		
	void OnGetStreamingLink(GetLinkVideoResponse getLinkVideoResponse)
	{
		try
		{
			MainAllController.instance.Streaming3D (video,getLinkVideoResponse.link);
		}
		catch (System.Exception e)
		{
			Debug.LogError ("OnGetStreamingLink Exception " + e.Message);

		} finally {

			ScreenLoading.instance.Stop ();
		}
	}

	void OnFailedGetStreamingLink()
	{
		ScreenLoading.instance.Stop ();
	}

	#endregion

	#region Streaming 2D

	public void OnClickStreaming2D()
	{
		Streaming2D (video.videoInfo.id);

		if(MainAllController.instance != null){
			MainAllController.instance.PlayButtonSound ();
		}
	}

	void Streaming2D(string id)
	{
		ScreenLoading.instance.Play ();
		Networking.instance.GetVideoLinkRequest (id, MainAllController.instance.user.token ,OnGetStreamingLink2D,OnFailedGetStreamingLink2D);
	}

	void OnGetStreamingLink2D(GetLinkVideoResponse getLinkVideoResponse)
	{
		try
		{
			MainAllController.instance.Streaming2D (video,getLinkVideoResponse.link);
		}
		catch (System.Exception e)
		{
			Debug.LogError ("OnGetStreamingLink Exception " + e.Message);

		} finally {

			ScreenLoading.instance.Stop ();
		}
	}

	void OnFailedGetStreamingLink2D()
	{
		ScreenLoading.instance.Stop ();
	}

	#endregion

	private void OnClickDownload()
	{
		if (currentShowUI) {
			if (currentShowUI is UserVideoUI) {
				(currentShowUI as UserVideoUI).Download ();
			}
		}
	}

	/// <summary>
	/// Downloads the thumbnail.
	/// </summary>
	/// <param name="url">URL.</param>
	IEnumerator DownloadThumbnail(string url, RawImage image)
	{
		yield return new WaitForSeconds (.5f);

		using(WebClient client = new WebClient ())
		{
			try
			{

				client.DownloadDataCompleted += DownloadDataCompleted;
				client.DownloadDataAsync(new System.Uri(url));

			}catch(Exception e) {

				Debug.LogError ("DownloadThumbnail Exception! " + e.Message);
				//StopLoadingScreen ();

			} 

			// Attemp to reset coroutine if client cannot connect 
			// Could be useful for when internet is lost during downloading
			if (!client.IsBusy) {
				this.StartCoroutine (DownloadThumbnail (video.videoInfo.thumbnail_link, image));
				Debug.Log ("Resetting Download Thumbnail...................................");
				yield break;
			}

			// Wait until client has completed download
			while(client.IsBusy)
			{
				yield return new WaitForSeconds (.25f);
			}

			yield return new WaitForEndOfFrame ();

			try
			{
				if(raw != null)
				{
					Texture2D thumbnailTexture = new Texture2D(4, 4, TextureFormat.DXT1, false);
					//Proceed to apply the texture image	
					thumbnailTexture.LoadImage (raw);
					thumbnailTexture.Apply ();
					image.texture = thumbnailTexture;
				}else
				{
					throw new Exception("Null Raw!");
				}

			}catch(Exception e) {
				Debug.LogError ("Exception! " + e.Message);
			} finally {
				// Stop Loading screen, no matter what
				//StopLoadingScreen ();
			}

		}

	}

	void DownloadDataCompleted(object sender,
		DownloadDataCompletedEventArgs e)
	{
		raw = e.Result;
	}

	/// <summary>
	/// Call this event when user click on favorite button
	/// </summary>
	public void OnClickFavoriteButton()
	{
		if(MainAllController.instance != null){
			MainAllController.instance.PlayButtonSound ();
		}

		if (video != null) {
			ScreenLoading.instance.Play ();
			Networking.instance.FavoriteVideoRequest (video.videoInfo.id,MainAllController.instance.user.token,OnCompleteFavorite,OnFailedFavorite);
		} else {
			Debug.LogError ("Video is null!");
		}
	}

	void OnCompleteFavorite(FavoriteVideoResponse callback)
	{
		if(MainAllController.instance != null){
			MainAllController.instance.UpdateFavorite ();
		}
		SetupFavoriteBtns ();

		favoriteBtn.SetActive (false);
		unfavoriteBtn.SetActive (true);
	}

	void OnFailedFavorite()
	{
		ScreenLoading.instance.Stop ();
	}

	/// <summary>
	/// Call this event when user click on unfavorite button
	/// </summary>
	public void OnClickUnfavoriteButton()
	{
		if(MainAllController.instance != null){
			MainAllController.instance.PlayButtonSound ();
		}

		if (video != null) {
			ScreenLoading.instance.Play ();
			Networking.instance.UnfavoriteVideoRequest (video.videoInfo.id,MainAllController.instance.user.token,OnCompleteUnfavorite,OnFailedFavorite);
		} else {
			Debug.LogError ("Video is null!");
		}
	}

	void OnCompleteUnfavorite(UnfavoriteVideoResponse callback)
	{
		if(MainAllController.instance != null){
			MainAllController.instance.UpdateFavorite ();
		}

		favoriteBtn.SetActive (true);
		unfavoriteBtn.SetActive (false);
	}

	void SetupFavoriteBtns()
	{
		bool isVideoFavorited = MainAllController.instance.user.IsVideoFavorited (video);
		if (isVideoFavorited) {
			favoriteBtn.SetActive (false);
			unfavoriteBtn.SetActive (true);
		} else {
			favoriteBtn.SetActive (true);
			unfavoriteBtn.SetActive (false);
		}
	}

	#region Play Video
	public void PlayIn2D()
	{	
		if(MainAllController.instance != null){
			MainAllController.instance.PlayButtonSound ();
		}

		if (video is LocalVideo) {
			//Handheld.PlayFullScreenMovie ((video as LocalVideo).videoURL, Color.black, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.AspectFill);
			MainAllController.instance.Play2D(video);

			if(MainAllController.instance != null){
				MainAllController.instance.DisableSubtitleVR ();
			}
		} else {
			string path = MainAllController.instance.user.GetPathToFile (video.videoInfo.id,video.videoInfo.video_name);
			//Handheld.PlayFullScreenMovie (path, Color.black, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.AspectFill);
			MainAllController.instance.Play2D(video);
		}
	}

	public void PlayIn3D()
	{
		if (MainAllController.instance != null){
			MainAllController.instance.Play3D (video);
		}

		if(MainAllController.instance != null){
			MainAllController.instance.PlayButtonSound ();
		}
	}
	#endregion

	#region UI switch when user is downloaded/not downloaded
	void UiSwitch( VideoUI currentShowUI)
	{
		if (currentShowUI == null) {
			Debug.Log ("currentShowUI not assigned!");
			return;
		}

		// Case: User comes from UserVideoMenu
		if (currentShowUI is UserVideoUI) {
			
			if (video != null) {

				if (video.isDownloaded ()) {
					ShowDownloadedUI ();
				} else {
					ShowHavenotDownloadedUI ();
				}

				// Show download button
				BtnDownload.gameObject.SetActive (true);
			}

		}

		// Case: User comes from DownloadMenu
		if (currentShowUI is DownloadVideoUI) {

			if (video != null) {

				if (video.isDownloaded ()) {
					ShowDownloadedUI ();
				} else {
					ShowHavenotDownloadedUI ();
				}

				// Hide download button
				BtnDownload.gameObject.SetActive (false);
			}

		}
	}

	void ShowDownloadedUI()
	{
		downloadedUI.SetActive (true);
		haventDownloadedUI.SetActive (false);
	}

	void ShowHavenotDownloadedUI()
	{
		downloadedUI.SetActive (false);
		haventDownloadedUI.SetActive (true);
	}
	#endregion

	// This event is called when a video has been downloaded
	// If the downloaded video is this video, make sure to change UI accordingly
	void OnDownloadedVideo(Video anotherVideo)
	{
		if (video != null && anotherVideo != null) {
			if (video.videoInfo.id == anotherVideo.videoInfo.id) {
				UiSwitch (currentShowUI);
			}
		} else {
			Debug.Log ("OnDownloadedVideo Null references!!!");
		}

	}
}
