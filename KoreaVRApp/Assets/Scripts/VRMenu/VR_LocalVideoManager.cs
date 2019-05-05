using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class VR_LocalVideoManager : MonoBehaviour
{
//	public List<LocalVideo> localVideos;
//
//	public static VR_LocalVideoManager instance;
//
//	private bool m_IsDisplayLocalVideo;
//
//	void Awake()
//	{
//		instance = this;
//		localVideos = new List<LocalVideo> ();
//	}
//
//	// Start is called before the first frame update
//	void Start()
//	{
//		m_IsDisplayLocalVideo = false;
//
//		Load ();
//	}
//
//	// Update is called once per frame
//	void Update()
//	{
//		
//	}
//
//	/// <summary>
//	/// DisplayBookmartVideo?
//	/// </summary>
//	public bool IsDisplayLocalVideo
//	{
//		get { return m_IsDisplayLocalVideo; }
//		set { m_IsDisplayLocalVideo = value; }
//	}
//
//	public void Load()
//	{
//		StopAllCoroutines ();
//		StartCoroutine (LoadProgress());
//	}
//
//	IEnumerator LoadProgress()
//	{
//		List<string> AllFolders = new List<string>();
//		string origin = string.Empty;
//		#if UNITY_EDITOR
//		//origin = "C:\\Users\\sonpd\\Downloads";
//		//origin = "E:\\Download\\Video";
//		origin = "D:\\Video";
//		//origin = "C:\\Users\\user\\AppData\\LocalLow\\avr\\KoreaVRA";
//		//AllFolders = (Directory.GetDirectories(origin)).ToList();
//
//		var tempFolders = (Directory.GetDirectories(origin));
//
//		foreach(var folder in tempFolders)
//		{
//			string fileName = Path.GetFileName(folder);
//
//			if(fileName.Substring(0,1) != ".")
//			{
//				AllFolders.Add(folder);
//			}
//		}
//
//		AllFolders.Remove ("D:\\Video\\a");
//		#endif 
//
//		// Enumerate all files in Android local storage, ignoring Android folder
//		#if UNITY_ANDROID  && !UNITY_EDITOR
//		Handheld.SetActivityIndicatorStyle(AndroidActivityIndicatorStyle.Large);
//		Handheld.StartActivityIndicator();
//
//		origin = "/storage/emulated/0";
//
//		var tempFolders = (Directory.GetDirectories(origin));
//
//		foreach(var folder in tempFolders)
//		{
//		string fileName = Path.GetFileName(folder);
//
//		if(fileName.Substring(0,1) != ".")
//		{
//		AllFolders.Add(folder);
//		}
//		}
//
//		AllFolders.Remove ("/storage/emulated/0/Android");
//
//		#endif 
//
//		#if UNITY_IOS  && !UNITY_EDITOR
//		origin = "/private/var/mobile/Media/DCIM/";
//		AllFolders = (Directory.GetDirectories(origin)).ToList();
//		#endif 
//
//		AllFolders.Add(origin);
//		RemoveInvalidVideos ();
//
//		foreach(var folder in AllFolders)
//		{
//			Debug.Log(folder);
//		}
//
//		for (int i = 0; i < AllFolders.Count; i++) {
//
//			// Only get paths to files that have .mp4 extensions
//			var paths = GetFileList("*.mp4",AllFolders[i]);
//			yield return new WaitForEndOfFrame ();
//
//			foreach (var path in paths) {
//
//				if (!CheckDuplicate (path)) {
//					LocalVideo newVideo = new LocalVideo (path);
//					localVideos.Add (newVideo);
//
//					Debug.Log ("Detected New video at: " + path);
//				} else {
//					//UpdateVideo (path);
//
//					Debug.Log ("Detected Duplicate video at: " + path);
//				}
//
//				yield return new WaitForEndOfFrame ();
//			}
//
//			yield return new WaitForEndOfFrame ();
//		}
//		yield return new WaitForEndOfFrame ();
//
//		Handheld.StopActivityIndicator ();
//
//		m_IsDisplayLocalVideo = true;
//	}
//
//	// Remove videos that have been deleted by user
//	void RemoveInvalidVideos()
//	{
//		for(int i= localVideos.Count - 1; i > -1; i--)
//		{
//			if(!localVideos [i].isExists())
//			{
//				localVideos.RemoveAt(i);
//			}
//		}
//	}
//
//	bool CheckDuplicate(string path)
//	{
//		for (int i = 0; i < localVideos.Count; i++) {
//			if (path == localVideos [i].fileInfo.FullName) {
//				return true;
//			}
//		}
//		return false;
//	}
//
//	void UpdateVideo(string path)
//	{
//		for (int i = 0; i < localVideos.Count; i++) {
//			if (path == localVideos [i].fileInfo.FullName) {
//				localVideos [i] = new LocalVideo (path);
//				return;
//			}
//		}
//	}
//
//	public LocalVideo[] GetAllLocalVideos()
//	{
//		return localVideos.ToArray ();
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
}
