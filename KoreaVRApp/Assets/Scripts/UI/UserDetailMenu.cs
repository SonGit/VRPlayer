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
	[SerializeField] private Text video_model = null;
	[SerializeField] private Text video_description = null;
	[SerializeField] private Text video_date = null;
	[SerializeField] private Text video_genre = null;
	[SerializeField] private Button BtnDownload;
    

    [Header("---- Favorite ----")]
	[SerializeField] private GameObject favoriteBtn;
	[SerializeField] private GameObject unfavoriteBtn;

	[Header("---- Downloaded ----")]
	[SerializeField] private GameObject downloadedUI;
	[SerializeField] private GameObject haventDownloadedUI;

    [SerializeField] private ScrollRect scrollRect;

	[Header("---- ScreenShot ----")]
	[SerializeField] private GameObject screenShotContent = null;
	[SerializeField] private GameObject screenShotPrefab = null;

    private byte[] raw ;
	[SerializeField] private ScreenShotUI[] ScreenShotUIArray;

    Video video;
    VideoUI currentShowUI;

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

		this.OnBack += InitScreenShot;

		// Register events
		MainAllController.instance.OnDownloadedVideo += OnDownloadedVideo;
	}

    #region Get Info

    public Text GetVideoName()
	{
		if (video_name != null){
			return video_name;
		}
		return null;
	}

	public Text GetVideoModel()
	{
		if (video_model != null){
			return video_model;
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

    #endregion


    #region ResetScrollView

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

    #endregion


    #region OnClickDownloadBnt

    private void OnClickDownload()
    {
        if (currentShowUI)
        {
            if (currentShowUI is UserVideoUI)
            {
                (currentShowUI as UserVideoUI).Download();
            }
        }
    }

    #endregion


    #region Setup Detail page

    public void Setup(Video video, VideoUI currentShowUI)
	{
		GetVideoName().text = video.videoInfo.video_name;
		GetVideoModel ().text = video.videoInfo.actor;
		GetVideoDescription ().text = Regex.Unescape (video.videoInfo.description);
	
		if (currentShowUI is UserVideoUI){
			GetVideoImage ().texture = (currentShowUI as UserVideoUI).thumbnailTexture;
		}

		if (currentShowUI is DownloadVideoUI){
			GetVideoImage ().texture = (currentShowUI as DownloadVideoUI).thumbnailTexture;
		}

		if (currentShowUI is InboxVideoUI){
			GetVideoImage ().texture = (currentShowUI as InboxVideoUI).thumbnailTexture;
		}

		GetVideoLength ().text = currentShowUI.MakeLengthString ();

		GetVideoDate().text = video.videoInfo.date;

		GetVideoGenre ().text = video.videoInfo.genre;
			
		this.video = video;
		this.currentShowUI = currentShowUI;

		if(GetVideoImage ().texture == null)
		this.StartCoroutine (DownloadThumbnail (video.videoInfo.thumbnail_link, video_image));

		SetupFavoriteBtns ();

		SetupScreenShot (video);

		UiSwitch (currentShowUI);

        ResetScrollView();
    }

    #endregion


    #region Streaming 3D

    /// <summary>
    /// This is called when user clicked on 3D Streaming button
    /// </summary>
    public void OnClickStreaming3D()
	{
		if(MainAllController.instance != null){
			MainAllController.instance.PlayButtonSound ();
		}

        if (video.videoInfo.status == "200")
        {
            Streaming3D(video.videoInfo.id);
        }
        else if (video.videoInfo.status == "405") // if Video need payment (405)
        {
            Debug.Log("VIDEO" + "_" + video.videoInfo.id + " : " + "Need payment");
            if (MainAllController.instance != null)
            {
                MainAllController.instance.PurchaseAlert();
            }
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
			MainAllController.instance.Streaming3D (video, currentShowUI, getLinkVideoResponse.link);
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

        if (SystemLanguageManager.instance != null)
        {
            SystemLanguageManager.instance.ErrorNetworkAlert();
        }
    }

	#endregion


	#region Streaming 2D

	public void OnClickStreaming2D()
	{
		if(MainAllController.instance != null){
			MainAllController.instance.PlayButtonSound ();
		}

        if (video.videoInfo.status == "200")
        {
            Streaming2D(video.videoInfo.id);
        }
        else if (video.videoInfo.status == "405") // if Video need payment (405)
        {
            Debug.Log("VIDEO" + "_" + video.videoInfo.id + " : " + "Need payment");
            if (MainAllController.instance != null)
            {
                MainAllController.instance.PurchaseAlert();
            }
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
			MainAllController.instance.Streaming2D (video, currentShowUI, getLinkVideoResponse.link);
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

        if (SystemLanguageManager.instance != null)
        {
            SystemLanguageManager.instance.ErrorNetworkAlert();
        }
    }

	#endregion


    #region DownloadThumbnail

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

    #endregion


    #region Favorite/Unfavorite Button

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
			MainAllController.instance.user.AddFavoriteVideo (video);
		}

		SetupFavoriteBtns ();

		favoriteBtn.SetActive (false);
		unfavoriteBtn.SetActive (true);

		//if(MainAllController.instance != null){
		//	MainAllController.instance.GoToFavoriteMenu ();
		//}

		if (ScreenLoading.instance != null){
			ScreenLoading.instance.Stop ();
		}
	}

	void OnFailedFavorite()
	{
		ScreenLoading.instance.Stop ();

        if (SystemLanguageManager.instance != null)
        {
            SystemLanguageManager.instance.ErrorNetworkAlert();
        }
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
			MainAllController.instance.user.RemoveFavoriteVideo (video);
			//MainAllController.instance.InitFavoriteMenu ();
		}

		SetupFavoriteBtns ();

		favoriteBtn.SetActive (true);
		unfavoriteBtn.SetActive (false);

		if (ScreenLoading.instance != null) {
			ScreenLoading.instance.Stop ();
		}
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

    #endregion


    #region Play Video 2D/3D
    public void PlayIn2D()
	{	
		if(MainAllController.instance != null){
			MainAllController.instance.PlayButtonSound ();
		}

		if (video is LocalVideo) {
			//Handheld.PlayFullScreenMovie ((video as LocalVideo).videoURL, Color.black, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.AspectFill);
			MainAllController.instance.Play2D(video,currentShowUI);

			if(MainAllController.instance != null){
				MainAllController.instance.DisableSubtitleVR ();
			}
		} else {
			string path = MainAllController.instance.user.GetPathToFile (video.videoInfo.id,video.videoInfo.video_name);
			//Handheld.PlayFullScreenMovie (path, Color.black, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.AspectFill);
			MainAllController.instance.Play2D(video,currentShowUI);
		}
	}

	public void PlayIn3D()
	{
        if (MainAllController.instance != null)
        {
            MainAllController.instance.PlayButtonSound();

            MainAllController.instance.GoVRPplayerMenu();

            MainAllController.instance.IsPlayVideo3D = true;

            MainAllController.instance.SetPlayVideo3DInfo(video, currentShowUI);
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
                BtnDownload.interactable = true;
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
                BtnDownload.interactable = false;
            }

		}

		// Case: User comes from InboxMenu
		if (currentShowUI is InboxVideoUI) {

			if (video != null) {

				if (video.isDownloaded ()) {
					ShowDownloadedUI ();
				} else {
					ShowHavenotDownloadedUI ();
				}

                // Hide download button
                BtnDownload.interactable = true;
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


    #region OnDownloadedVideo
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

    #endregion


    #region ScreenShot

    private ScreenShotUI screenShotUI;

	void SetupScreenShot(Video video)
	{
		InitScreenShot ();

		video.videoInfo.screenShot_links = new string[2];

		foreach (string screenShot_link in  video.videoInfo.screenShot_links) {
			if (screenShotPrefab != null && screenShotContent != null) {
				GameObject screenShotObj = (GameObject)Instantiate (screenShotPrefab);
				screenShotUI = screenShotObj.GetComponent<ScreenShotUI> ();
				screenShotObj.transform.SetParent (screenShotContent.transform, false);
			} else {
				Debug.LogError ("NULL.....................................");
			}

			if (screenShotUI != null){
				screenShotUI.SetupScreenShot (screenShot_link);
			}else {
				Debug.LogError ("NULL.....................................");
			}
		}
	}

	private void InitScreenShot(){
		if (screenShotContent != null){
			ScreenShotUIArray = screenShotContent.GetComponentsInChildren<ScreenShotUI> ();
		}

		if (ScreenShotUIArray != null){
			for (int i = 0; i < ScreenShotUIArray.Length; i++) {
				Destroy (ScreenShotUIArray[i].gameObject);
			}
		}
	}

	#endregion
}
