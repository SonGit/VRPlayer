﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_FavoriteVideoUI : UserVideoUI
{

    // Start is called before the first frame update
    void Start()
    {

    }

	#region setup info video
	public override void Setup(Video video)
	{
		this.video = video;
		video_name.text = video.videoInfo.video_name;
		//video_length.text = (video.videoInfo.length).ToString();
		this.video_length.text = "00:00:00";
		SetupFavoriteBtns ();
	}
	#endregion	

	void Update()
	{
		UIAnimation ();
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
		if (favoriteBtn != null && unfavoriteBtn != null) {
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
			} else {
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

		VR_FavoriteMenu.instance.RemoveUIPerma (this);
	}

	void OnDestroy()
	{
		VR_FavoriteMenu.instance.Rearrange ();
	}
}
