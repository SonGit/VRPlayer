using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		

	/// <summary>
	/// Call this event when user click on favorite button
	/// </summary>
	public override void OnClickFavoriteButton ()
	{
		if (video != null) {
			Networking.instance.FavoriteVideoRequest (video.videoInfo.id,MainAllController.instance.user.token,OnCompleteFavorite,OnFailedFavorite);
		} else {
			Debug.LogError ("Video is null!");
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
		if (video != null) {
			Networking.instance.UnfavoriteVideoRequest (video.videoInfo.id,MainAllController.instance.user.token,OnCompleteUnfavorite,OnFailedFavorite);
		} else {
			Debug.LogError ("Video is null!");
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
