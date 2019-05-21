using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Net;
using UnityEngine.UI;
using System;
using CielaSpike;
using EasyMobile;
using SimpleDiskUtils;

public class VideoUI : Cacheable
{
	public GameObject rootLoading = null;

	public Texture2D thumbnailTexture;

	private UserDetailMenu userDetailMenu;

	public Video video
	{
		get {
			return _video;
		}

		set {
			
			_video = value;
		}
	}

	protected Video _video;

	private bool downloadingThumbnail;

    // Start is called before the first frame update
    void Start()
    {
		print ("CREATE THUMBNAIL");
		thumbnailTexture = new Texture2D(4, 4, TextureFormat.DXT1, false);
    }

    // Update is called once per frame
    void Update()
    {

    }

	public virtual void Setup(Video video)
	{
		this.video = video;
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
		if (userDetailMenu == null){
			userDetailMenu = UnityEngine.Object.FindObjectOfType<UserDetailMenu> ();
		}

		if (userDetailMenu != null){
			userDetailMenu.Setup(video,this);
		}
	}
	#endregion

	protected long GetDownloadProgress()
	{
		string filepath = MainAllController.instance.user.GetPathToFile (video.videoInfo.id,video.videoInfo.video_name);

		if (File.Exists (filepath)) {

			FileInfo fINfo = new FileInfo (filepath);
			return (fINfo.Length * 100) / video.videoInfo.size;
		}
		return 0;
	}



	protected void LoadThumbnail(string thumbnailURL)
	{
		try
		{
			byte[] fileData = File.ReadAllBytes( thumbnailURL );

			if (thumbnailTexture == null) {
				thumbnailTexture = new Texture2D(4, 4, TextureFormat.RGB565, false);
			}

			thumbnailTexture.LoadImage(fileData);
			thumbnailTexture.name = video.videoInfo.id;

//			LocalVideoManager.instance.AddThumbnailToCache(thumbnailURL,thumbnailTexture);

			OnLoadedThumbnail();
		} catch(Exception e) {
			Debug.Log ("Exception!  " + e.Message);
		} finally {
			//StopLoadingScreen ();
		}

	}

	public virtual void OnLoadedThumbnail()
	{
//		downloadingThumbnail = false;
//		StopLoadingScreen ();
	}
		
	/// <summary>
	/// Attemp to check if thumbnail has been downloaded. It not, download and set thumbnail image
	/// </summary>
	public void CheckAndDownloadThumbnail()
	{
		string thumbnailURL = string.Empty;

		if (MainAllController.instance.user == null || video == null) {
			return;
		}

		thumbnailURL = MainAllController.instance.user.GetPathToVideoThumbnail (video.videoInfo.id);

		// If thumbnail exists, load it
		if (File.Exists (thumbnailURL)) {
			
			LoadThumbnail (thumbnailURL);

		} else {

			// if not, download from link
			if(gameObject.activeInHierarchy)
			StartCoroutine(DownloadThumbnail(video.videoInfo.thumbnail_link));
		}
			
	}

	private byte[] raw ;
	/// <summary>
	/// Downloads the thumbnail.
	/// </summary>
	/// <param name="url">URL.</param>
	IEnumerator DownloadThumbnail(string url)
	{
		yield return new WaitForSeconds (.5f);

		using(WebClient client = new WebClient ())
		{
			try
			{
				PlayLoadingScreen();
				client.DownloadDataCompleted += DownloadDataCompleted;
				client.DownloadDataAsync(new System.Uri(url));

			}catch(Exception e) {

				Debug.LogError ("DownloadThumbnail Exception! " + e.Message);
				StopLoadingScreen ();
			} 

			// Attemp to reset coroutine if client cannot connect 
			// Could be useful for when internet is lost during downloading
			if (!client.IsBusy) {
				this.StartCoroutine (DownloadThumbnail (url));
				Debug.Log ("Resetting Download Thumbnail...................................");
				// Stop Loading screen, no matter what
				StopLoadingScreen ();
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
					thumbnailTexture = new Texture2D(4, 4, TextureFormat.DXT1, false);
					//Proceed to apply the texture image	
					thumbnailTexture.LoadImage (raw);
					thumbnailTexture.Apply ();

					print("DOWNLAODED THUMBNAIL COMPLETED " + video.videoInfo.id);

					File.WriteAllBytes(MainAllController.instance.user.GetPathToVideoThumbnail (video.videoInfo.id),raw);

					OnLoadedThumbnail();

				}else
				{
					throw new Exception("Null Raw!");
				}

			}catch(Exception e) {
				Debug.LogError ("Exception! " + e.Message);
			} finally {
				// Stop Loading screen, no matter what
				StopLoadingScreen ();
			}

		}

	}

	void DownloadDataCompleted(object sender,
		DownloadDataCompletedEventArgs e)
	{
		raw = e.Result;
	}

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

        if (playBnt != null)
        {
            playBnt.SetActive(false);
        }
    }

	System.TimeSpan ts;
	public string MakeLengthString()
	{
		if (video.videoInfo.length != null) {
			ts = System.TimeSpan.FromMilliseconds((double)video.videoInfo.length);
			return System.String.Format("{0:00}", ts.Hours) + ":" + System.String.Format("{0:00}", ts.Minutes) + ":" + System.String.Format("{0:00}", ts.Seconds);  ;
		}
		return string.Empty;
	}

	#region VR UI Animation
	// Caching
	private Vector3 currentPos;

	private bool pointerEntered;

	[SerializeField]
	private Transform root;

	[SerializeField]
	private GameObject playBnt;

	[SerializeField]
	private float animUpLimit = -60;

	// Put this in Update()
	protected void UIAnimation()
	{
        //currentPos = root.localPosition;

        //if (pointerEntered) {

        //	if (currentPos.z > animUpLimit) {
        //		root.localPosition += Vector3.back * Time.deltaTime * 500;
        //		if (playBnt != null){
        //			playBnt.SetActive (true);
        //		}

        //	}

        //} else {

        //	if (currentPos.z < 0) {
        //		root.localPosition -= Vector3.back * Time.deltaTime * 500;
        //		if (playBnt != null){
        //			playBnt.SetActive (false);
        //		}
        //	}

        //}
    }

    public void OnPointerEnter()
	{
		pointerEntered = true;
        if (playBnt != null)
        {
            playBnt.SetActive(true);
        }
    }

	public void OnPointerExit()
	{
		pointerEntered = false;
        if (playBnt != null)
        {
            playBnt.SetActive(false);
        }
    }

	#endregion

	#region Object Pool implementation

	public override void OnDestroy ()
	{
		gameObject.SetActive (false);
	}

	public override void OnLive ()
	{
		gameObject.SetActive (true);
	}

	#endregion

	#region NativeUI AlertPopup	
	/// <summary>
	/// Gets the alert when not loggin.
	/// </summary>
	public virtual void GetAlertDelete(){

        AndroidDialog.instance.showLoginDialog("Delete?", OnAlertDeleteComplete);

       // NativeUI.AlertPopup alert = NativeUI.ShowTwoButtonAlert("Notification!", "Delete?", "CANCEL", "CONFIRM");
		//if (alert != null)
			//alert.OnComplete += OnAlertDeleteComplete;
	}

	//public virtual void OnAlertDeleteComplete(int buttonIndex){
		
	//}

    public virtual void OnAlertDeleteComplete()
    {

    }
    #endregion

    /// <summary>
    /// Checks if enough space for download video.
    /// </summary>
    /// <returns><c>true</c>, if if enough space was checked, <c>false</c> otherwise.</returns>
    public bool CheckIfEnoughSpace()
	{
		try
		{
			// in Megabytes
			int availableSpace = DiskUtils.CheckAvailableSpace ();

			int videoSizeInMB = (int)((video.videoInfo.size / 1024) / 1024);

			if (videoSizeInMB > availableSpace) {
				NativeUI.Alert ("Cannot download!", "Not enough space!");
				return false;
			} else {
				return true;
			}

		}catch(Exception e) {
			Debug.Log ("CheckIfEnoughSpace() " + e.Message);
			return true;
		}
	}

	protected String MakeRegistrationDateString()
	{
		if (video != null && video.videoInfo != null) {
			return video.videoInfo.dateTime.Date.Day + "-" + video.videoInfo.dateTime.Date.Month + "-" + video.videoInfo.dateTime.Date.Year;
		}
		return String.Empty;
	}

}
