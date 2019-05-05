using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using EasyMobile;

public class InternetVideoManager : MonoBehaviour
{
	public List<FavoriteVideo> bookmarkVideos;

	public static InternetVideoManager instance;

	void Awake()
	{
		instance = this;
	}

	// Start is called before the first frame update
	void Start()
	{
		bookmarkVideos = new List<FavoriteVideo> (); 
	}
		
	void Update(){
		
	}

	public void GetFavoriteVideo(){
		if (Networking.instance != null && MainAllController.instance != null) {
			string authToken = MainAllController.instance.user.token;
			if (authToken != null){
				Networking.instance.GetFavoriteVideoRequest (authToken, OnGetBookmark, ErrorGetBookmarkCallback);
			}
		}
	}

	public void ErrorGetBookmarkCallback(){
		NativeUI.AlertPopup alert = NativeUI.Alert("Check your Connection!", "");
	}
		
	private void OnGetBookmark(Video_Info[] infos){
			
		for (int i = 0; i < infos.Length; i++) {
			FavoriteVideo newVideo = new FavoriteVideo (infos[i]);
			bookmarkVideos.Add (newVideo);
		}
	}
}


