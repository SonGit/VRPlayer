using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ATTACH TO ObjectPool gameObject
using System.IO;
using System.Net;
using System;

public class ObjectPool : MonoBehaviour {

	public static ObjectPool instance;

	GenericObject<UserVideoUI> userVideoUIs;

	GenericObject<LocalVideoUI> localVideoUIs;

	GenericObject<DownloadVideoUI> downloadVideoUIs;

	GenericObject<InboxVideoUI> inboxVideoUIs;

	GenericObject<FavoriteVideoUI> favoriteVideoUIs;

	void Awake()
	{
		instance = this;
		userVideoUIs = new GenericObject<UserVideoUI>(ObjectFactory.PrefabType.UserVideoUI,2);
		localVideoUIs = new GenericObject<LocalVideoUI>(ObjectFactory.PrefabType.LocalVideoUI,2);
		downloadVideoUIs = new GenericObject<DownloadVideoUI>(ObjectFactory.PrefabType.DownloadVideoUI,1);
		inboxVideoUIs = new GenericObject<InboxVideoUI>(ObjectFactory.PrefabType.InboxVideoUI,1);
		favoriteVideoUIs = new GenericObject<FavoriteVideoUI>(ObjectFactory.PrefabType.FavoriteVideoUI,1);
	}
//
//	// Use this for initialization
	void Start () {

	}
//
//	#region Effect
	public UserVideoUI GetUserVideoUI()
	{
		return userVideoUIs.GetObj ();
	}
//
	public LocalVideoUI GetLocalVideoUI()
	{
		return localVideoUIs.GetObj ();
	}

	public DownloadVideoUI GetDownloadVideoUI()
	{
		return downloadVideoUIs.GetObj ();
	}
		
	public InboxVideoUI GetInboxVideoUI()
	{
		return inboxVideoUIs.GetObj ();
	}

	public FavoriteVideoUI GetFavoriteVideoUI()
	{
		return favoriteVideoUIs.GetObj ();
	}

	Dictionary<string,Texture2D> thumbnailList = new Dictionary<string, Texture2D>();

	public delegate void GetThumbnailTexCallback(Texture2D texture);

	public void CheckThumbnail(Video video,GetThumbnailTexCallback callback)
	{
		string thumbnailURL = string.Empty;
		//
		thumbnailURL = MainAllController.instance.user.GetPathToVideoThumbnail (video.videoInfo.id);
		//print ("thumbnailURL  " + thumbnailURL);
		// if the thumbnail is downloaded, simply load it
		if (File.Exists (thumbnailURL)) {

			FileInfo info = new FileInfo (thumbnailURL);

			if (info.Length == 0) {
				return;
			}
			
			print ("LoadThumbnail  " + thumbnailURL);

			if (callback != null) {
				callback (LoadThumbnail (thumbnailURL));
			}


		} else {
			print ("attempt to download it again  " + video.videoInfo.id);
			// or, attempt to download it again
			StartCoroutine(DownloadThumbnail(video.videoInfo.id,video.videoInfo.thumbnail_link,callback));
		}
	}

	protected Texture2D LoadThumbnail(string thumbnailURL)
	{
		Texture2D thumbnailTexture = null;

		try
		{
			byte[] fileData = File.ReadAllBytes( thumbnailURL );

			if (thumbnailTexture == null) {
				thumbnailTexture = new Texture2D(4, 4, TextureFormat.DXT1, false);
			}

			thumbnailTexture.LoadImage(fileData);

		} catch(System.Exception e) {
			FileInfo info = new FileInfo (thumbnailURL);
			Debug.Log ("Exception!  " + e.Message);
		} 

		return thumbnailTexture;
	}

	/// <summary>
	/// Downloads the thumbnail.
	/// </summary>
	/// <param name="url">URL.</param>
	IEnumerator DownloadThumbnail(string videoID,string subtitleLink,GetThumbnailTexCallback callback)
	{

		string filepath = MainAllController.instance.user.GetPathToVideoThumbnail (videoID);
		yield return new WaitForSeconds (.5f);

		using(WebClient client = new WebClient ())
		{
			try
			{

				client.DownloadFileAsync(new Uri(subtitleLink) , filepath);

			}catch(Exception e) {

				Debug.LogError ("DownloadThumbnail Exception! " + e.Message);

			} 

			// Attemp to reset coroutine if client cannot connect 
			// Could be useful for when internet is lost during downloading
			if (!client.IsBusy) {
				this.StartCoroutine (DownloadThumbnail ( videoID,subtitleLink,callback ));
				Debug.Log ("Resetting Download Thumbnail...................................");
				yield break;
			}

			// Wait until client has completed download
			while(client.IsBusy)
			{
				yield return new WaitForSeconds (.25f);
			}

			yield return new WaitForEndOfFrame ();

			try
			{

				if (callback != null) {
					callback (LoadThumbnail (filepath));
				}

			}catch(Exception e) {
				Debug.LogError ("Exception! " + e.Message);
			} finally {
				// Stop Loading screen, no matter what

			}

		}

	}
}
