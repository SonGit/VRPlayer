using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;
using UnityEngine.UI;
using System.Linq;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;

public class DownloadMenu : BasicMenuNavigation
{

	public static DownloadMenu instance;

	private VideoUI videoUI;

	void Awake()
	{
		instance = this;
	}

	protected override void Start ()
	{
		base.Start ();
		MainAllController.instance.OnGetUserVideo += Init;
		MainAllController.instance.OnLoggedOut += Reset;
	}
		
	public override void Init()
	{
		videos = GetUserVideo ();

		if (scroller != null){
			scroller.ReloadData ();
		}
			
		CheckThumbnail ();

		UpdateNetworkConnectionUI ();
		UpdateNoVideoUI ();
	}

	public override void Refresh()
	{
		Init ();
	}

	protected override bool CanBeAdded(Video video)
	{
		UserVideo userVideo = video as UserVideo;

		if (userVideo.isPartial () && !userVideo.isDownloaded ()) {
			return true;
		} else {
			
//			GameObject videoDownloaderObj = GameObject.Find ("VideoDownLoader" + "-" + video.videoInfo.id);
//
//			if (videoDownloaderObj != null) {
//				return true;
//			}
			return false;
		}

	}

	#region Streaming 3D
	Video currentVideo;
	/// <summary>
	/// This is called when user clicked on 3D Streaming button
	/// </summary>
	/// 
	public void Streaming3D(Video video)
	{
		if(MainAllController.instance != null){
			MainAllController.instance.PlayButtonSound ();
		}

		currentVideo = video;

		ScreenLoading.instance.Play ();
		Networking.instance.GetVideoLinkRequest (video.videoInfo.id, MainAllController.instance.user.token ,OnGetStreamingLink,OnFailedGetStreamingLink);
	}

	void OnGetStreamingLink(GetLinkVideoResponse getLinkVideoResponse)
	{
		try
		{
			MainAllController.instance.Streaming3D (currentVideo,getLinkVideoResponse.link);
		}
		catch (System.Exception e)
		{
			Debug.LogError ("OnGetStreamingLink Exception " + e.Message);

		} finally {

			ScreenLoading.instance.Stop ();
		}
	}

	void OnFailedGetStreamingLink()
	{
		ScreenLoading.instance.Stop ();
	}

	#endregion

	#region Streaming 2D

	public void Streaming2D(Video video)
	{
		if(MainAllController.instance != null){
			MainAllController.instance.PlayButtonSound ();
		}

		currentVideo = video;

		ScreenLoading.instance.Play ();
		Networking.instance.GetVideoLinkRequest (video.videoInfo.id, MainAllController.instance.user.token ,OnGetStreamingLink2D,OnFailedGetStreamingLink2D);
	}

	void OnGetStreamingLink2D(GetLinkVideoResponse getLinkVideoResponse)

	{
		try
		{
			MainAllController.instance.Streaming2D (currentVideo,getLinkVideoResponse.link);
		}
		catch (System.Exception e)
		{
			Debug.LogError ("OnGetStreamingLink2D Exception " + e.Message);

		} finally {

			ScreenLoading.instance.Stop ();
		}
	}

	void OnFailedGetStreamingLink2D()
	{
		ScreenLoading.instance.Stop ();
	}

	#endregion


	public void StartDownload(Video video)
	{
		
		GameObject videoDownloaderObj = GameObject.Find ("VideoDownLoader" + "-" + video.videoInfo.id);

		if (videoDownloaderObj != null) {
			VideoDownloader downloader = videoDownloaderObj.GetComponent<VideoDownloader> ();
			if (downloader != null) {
				downloader.Download (video);
			}
		}
	}


	#region EnhancedScroller Handlers

	public override int GetNumberOfCells (EnhancedScroller scroller)
	{
		if (videos != null){
			return videos.Count;
		}
		return 0;
	}

	public override float GetCellViewSize (EnhancedScroller scroller, int dataIndex)
	{
		// header views
		return 500f;
	}

	public override EnhancedScrollerCellView GetCellView (EnhancedScroller scroller, int dataIndex, int cellIndex)
	{
		// first, we get a cell from the scroller by passing a prefab.
		// if the scroller finds one it can recycle it will do so, otherwise
		// it will create a new cell.
		videoUI = scroller.GetCellView(videoUIPrefab) as DownloadVideoUI;

		// set the name of the game object to the cell's data index.
		// this is optional, but it helps up debug the objects in 
		// the scene hierarchy.
		videoUI.name = "DownloadVideo " + dataIndex.ToString();
	

		// we just pass the data to our cell's view which will update its UI
		videoUI.Setup(videos[dataIndex]);

		// return the cell to the scroller
		return videoUI;
	}

	#endregion


}
