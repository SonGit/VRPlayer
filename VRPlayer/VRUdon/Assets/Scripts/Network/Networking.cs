using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Net;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;
using CielaSpike;
using VRUdon.VR;

public class Networking : MonoBehaviour
{
	public static Networking instance;

	public delegate void GetUserVideoCallback(Video_Info[] infos);
	public delegate void GetFavoriteVideoCallback(Video_Info[] infos);
	public delegate void GetLoginInfoCallback(LoginRespone respone);
	public delegate void GetLogoutCallback(LogoutRespone respone);
	public delegate void GetVideoSizeCallback(string id,long size);
	public delegate void GetVideoLinkCallback(GetLinkVideoResponse getLinkVideoResponse);
	public delegate void FavoriteVideoCallback(FavoriteVideoResponse respone);
	public delegate void UnfavoriteVideoCallback(UnfavoriteVideoResponse respone);
	public delegate void GetRecommendedVideoCallback(Video_Info[] infos);
	public delegate void ErrorCallback();


	private Queue<Action> queue = new Queue<Action>();
	private Task task;

    // Start is called before the first frame update
    void Awake()
    {
		instance = this;
    }

	void Start()
	{
		ServicePointManager.ServerCertificateValidationCallback =
			delegate(object s, X509Certificate certificate,
				X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{ return true; };



        //		GetAllVideoRequest ("fb8c6791a11e03bb9bca4d1d75141e92dea3143a");
        //		GetRecommendedVideosRequest ("fb8c6791a11e03bb9bca4d1d75141e92dea3143a");
        //		GetFavoriteVideoRequest ("fb8c6791a11e03bb9bca4d1d75141e92dea3143a");
    }

    void MoveNext()
	{
		if (queue != null && queue.Count > 0) {
			Action act = queue.Dequeue ();
			act ();
			print ("act");
		}
	}

	void Update()
	{
		if (task != null) {

			if (task.State == TaskState.Done) {
				MoveNext ();
			}

		} else {
			MoveNext ();
		}
	}

	public void LoginRequest(string username,string password,GetLoginInfoCallback callback = null, ErrorCallback errorCallback = null)
	{
		queue.Enqueue(() => { this.StartCoroutineAsync (GetLoginInfoRequestProcess(username,password,callback,errorCallback), out task); });
	}

	public void LogoutRequest(string authToken,GetLogoutCallback callback = null, ErrorCallback errorCallback = null)
	{
		queue.Enqueue(() => { this.StartCoroutineAsync (LogoutRequestProcess(authToken,callback,errorCallback), out task); });
	}

	public void GetUserVideoRequest(string authToken ,GetUserVideoCallback callback = null, ErrorCallback errorCallback = null)
	{
		queue.Enqueue(() => { this.StartCoroutineAsync (GetUserVideoRequestProcess(authToken,callback,errorCallback), out task); });
	}

	public void GetVideoLinkRequest(string id,string authToken,GetVideoLinkCallback callback = null, ErrorCallback errorCallback = null)
	{
		queue.Enqueue(() => { this.StartCoroutineAsync (GetVideoLinkRequestProcess(id,authToken,callback,errorCallback), out task); });
	}

	public void GetFavoriteVideoRequest(string authToken,GetFavoriteVideoCallback callback = null, ErrorCallback errorCallback = null)
	{
		queue.Enqueue(() => { 	this.StartCoroutineAsync (GetFavoriteVideoProcess(authToken,callback,errorCallback), out task); });
	}

	public void FavoriteVideoRequest(string id,string authToken,FavoriteVideoCallback callback = null, ErrorCallback errorCallback = null)
	{
		queue.Enqueue(() => { 	this.StartCoroutineAsync (FavoriteVideoProcess(id,authToken,callback,errorCallback), out task); });
	}

	public void UnfavoriteVideoRequest(string id,string authToken,UnfavoriteVideoCallback callback = null, ErrorCallback errorCallback = null)
	{
		queue.Enqueue(() => { 	this.StartCoroutineAsync (UnfavoriteVideoProcess(id,authToken,callback,errorCallback), out task); });
	}

	public void GetRecommendedVideosRequest(string authToken,GetRecommendedVideoCallback callback = null, ErrorCallback errorCallback = null)
	{
		queue.Enqueue(() => { 	this.StartCoroutineAsync (GetRecommendedVideoProcess(authToken,callback,errorCallback), out task); });
	}

	public void GetVideoSizeRequest(string url,string id,GetVideoSizeCallback callback = null, ErrorCallback errorCallback = null)
	{
		queue.Enqueue(() => { 	this.StartCoroutineAsync (GetVideoSizeProcess(url,id,callback,errorCallback), out task); });
	}

	IEnumerator GetVideoSizeProcess(string url ,string id,GetVideoSizeCallback callback, ErrorCallback errorCallback = null)
	{
		yield return Ninja.JumpBack;

		long iFileSize = GetLength (url);

		yield return Ninja.JumpToUnity;

		if(callback != null)
		{
			callback(id,iFileSize);
		}

	}

	IEnumerator GetLoginInfoRequestProcess(string username,string password,GetLoginInfoCallback callback, ErrorCallback errorCallback = null)
	{
		string errorMsg = string.Empty;

		string URL = string.Format ("https://api.fewoov.com/api/app/v1/Login?Username={0}&Password={1}&Forced=yes", username, password);

		var request = (HttpWebRequest)WebRequest.Create(URL);

		bool failed = false;

		LoginRespone loginResponse = null;

		try
		{
			using (var response = (HttpWebResponse)request.GetResponse ()) { 

				using (var reader = new StreamReader (response.GetResponseStream ())) {

					string readText = reader.ReadToEnd ();
					// Attempt to cast JSON to class
					loginResponse = JsonConvert.DeserializeObject<LoginRespone> (readText);


				}
			}
		} catch (Exception e) {
			failed = true;
			errorMsg = e.Message;
			Debug.Log ("GetLoginInfoRequestProcess exception: " + e.Message);
		}

		yield return Ninja.JumpToUnity;

		if (failed) {
			
			//NativeUI.Alert("Server Returned Error ",errorMsg);

			//AndroidDialog.instance.showWarningDialog(errorMsg);

			if (errorCallback != null)
				errorCallback ();
			yield break;

		} else {
			
			if (loginResponse != null) {

				Debug.Log ("getLoginResponse eventcode: " + loginResponse.event_code + "  auth_token: " + loginResponse.auth_token);

				if (callback != null)
					callback (loginResponse);
			}

		}
			
	}

	IEnumerator LogoutRequestProcess(string authToken,GetLogoutCallback callback, ErrorCallback errorCallback = null)
	{
		string errorMsg = string.Empty;

		string URL = string.Format ("https://m.fetishwoman.co.kr/api/app/v1/Logout?auth_token={0}", authToken);

		var request = (HttpWebRequest)WebRequest.Create(URL);

		LogoutRespone logoutResponse = null;

		bool failed = false;

		try
		{
			using (var response = (HttpWebResponse)request.GetResponse ()) { 

				using (var reader = new StreamReader (response.GetResponseStream ())) {

					string readText = reader.ReadToEnd ();
					// Attempt to cast JSON to class
					logoutResponse = JsonConvert.DeserializeObject<LogoutRespone> (readText);

				}
			}
		} catch (Exception e) {
			failed = true;
	
			Debug.Log ("GetLoginInfoRequestProcess exception: " + e.Message);

			errorMsg = e.Message;
		}

		yield return Ninja.JumpToUnity;

		if (failed) {
			
			//NativeUI.Alert("Server Returned Error",errorMsg);

			//AndroidDialog.instance.showWarningDialog(errorMsg);

			if (errorCallback != null)
				errorCallback ();
			yield break;

		} else {
			if (logoutResponse != null) {

				Debug.Log ("getLoginResponse eventcode: " + logoutResponse.event_code);

				if (callback != null)
					callback (logoutResponse);
			}
		}
	}

	IEnumerator GetUserVideoRequestProcess(string authToken,GetUserVideoCallback callback = null, ErrorCallback errorCallback = null) {
		
		string errorMsg = string.Empty;

		string URL = string.Format ("https://api.fewoov.com/api/app/v1/GetVideos?auth_token={0}", authToken);

		var request = (HttpWebRequest)WebRequest.Create(URL);

		GetUserVideoResponse getUserVideoResponse = null;

		bool failed = false;

		try
		{
			using (var response = (HttpWebResponse)request.GetResponse()) 
			{ 
				using (var reader = new StreamReader (response.GetResponseStream ())) {

					string readText = String.Empty;

					readText = reader.ReadToEnd ();

					// Attempt to cast JSON to class
					getUserVideoResponse = JsonConvert.DeserializeObject<GetUserVideoResponse> (readText);
				}
			}

		} catch(Exception e) {
			failed = true;
			Debug.Log ("GetUserVideoRequestProcess exception: " + e.Message);
			errorMsg = e.Message;
		}

		// Request/Response is done, now return to main thread
		yield return Ninja.JumpToUnity;

		if (failed) {
			
			//NativeUI.Alert("Server Returned Error",errorMsg);

			//AndroidDialog.instance.showWarningDialog(errorMsg);

			if (errorCallback != null)
				errorCallback ();
			yield break;

		} else {
			
			if (getUserVideoResponse != null) {

				Debug.Log ("GetUserVideoRequestProcess eventcode: " + getUserVideoResponse.event_code);

				if(callback != null)
					callback (getUserVideoResponse.video_list);

			}
		}

	}

	IEnumerator GetVideoLinkRequestProcess(string id,string authToken,GetVideoLinkCallback callback = null, ErrorCallback errorCallback = null) {
		
		string errorMsg = string.Empty;

		string URL = string.Format ("https://m.fetishwoman.co.kr/api/app/v1/Download360Video?auth_token={0}&video_id={1}", authToken,id);

		var request = (HttpWebRequest)WebRequest.Create(URL);

		bool failed = false;

		GetLinkVideoResponse getLinkVideoResponse = null;

		try
		{
			using (var response = (HttpWebResponse)request.GetResponse ()) { 

				using (var reader = new StreamReader (response.GetResponseStream ())) {

					string readText = reader.ReadToEnd ();
					// Attempt to cast JSON to class
					getLinkVideoResponse = JsonConvert.DeserializeObject<GetLinkVideoResponse> (readText);

				}
			}

		} catch(Exception e) {
			failed = true;
			Debug.Log ("GetUserVideoRequestProcess exception: " + e.Message);

			errorMsg = e.Message;
		}

		yield return Ninja.JumpToUnity;

		if (failed) {
			
			//NativeUI.Alert("Server Returned Error",errorMsg);

			//AndroidDialog.instance.showWarningDialog(errorMsg);

			if (errorCallback != null)
				errorCallback ();
			yield break;

		} else {
			
			if (getLinkVideoResponse != null) {
				getLinkVideoResponse.id = id;
				Debug.Log ("GetVideoLinkRequestProcess eventcode: " + getLinkVideoResponse.event_code);

				yield return Ninja.JumpToUnity;

				if (callback != null)
					callback (getLinkVideoResponse);
			}
		}

	}

	IEnumerator GetFavoriteVideoProcess(string authToken,GetFavoriteVideoCallback callback = null, ErrorCallback errorCallback = null) {
		
		string errorMsg = string.Empty;

		string URL = string.Format ("https://api.fewoov.com/api/app/v1/GetFavoriteVideos?auth_token={0}", authToken);

		var request = (HttpWebRequest)WebRequest.Create(URL);

		GetFavoriteVideoResponse getFavoriteVideoResponse = null;

		bool failed = false;

		try
		{
			using (var response = (HttpWebResponse)request.GetResponse ()) { 

				using (var reader = new StreamReader (response.GetResponseStream ())) {

					string readText = reader.ReadToEnd ();
					// Attempt to cast JSON to class
					getFavoriteVideoResponse = JsonConvert.DeserializeObject<GetFavoriteVideoResponse> (readText);


				}
			}

		} catch (Exception e) {
			failed = true;
			Debug.Log ("GetUserVideoRequestProcess exception: " + e.Message);
			errorMsg = e.Message;
		}

		yield return Ninja.JumpToUnity;

		if (failed) {
			
			//NativeUI.Alert("Server Returned Error",errorMsg);

			//AndroidDialog.instance.showWarningDialog(errorMsg);

			if (errorCallback != null)
				errorCallback ();
			yield break;

		} else {
			
			if (getFavoriteVideoResponse != null) {

				Debug.Log ("GetFavoriteVideoProcess eventcode: " + getFavoriteVideoResponse.event_code);

				if (callback != null)
					callback (getFavoriteVideoResponse.video_list);
			}
		}

	}

	IEnumerator FavoriteVideoProcess(string id,string authToken,FavoriteVideoCallback callback = null, ErrorCallback errorCallback = null) {
		
		string errorMsg = string.Empty;

		string URL = string.Format ("https://api.fewoov.com/api/app/v1/FavoriteVideos?auth_token={0}&video_id={1}", authToken,id);

		var request = (HttpWebRequest)WebRequest.Create(URL);

		bool failed = false;

		FavoriteVideoResponse favoriteVideoResponse = null;

		try
		{
			using (var response = (HttpWebResponse)request.GetResponse ()) { 

				using (var reader = new StreamReader (response.GetResponseStream ())) {
					
					string readText = reader.ReadToEnd ();
					// Attempt to cast JSON to class
					favoriteVideoResponse = JsonConvert.DeserializeObject<FavoriteVideoResponse> (readText);

				}
			}
		} catch (Exception e) {
			failed = true;
			Debug.Log ("GetUserVideoRequestProcess exception: " + e.Message);
			errorMsg = e.Message;
		}

		yield return Ninja.JumpToUnity;

		if (failed) {
			
			//NativeUI.Alert("Server Returned Error",errorMsg);

			//AndroidDialog.instance.showWarningDialog(errorMsg);

			if (errorCallback != null)
				errorCallback ();
			yield break;

		} else {
			if (favoriteVideoResponse != null) {

				Debug.Log ("FavoriteVideoProcess eventcode: " + favoriteVideoResponse.event_code);

				if (callback != null)
					callback (favoriteVideoResponse);
			}
		}
	}

	IEnumerator UnfavoriteVideoProcess(string id,string authToken,UnfavoriteVideoCallback callback = null, ErrorCallback errorCallback = null) {
		
		string errorMsg = string.Empty;

		string URL = string.Format ("https://m.fetishwoman.co.kr/api/app/v1/UnfavoriteVideos?auth_token={0}&video_id={1}", authToken,id);

		var request = (HttpWebRequest)WebRequest.Create(URL);

		UnfavoriteVideoResponse unfavoriteVideoResponse = null;

		bool failed = true;

		try
		{
			using (var response = (HttpWebResponse)request.GetResponse ()) { 

				using (var reader = new StreamReader (response.GetResponseStream ())) {

					string readText = reader.ReadToEnd ();
					// Attempt to cast JSON to class
					unfavoriteVideoResponse = JsonConvert.DeserializeObject<UnfavoriteVideoResponse> (readText);

				}
				failed = false;
			}

		} catch (Exception e) {
			failed = true;
			Debug.Log ("GetUserVideoRequestProcess exception: " + e.Message);
			errorMsg = e.Message;
		}

		yield return Ninja.JumpToUnity;

		if (failed) {
			
			//NativeUI.Alert("Server Returned Error",errorMsg);

			//AndroidDialog.instance.showWarningDialog(errorMsg);

			if (errorCallback != null)
				errorCallback ();
			yield break;
		} else {
			if (unfavoriteVideoResponse != null) {

				Debug.Log ("UnfavoriteVideoProcess eventcode: " + unfavoriteVideoResponse.event_code);

				if (callback != null)
					callback (unfavoriteVideoResponse);
			}
		}

	}

	IEnumerator GetRecommendedVideoProcess(string authToken,GetRecommendedVideoCallback callback = null, ErrorCallback errorCallback = null) {
		
		string errorMsg = string.Empty;

		string URL = string.Format ("https://m.fetishwoman.co.kr/api/app/v1/GetRecommendedVideos?auth_token={0}", authToken);

		var request = (HttpWebRequest)WebRequest.Create(URL);

		GetRecommendedVideoResponse getRecommendedVideoResponse = null;

		bool failed = false;

		try
		{
			using (var response = (HttpWebResponse)request.GetResponse ()) { 

				using (var reader = new StreamReader (response.GetResponseStream ())) {

					string readText = reader.ReadToEnd ();
					// Attempt to cast JSON to class
					getRecommendedVideoResponse = JsonConvert.DeserializeObject<GetRecommendedVideoResponse> (readText);

				}
			}

		} catch (Exception e) {
			failed = true;
			Debug.Log ("GetUserVideoRequestProcess exception: " + e.Message);
			errorMsg = e.Message;
		}

		yield return Ninja.JumpToUnity;

		if (failed) {
			
		//	NativeUI.Alert("Server Returned Error",errorMsg);

			//AndroidDialog.instance.showWarningDialog(errorMsg);

			if (errorCallback != null)
				errorCallback ();
			yield break;

		} else {
			if (getRecommendedVideoResponse != null) {

				Debug.Log ("GetRecommendedVideoProcess eventcode: " + getRecommendedVideoResponse.event_code);

				if (callback != null)
					callback (getRecommendedVideoResponse.video_list);
			}
		}
	}

	long GetLength(string URL)
	{
		var request = (HttpWebRequest)WebRequest.Create(URL); 
		using (var response = (HttpWebResponse)request.GetResponse()) 
		{ 
			return response.ContentLength;
		}
	}

}

public class VideoInfoResponse
{
	public string video_id;
	public string video_name;
	public string description;
	public string thumbnail_link;
	public string date;
	public string length;
	public string tag;
	public Subtitle[] subtitle_list;
}

[System.Serializable]
public class Subtitle
{
	public string language { get; set;}
	public string subtitle_link { get; set;}
}


public class LoginRespone
{
	public string event_code;
	public string auth_token;
}

public class LogoutRespone
{
	public string event_code;
}

public class GetUserVideoResponse
{
	public string event_code;
	public Video_Info[] video_list;
}

public class GetLinkVideoResponse
{
	public string id;
	public string event_code;
	public string link;
}

public class GetFavoriteVideoResponse
{
	public string event_code;
	public Video_Info[] video_list;
}

public class FavoriteVideoResponse
{
	public string event_code;
}

public class UnfavoriteVideoResponse
{
	public string event_code;
}

public class GetRecommendedVideoResponse
{
	public string event_code;
	public Video_Info[] video_list;
}