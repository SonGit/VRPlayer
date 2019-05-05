using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.Networking;

public class CloudVideoManager : MonoBehaviour
{
//	public List<BookmarkVideo> bookmarkVideos;
//
//	public static CloudVideoManager instance;
//
//	GetVideoResponse getVideoResponse;
//	public byte[] a;
//	void Awake()
//	{
//		if (instance == null) 
//		{
//			instance = this;
//		} 
//
//		else 
//		{
//			return;
//		}
//	}
//
//	// Start is called before the first frame update
//	void Start()
//	{
////		bookmarkVideos = new List<BookmarkVideo> (); 
////		getVideoResponse = new GetVideoResponse ();
//		a = www.downloadHandler.text;
//		a = StartCoroutine ();
//	}
//
//	// Update is called once per frame
//	void Update()
//	{
//
//	}
//
//	public UnityWebRequest www;
//	IEnumerator GetJSON() {
//		www = UnityWebRequest.Get("https://www.avr-creative.com/SmectaGame/GetVideos.txt");
//		www.SetRequestHeader("User-Agent", "runscope/0.1");
//		yield return www.SendWebRequest();
//
//		if(www.isNetworkError || www.isHttpError) {
//			Debug.Log(www.error);
//		}
//		else {
//			// Show results as text
//			Debug.Log(www.downloadHandler.text);
//			// Or retrieve results as binary data
//			byte[] results = www.downloadHandler.data;
//			yield return results;
//			//			GetVideoResponse getvideoJSON = JsonConvert.DeserializeObject<GetVideoResponse> (www.downloadHandler.text);
//			//			video_info[] info = getvideoJSON.video_list;
//			//			Subtitle[] sub = info[0].subtitle_list;
//			//			print (info[1].description);
//			//			print (sub[1].language);
//		}
//	}
//
//	public void GetBookmark(){
//		for (int i = 0; i < getVideoResponse.video_list.Length; i++) {
//			BookmarkVideo bookmarkVideo = new BookmarkVideo (getVideoResponse.video_list[i]);
//			bookmarkVideos.Add (bookmarkVideo);
//		}
//	}

}
