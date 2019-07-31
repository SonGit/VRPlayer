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

	void Awake()
	{
		instance = this;
	}

	protected override void Start ()
	{
		base.Start ();
	}

	private void Update(){
		
	}

	#region RefreshVideo

	public override void Refresh()
	{
		print ("Storage Refresh()");

		if (LocalVideoManager.instance != null) {
			LocalVideoManager.instance.Load (OnGetLocalVideo);
		}
	}

	public void FastRefresh()
	{
		OnGetLocalVideo ();
	}
	#endregion

	public void OnGetLocalVideo()
	{
		print ("OnGetLocalVideo...................................................");
		videos = LocalVideoManager.instance.GetAllLocalVideos ();

		if (scroller != null){
			scroller.ReloadData ();
		}

		SortByCurrentStyle ();

		//ALso refresh the VR counterpart too
		if (VR_MainMenu.instance != null) {
			VR_MainMenu.instance.InitStorageMenu ();
		} else {
			Debug.LogError ("Exception! " + "  VR_MainMenu.instance null! ");
		}
			
		DisableNetworkAlert ();
		UpdateNoVideoUI ();
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
        return 500f;
    }

	public override EnhancedScrollerCellView GetCellView (EnhancedScroller scroller, int dataIndex, int cellIndex)
	{
		if (videos[dataIndex] is LocalVideo)
		{
			// first, we get a cell from the scroller by passing a prefab.
			// if the scroller finds one it can recycle it will do so, otherwise
			// it will create a new cell.
			videoUI = scroller.GetCellView(videoUIPrefab) as LocalVideoUI;

			// set the name of the game object to the cell's data index.
			// this is optional, but it helps up debug the objects in 
			// the scene hierarchy.
			videoUI.name = "LocalVideo " + dataIndex.ToString();
		}

//		print ("dataIndex" +  dataIndex + "  cellIndex " + cellIndex);

		// we just pass the data to our cell's view which will update its UI
		videoUI.Setup(videos[dataIndex]);

		// return the cell to the scroller
		return videoUI;
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
