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


public class FavoriteVideoUI : UserVideoUI
{
	[Header("Components")]
	[SerializeField] private Transform downloadedUI;
	[SerializeField] private Transform havenotDownloadedUI;

	#region setup info video
	public override void Setup(Video video)
	{
		this.video = video;
		video_name.text = video.videoInfo.video_name;
		//video_length.text = (video.videoInfo.length).ToString();
		//this.video_length.text = "00:00:00";

		video_size.text = video.videoInfo.date + "  |  " + ((video.videoInfo.size / 1024) / 1024) + " MB";
		//video_desc.text = video.videoInfo.description;

		SetupFavoriteBtns ();

		OnEnable ();

		UiSwitch ();
	}
	#endregion	
		

	#region Object Pool implementation

	public override void OnDestroy ()
	{
		gameObject.SetActive (false);
		if (MainAllController.instance != null) {
			MainAllController.instance.OnDownloadedVideo -= OnDownloadedVideo;
		}
	}

	public override void OnLive ()
	{
		gameObject.SetActive (true);
		if (MainAllController.instance != null) {
			MainAllController.instance.OnDownloadedVideo += OnDownloadedVideo;
		}
	}

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

	void UiSwitch()
	{
		if (video != null) {
			if (video.isPartial ()) {
				ShowHavenotDownloadedUI ();
			} 

			if (video.isDownloaded ()) {
				ShowDownloadedUI ();
			} 
		} else {
			Debug.Log ("VIDEO IS NULL!");
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

	public override void OnFailedFavorite ()
	{
		base.OnFailedFavorite ();
	}

	/// <summary>
	/// Call this event when user click on unfavorite button
	/// </summary>
	public override void OnClickUnfavoriteButton ()
	{
		base.OnClickUnfavoriteButton ();
	}

	public override void OnCompleteUnfavorite (UnfavoriteVideoResponse callback)
	{
		base.OnCompleteUnfavorite (callback);
	}

	public override void SetupFavoriteBtns ()
	{
		base.SetupFavoriteBtns ();
	}

	protected override void Custom()
	{
		DownloadMenu.instance.StartDownload (video.videoInfo.id);
	}
}
