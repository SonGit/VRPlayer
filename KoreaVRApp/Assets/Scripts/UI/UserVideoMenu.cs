using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using System;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;

public class UserVideoMenu : BasicMenuNavigation
{
	public static UserVideoMenu instance;

	public delegate void UserVideoDownloadCallback(UserVideoUI UI);

	private VideoUI videoUI;

	void Awake()
	{
		base.Awake ();
		instance = this;
	}

	private void Update (){

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


//		List<Video> videoToShow = GetUserVideo ();
//
//		List<Video> currentUserVideo = new List<Video> ();
//
//		foreach (VideoUI UI in listObject) {
//			currentUserVideo.Add (UI.video);
//		}
//
//		// Case: Current UserVideo contain more elements than server
//		// Trim elements that was deleted in server database
//		var TrimList = currentUserVideo.Where(p => !videoToShow.Any(p2 => p2.videoInfo.id == p.videoInfo.id)).ToList();
//		TrimUI (TrimList);
//
//		// Case: Current UserVideo contain less elements than server
//		// Add elements that are present in server database, but not on local
//		var Addlist = videoToShow.Where(p => !currentUserVideo.Any(p2 => p2.videoInfo.id == p.videoInfo.id)).ToList();
//		AddUI (Addlist);
//
//		// update infomation from server
//		UpdateUI (videoToShow);

		UpdateNetworkConnectionUI ();
		UpdateNoVideoUI ();
	}
		

	public override void Refresh()
	{
		MainAllController.instance.UpdateUserVideo ();
	}

	public void DeleteVideo(string filePath){
		if (filePath != null){
			File.Delete (filePath);
		}
	}

	protected override bool CanBeAdded(Video video)
	{
		UserVideo userVideo = video as UserVideo;

		if (!userVideo.isPartial ()) {
			return true;
		} else {
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
		if (videos[dataIndex] is UserVideo)
		{
			// header views
			return 500f;
		}

		return 0f;
	}

	public override EnhancedScrollerCellView GetCellView (EnhancedScroller scroller, int dataIndex, int cellIndex)
	{
		if (videos[dataIndex] is UserVideo)
		{
			// first, we get a cell from the scroller by passing a prefab.
			// if the scroller finds one it can recycle it will do so, otherwise
			// it will create a new cell.
			videoUI = scroller.GetCellView(videoUIPrefab) as UserVideoUI;

			// set the name of the game object to the cell's data index.
			// this is optional, but it helps up debug the objects in 
			// the scene hierarchy.
			videoUI.name = "UserVideo " + dataIndex.ToString();
		}

		// we just pass the data to our cell's view which will update its UI
		videoUI.Setup(videos[dataIndex]);

		// return the cell to the scroller
		return videoUI;
	}

	#endregion
		
}
