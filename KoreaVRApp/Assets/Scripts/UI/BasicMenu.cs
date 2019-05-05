using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public enum SortStyle
{
	SORT_BY_DATE,
	SORT_BY_NAME,
	SORT_BY_SIZE,
}

public class BasicMenu : MonoBehaviour
{
	[SerializeField] private GameObject Root;

	[SerializeField] private GameObject networkConnectionObj;

	[SerializeField] private GameObject noVideoObj;

	[SerializeField] protected GameObject videoUIPrefab;

	[SerializeField] protected VerticalLayoutGroup verticalGrid;

	[SerializeField] protected SortStyle currentSortStyle;

	[SerializeField] protected Canvas canvas;

	public bool isNoVideo;

	private CanvasGroup _canvasGroup;

	protected List<VideoUI> listObject = new List<VideoUI>();

	protected virtual void Awake()
	{

	}

	protected virtual void Start()
	{
		
	}

	public virtual void Init()
	{
		Debug.Log ("Init");
	}

	public void SetActive(bool value)
	{
		if (canvas == null) {
			canvas = transform.GetComponentInParent<Canvas> ();
		}

		if (canvas != null) {
			canvas.enabled = value;
		} else {
			Debug.Log ("Canvas is null!");
		}
//		if (Root != null){
//			Root.SetActive(value);
//		}
//
//		StopAllCoroutines();
//
//		if (_canvasGroup == null) {
//			_canvasGroup = Root.AddComponent<CanvasGroup>();
//			_canvasGroup.alpha = 0.0f;
//		}
//
//		StartCoroutine(value
//			? Utils.FadeIn(_canvasGroup, 1.0f, 0.5f)
//			: Utils.FadeOut(_canvasGroup, 0.0f, 0.5f));
	}

	#region CheckNetworkConnection

	public bool CheckNetworkConnection (){
		print (Application.internetReachability);
		if(Application.internetReachability == NetworkReachability.NotReachable)
		{
			return false;
		}

		return true;
	}

	public void UpdateNetworkConnectionUI(){
//		bool isConnect = CheckNetworkConnection ();
//
//		if (isConnect) {
//			DisableNetworkAlert ();
//		} else {
//			EnableNetworkAlert ();
//		}
	}

	public void DisableNetworkAlert(){
		if (networkConnectionObj != null) {
			networkConnectionObj.SetActive (false);
		}
	}

	public void EnableNetworkAlert(){
		if (networkConnectionObj != null) {
			networkConnectionObj.SetActive (true);
		}
	}

	#endregion


	#region CheckNoVideos

	public bool CheckNoVideos(){

		if (verticalGrid.transform.childCount > 0){
			VideoUI[] videoUIs = verticalGrid.GetComponentsInChildren<VideoUI>();

			foreach (var videoUI in videoUIs) {
				if (videoUI.gameObject.activeSelf){
					Debug.Log (videoUI.gameObject.name);
					return false;
					break;
				}
			}
		}

		return true;
	}

	public void UpdateNoVideoUI(){
		isNoVideo = CheckNoVideos ();

		if (isNoVideo) {
			EnableNoVideoAlert ();
		} else {
			DisableNoVideoAlert ();
		}
	}

	public void DisableNoVideoAlert(){
		if (noVideoObj != null) {
			noVideoObj.SetActive (false);
		}
	}

	public void EnableNoVideoAlert(){
		if (noVideoObj != null) {
			noVideoObj.SetActive (true);
		}
	}

	#endregion

	public virtual void Refresh()
	{

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
			ui.Destroy ();
		}
	}
		
	protected virtual void AddUI(List<Video> addThese)
	{
		try
		{
			for(int i = 0 ; i < addThese.Count ; i ++)
			{
				if (CanBeAdded (addThese[i])) {
					AddVideoUI (addThese[i]);
				}
			}

			SortByCurrentStyle ();

		}catch(System.Exception e) {

		} finally{
			//Handheld.StopActivityIndicator ();
		}

	}

	protected virtual void AddVideoUI(Video video)
	{
		VideoUI userVideoUI = CreateUserVideoUI ();

		if (userVideoUI != null) {
			
			userVideoUI.Setup (video);

			if (verticalGrid == null) {
				verticalGrid = this.GetComponentInChildren<VerticalLayoutGroup> ();
			}

			userVideoUI.transform.SetParent (verticalGrid.transform,false);

			listObject.Add (userVideoUI);

		} else {
			Debug.LogError ("Wrong Prefab!");
		}
			
	}

	protected virtual VideoUI CreateUserVideoUI()
	{
		if (this is StorageMenu) {
			return ObjectPool.instance.GetLocalVideoUI ();
		}

		if (this is UserVideoMenu) {
			return ObjectPool.instance.GetUserVideoUI ();
		}

		if (this is DownloadMenu) {
			return ObjectPool.instance.GetDownloadVideoUI ();
		}

		if (this is InboxMenu) {
			return ObjectPool.instance.GetInboxVideoUI ();
		}

		if (this is FavoriteVideoMenu) {
			return ObjectPool.instance.GetFavoriteVideoUI ();
		}

		return null;
	}

	protected virtual void UpdateUI(List<Video> videoToUpdate)
	{
		for (int i = 0; i < videoToUpdate.Count; i++) {
			for (int y = 0; y < listObject.Count; y++)
			{
				if (listObject [y].video.videoInfo.id == videoToUpdate [i].videoInfo.id) {
					listObject [y].Setup (videoToUpdate [i]);
				}
			}
		}
	}

	protected virtual bool CanBeAdded(Video video)
	{
		return true;
	}


	public virtual void Reset()
	{
		for (int i = 0; i < listObject.Count; i++) {
			listObject [i].Destroy ();
		}
		listObject = new List<VideoUI>();
	}

	protected virtual List<Video> GetUserVideo()
	{
		User userReference = MainAllController.instance.user;

		if (userReference == null) {
			Debug.Log ("User has not logged in!");
			return new List<Video> ();
		}

		Video[] videos = userReference.userVideos.ToArray ();

		List<Video> videoToShow = new List<Video> ();

		foreach (Video video in videos) {
			if(CanBeAdded(video))
				videoToShow.Add (video);
		}

		return videoToShow;
	}

	protected List<Video> GetFavoriteVideo()
	{
		User userReference = MainAllController.instance.user;

		Video[] videos = userReference.favoriteVideos.ToArray ();

		List<Video> videoToShow = new List<Video> ();

		foreach (Video video in videos) {
			if(CanBeAdded(video))
				videoToShow.Add (video);
		}

		return videoToShow;
	}

	public void RemoveUI(VideoUI videoUI)
	{
		listObject.Remove (videoUI);
		videoUI.Destroy ();
	}

	public void RemoveUIPerma(VideoUI videoUI)
	{
		listObject.Remove (videoUI);
		Destroy (videoUI.gameObject);
	}

	#region Sort

	public virtual void SortByCurrentStyle()
	{
		switch (currentSortStyle) {
		case SortStyle.SORT_BY_DATE:
			SortByDate ();
			break;
		case SortStyle.SORT_BY_NAME:
			SortByName ();
			break;
		case SortStyle.SORT_BY_SIZE:
			SortBySize ();
			break;
		}
	}

	public void SortByName()
	{
		listObject = listObject.OrderBy (obj => obj.video.videoInfo.video_name).ToList();
		for (int i = 0; i < listObject.Count; i++)
		{
			listObject[i].transform.SetSiblingIndex(i);
		}
		currentSortStyle = SortStyle.SORT_BY_NAME;
	}

	public void SortByDate()
	{
		listObject = listObject.OrderByDescending (obj => obj.video.videoInfo.dateTime).ToList();
		for (int i = 0; i < listObject.Count; i++)
		{
			listObject[i].transform.SetSiblingIndex(i);
		}
		currentSortStyle = SortStyle.SORT_BY_DATE;
	}

	public void SortBySize()
	{
		listObject = listObject.OrderBy(obj => obj.video.videoInfo.size).ToList();
		for (int i = 0; i < listObject.Count; i++)
		{
			listObject[i].transform.SetSiblingIndex(i);
		}
		currentSortStyle = SortStyle.SORT_BY_SIZE;
	}

	#endregion

	public void CheckThumbnail()
	{
//		Debug.LogError ("CheckThumbnailCheckThumbnailCheckThumbnailCheckThumbnail");
//		foreach (VideoUI uiObject in listObject) {
//			uiObject.CheckAndDownloadThumbnail ();
//		}
	}
		

	/// <summary>
	/// Get video thumnail that have been generated
	/// Use for re-using generated thumbnail
	/// </summary>
	/// <returns>The video thumbnail.</returns>
	public Texture2D GetVideoThumbnail(Video video)
	{
		if (video == null) {
			return null;
		}

		for (int i = 0; i < listObject.Count; i++) {
			if (video.videoInfo.id == listObject [i].video.videoInfo.id) {

				return listObject [i].thumbnailTexture;
			}
		}
		return null;
	}
}