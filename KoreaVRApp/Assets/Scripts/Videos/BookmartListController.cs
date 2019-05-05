using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class BookmartListController : MonoBehaviour
{
//	[SerializeField]
//	private GameObject bookmarkVideoPrefab = null;
//
//	List<BookmarkVideoUI> ListObject = new List<BookmarkVideoUI>();
//
//    // Start is called before the first frame update
//	void Start()
//    {
//
//    }
//
//    // Update is called once per frame
//    void Update()
//    {
//        
//    }
//
//	public void Display(BookmarkVideo[] bookmarkVideos)
//	{
//		foreach (BookmarkVideo bookmarkVideo in bookmarkVideos) {
//			if (!bookmarkVideo.isDuplicate){
//				AddBookmarkVideoUI (bookmarkVideo);
//				bookmarkVideo.isDuplicate = true;
//			}
//		}
//	}
//
//	void AddBookmarkVideoUI(BookmarkVideo bookmarkVideo)
//	{
//		GameObject obj = (GameObject)Instantiate (bookmarkVideoPrefab);
//		BookmarkVideoUI bookmarkVideoUI = obj.GetComponent<BookmarkVideoUI> ();
//
//		if (bookmarkVideoUI != null) {
//			bookmarkVideoUI.Setup (bookmarkVideo,this);
//
//			obj.transform.SetParent (this.transform,false);
//
//			ListObject.Add (bookmarkVideoUI);
//		} else {
//			Debug.LogError ("Wrong Prefab!");
//		}
//
//	}
//
//	public void Reset()
//	{
//		for (int i = 0; i < ListObject.Count; i++) {
//			Destroy (ListObject[i].gameObject);
//		}
//		ListObject = new List<BookmarkVideoUI>();
//	}
//
//	public void DeleteVideo(string filePath){
//		if (filePath != null){
//			File.Delete (filePath);
//		}
//	}
}
