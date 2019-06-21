using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using EnhancedUI.EnhancedScroller;

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
		MainAllController.instance.OnGetFavoriteVideo += SetupEnhancedScroller;
		//MainAllController.instance.OnLoggedOut += Reset;
	}

	public override void Init()
	{
		videos = GetFavoriteVideo ();

		if (scroller != null){
			scroller.ReloadData ();
		}

		Debug.Log ("VR_FavoriteMenu.Count---------------:     " + videos.Count);

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
//
//
//		Debug.LogError ("Init FavoriteVideo completed!!!!!!!!      " + listObject.Count);
//
//		Rearrange (false);
//
//		Debug.Log("SetupPageController");
//		SetupPageController ();
	}

	public override void Refresh(){
		if(MainAllController.instance != null){
			MainAllController.instance.UpdateFavorite ();
		}
	}

	public override void FastRefresh()
	{
		Init ();
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

	public override void Show()
	{
		menuActive = true;

		ShowEnhancedScroller ();

		Refresh ();
	}

	public override void SetupEnhancedScroller ()
	{
		if (menuActive){
			FastRefresh ();

			Debug.Log ("Show()          " + menuActive);

			SetupPageController ();

			if (VR_MainMenu.instance != null){
				VR_MainMenu.instance.VR_CheckNovideos ();
			}
		}
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

		vr_VideoCellView.name = "VR_FavoriteVideoCellView " + dataIndex.ToString();  //(dataIndex * numberOfCellsPerRow).ToString() + " to " + ((dataIndex * numberOfCellsPerRow) + numberOfCellsPerRow - 1).ToString();

		// pass in a reference to our data set with the offset for this cell
		vr_VideoCellView.SetData(videos, currentPage);

		// return the cell to the scroller
		return vr_VideoCellView;
	}

	#endregion
}
