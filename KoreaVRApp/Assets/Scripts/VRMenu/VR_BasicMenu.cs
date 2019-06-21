using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_BasicMenu : BasicMenu
{
	public static int MAX_ITEM_PER_PAGE = 8;

	[SerializeField]
	protected VR_PageController pageController;

//	[SerializeField]
//	protected Transform grid;

	protected int currentPage;

	[SerializeField] protected bool menuActive;

    protected bool firstTime;

    protected override void Awake()
	{
		//tiles = tilesHolder.GetComponentsInChildren<VR_VideoTiles> ();
		pageController = Object.FindObjectOfType<VR_PageController> ();

		//MainAllController.instance.OnLoggedOut += Reset;
	}
		
	public void ShowPage(int pageNo)
	{
		if (pageNo < 0) {
			Debug.LogError ("Invalid Page Number! " + pageNo);
			pageNo = 0;
		}

		currentPage = pageNo;

//		int start = pageNo * MAX_ITEM_PER_PAGE;
//
//		int end = start + MAX_ITEM_PER_PAGE;
//
//		if (end > listObject.Count) {
//			end = listObject.Count;
//		}
//
//		int count = 0;
//
//		for (int i = 0; i < listObject.Count; i++) {
//
//			if (start - 1 < i && i < end) {
//
//				if (listObject [i].gameObject != null) {
//					
//					listObject [i].gameObject.SetActive (true);
//
//					// Reshuffle the tiles position
//					//listObject [i].transform.SetParent (tiles[count].transform,false);
//
//				}
//				count++;
//			}
//			else {
//
//				if (listObject [i].gameObject != null) {
//					listObject [i].gameObject.SetActive (false);
//				}
//		
//			}
//		}
	}

	protected override void AddUI(List<Video> addThese)
	{
		foreach (Video UI in addThese) {

			if (CanBeAdded (UI)) {
				AddVideoUI (UI);
			}

		}
	}

	protected override void AddVideoUI(Video video)
	{
		GameObject obj = (GameObject)Instantiate (videoUIPrefab.gameObject);
		VideoUI userVideoUI = obj.GetComponent<VideoUI> ();

		if (userVideoUI != null) {

			userVideoUI.Setup (video);

			listObject.Add (userVideoUI);

//			if (count >= MAX_ITEM_PER_PAGE) {
//				count = 0;
//			}

			//obj.transform.SetParent (grid,false);

			obj.name = video.videoInfo.id;

//			count++;

			obj.SetActive (false);

		} else {
			Debug.LogError ("Wrong Prefab!");
		}

	}

	protected virtual void TrimUI(List<Video> destroyThese)
	{
		List<VideoUI> videoUIToDestroy = new List<VideoUI> ();

		for (int i = 0; i < destroyThese.Count; i++) {
			for (int y = 0; y < listObject.Count; y++) {
				if (listObject [y].video.videoInfo.id == destroyThese [i].videoInfo.id) {
					videoUIToDestroy.Add (listObject [y]);
				}
			}
		}

		foreach (VideoUI ui in videoUIToDestroy) {
			listObject.Remove (ui);
			Destroy (ui.gameObject);
		}
	}

	public virtual void Show()
	{
		//MAX_ITEM_PER_PAGE = tiles.Length;
		//menuActive = true;

		//FastRefresh ();

//		foreach (VR_VideoTiles tile in tiles) {
//			tile.gameObject.SetActive (true);
//		}

//		for (int i = 0; i < listObject.Count; i++) {
//			listObject [i].gameObject.SetActive (true);
//		}
//
//		SetupPageController ();
	}

	public virtual void Hide()
	{
//		for (int i = 0; i < listObject.Count; i++) {
//			listObject [i].gameObject.SetActive (false);
//		}

		//menuActive = false;
	}

	protected void SetupPageController()
	{
		if (menuActive) {
			Debug.Log ("SetupPageController()          " + menuActive);
			if (pageController != null) {
				//pageController.Refresh (listObject.Count);
				pageController.Refresh (videos.Count);
			}
			currentPage = 0;
		}
	}

	public virtual void FastRefresh()
	{
		//MAX_ITEM_PER_PAGE = tiles.Length;
	}

	public override void Reset()
	{
//		for (int i = 0; i < listObject.Count; i++) {
//			Destroy (listObject[i].gameObject);
//		}
//		listObject = new List<VideoUI>();
	}

	public void Rearrange(bool gotoLastPage = true)
	{
//		int start = currentPage * MAX_ITEM_PER_PAGE;
//
//		int end = start + MAX_ITEM_PER_PAGE;
//
//		if (end > listObject.Count) {
//			end = listObject.Count;
//		}
//
//		int count = 0;
//
//		for (int i = 0; i < listObject.Count; i++) {
//
//			if (start - 1 < i && i < end) {
//
//				// Reshuffle the tiles position
//				//listObject [i].transform.SetParent (tiles[count].transform,false);
//
//				count++;
//			}
//		}

		if(gotoLastPage && menuActive)
		pageController.RefreshLastPage (listObject.Count);
	}

	public void HideEnhancedScroller(){
		
		if (scroller != null) {
			scroller.gameObject.SetActive (false);
		}else{
			Debug.LogError ("Null................");
		}
	}

	public void ShowEnhancedScroller(){

		if (scroller != null) {
			scroller.gameObject.SetActive (true);
		}else{
			Debug.LogError ("Null................");
		}
	}


	#region CheckNoVideos

	public bool VR_CheckNoVideos(){

		if(scroller != null){
			VideoUI[] videoUIs = scroller.GetComponentsInChildren<VideoUI>();

			foreach (var videoUI in videoUIs) {
				if (videoUI.root.gameObject.activeSelf){
					return false;
					break;
				}
			}
		}
		return true;
	}

	#endregion

	public virtual void SetupEnhancedScroller(){

	}


}
