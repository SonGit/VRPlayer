using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DownloadVideoListController : MonoBehaviour
{
//	public GameObject downloadVideoPrefab = null;
//
//    // Start is called before the first frame update
//	void OnEnable()
//    {
//		//Display ();
//    }
//
//    // Update is called once per frame
//    void Update()
//    {
//        
//    }
//
//	public void Display()
//	{
//		for (int i = 0; i < MainAllController.instance.user.userVideos.Count; i++) {
//			string filepath = MainAllController.instance.user.GetPathToFile (MainAllController.instance.user.userVideos[i].videoInfo.id,MainAllController.instance.user.userVideos[i].videoInfo.video_name);
//
//			if (File.Exists (filepath)) {
//				if (!MainAllController.instance.user.userVideos[i].isDuplicate){
//					AddDownloadVideoUI (MainAllController.instance.user.userVideos [i]);
//					MainAllController.instance.user.userVideos[i].isDuplicate = true;
//				}
//
//			}
//		}
//	}
//
//	void AddDownloadVideoUI(UserVideo UserVideo)
//	{
//		GameObject obj = (GameObject)Instantiate (downloadVideoPrefab);
//		DownloadVideoUI downloadVideoUI = obj.GetComponent<DownloadVideoUI> ();
//
//		if (downloadVideoUI != null) {
//			downloadVideoUI.Setup (UserVideo);
//
//			obj.transform.SetParent (this.transform,false);
//		} else {
//			Debug.LogError ("Wrong Prefab!");
//		}
//	}
//
//	public void DeleteVideo(string filePath){
//		if (filePath != null){
//			File.Delete (filePath);
//		}
//	}
}
