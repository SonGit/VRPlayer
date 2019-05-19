using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VR_FavoriteMenu : VR_BasicMenu
{
	
	public static VR_FavoriteMenu instance;

	void Awake()
	{
		base.Awake ();
		instance = this;
	}

	protected override void Start ()
	{
		base.Start ();
		//MainAllController.instance.OnGetFavoriteVideo += Init;
		//MainAllController.instance.OnLoggedOut += Reset;
	}

	void Update()
	{

	}

	public override void Init()
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


		Debug.LogError ("Init FavoriteVideo completed!!!!!!!!      " + listObject.Count);

		Rearrange (false);

		Debug.Log("SetupPageController");
		SetupPageController ();
	}

	public override void Refresh(){
		if(MainAllController.instance != null){
			MainAllController.instance.UpdateFavorite ();
		}
	}

	public override void FastRefresh()
	{
		base.FastRefresh ();
		Init ();
	}
}
