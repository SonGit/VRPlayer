using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_FavoriteVideoUI : VR_UserVideoUI
{
    [Header("Component")]
    [SerializeField] Button downloadBnt;

    // Start is called before the first frame update
    void Start()
    {

    }

	#region setup info video
	public override void Setup(Video video)
	{
		print ("VR_FavoriteVideoUI");
		if (root != null){
			root.gameObject.SetActive(video != null);
		}
			
		if (video != null) {
			this.video = video;
			video_name.text = video.videoInfo.video_name;
			//video_length.text = (video.videoInfo.length).ToString();
			this.videoRegistration_videoSize.text = MakeLengthString ();
			SetupFavoriteBtns ();
			video_image.texture = null;

			CheckThumbnail ();

            UiSwitch();
        }
	
	}
	#endregion	

	public override void Update ()
	{
		base.Update ();
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
        if (vr_PlayBnt != null)
        {
            vr_PlayBnt.gameObject.SetActive(false);
            vr_PlayBnt.onClick.RemoveAllListeners();
            vr_PlayBnt.onClick.AddListener(PlayIn3D);
            downloadBnt.gameObject.SetActive(false);
        }
    }

    void ShowHavenotDownloadedUI()
    {
        if (vr_PlayBnt != null)
        {
            vr_PlayBnt.gameObject.SetActive(false);
            vr_PlayBnt.onClick.RemoveAllListeners();
            vr_PlayBnt.onClick.AddListener(ClickPlayBnt);
            downloadBnt.gameObject.SetActive(true);
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
			VR_MainMenu.instance.HideLoadingUI ();
			VR_MainMenu.instance.OpenFavoriteMenu ();
		}

		SetupFavoriteBtns ();
		if (favoriteBtn != null && unfavoriteBtn != null) {
			favoriteBtn.SetActive (false);
			unfavoriteBtn.SetActive (true);
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
			} else {
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
			VR_MainMenu.instance.OpenFavoriteMenu ();
		}

		//VR_FavoriteMenu.instance.RemoveUIPerma (this);
	}

	void OnDestroy()
	{
		VR_FavoriteMenu.instance.Rearrange ();
	}

    public override void PlayIn3D()
    {
        if (MainAllController.instance != null)
        {
            MainAllController.instance.SetPlayVideo3DInfo(video, this);
            MainAllController.instance.Play3D(video, this);
        }

        if (vr_PlayBnt != null)
        {
            vr_PlayBnt.gameObject.SetActive(false);
        }
    }

}
