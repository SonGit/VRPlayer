using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using EnhancedUI.EnhancedScroller;

public class VR_DownloadMenu : VR_BasicMenu
{
	protected override void Start ()
	{
		base.Start ();
		//MainAllController.instance.OnGetUserVideo += Init;
		//MainAllController.instance.OnLoggedOut += Reset;
	}

	public override void Init()
	{
//		videos = GetUserVideo ();
//
//		if (scroller != null){
//			scroller.ReloadData ();
//		}


//		List<Video> videoToShow = GetUserVideo ();
//
//		List<Video> currentUserVideo = new List<Video> ();
//
//		foreach (VideoUI UI in listObject) {
//			currentUserVideo.Add (UI.video);
//		}
//
//		//		 Case: Current UserVideo contain more elements than server
//		//		 Trim elements that was deleted in server database
//		var TrimList = currentUserVideo.Where(p => !videoToShow.Any(p2 => p2.videoInfo.id == p.videoInfo.id)).ToList();
//		TrimUI (TrimList);
//
//		//		 Case: Current UserVideo contain less elements than server
//		//		 Add elements that are present in server database, but not on local
//		var Addlist = videoToShow.Where(p => !currentUserVideo.Any(p2 => p2.videoInfo.id == p.videoInfo.id)).ToList();
//		AddUI (Addlist);
	
	}

	public override void Refresh()
	{
		//Init ();
	}

	public override void FastRefresh()
	{
		//Init ();
	}

//	protected override bool CanBeAdded(Video video)
//	{
//		UserVideo userVideo = video as UserVideo;
//
//		if (userVideo.isPartial () && !userVideo.isDownloaded ()) {
//			return true;
//		} else {
//			return false;
//		}
//
//	}

}
