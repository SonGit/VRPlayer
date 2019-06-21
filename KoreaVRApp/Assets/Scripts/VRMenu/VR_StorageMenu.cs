using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;

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
        OnGetLocalVideo();
    }
	#endregion

	public void OnGetLocalVideo()
	{
		videos = LocalVideoManager.instance.GetAllLocalVideos ();

		if (scroller != null){
			scroller.ReloadData ();
		}

		Debug.Log ("VR_StorageMenu.Count---------------:     " + videos.Count);

//		List<Video> videoToShows = LocalVideoManager.instance.GetAllLocalVideos ();
//
//		List<Video> currentLocalVideos = new List<Video> ();
//
//		foreach (VideoUI localVideoUI in listObject) {
//			currentLocalVideos.Add (localVideoUI.video as Video);
//		}
//
//		// Case: Current LocalVideos contain more elements than videoToShows
//		var TrimList = currentLocalVideos.Where(p => !videoToShows.Any(p2 => (p2 as LocalVideo).videoURL == (p as LocalVideo).videoURL)).ToList();
//		TrimUI (TrimList);
//
//		// Case: Current LocalVideos contain less elements than videoToShows
//		var Addlist = videoToShows.Where(p => !currentLocalVideos.Any(p2 => (p2 as LocalVideo).videoURL == (p as LocalVideo).videoURL)).ToList();
//		AddUI (Addlist);
			
//		currentPage = 0;
//
//		Rearrange (false);
//
//		SetupPageController ();
	}

	public override void Show()
	{
		ShowEnhancedScroller ();

		FastRefresh ();

		menuActive = true;

		Debug.Log ("Show()          " + menuActive);

		SetupPageController ();
	}

	public override void Hide ()
	{
		menuActive = false;

		HideEnhancedScroller ();
	}


	#region EnhancedScroller Handlers

	public override int GetNumberOfCells (EnhancedScroller scroller)
	{
		if (videos != null){
			//return Mathf.CeilToInt((float)videos.Count / (float)numberOfCellsPerRow);
			return 1;
		}
		return 0;
	}

	public override float GetCellViewSize (EnhancedScroller scroller, int dataIndex)
	{
		return 690f;
	}

	public override EnhancedScrollerCellView GetCellView (EnhancedScroller scroller, int dataIndex, int cellIndex)
	{
		// first, we get a cell from the scroller by passing a prefab.
		// if the scroller finds one it can recycle it will do so, otherwise
		// it will create a new cell.
		VR_VideoCellView vr_VideoCellView = scroller.GetCellView(videoUIPrefab) as VR_VideoCellView;

		vr_VideoCellView.name = "VR_LocalVideoCellView " + dataIndex.ToString();  //(dataIndex * numberOfCellsPerRow).ToString() + " to " + ((dataIndex * numberOfCellsPerRow) + numberOfCellsPerRow - 1).ToString();

		// pass in a reference to our data set with the offset for this cell
		vr_VideoCellView.SetData(videos, currentPage);

		// return the cell to the scroller
		return vr_VideoCellView;
	}

	#endregion


}
