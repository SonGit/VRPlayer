using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VR_StorageMenu : VR_BasicMenu
{
	//public VR_NarrowTile[] narrowTiles;

	protected override void Awake()
	{
		//tiles = tilesHolder.GetComponentsInChildren<VR_WideTile> ();
		//narrowTiles = tilesHolder.GetComponentsInChildren<VR_NarrowTile> ();
	}

	protected override void Start ()
	{
		base.Start ();
	}

	private void Update(){

	}

	#region RefreshVideo
	public override void Refresh(){
		
		//MAX_ITEM_PER_PAGE = tiles.Length;

		if (LocalVideoManager.instance != null) {
			LocalVideoManager.instance.Load (OnGetLocalVideo);
		}
	}

	public override void FastRefresh()
	{
		//MAX_ITEM_PER_PAGE = tiles.Length;
//
//		foreach (VR_NarrowTile narrowTile in narrowTiles) {
//			narrowTile.gameObject.SetActive (false);
//		}

		OnGetLocalVideo ();
	}
	#endregion

	public void OnGetLocalVideo()
	{
		List<Video> videoToShows = LocalVideoManager.instance.GetAllLocalVideos ();

		List<Video> currentLocalVideos = new List<Video> ();

		foreach (VideoUI localVideoUI in listObject) {
			currentLocalVideos.Add (localVideoUI.video as Video);
		}

		// Case: Current LocalVideos contain more elements than videoToShows
		var TrimList = currentLocalVideos.Where(p => !videoToShows.Any(p2 => (p2 as LocalVideo).videoURL == (p as LocalVideo).videoURL)).ToList();
		TrimUI (TrimList);

		// Case: Current LocalVideos contain less elements than videoToShows
		var Addlist = videoToShows.Where(p => !currentLocalVideos.Any(p2 => (p2 as LocalVideo).videoURL == (p as LocalVideo).videoURL)).ToList();
		AddUI (Addlist);

		currentPage = 0;

		if(VR_NavMenuManager.instance != null)
		VR_NavMenuManager.instance.OnClick_PhoneVideoMenu ();

		Rearrange (false);

		SetupPageController ();
	}

	public override void Show()
	{
		FastRefresh ();

//		foreach (VR_NarrowTile narrowTile in narrowTiles) {
//			narrowTile.gameObject.SetActive (false);
//		}
		menuActive = true;

		Debug.Log ("Show()          " + menuActive);

		SetupPageController ();
	}


}
