using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class User
{
	public string username = String.Empty;

	public string token = String.Empty;
	public List<UserVideo> userVideos = new List<UserVideo>();
	public List<FavoriteVideo> favoriteVideos = new List<FavoriteVideo>();

	public string persistentDataPath;

	public User(string username ,string token)
	{
		this.username = username;

		this.token = token;

		persistentDataPath = Application.persistentDataPath;

		CreateFolderOfUser ();

		CreateTempFolder ();
	}

	public string GetPath()
	{
		string path = string.Empty;
		#if UNITY_ANDROID && !UNITY_EDITOR
		try
		{
		using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
		using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
		{
		path = jo.Call<AndroidJavaObject>("getDir", "pFiles", 0).Call<string>("getAbsolutePath") + "/" + username;
		}
		}
		}
		catch (System.Exception e)
		{
		Debug.LogWarning(e.ToString());
		}
		#else
		path = Path.GetFullPath(persistentDataPath) + "/" + username;
		#endif

		return path;
	}
		
	public string GetPathToFile(string videoID, string filename)
	{
		return Path.Combine (GetPath() + "/" + videoID ,filename + ".mp4");
	}

	public string GetPathToSubtitle(string videoID, string filename)
	{
		return Path.Combine (GetPath() + "/" + videoID ,filename + ".srt");
	}

	public string GetPathToVideoFolder(string videoID)
	{
		return GetPath () + "/" + videoID;
	}

	public string GetPathToVideoThumbnail(string videoID)
	{
		return Path.GetFullPath(persistentDataPath) + "/" + "temp" + "/" + videoID;
	}

	private void CreateFolderOfUser(){

		string path = GetPath ();

		if (!Directory.Exists(path))
		{
            Debug.Log("CreateFolderOfUser");
			Directory.CreateDirectory(path);
            Debug.Log("CreateFolderOfUserCreateFolderOfUserCreateFolderOfUser");
        }

	}

	private void CreateTempFolder(){
		
		string path = Path.GetFullPath(persistentDataPath) + "/" + "temp";

		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
	}

	public void ClearAllInfo(){
		username = String.Empty;
		token = String.Empty;
		userVideos.Clear ();
		favoriteVideos.Clear ();
	}

	public bool IsVideoFavorited(Video video)
	{
		for (int i = 0; i < favoriteVideos.Count; i++) {
			if (video.videoInfo.id == favoriteVideos [i].videoInfo.id) {
				return true;
			}
		}
		return false;
	}

	public void RemoveFavoriteVideo(Video video)
	{
		for (int i = 0; i < favoriteVideos.Count; i++) {
			if (video.videoInfo.id == favoriteVideos [i].videoInfo.id) {
				favoriteVideos.RemoveAt (i);
				return;
			}
		}
	}

	public void AddFavoriteVideo(Video video)
	{
		bool duplicate = false;
		for (int i = 0; i < favoriteVideos.Count; i++) {
			if (video.videoInfo.id == favoriteVideos [i].videoInfo.id) {
				duplicate = true;
			}
		}

		if (!duplicate) {
			if (!(video is FavoriteVideo)) {
				FavoriteVideo favoriteVideo = new FavoriteVideo (video.videoInfo);
				favoriteVideos.Add (favoriteVideo);
			} else {
				favoriteVideos.Add (video as FavoriteVideo);
			}
		}
	}

	public void RemoveUserVideo(Video video)
	{
		for (int i = 0; i < userVideos.Count; i++) {
			if (video.videoInfo.id == userVideos [i].videoInfo.id) {
				userVideos.RemoveAt (i);
				return;
			}
		}
	}
} 