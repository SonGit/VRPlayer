using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;
using System;
using System.Linq;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;

public class FavoriteVideoMenu : BasicMenuNavigation
{
	public static FavoriteVideoMenu instance;

	protected override void Awake ()
	{
		base.Awake ();
		instance = this;
	}

	private void Update (){
		
	}

	protected override void Start ()
	{
		base.Start ();
		MainAllController.instance.OnGetFavoriteVideo += Init;
		MainAllController.instance.OnLoggedOut += Reset;
	}

	public override void Init ()
	{
		videos = GetFavoriteVideo ();

		if (scroller != null){
			scroller.ReloadData ();
		}

		SortByCurrentStyle ();

		Debug.Log ("FavoriteVideoMenu.Count---------------:     " + videos.Count);

//		List<Video> videoToShow = GetFavoriteVideo ();
//
//		List<Video> currentUserVideo = new List<Video> ();
//
//		foreach (VideoUI UI in listObject) {
//			currentUserVideo.Add (UI.video);
//		}
//
//		// Case: Current FavoriteVideo contain more elements than server
//		// Trim elements that was deleted in server database
//		var TrimList = currentUserVideo.Where(p => !videoToShow.Any(p2 => p2.videoInfo.id == p.videoInfo.id)).ToList();
//		TrimUI (TrimList);
//
//		// Case: Current FavoriteVideo contain less elements than server
//		// Add elements that are present in server database, but not on local
//		var Addlist = videoToShow.Where(p => !currentUserVideo.Any(p2 => p2.videoInfo.id == p.videoInfo.id)).ToList();
//		AddUI (Addlist);
//
//		// update infomation from server
//		UpdateUI (videoToShow);

		UpdateNetworkConnectionUI ();
		UpdateNoVideoUI ();

    }

	public override void Refresh(){
        if (MainAllController.instance != null)
        {
            MainAllController.instance.UpdateFavorite();
        }
    }

	protected override bool CanBeAdded(Video video)
	{
		FavoriteVideo userVideo = video as FavoriteVideo;

		if (!userVideo.isPartial () || userVideo.isDownloaded ()) {
			return true;
		} else {

			GameObject videoDownloaderObj = GameObject.Find ("VideoDownLoader" + "-" + video.videoInfo.id);

			if (videoDownloaderObj != null) {
				return false;
			}
				
			return false;
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
		if (videos[dataIndex] is FavoriteVideo)
		{
			// header views
			return 500f;
		}

		return 0f;
	}

	public override EnhancedScrollerCellView GetCellView (EnhancedScroller scroller, int dataIndex, int cellIndex)
	{
		if (videos[dataIndex] is FavoriteVideo)
		{
			// first, we get a cell from the scroller by passing a prefab.
			// if the scroller finds one it can recycle it will do so, otherwise
			// it will create a new cell.
			videoUI = scroller.GetCellView(videoUIPrefab) as FavoriteVideoUI;

			// set the name of the game object to the cell's data index.
			// this is optional, but it helps up debug the objects in 
			// the scene hierarchy.
			videoUI.name = "FavoriteVideo " + dataIndex.ToString();
		}

		// we just pass the data to our cell's view which will update its UI
		videoUI.Setup(videos[dataIndex]);

		// return the cell to the scroller
		return videoUI;
	}

	#endregion


}
