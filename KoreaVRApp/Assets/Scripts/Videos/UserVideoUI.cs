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
using System.Text.RegularExpressions;
using SimpleDiskUtils;


public class UserVideoUI : VideoUI
{
	[SerializeField] protected RawImage video_image = null;
	[SerializeField] protected Text video_name = null;
	[SerializeField] protected Text videoRegistration_videoSize = null;
    //[SerializeField] protected Text video_desc = null;
    //[SerializeField] protected RawImage bookmarkIcon = null;

    public GameObject videoDownloaderPrefab = null;
	private VideoDownloader videoDownloader;

	[Header("Favorite")]
	[SerializeField] protected GameObject favoriteBtn;
    [SerializeField] protected Text favoriteText;
    [SerializeField] protected Text unFavoriteText;
    [SerializeField] protected GameObject unfavoriteBtn;
    [SerializeField] private GameObject grayDownloadedUI;

    private string path;

	UserVideoMenu.UserVideoDownloadCallback callback;

	// Use this for initialization
	void Start () 
	{
		if (thumbnailTexture == null) {
			thumbnailTexture = new Texture2D(4, 4, TextureFormat.DXT1, false);
		}
	}

	public override void Update ()
	{
		base.Update ();
	}


	public void Download ()
	{
		float space = DiskUtils.CheckAvailableSpace ();

		float viseoSize  = video.videoInfo.size / 1024;

        
        if (video.videoInfo.status == "200") // if Video can download (200)
        {
            // compare KB With KB
            if ((space * 1024) > viseoSize)
            {

                if (MainAllController.instance != null)
                {

                    MainAllController.instance.PlayButtonSound();

                    if (video.isDownloaded())
                    {
                        NativeUI.Alert("", "You have downloaded this video!");
                        return;
                    }

                    GameObject videoDownloaderObj = GameObject.Find("VideoDownLoader" + "-" + video.videoInfo.id);

                    if (videoDownloaderObj != null)
                    {

                        Debug.Log("Already in Download Menu!!!");
                        DownloadMenu.instance.StartDownload(video);
                        GoToDownloadMenu();

                    }
                    else
                    {
                        string authToken = MainAllController.instance.user.token;
                        if (authToken != null)
                        {

                            ScreenLoading.instance.Play();

                            Networking.instance.GetVideoLinkRequest(video.videoInfo.id, authToken, OnGetLink, OnFailedGetStreamingLink);
                        }
                    }
                }
            }
            else
            {

                if (SystemLanguageManager.instance != null)
                {
                    if (SystemLanguageManager.instance.IsEnglishLanguage)
                    {
                        AndroidDialog.instance.showLoginDialog("Usable capacity is not available!", OnAlertDownloadComplete, "Yes", "No", true);
                    }

                    if (SystemLanguageManager.instance.IsKoreanLanguage)
                    {
                        AndroidDialog.instance.showLoginDialog("사용 가능한 용량을 사용할 수 없습니다!", OnAlertDownloadComplete, "예", "아니오", true);
                    }

                    if (SystemLanguageManager.instance.IsJapaneseLanguage)
                    {
                        AndroidDialog.instance.showLoginDialog("使用可能容量がありません!", OnAlertDownloadComplete, "はい", "いいえ", true);
                    }

                    if (SystemLanguageManager.instance.IsChineseLanguage)
                    {
                        AndroidDialog.instance.showLoginDialog("可用容量不可用!", OnAlertDownloadComplete, "是", "沒有", true);
                    }

                    if (SystemLanguageManager.instance.IsOtherLanguage)
                    {
                        AndroidDialog.instance.showLoginDialog("Usable capacity is not available!", OnAlertDownloadComplete, "Yes", "No", true);
                    }
                }

            }
        }
        else if(video.videoInfo.status == "405") // if Video need payment (405)
        {
            Debug.Log("VIDEO" + "_" + video.videoInfo.id + " : " + "Need payment");
            if (MainAllController.instance != null)
            {
                MainAllController.instance.PurchaseAlert();
            }
        }
	}

	private void OnAlertDownloadComplete(){
	
	}

	protected virtual void OnFailedGetStreamingLink()
	{
        if (SystemLanguageManager.instance != null)
        {
            SystemLanguageManager.instance.ErrorNetworkAlert();
        }

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
//					Debug.Log("CREATE +++++++++++++++++++++++" + video.videoInfo.id);
					videoDownloader = ((GameObject)Instantiate (videoDownloaderPrefab)).GetComponent<VideoDownloader> ();
					videoDownloader.name = "VideoDownLoader" + "-" + video.videoInfo.id;
//
					Custom();
				}

				GoToDownloadMenu ();

                VR_UserVideoMenu vr_UserVideoMenu = UnityEngine.Object.FindObjectOfType<VR_UserVideoMenu>();
                VR_FavoriteMenu vr_FavoriteMenu = UnityEngine.Object.FindObjectOfType<VR_FavoriteMenu>();

                if (vr_UserVideoMenu != null)
                {
                    vr_UserVideoMenu.FastRefresh();
                }

                if (vr_FavoriteMenu != null)
                {
                    vr_FavoriteMenu.FastRefresh();
                }

                // Remove self from menu
                //DestroySelf ();
            }

		} catch (System.Exception e)
		{
			Debug.LogError ("OnGetLink Exception " + e.Message);
		
		} finally {
			
			ScreenLoading.instance.Stop ();

			if (VR_MainMenu.instance != null){
				VR_MainMenu.instance.HideLoadingUI ();
			}

		}
			
	}

	protected virtual void Custom()
	{
		DownloadMenu.instance.StartDownload (video);
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

		this.videoRegistration_videoSize.text = MakeRegistrationDateString() + " / " +((video.videoInfo.size / 1024) / 1024) + " MB";
		//video_desc.text = Regex.Unescape (video.videoInfo.description);
		video_image.texture = null;

		CheckThumbnail ();

		bool isVideoFavorited = MainAllController.instance.user.IsVideoFavorited (video);
		//bookmarkIcon.enabled = isVideoFavorited;
		SetupFavoriteBtns();

        SetFavoriteLanguage();
        SetPlayVideoBntLanguage();

        UiSwitch();

        videoDownloader = null;
    }
    #endregion

    void OnDownloadedVideo(Video anotherVideo)
    {
        if (video != null && anotherVideo != null)
        {
            if (video.videoInfo.id == anotherVideo.videoInfo.id)
            {
                UiSwitch();
            }
        }
        else
        {
            Debug.Log("OnDownloadedVideo Null references!!!");
        }

    }

    public override void UiSwitch()
    {
        base.UiSwitch();

        if (video != null)
        {
            if (video.isDownloaded())
            {
                if (grayDownloadedUI != null)
                {
                    grayDownloadedUI.SetActive(true);
                }
            }
            else
            {
                if (grayDownloadedUI != null)
                {
                    grayDownloadedUI.SetActive(false);
                }
            }
        }
        else
        {
            Debug.Log("VIDEO IS NULL!");
        }
    }


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

	public override void OnLoadedThumbnail()
	{
		base.OnLoadedThumbnail ();
		video_image.texture = thumbnailTexture;
		StopLoadingScreen ();
	}

	protected void CheckThumbnail()
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
			MainAllController.instance.user.AddFavoriteVideo (video);
		}

        SetupFavoriteBtns();
        if (favoriteBtn != null && unfavoriteBtn != null)
        {
            favoriteBtn.SetActive(false);
            unfavoriteBtn.SetActive(true);
        }

        //if(MainAllController.instance != null){
        //	MainAllController.instance.GoToFavoriteMenu ();
        //}

        if (ScreenLoading.instance != null){
			ScreenLoading.instance.Stop ();
		}
    }

	protected virtual void OnFailedFavorite()
	{
		if (ScreenLoading.instance != null){
			ScreenLoading.instance.Stop ();
		}

        if (SystemLanguageManager.instance != null)
        {
            SystemLanguageManager.instance.ErrorNetworkAlert();
        }
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
			MainAllController.instance.user.RemoveFavoriteVideo (video);
		}

		SetupFavoriteBtns();
		if (favoriteBtn != null && unfavoriteBtn != null) {
			favoriteBtn.SetActive (true);
			unfavoriteBtn.SetActive (false);
		}

		if (ScreenLoading.instance != null){
			ScreenLoading.instance.Stop ();
		}
	}

	public virtual void SetupFavoriteBtns()
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

	public override void RefreshCellView()
	{
		Setup(UserVideoMenu.instance.getVideoAtIndex(dataIndex));
	}

    protected void SetFavoriteLanguage() {
        if (SystemLanguageManager.instance != null)
        {
            SystemLanguageManager.instance.SetFavorite_UnfavoriteLanguage(favoriteText,unFavoriteText);
        }
    }
}
