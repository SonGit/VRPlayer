using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CielaSpike;
using System.IO;
using EasyMobile;
using System.Net;
using UnityEngine.Networking;
using System;
using System.Text.RegularExpressions;


public class FavoriteVideoUI : UserVideoUI
{
	[Header("Components")]
	[SerializeField] private Transform downloadedUI;
	[SerializeField] private Transform havenotDownloadedUI;

	void Start()
	{
		if (MainAllController.instance != null) {
			MainAllController.instance.OnDownloadedVideo += OnDownloadedVideo;
		}
	}

	public override void Update ()
	{
		base.Update ();
	}

	#region setup info video
	public override void Setup(Video video)
	{
		this.video = video;
		video_name.text = video.videoInfo.video_name;
        //video_length.text = (video.videoInfo.length).ToString();
        //this.video_length.text = "00:00:00";

        videoRegistration_videoSize.text = MakeRegistrationDateString() + " / " + ((video.videoInfo.size / 1024) / 1024) + " MB";
		//video_desc.text = Regex.Unescape (video.videoInfo.description);
		video_image.texture = null;

		SetupFavoriteBtns ();

		CheckThumbnail ();

        SetFavoriteLanguage();
        SetPlayVideoBntLanguage();

		UiSwitch ();
	}

	#endregion
		

	#region Object Pool implementation

//	public override void OnDestroy ()
//	{
//		gameObject.SetActive (false);
//		if (MainAllController.instance != null) {
//			MainAllController.instance.OnDownloadedVideo -= OnDownloadedVideo;
//		}
//	}
//
//	public override void OnLive ()
//	{
//		gameObject.SetActive (true);
//		if (MainAllController.instance != null) {
//			MainAllController.instance.OnDownloadedVideo += OnDownloadedVideo;
//		}
//	}
//
	#endregion
	// This event is called when a video has been downloaded
	// If the downloaded video is this video, make sure to change UI accordingly
	void OnDownloadedVideo(Video anotherVideo)
	{
		if (video != null && anotherVideo != null) {
			if (video.videoInfo.id == anotherVideo.videoInfo.id) {
				UiSwitch ();
			}
		} else {
			Debug.Log ("OnDownloadedVideo Null references!!!");
		}

	}

    public override void UiSwitch()
    {
        base.UiSwitch();

        if (video != null)
        {
            if (video.isDownloaded())
            {
                ShowDownloadedUI();
            }
            else
            {
                ShowHavenotDownloadedUI();
            }

        }
        else
        {
            Debug.Log("VIDEO IS NULL!");
        }
    }

    void ShowDownloadedUI()
	{
		downloadedUI.gameObject.SetActive (true);
		havenotDownloadedUI.gameObject.SetActive (false);
	}

	void ShowHavenotDownloadedUI()
	{
		downloadedUI.gameObject.SetActive (false);
		havenotDownloadedUI.gameObject.SetActive (true);
	}


	/// <summary>
	/// Call this event when user click on favorite button
	/// </summary>
	public override void OnClickFavoriteButton ()
	{
		base.OnClickFavoriteButton ();
	}

	public override void OnCompleteFavorite (FavoriteVideoResponse callback)
	{
		base.OnCompleteFavorite (callback);
	}

	protected override void OnFailedFavorite ()
	{
		base.OnFailedFavorite ();
	}

	/// <summary>
	/// Call this event when user click on unfavorite button
	/// </summary>
	public override void OnClickUnfavoriteButton ()
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

	public override void OnCompleteUnfavorite (UnfavoriteVideoResponse callback)
	{
//		SetupFavoriteBtns();
//		if (favoriteBtn != null && unfavoriteBtn != null) {
//			favoriteBtn.SetActive (true);
//			unfavoriteBtn.SetActive (false);
//		}

		if(MainAllController.instance != null){
			MainAllController.instance.user.RemoveFavoriteVideo (video);
			MainAllController.instance.InitFavoriteMenu ();
		}

		if (ScreenLoading.instance != null){
			ScreenLoading.instance.Stop ();
		}
	}

	public override void SetupFavoriteBtns ()
	{
		base.SetupFavoriteBtns ();
	}

	protected override void Custom()
	{
		DownloadMenu.instance.StartDownload (video);
	}

	public override void RefreshCellView()
	{
		Setup(FavoriteVideoMenu.instance.getVideoAtIndex(dataIndex));
	}

    #region Play 3D

    public override void PlayIn3D()
    {
        if (MainAllController.instance != null)
        {
            MainAllController.instance.PlayButtonSound();

            MainAllController.instance.GoVRPplayerMenu();

            MainAllController.instance.IsPlayVideo3D = true;

            MainAllController.instance.SetPlayVideo3DInfo(video, this);
        }
    }

    #endregion
}
