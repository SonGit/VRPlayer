using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;

public class VR_LocalVideoListController : MonoBehaviour
{
//	public static VR_LocalVideoListController instance;
//	[SerializeField]
//	private GameObject localVRVideoPrefab = null;
//	private LocalVideo[] localVideos; // list all video load
//	private LocalVideoUI[] localVideoContents; // list Scroll View content
//	public VR_VideoTiles[] videoTiles;
//
//
//
//
//	private List<string> nameLocalVideos = new List<string>(); // list all element names of list localvideos
//	private List<string> nameLocalVideoContents = new List<string>(); // list all element names of list localVideoContents
//	private List<string> results = new List<string>(); // results of nameLocalVideoContents.Except (nameLocalVideos)
//
//	void Awake(){
//		if (instance == null) {
//			instance = this;
//		}
//	}
//
//	void Start()
//	{
//		RefreshVideo ();
//		videoTiles = GetComponentsInChildren<VR_VideoTiles> ();
//	}
//
//	// Update is called once per frame
//	void Update()
//	{
//		if (VR_LocalVideoManager.instance != null && VR_LocalVideoManager.instance.IsDisplayLocalVideo) {
//			DisplayVideoUI ();
//			//			if (ScreenLoading.instance != null) {
//			//				ScreenLoading.instance.Stop ();
//			//			}
//			VR_LocalVideoManager.instance.IsDisplayLocalVideo =false;
//		}
//	}
//
//
//
//	#region RefreshVideo
//	public void RefreshVideo(){
//		//		if (ScreenLoading.instance != null) {
//		//			ScreenLoading.instance.Play ();
//		//		}
//
//		if (LocalVideoManager.instance != null) {
//			LocalVideoManager.instance.Load ();
//		}
//	}
//	#endregion
//
//
//	#region DisplayVideoUI
//	public void DisplayVideoUI()
//	{
////		if (VR_LocalVideoManager.instance != null) {
////			localVideos = VR_LocalVideoManager.instance.GetAllLocalVideos ();
//////			UpdateElementlocalVideos ();
////
////			for (int i = VR_PageController.instance.currentPage*6-6; i < VR_PageController.instance.currentPage*6; i++) {
//////				if (!localVideos [i].isDuplicate) {
////					if (i < localVideos.Length) {
////					Debug.Log (i);
////						AddLocalVideoUI (localVideos [i], i % 6);
////					}
////
//////				}else {
//////					UpdateVideo ();
////				}
////			}
//
////			UpdateElementlocalVideoContents ();
//
//	}
//
//
//	private void AddLocalVideoUI(LocalVideo localVideo, int id)
//	{
//		GameObject obj = (GameObject)Instantiate (localVRVideoPrefab);
//		VR_LocalVideoUI vr_LocalVideoUI = obj.GetComponent<VR_LocalVideoUI> ();
//		for (int i = 0; i < 5; i++) {
//			if (vr_LocalVideoUI != null) {
//				//vr_LocalVideoUI.Setup (localVideo,this);
//
//				obj.transform.SetParent (videoTiles[id].transform,false);
//			} else {
//				Debug.LogError ("Wrong Prefab!");
//			}
//		}
//	}
//	#endregion
//
//	public void ClearDisplay(){
//		VR_LocalVideoUI[] obj = GetComponentsInChildren<VR_LocalVideoUI>();
//		for (int i = 0; i < obj.Length; i++) {
//			Destroy (obj[i].gameObject);
//		}
//	}
//
//	#region DeleteVideo
//	public void DeleteVideo(string filePath, LocalVideoUI localVideoUI){
//		if (filePath != null){
//			File.Delete (filePath);
//			if (LocalVideoManager.instance != null) {
//				DeleteLocalVideoUI (localVideoUI);
//				RefreshVideo ();
//			}
//		}
//	}
//
//	public void DeleteLocalVideoUI(LocalVideoUI localVideoUI)
//	{
//		if (localVideoUI != null) {
//			Destroy (localVideoUI.gameObject);
//		}
//	}
//	#endregion
//
//
//	#region UpdateVideo	
//	private void UpdateElementlocalVideos(){
//		nameLocalVideos = new List<string>();
//		if (localVideos.Length > 0){
//			for (int i = 0; i < localVideos.Length; i++) {
//				nameLocalVideos.Add (localVideos [i].videoName);
//			}
//		}
//	}
//
//	private void UpdateElementlocalVideoContents(){
//		localVideoContents = this.GetComponentsInChildren<LocalVideoUI> ();
//
//		nameLocalVideoContents = new List<string> ();
//		if (localVideoContents.Length > 0){
//			for (int i = 0; i < localVideoContents.Length; i++) {
//				//nameLocalVideoContents.Add (localVideoContents[i].videoTitle.text);
//			}
//		}
//	}
//
//	void UpdateVideo()
//	{
//		results = nameLocalVideoContents.Except (nameLocalVideos).ToList();
//		if (results.Count > 0){
//			for (int j = 0; j < results.Count; j++) {
//				for (int k = 0; k < localVideoContents.Length; k++) {
////					if (results[j] == localVideoContents[k].videoTitle.text){
////						DeleteLocalVideoUI (localVideoContents[k]);
////					}
//				}
//			}
//		}
//	}
//	#endregion
}