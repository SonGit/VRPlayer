﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using EnhancedUI.EnhancedScroller;

public class VR_InboxMenu : VR_BasicMenu
{
	private bool m_IsDisplayUserVideo;

	void Awake()
	{
		base.Awake ();
	}

	private void Update (){

	}

	/// <summary>
	/// DisplayUserVideo?
	/// </summary>
	public bool IsDisplayUserVideo
	{
		get { return m_IsDisplayUserVideo; }
		set { m_IsDisplayUserVideo = value; }
	}

	protected override void Start ()
	{
		base.Start ();
		//MainAllController.instance.OnGetInboxVideo += SetupEnhancedScroller;
		//MainAllController.instance.OnLoggedOut += Reset;

	}

	public override void Init()
	{
		videos = GetUserVideo ();

		if (scroller != null){
			scroller.ReloadData ();
		}

		Debug.Log ("VR_InboxMenu.Count---------------:     " + videos.Count);

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
//		
//		Rearrange (false);
//
//		SetupPageController ();
	}


	public override void Refresh()
	{
//		if (MainAllController.instance != null){
//			MainAllController.instance.UpdateInboxVideo ();
//		}
	}

	public override void FastRefresh()
	{
		Init ();
	}

	protected override bool CanBeAdded(Video video)
	{
		UserVideo userVideo = video as UserVideo;

		if (userVideo.isPartial () && userVideo.isDownloaded ()) {
			return true;
		} else {
			return false;
		}

	}

	public override void Show()
	{
		menuActive = true;

		ShowEnhancedScroller ();

		SetupEnhancedScroller ();
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

		vr_VideoCellView.name = "VR_InboxVideoCellView " + dataIndex.ToString();  //(dataIndex * numberOfCellsPerRow).ToString() + " to " + ((dataIndex * numberOfCellsPerRow) + numberOfCellsPerRow - 1).ToString();

		// pass in a reference to our data set with the offset for this cell
		vr_VideoCellView.SetData(videos, currentPage);

		// return the cell to the scroller
		return vr_VideoCellView;
	}

	#endregion
}
