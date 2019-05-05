using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using CielaSpike;
using System.IO;
using NatShareU;
using System.Linq;


//public class ScrollListController : MonoBehaviour
//{
//	[SerializeField]
//	private GameObject videoUIPrefab;
//	[SerializeField]
//	private List<VideoContentUI>  videoUIList;
//	[SerializeField]
//	private VerticalLayoutGroup layoutGroup;
//
//	// Use this for initialization
//	void OnEnable () 
//	{
//		RefeshDisplay();
//	}
//
//	void Update () 
//	{
//		if (Input.GetKeyDown (KeyCode.A)) {
//			RefeshDisplay ();
//		}
//	}
//
//	public void RefeshDisplay()
//	{
//		StartCoroutine(LoadVideo());
//	}
//		
//	void AddItem(VideoInfo itemToAdd)
//	{
//		GameObject obj = (GameObject)Instantiate (videoUIPrefab);
//		VideoContentUI videoContentUI = obj.GetComponent<VideoContentUI> ();
//
//		if (videoContentUI != null) {
//			videoContentUI.Setup (itemToAdd,this);
//			videoUIList.Add (videoContentUI);
//
//			obj.transform.SetParent (layoutGroup.transform);
//			obj.transform.localScale = new Vector3 (1,1,1);
//			obj.GetComponent<RectTransform> ().anchoredPosition3D = Vector3.zero;
//		} else {
//			Debug.LogError ("Wrong Prefab!");
//		}
//	}
//
////	public IEnumerator LoadVideo() {
////		List<string> AllFolders = new List<string>();
////		string[] tempFolders;
////
////		// Enumerate all files in Windows, for test purposes
////		#if UNITY_EDITOR
////		//AllFolders = (Directory.GetDirectories("C:\\Users\\sonpd\\Downloads")).ToList();
////		//AllFolders = (Directory.GetDirectories("C:\\Users\\user\\Downloads")).ToList();
////		AllFolders = (Directory.GetDirectories(MainAllController.ap)).ToList();
////		#endif 
////
////		// Enumerate all files in Android local storage, ignoring Android folder
////		#if UNITY_ANDROID  && !UNITY_EDITOR
////		//tempFolders = (Directory.GetDirectories("/storage/emulated/0"));
////		tempFolders = (Directory.GetDirectories(MainAllController.ap));
////
////		foreach(var folder in tempFolders)
////		{
////			string fileName = Path.GetFileName(folder);
////
////			if(fileName.Substring(0,1) != ".")
////			{
////				AllFolders.Add(folder);
////			}
////		}
////		AllFolders.Remove ("/storage/emulated/0/Android");
////		#endif 
////
////		CheckDelete ();
////
////		for (int i = 0; i < AllFolders.Count; i++) {
////
////			// Only get paths to files that have .mp4 extensions
////			var paths = GetFileList("*.mp4",AllFolders[i]);
////			yield return new WaitForEndOfFrame ();
////
////			foreach (var path in paths) {
////
////				if (!CheckDuplicate (path)) {
////					VideoInfo info = new VideoInfo (path);
////					if (info != null) {
////						AddItem (info);
////					}
////				} else {
////					VideoInfo info = new VideoInfo (path);
////					if (info != null) {
/// 
////						UpdateDuplicate (info);
////					}
////				}
////				yield return new WaitForEndOfFrame ();
////			}
////			yield return new WaitForEndOfFrame ();
////		}
////		yield return new WaitForEndOfFrame ();
////
////	}
//
//	public IEnumerator LoadVideo() {
//
//		// Enumerate all files in Windows, for test purposes
//		#if UNITY_EDITOR
//	
//		#endif 
//
//		// Enumerate all files in Android local storage, ignoring Android folder
//		#if UNITY_ANDROID  && !UNITY_EDITOR
//
//		#endif 
//
//		CheckDelete ();
//
//		// Only get paths to files that have .mp4 extensions
//		var paths = GetFileList("*.mp4", MainAllController.ap);
//		yield return new WaitForEndOfFrame ();
//
//		foreach (var path in paths) {
//
//			if (!CheckDuplicate (path)) {
//				VideoInfo info = new VideoInfo (path);
//				if (info != null) {
//					AddItem (info);
//				}
//			} else {
//				VideoInfo info = new VideoInfo (path);
//				if (info != null) {
//					UpdateDuplicate (info);
//				}
//			}
//			yield return new WaitForEndOfFrame ();
//		}
//		yield return new WaitForEndOfFrame ();
//
//	}
//
//	void CheckDelete()
//	{
//		List<VideoContentUI>  videoUIToDelete = new List<VideoContentUI>();
//
//		foreach (VideoContentUI ui in videoUIList) {
//			if (!ui.info.isExists ()) {
//				videoUIToDelete.Add (ui);
//			}
//		}
//
//		foreach (VideoContentUI ui in videoUIToDelete) {
//			Destroy (ui.gameObject);
//			videoUIList.Remove (ui);
//		}
//	}
//
//	bool CheckDuplicate(string path)
//	{
//		for (int i = 0; i < videoUIList.Count; i++) {
//			if (path == videoUIList [i].info.GetFilePath()) {
//				return true;
//			}
//		}
//		return false;
//	}
//
//	void UpdateDuplicate(VideoInfo newInfo)
//	{
//		for (int i = 0; i < videoUIList.Count; i++) {
//			if (newInfo.GetFilePath() == videoUIList [i].info.GetFilePath()) {
//				videoUIList [i].Setup(newInfo,this);
//				return;
//			}
//		}
//	}
//
//	public static IEnumerable<string> GetFileList(string fileSearchPattern, string rootFolderPath)
//	{
//		Queue<string> pending = new Queue<string>();
//		pending.Enqueue(rootFolderPath);
//		string[] tmp;
//		while (pending.Count > 0)
//		{
//			rootFolderPath = pending.Dequeue();
//			try
//			{
//				tmp = Directory.GetFiles(rootFolderPath, fileSearchPattern);
//			}
//			catch (System.UnauthorizedAccessException)
//			{
//				continue;
//			}
//			for (int i = 0; i < tmp.Length; i++)
//			{
//				yield return tmp[i];
//			}
//			tmp = Directory.GetDirectories(rootFolderPath);
//			for (int i = 0; i < tmp.Length; i++)
//			{
//				pending.Enqueue(tmp[i]);
//			}
//		}
//	}
//
//	public void DeleteVideo(string filePath){
//		if (filePath != null){
//			File.Delete (filePath);
//			RefeshDisplay ();
//		}
//
//	}
//
//}
