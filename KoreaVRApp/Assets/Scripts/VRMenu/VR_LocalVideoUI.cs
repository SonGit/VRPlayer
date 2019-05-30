
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_LocalVideoUI : LocalVideoUI
{
	
	public override void Setup(Video currentlocalVideo)
	{
		this.video = currentlocalVideo;
		this.videoTitle.text = (video as LocalVideo).videoName;
		this.videoLength.text = MakeLengthString ();
		SetupThumbnail ();
//		try
//		{
//			videoImage.texture = NatShareU.NatShare.GetThumbnail (video.fileInfo.FullName);
//		}
//		catch
//		{
//			Debug.Log("Failed to GetThumbnail");
//		}
	}

//	public void PlayVideo()
//	{
//        SceneVR sceneVR = transform.root.GetComponentInParent<SceneVR>();
//        if(sceneVR != null)
//        {
//            sceneVR.PlayFromURL(video);
//        }
//	}

	public override void PlayIn3D ()
	{
		if (MainAllController.instance != null){
			MainAllController.instance.Play3D (video);
		}
	}


	void Update()
	{
		UIAnimation ();
	}


	void SetupThumbnail()
	{
		//videoImage.texture = StorageMenu.instance.GetVideoThumbnail (video);
	}

	void OnEnable()
	{
		SetupThumbnail ();
	}

}
 