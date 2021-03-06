﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleDiskUtils;

public class VR_UserVideoUI : UserVideoUI
{
	private VR_MainMenu vr_MainMenu;

	public override void Setup(Video currentuserVideo)
	{
		this.video = currentuserVideo;
		this.video_name.text = currentuserVideo.videoInfo.video_name;
		//this.video_length.text = (currentuserVideo.videoInfo.length).ToString();
		this.video_length.text = "00:00:00";
		SetupThumbnail ();

		SetupFavoriteBtns ();
	}

	void Update()
	{
		UIAnimation ();
	}

	void SetupThumbnail()
	{
		video_image.texture = UserVideoMenu.instance.GetVideoThumbnail (video);

		if (video_image.texture == null) {
			CheckAndDownloadThumbnail ();
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
		if (thumbnailTexture == null) {
			thumbnailTexture = new Texture2D(4, 4, TextureFormat.DXT1, false);
		}

		if (video_image.texture == null || thumbnailTexture.name != video.videoInfo.id) {
			CheckAndDownloadThumbnail ();
		} else {
			StopLoadingScreen ();
		}
	}

	public void VR_DownloadBnt_OnClick ()
	{
		if (VR_MainMenu.instance != null){
			
			bool isConnect = VR_MainMenu.instance.CheckNetworkConnection ();

			if (isConnect) {

				float space = DiskUtils.CheckAvailableSpace ();

				float viseoSize = video.videoInfo.size / 1024;

				// compare KB With KB
				if ((space * 1024) > viseoSize) {
					if (MainAllController.instance != null) {

						if (video.isDownloaded ()) {
							return;
						}

						VR_UserVideoMenu menu = UnityEngine.Object.FindObjectOfType<VR_UserVideoMenu> ();
						if (menu != null) {
							menu.RemoveUIPerma (this);
						}

						GameObject videoDownloaderObj = GameObject.Find ("VideoDownLoader" + "-" + video.videoInfo.id);

						if (videoDownloaderObj != null) {

							Debug.Log ("Already in Download Menu!!!");
							DownloadMenu.instance.StartDownload (video);
						} else {
							string authToken = MainAllController.instance.user.token;
							if (authToken != null) {
								Networking.instance.GetVideoLinkRequest (video.videoInfo.id, authToken, OnGetLink, OnFailedGetStreamingLink);
							}
						}
					}
				} else {
					VR_MainMenu.instance.ShowUsablecapacityAlert ();
				}
			} else {
				VR_MainMenu.instance.ShowNetworkAlert ();
			}
		}
	}
		

	/// <summary>
	/// Call this event when user click on favorite button
	/// </summary>
	public override void OnClickFavoriteButton ()
	{
		if (VR_MainMenu.instance != null) {

			bool isConnect = VR_MainMenu.instance.CheckNetworkConnection ();

			if (isConnect) {
				if (video != null) {
					Networking.instance.FavoriteVideoRequest (video.videoInfo.id, MainAllController.instance.user.token, OnCompleteFavorite, OnFailedFavorite);
				} else {
					Debug.LogError ("Video is null!");
				}
			} else {
				VR_MainMenu.instance.ShowNetworkAlert ();
			}
		}
	}

	public override void OnCompleteFavorite (FavoriteVideoResponse callback)
	{
		if(MainAllController.instance != null){
			MainAllController.instance.user.AddFavoriteVideo (video);
			MainAllController.instance.FastUpdateFavorite ();
		}
		SetupFavoriteBtns ();
		if (favoriteBtn != null && unfavoriteBtn != null){
			favoriteBtn.SetActive (false);
			unfavoriteBtn.SetActive (true);
		}
	}

	public override void OnFailedFavorite ()
	{
		
	}

	/// <summary>
	/// Call this event when user click on unfavorite button
	/// </summary>
	public override void OnClickUnfavoriteButton ()
	{
		if (VR_MainMenu.instance != null) {

			bool isConnect = VR_MainMenu.instance.CheckNetworkConnection ();

			if (isConnect) {
				if (video != null) {
					Networking.instance.UnfavoriteVideoRequest (video.videoInfo.id, MainAllController.instance.user.token, OnCompleteUnfavorite, OnFailedFavorite);
				} else {
					Debug.LogError ("Video is null!");
				}
			}else {
				VR_MainMenu.instance.ShowNetworkAlert ();
			}
		}
	}

	public override void OnCompleteUnfavorite (UnfavoriteVideoResponse callback)
	{
		if(MainAllController.instance != null){
			MainAllController.instance.user.RemoveFavoriteVideo (video);
			MainAllController.instance.FastUpdateFavorite ();
		}
		SetupFavoriteBtns ();
		if (favoriteBtn != null && unfavoriteBtn != null) {
			favoriteBtn.SetActive (true);
			unfavoriteBtn.SetActive (false);
		}
	}


	void OnDestroy()
	{
		VR_UserVideoMenu.instance.Rearrange ();
	}

	public void ClickPlayBnt()
	{
		if (vr_MainMenu == null){
			vr_MainMenu = Object.FindObjectOfType<VR_MainMenu>();
		}

		vr_MainMenu.ShowStreamingAlert (this);
	}

	#region Streaming 3D

	/// <summary>
	/// This is called when user clicked on 3D Streaming button
	/// </summary>
	public void OnClickStreaming3D()
	{
		Streaming3D (video.videoInfo.id);
	}

	void Streaming3D(string id)
	{
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

		}
	}

	void OnFailedGetStreamingLink()
	{
		
	}

	#endregion
}
