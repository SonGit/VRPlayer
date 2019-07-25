using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class VR_InboxVideoUI : UserVideoUI
{
	private VR_MainMenu vr_MainMenu;

	void Start(){

	}

	public override void Setup(Video video)
	{
		if (root != null){
			root.gameObject.SetActive(video != null);
		}
			
		if (video != null) {
			this.video = video;
			this.video_name.text = video.videoInfo.video_name;
			//this.video_length.text = (currentuserVideo.videoInfo.length).ToString();
			this.video_length.text = "00:00:00";
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

	public void Delete()
	{
		if (vr_MainMenu == null){
			vr_MainMenu = Object.FindObjectOfType<VR_MainMenu>();
		}

		vr_MainMenu.ShowDeleteAlert (video, this);
	}
}
