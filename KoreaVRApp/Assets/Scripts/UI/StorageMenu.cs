using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System;

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

		RefreshVideo ();
	}

	private void Update(){
		
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

	public void OnGetLocalVideo()
	{
		List<Video> videoToShows = LocalVideoManager.instance.GetAllLocalVideos ();

		Debug.LogError ("videoToShows " +  videoToShows.Count);

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

	#region DeleteVideo
	public void DeleteVideo(string filePath, VideoUI localVideoUI){
		
		DeleteLocalVideoUI (localVideoUI);

			try
			{
				File.Delete (filePath);
			} catch (Exception e) {
				Debug.Log ("DeleteVideo exception " + e.Message);
			}

			Refresh ();

	}

	public void DeleteLocalVideoUI(VideoUI localVideoUI)
	{
		if (localVideoUI != null) {
			localVideoUI.Destroy ();
			listObject.Remove (localVideoUI);
		}
	}
	#endregion

	public override void 
	Refresh()
	{
		print ("Storage Refresh()");
		RefreshVideo ();
	}

	public void SortByName_Local()
	{
		listObject = listObject.OrderBy (obj => (obj.video as LocalVideo).videoName).ToList();
		for (int i = 0; i < listObject.Count; i++)
		{
			listObject[i].transform.SetSiblingIndex(i);
		}
		currentSortStyle = SortStyle.SORT_BY_NAME;
	}

	public void SortByDate_Local()
	{
		listObject = listObject.OrderByDescending (obj => (obj.video as LocalVideo).videoDate).ToList();
		for (int i = 0; i < listObject.Count; i++)
		{
			listObject[i].transform.SetSiblingIndex(i);
		}
		currentSortStyle = SortStyle.SORT_BY_DATE;
	}

	public void SortBySize_Local()
	{
		listObject = listObject.OrderBy(obj => (obj.video as LocalVideo).videoSize).ToList();
		for (int i = 0; i < listObject.Count; i++)
		{
			listObject[i].transform.SetSiblingIndex(i);
		}
		currentSortStyle = SortStyle.SORT_BY_SIZE;
	}

	public override void SortByCurrentStyle()
	{
		switch (currentSortStyle) {
		case SortStyle.SORT_BY_DATE:
			SortByDate_Local ();
			break;
		case SortStyle.SORT_BY_NAME:
			SortByName_Local ();
			break;
		case SortStyle.SORT_BY_SIZE:
			SortBySize_Local ();
			break;
		}
	}

}
