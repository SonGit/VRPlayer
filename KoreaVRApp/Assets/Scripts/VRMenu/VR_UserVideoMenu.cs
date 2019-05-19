using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class VR_UserVideoMenu : VR_BasicMenu
{
	public static VR_UserVideoMenu instance;
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
		//MainAllController.instance.OnGetUserVideo += Init;
		//MainAllController.instance.OnLoggedOut += Reset;
	}

	public override void Init()
	{
		List<Video> videoToShow = GetUserVideo ();

		List<Video> currentUserVideo = new List<Video> ();

		foreach (VideoUI UI in listObject) {
			currentUserVideo.Add (UI.video);

		}

		// Case: Current UserVideo contain more elements than server
		// Trim elements that was deleted in server database
		var TrimList = currentUserVideo.Where(p => !videoToShow.Any(p2 => p2.videoInfo.id == p.videoInfo.id)).ToList();
		TrimUI (TrimList);

		// Case: Current UserVideo contain less elements than server
		// Add elements that are present in server database, but not on local
		var Addlist = videoToShow.Where(p => !currentUserVideo.Any(p2 => p2.videoInfo.id == p.videoInfo.id)).ToList();
		AddUI (Addlist);

		// update infomation from server
		UpdateUI (videoToShow);

		Rearrange (false);
		SetupPageController ();
	}


	public override void Refresh()
	{
		MainAllController.instance.UpdateUserVideo ();
	}

	public override void FastRefresh()
	{
		base.FastRefresh ();
		Init ();
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

}
