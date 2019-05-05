using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UserVideoListController : MonoBehaviour
{
//	[SerializeField]
//	private GameObject userVideoPrefab = null;
//
//	List<UserVideoUI> listObject = new List<UserVideoUI>();
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
//			if (!File.Exists (filepath)) {
//				if (!MainAllController.instance.user.userVideos[i].isDuplicate){
//					AddUserVideoUI (MainAllController.instance.user.userVideos [i]);
//					MainAllController.instance.user.userVideos[i].isDuplicate = true;
//				}
//			}
//		}
//	}
//
//	void AddUserVideoUI(UserVideo userVideo)
//	{
//		GameObject obj = (GameObject)Instantiate (userVideoPrefab);
//		UserVideoUI userVideoUI = obj.GetComponent<UserVideoUI> ();
//
//		if (userVideoUI != null) {
//			userVideoUI.Setup (userVideo);
//
//			obj.transform.SetParent (this.transform,false);
//
//			listObject.Add (userVideoUI);
//		} else {
//			Debug.LogError ("Wrong Prefab!");
//		}
//
//	}
//
//	public void Reset()
//	{
//		for (int i = 0; i < listObject.Count; i++) {
//			Destroy (listObject[i].gameObject);
//		}
//		listObject = new List<UserVideoUI>();
//	}
//
//	public void DeleteVideo(string filePath){
//		if (filePath != null){
//			File.Delete (filePath);
//		}
//	}
}
