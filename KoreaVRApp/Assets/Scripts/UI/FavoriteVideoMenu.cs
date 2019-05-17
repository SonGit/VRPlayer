using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;
using System;
using System.Linq;

public class FavoriteVideoMenu : BasicMenuNavigation
{
	public List<FavoriteVideo> favoriteVideos = new List<FavoriteVideo>();

	public static FavoriteVideoMenu instance;

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
		MainAllController.instance.OnGetFavoriteVideo += Init;
		MainAllController.instance.OnLoggedOut += Reset;
	}

	public void Init()
	{
		List<Video> videoToShow = GetFavoriteVideo ();

		List<Video> currentUserVideo = new List<Video> ();

		foreach (VideoUI UI in listObject) {
			currentUserVideo.Add (UI.video);
		}

		// Case: Current FavoriteVideo contain more elements than server
		// Trim elements that was deleted in server database
		var TrimList = currentUserVideo.Where(p => !videoToShow.Any(p2 => p2.videoInfo.id == p.videoInfo.id)).ToList();
		TrimUI (TrimList);

		// Case: Current FavoriteVideo contain less elements than server
		// Add elements that are present in server database, but not on local
		var Addlist = videoToShow.Where(p => !currentUserVideo.Any(p2 => p2.videoInfo.id == p.videoInfo.id)).ToList();
		AddUI (Addlist);

		// update infomation from server
		UpdateUI (videoToShow);

		UpdateNetworkConnectionUI ();
		UpdateNoVideoUI ();
	}

	public override void Refresh(){
		if(MainAllController.instance != null){
			MainAllController.instance.UpdateFavorite ();
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


}
