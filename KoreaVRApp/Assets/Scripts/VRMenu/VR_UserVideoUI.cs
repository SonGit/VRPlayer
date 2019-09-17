using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleDiskUtils;

public class VR_UserVideoUI : UserVideoUI
{
	private VR_MainMenu vr_MainMenu;

	public override void Setup(Video video)
	{
		if (root != null){
			root.gameObject.SetActive(video != null);
		}
			
		if (video != null) {
			this.video = video;
			this.video_name.text = video.videoInfo.video_name;
			//this.video_length.text = (currentuserVideo.videoInfo.length).ToString();
			this.videoRegistration_videoSize.text = MakeLengthString ();
			SetupFavoriteBtns ();
			video_image.texture = null;

			CheckThumbnail ();
		}
	}

	public override void Update ()
	{
		base.Update ();
	}
		
	public override void OnLoadedThumbnail()
	{
		base.OnLoadedThumbnail ();
		video_image.texture = thumbnailTexture;
		StopLoadingScreen ();
	}

	public void VR_DownloadBnt_OnClick ()
	{
        if (video.videoInfo.status == "200") // if Video can download (200)
        {
            if (VR_MainMenu.instance != null)
            {

                bool isConnect = VR_MainMenu.instance.CheckNetworkConnection();

                if (isConnect)
                {

                    float space = DiskUtils.CheckAvailableSpace();

                    float viseoSize = video.videoInfo.size / 1024;

                    // compare KB With KB
                    if ((space * 1024) > viseoSize)
                    {
                        if (MainAllController.instance != null)
                        {

                            if (video.isDownloaded())
                            {
                                return;
                            }

                            GameObject videoDownloaderObj = GameObject.Find("VideoDownLoader" + "-" + video.videoInfo.id);

                            if (videoDownloaderObj != null)
                            {

                                Debug.Log("Already in Download Menu!!!");
                                DownloadMenu.instance.StartDownload(video);

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

                            }
                            else
                            {
                                string authToken = MainAllController.instance.user.token;
                                if (authToken != null)
                                {
                                    if (VR_MainMenu.instance != null)
                                    {
                                        VR_MainMenu.instance.ShowLoadingUI();
                                    }

                                    Networking.instance.GetVideoLinkRequest(video.videoInfo.id, authToken, OnGetLink, OnFailedGetStreamingLink);
                                }
                            }
                        }
                    }
                    else
                    {
                        VR_MainMenu.instance.ShowUsablecapacityAlert();
                    }
                }
                else
                {
                    VR_MainMenu.instance.ShowNetworkAlert();
                }
            }
        }
        else if (video.videoInfo.status == "405") // if Video need payment (405)
        {
            Debug.Log("VIDEO" + "_" + video.videoInfo.id + " : " + "Need payment");
            if (VR_MainMenu.instance != null)
            {
                VR_MainMenu.instance.ShowPurchaseAlert();
            }
        }
    }
		

	/// <summary>
	/// Call this event when user click on favorite button
	/// </summary>
	public override void OnClickFavoriteButton ()
	{
		if (VR_MainMenu.instance != null) {

			VR_MainMenu.instance.ShowLoadingUI ();

			bool isConnect = VR_MainMenu.instance.CheckNetworkConnection ();

			if (isConnect) {
				if (video != null) {
					Networking.instance.FavoriteVideoRequest (video.videoInfo.id, MainAllController.instance.user.token, OnCompleteFavorite, OnFailedFavorite);
				} else {
					Debug.LogError ("Video is null!");
				}
			} else {
				VR_MainMenu.instance.ShowNetworkAlert ();
                VR_MainMenu.instance.HideLoadingUI();
            }
		}
	}

	public override void OnCompleteFavorite (FavoriteVideoResponse callback)
	{
		if(MainAllController.instance != null){
			MainAllController.instance.user.AddFavoriteVideo (video);
			//MainAllController.instance.FastUpdateFavorite ();
		}

		if (VR_MainMenu.instance != null){
			//VR_MainMenu.instance.OpenFavoriteMenu ();
			VR_MainMenu.instance.HideLoadingUI ();
		}

        SetupFavoriteBtns();
        if (favoriteBtn != null && unfavoriteBtn != null)
        {
            favoriteBtn.SetActive(false);
            unfavoriteBtn.SetActive(true);
        }
    }

	protected override void OnFailedFavorite ()
	{
		if (VR_MainMenu.instance != null){
			VR_MainMenu.instance.HideLoadingUI ();
            VR_MainMenu.instance.ShowNetworkAlert();
        }
	}

	/// <summary>
	/// Call this event when user click on unfavorite button
	/// </summary>
	public override void OnClickUnfavoriteButton ()
	{
		if (VR_MainMenu.instance != null) {

			VR_MainMenu.instance.ShowLoadingUI ();

			bool isConnect = VR_MainMenu.instance.CheckNetworkConnection ();

			if (isConnect) {
				if (video != null) {
					Networking.instance.UnfavoriteVideoRequest (video.videoInfo.id, MainAllController.instance.user.token, OnCompleteUnfavorite, OnFailedFavorite);
				} else {
					Debug.LogError ("Video is null!");
				}
			}else {
				VR_MainMenu.instance.ShowNetworkAlert ();
                VR_MainMenu.instance.HideLoadingUI();
            }
		}
	}

	public override void OnCompleteUnfavorite (UnfavoriteVideoResponse callback)
	{
		if(MainAllController.instance != null){
			MainAllController.instance.user.RemoveFavoriteVideo (video);
			//MainAllController.instance.FastUpdateFavorite ();
		}

		if (VR_MainMenu.instance != null){
			VR_MainMenu.instance.HideLoadingUI ();
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
        if (video.videoInfo.status == "200")
        {
            if (vr_MainMenu == null)
            {
                vr_MainMenu = Object.FindObjectOfType<VR_MainMenu>();
            }

            vr_MainMenu.ShowStreamingAlert(this);
        }
        else if (video.videoInfo.status == "405") // if Video need payment (405)
        {
            Debug.Log("VIDEO" + "_" + video.videoInfo.id + " : " + "Need payment");
            if (VR_MainMenu.instance != null)
            {
                VR_MainMenu.instance.ShowPurchaseAlert();
            }
        }
    }

    #region Streaming 3D

    public override void OnClickStreaming3D()
    {
        if (VR_MainMenu.instance != null)
        {
            VR_MainMenu.instance.ShowLoadingUI();
        }

        Streaming3D(video.videoInfo.id);
    }

    void Streaming3D(string id)
	{
		Networking.instance.GetVideoLinkRequest (id, MainAllController.instance.user.token ,OnGetStreamingLink,OnFailedGetStreamingLink);
	}

	void OnGetStreamingLink(GetLinkVideoResponse getLinkVideoResponse)
	{
		try
		{
			MainAllController.instance.Streaming3D (video, this, getLinkVideoResponse.link);
		}
		catch (System.Exception e)
		{
			Debug.LogError ("OnGetStreamingLink Exception " + e.Message);

		} finally {
			
			if (VR_MainMenu.instance != null) {
				VR_MainMenu.instance.HideLoadingUI ();
			}
		}
	}

    protected override void OnFailedGetStreamingLink()
    {
        if (VR_MainMenu.instance != null)
        {
            VR_MainMenu.instance.HideLoadingUI();
            VR_MainMenu.instance.ShowNetworkAlert();
        }
    }

    #endregion
}
