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
using EnhancedUI.EnhancedScroller;

public class VideoUI : EnhancedScrollerCellView
{
	public GameObject rootLoading = null;

	public Texture2D thumbnailTexture;

	public Texture2D screenShotTexture;

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
        if(pendingDelete)
        {
            DeleteProcess();
            pendingDelete = false;
        }
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

	public virtual void OnLoadedScreenShot()
	{
		
	}
		


	#region DownloadThumbnail

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

	private byte[] thumbnailRaw ;
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
				client.DownloadDataCompleted += DownloadThumbnailDataCompleted;
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
				if(thumbnailRaw != null)
				{
					thumbnailTexture = new Texture2D(4, 4, TextureFormat.DXT1, false);
					//Proceed to apply the texture image	
					thumbnailTexture.LoadImage (thumbnailRaw);
					thumbnailTexture.Apply ();

					print("DOWNLAODED THUMBNAIL COMPLETED " + video.videoInfo.id);

					File.WriteAllBytes(MainAllController.instance.user.GetPathToVideoThumbnail (video.videoInfo.id),thumbnailRaw);

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

	void DownloadThumbnailDataCompleted(object sender,
		DownloadDataCompletedEventArgs e)
	{
		thumbnailRaw = e.Result;
	}

	#endregion


	#region DownloadScreenShot

	/// <summary>
	/// Attemp to check if ScreenShot has been downloaded. It not, download and set ScreenShot image
	/// </summary>
	public void CheckAndDownloadScreenShot(string url)
	{
		StartCoroutine(DownloadScreenShot(url));
	}

	private byte[] screenShotRaw ;
	/// <summary>
	/// Downloads the thumbnail.
	/// </summary>
	/// <param name="url">URL.</param>
	IEnumerator DownloadScreenShot(string url)
	{
		yield return new WaitForSeconds (.5f);

		using(WebClient client = new WebClient ())
		{
			try
			{
				PlayLoadingScreen();
				client.DownloadDataCompleted += DownloadScreenShotDataCompleted;
				client.DownloadDataAsync(new System.Uri(url));

			}catch(Exception e) {

				Debug.LogError ("DownloadScreenShot Exception! " + e.Message);
				StopLoadingScreen ();
			} 

			// Attemp to reset coroutine if client cannot connect 
			// Could be useful for when internet is lost during downloading
			if (!client.IsBusy) {
				this.StartCoroutine (DownloadScreenShot (url));
				Debug.Log ("Resetting Download ScreenShot...................................");
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
				if(screenShotRaw != null)
				{
					screenShotTexture = new Texture2D(4, 4, TextureFormat.DXT1, false);
					//Proceed to apply the texture image	
					screenShotTexture.LoadImage (screenShotRaw);
					screenShotTexture.Apply ();

					OnLoadedScreenShot();

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

	void DownloadScreenShotDataCompleted(object sender,
		DownloadDataCompletedEventArgs e)
	{
		screenShotRaw = e.Result;
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

	public virtual void PlayIn3D()
	{
		if(MainAllController.instance != null){
			MainAllController.instance.PlayButtonSound ();
		}

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

	public Transform root;

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

	#region NativeUI AlertPopup	
	/// <summary>
	/// Gets the alert when not loggin.
	/// </summary>
	public virtual void GetAlertDelete(){

    #if UNITY_ANDROID
     AndroidDialog.instance.showLoginDialog("Delete?", OnAlertDeleteComplete);
    #endif

    #if UNITY_IOS
        NativeUI.AlertPopup alert = NativeUI.ShowTwoButtonAlert("Notification!", "Delete?", "CANCEL", "CONFIRM");

		if (alert != null)
			alert.OnComplete += OnAlertDeleteComplete_IOS;

    #endif

    }

    protected virtual void OnAlertDeleteComplete_IOS(int buttonIndex){

        if(buttonIndex == 1)
            pendingDelete = true;

    }

    protected bool pendingDelete;

    public virtual void OnAlertDeleteComplete()
    {
        pendingDelete = true;
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
			return video.videoInfo.dateTime.Date.Year + "-" + video.videoInfo.dateTime.Date.Month + "-" + video.videoInfo.dateTime.Date.Day;
		}
		return String.Empty;
	}

	protected void Delete()
	{
		if(MainAllController.instance != null){
			MainAllController.instance.PlayButtonSound ();
		}

#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
		GetAlertDelete ();
#endif

#if UNITY_EDITOR
        DeleteProcess();
#endif
    }

	protected void DeleteProcess()
    {
        if (this is LocalVideoUI)
        {
            try
            {
                File.Delete(video.videoInfo.id);
            }
            catch (Exception e)
            {
                Debug.Log("DeleteVideo exception " + e.Message);
            }

            StorageMenu menu = UnityEngine.Object.FindObjectOfType<StorageMenu>();
            if (menu != null)
            {
                menu.Refresh();
            }
        }

        if (this is DownloadVideoUI)
        {
            string path = String.Empty;
            try
            {

                path = Path.Combine(MainAllController.instance.user.GetPath(), video.videoInfo.id);
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }

            }
            catch (Exception e)
            {
                Debug.Log("DeleteVideo exception " + e.Message);
            }

            DownloadMenu menu = UnityEngine.Object.FindObjectOfType<DownloadMenu>();
            if (menu != null)
            {
                menu.Refresh();
            }
        }

        if (this is InboxVideoUI)
        {
            try
            {
                File.Delete(video.videoInfo.id);
            }
            catch (Exception e)
            {
                Debug.Log("DeleteVideo exception " + e.Message);
            }

            InboxMenu menu = UnityEngine.Object.FindObjectOfType<InboxMenu>();
            if (menu != null)
            {
                menu.Refresh();
            }
        }
    }

}
