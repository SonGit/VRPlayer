using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CielaSpike;
using System.IO;
using EasyMobile;
using System.Net;
using UnityEngine.Networking;
using System.Text.RegularExpressions;


public class InboxVideoUI : VideoUI
{
	[SerializeField] protected RawImage video_image;
	[SerializeField] protected Text video_name;
	[SerializeField] protected Text video_length;
	[SerializeField] protected Text video_size;
	[SerializeField] protected Text video_desc;
	[SerializeField] protected Button videoPlay2D;
	[SerializeField] protected Button videoPlayVR;
	[SerializeField] protected Button videoDelete;

	// Use this for initialization
	void Start () 
	{
		if (videoDelete != null){
			videoDelete.onClick.AddListener(() =>
				{
					if(MainAllController.instance != null){
						MainAllController.instance.PlayButtonSound ();
					}
				});
		}
	}

	void Update (){
		
	}

	#region setup info video
	public override void Setup(Video video)
	{
		base.Setup (video);
		video_name.text = video.videoInfo.video_name;
		//video_length.text = (video.videoInfo.length).ToString();
		this.video_length.text = MakeRegistrationDateString() + " | " +((video.videoInfo.size / 1024) / 1024) + " MB"; ;
		//video_size.text = ((video.videoInfo.size / 1024) / 1024) + " MB";
		video_desc.text = Regex.Unescape (video.videoInfo.description);

		OnEnable ();
	}
	#endregion	

	#region LoadingScreen
	public void PlayLoadingScreen()
	{
		if (rootLoading != null) {
			rootLoading.SetActive(true);
		}
	}

	public void StopLoadingScreen()
	{
		if (rootLoading != null) {
			rootLoading.SetActive(false);
		}
	}
	#endregion

	public override void OnLoadedThumbnail()
	{
		base.OnLoadedThumbnail ();
		video_image.texture = thumbnailTexture;
		StopLoadingScreen ();
	}

	void OnEnable()
	{
		if (video != null) {

			if (thumbnailTexture == null) {
				thumbnailTexture = new Texture2D(4, 4, TextureFormat.DXT1, false);
			}

			if (video_image.texture == null || thumbnailTexture.name != video.videoInfo.id) {
				CheckAndDownloadThumbnail ();
			} else {
				StopLoadingScreen ();
			}
		}
	}

	#region NativeUI AlertPopup	
	/// <summary>
	/// Gets the alert when not loggin.
	/// </summary>
	public override void GetAlertDelete ()
	{
		base.GetAlertDelete ();
	}

	public override void OnAlertDeleteComplete ()
	{
        base.OnAlertDeleteComplete();
	}

	#endregion

	public override void RefreshCellView()
	{
		Setup(InboxMenu.instance.getVideoAtIndex(dataIndex));
	}
}
