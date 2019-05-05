using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class VR_InboxVideoUI : UserVideoUI
{
	private VR_MainMenu vr_MainMenu;

	void Start(){

	}

	public override void Setup(Video currentuserVideo)
	{
		this.video = currentuserVideo;
		this.video_name.text = currentuserVideo.videoInfo.video_name;
		//this.video_length.text = (currentuserVideo.videoInfo.length).ToString();
		this.video_length.text = "00:00:00";
		SetupThumbnail ();
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
		if (video_image.texture == null) {
			CheckAndDownloadThumbnail ();
		} else {
			StopLoadingScreen ();
		}
	}

	public void Delete()
	{
		if (vr_MainMenu == null){
			vr_MainMenu = Object.FindObjectOfType<VR_MainMenu>();
		}

		vr_MainMenu.ShowDeleteAlert (video, this);
	}
}
