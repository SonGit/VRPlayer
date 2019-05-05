using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class LocalVideoListController : MonoBehaviour
{
//	[SerializeField]
//	private GameObject localVideoPrefab = null;
//	private LocalVideo[] localVideos; // list all video load
//	private LocalVideoUI[] localVideoContents; // list Scroll View content
//
//	private List<string> nameLocalVideos = new List<string>(); // list all element names of list localvideos
//	private List<string> nameLocalVideoContents = new List<string>(); // list all element names of list localVideoContents
//	private List<string> results = new List<string>(); // results of nameLocalVideoContents.Except (nameLocalVideos)
//
//    // Start is called before the first frame update
//	void Start()
//    {
//		RefreshVideo ();
//    }
//
//    // Update is called once per frame
//    void Update()
//    {
//		if (LocalVideoManager.instance != null && LocalVideoManager.instance.IsDisplayLocalVideo) {
//			DisplayVideoUI ();
//
////			if (ScreenLoading.instance != null) {
////				ScreenLoading.instance.Stop ();
////			}
//		}
//    }
//
//	#region RefreshVideo
//	public void RefreshVideo(){
////		if (ScreenLoading.instance != null) {
////			ScreenLoading.instance.Play ();
////		}
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
//		if (LocalVideoManager.instance != null) {
//			//localVideos = LocalVideoManager.instance.GetAllLocalVideos ();
//			UpdateElementlocalVideos ();
//
//			for (int i = 0; i < localVideos.Length; i++) {
//				if (!localVideos [i].isDuplicate) {
//					AddLocalVideoUI (localVideos [i]);
//					localVideos [i].isDuplicate = true;
//				}else {
//					UpdateVideo ();
//				}
//			}
//
//			UpdateElementlocalVideoContents ();
//			LocalVideoManager.instance.IsDisplayLocalVideo = false;
//		}
//	}
//
//
//	private void AddLocalVideoUI(LocalVideo localVideo)
//	{
//		GameObject obj = (GameObject)Instantiate (localVideoPrefab);
//		LocalVideoUI localVideoUI = obj.GetComponent<LocalVideoUI> ();
//
//		if (localVideoUI != null) {
//			//localVideoUI.Setup (localVideo,this);
//
//			obj.transform.SetParent (this.transform,false);
////			obj.transform.localScale = new Vector3 (1,1,1);
////			obj.GetComponent<RectTransform> ().anchoredPosition3D = Vector3.zero;
//		} else {
//			Debug.LogError ("Wrong Prefab!");
//		}
//	}
//	#endregion
//
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
//				nameLocalVideos.Add (localVideos [i].fileInfo.Name);
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
//				nameLocalVideoContents.Add (localVideoContents[i].videoTitle.text);
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
//					if (results[j] == localVideoContents[k].videoTitle.text){
//						DeleteLocalVideoUI (localVideoContents[k]);
//					}
//				}
//			}
//		}
//	}
//	#endregion
}
