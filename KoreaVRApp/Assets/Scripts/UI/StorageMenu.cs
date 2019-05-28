using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;

public class StorageMenu : BasicMenuNavigation
{
	public static StorageMenu instance;

	private VideoUI videoUI;

	void Awake()
	{
		instance = this;
	}

	protected override void Start ()
	{
		base.Start ();

		RefreshVideo ();
	}

	private void Update(){
		if (Input.GetKeyDown (KeyCode.A)) {
			videos = new List<Video> ();
			scroller.ReloadData ();
		}
	}

	#region RefreshVideo
	public void RefreshVideo(){
//		if (ScreenLoading.instance != null) {
//			ScreenLoading.instance.Play ();
//		}

		if (LocalVideoManager.instance != null) {
			LocalVideoManager.instance.Load (OnGetLocalVideo);
		}
	}

	public void FastRefresh()
	{
		OnGetLocalVideo ();
	}
	#endregion

	bool first = true;

	public void OnGetLocalVideo()
	{
		videos = LocalVideoManager.instance.GetAllLocalVideos ();

		if (scroller != null) {
			scroller.ReloadData ();
		}

		//SortByCurrentStyle ();

		////ALso refresh the VR counterpart too
		//if (VR_MainMenu.instance != null) {
		//	VR_MainMenu.instance.InitStorageMenu ();
		//} else {
		//	Debug.LogError ("Exception! " + "  VR_MainMenu.instance null! ");
		//}
			
		//DisableNetworkAlert ();
		//UpdateNoVideoUI ();

	}

	public void Test()
	{
		videos.RemoveAt (videos.Count - 1);
		//videos.Add (new LocalVideo( "videoName ","videoName ","videoName ","videoName ", "videoName "));

		if (scroller != null) {
			scroller.ReloadData ();
		}
	}
		

	public override void Refresh()
	{
		print ("Storage Refresh()");
		RefreshVideo ();
	}

	#region EnhancedScroller Handlers

	public override int GetNumberOfCells (EnhancedScroller scroller)
	{
        return videos.Count;
    }

	public override float GetCellViewSize (EnhancedScroller scroller, int dataIndex)
	{
        return 500f;

    }

	public override EnhancedScrollerCellView GetCellView (EnhancedScroller scroller, int dataIndex, int cellIndex)
	{
        // first, we get a cell from the scroller by passing a prefab.
        // if the scroller finds one it can recycle it will do so, otherwise
        // it will create a new cell.
        LocalVideoUI cellView = scroller.GetCellView(videoUIPrefab) as LocalVideoUI;

        // set the name of the game object to the cell's data index.
        // this is optional, but it helps up debug the objects in 
        // the scene hierarchy.
        cellView.name = "Cell " + dataIndex.ToString();

        // in this example, we just pass the data to our cell's view which will update its UI
        // cellView.SetData(_data[dataIndex]);
        cellView.Setup(videos[dataIndex]);
        // return the cell to the scroller
        return cellView;
    }
		
	#endregion

	public override void SortByName()
	{
		videos = videos.OrderBy(obj => (obj as LocalVideo).videoName).ToList();

		scroller.RefreshActiveCellViews();

		currentSortStyle = SortStyle.SORT_BY_NAME;
	}

	public override void SortByDate()
	{
		videos = videos.OrderBy(obj => (obj as LocalVideo).videoDate).ToList();

		scroller.RefreshActiveCellViews();

		currentSortStyle = SortStyle.SORT_BY_DATE;
	}

	public override void SortBySize()
	{
		videos = videos.OrderBy(obj => (obj as LocalVideo).videoSize).ToList();

		scroller.RefreshActiveCellViews();

		currentSortStyle = SortStyle.SORT_BY_SIZE;
	}

}
