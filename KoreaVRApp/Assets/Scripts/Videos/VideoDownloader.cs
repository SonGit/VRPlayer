using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using CielaSpike;
using System.Threading;
using System;
using System.Net;
using UnityEngine.Networking;
using EasyMobile.Demo;


public class VideoDownloader : MonoBehaviour
{
	#region Callbacks
	public delegate void OnDownloadCallback(float progress, float downloadSpeed);
	public delegate void OnSuccessCallback();
	public delegate void OnFailCallback();
	#endregion 

	#region Multithreading
	public Task task;
	#endregion 

	#region Networking variables
	private Stream smRespStream;
	private int iBufferSize;
	private FileStream saveFileStream;
	private FileInfo fINfo = null;
	private int iByteSize;
	private byte[] downBuffer;
	private string fullpath ;
	[SerializeField]
	private long iFileSize;
	[SerializeField]
	private long iExistLen;
	[SerializeField]
	private long ContentLength;
	public double downloadProgress = 0;
	public float tmp_down, nextUpdate, downloadSpeed;
	float time;
	#endregion  

	#region cache
	private string path;
	public Video video;
	#endregion  

	public DownloadState downloadState;

	void Start()
	{
		iBufferSize = 1024 * 1000;
		downBuffer = new byte[iBufferSize];
		iFileSize = 0;
		iExistLen = 0;
		downloadProgress = 0;
		nextUpdate = 0.5f;
	}

	void Update(){
		time = Time.time;
	}

	public void DownLoad (string url,string filepath,OnDownloadCallback downloadCallback= null, OnSuccessCallback successCallback= null, OnFailCallback failCallback= null){

		if (task != null && task.State == TaskState.Running) {
			task.Cancel ();
		}

		this.url = url;
		this.filepath = filepath;
		this.downloadCallback = downloadCallback;
		this.successCallback = successCallback;
		this.failCallback = failCallback;

		this.StartCoroutineAsync(DownloadFile( url,filepath,downloadCallback,successCallback,failCallback ), out task);
	}
		
	public void Pause (){
		if (task != null) {
			task.Cancel ();
		}
		DisposeStreams ();
		Destroy (gameObject);
	}

	public void StopThread (){
		if (task != null) {
			task.Cancel ();
		}
		DisposeStreams ();
	}

	string url;
	string filepath;
	OnDownloadCallback downloadCallback;
	OnSuccessCallback successCallback;
	OnFailCallback failCallback;

	public void Reset()
	{
		DisposeStreams ();
		if(task != null)
			task.Cancel ();
		DownLoad (url, filepath,downloadCallback,successCallback,failCallback);
	}

	/// <summary>
	/// Download file based on URL. Resume download if needed
	/// </summary>
	public IEnumerator DownloadFile(string sSourceURL, string fullpath,OnDownloadCallback downloadCallback= null, OnSuccessCallback successCallback = null, OnFailCallback failCallback= null)
	{
		downloadState = DownloadState.Downloading;
		// Make sure code is run on Thread
		iFileSize = 0;
		iExistLen = 0;

		System.Net.HttpWebRequest hwRq = null;
		System.Net.HttpWebResponse hwRes = null;


		// If file is already exists
		if (File.Exists (fullpath)) {
			fINfo = new FileInfo (fullpath);
			iExistLen = fINfo.Length;
		}

		// If network stream is already created, no need for further requests
		if (smRespStream != null && saveFileStream != null) {


		} else {

			try
			{
				// Try to create network stream
				// If file exists, create partial content stream
				if (iExistLen > 0) {
					// Resume download
					saveFileStream = new FileStream (fullpath, 
						FileMode.Append, FileAccess.Write, 
						FileShare.ReadWrite);
				}
				else
				{
					// If not, download from the beginning
					saveFileStream = new FileStream (fullpath,
						FileMode.Create, FileAccess.ReadWrite, 
						FileShare.ReadWrite);
				}

			} catch(Exception e) {
				
				Debug.LogError ("++++ Exception:  " + e.Message);

				downloadState = DownloadState.Fail;

				if(failCallback != null)
					failCallback ();

				yield break;
			}

			Debug.Log ("Downloading video to this....... "+ fullpath + " saveFileStream  "+saveFileStream);

			// Create new Request to server

			hwRq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create (sSourceURL);

			try
			{
					// Define iExistLen again to prevent bug
					fINfo = new FileInfo (fullpath);
					iExistLen = fINfo.Length;

					hwRq.AddRange ((int)iExistLen);

					using(hwRes = (System.Net.HttpWebResponse)hwRq.GetResponse ())
					{
						iFileSize = (long)GetTotalSizeFromHeader (hwRes.Headers);

						using (smRespStream = hwRes.GetResponseStream ()) {

							using (saveFileStream) {

								if(smRespStream == null)
								{
									Debug.LogError("smRespStream is null! thworing exception....");
									throw new Exception();
								}

								while ((iByteSize = smRespStream.Read (downBuffer, 0, downBuffer.Length)) > 0) {

									try
									{
										saveFileStream.Write (downBuffer, 0, iByteSize);
										downloadState = DownloadState.Downloading;
									}catch(Exception e)
									{
										downloadState = DownloadState.Fail;
										yield break;
									}


									fINfo = new FileInfo (fullpath);
									iExistLen = fINfo.Length;

									if (time >= nextUpdate) 
									{
										nextUpdate = Mathf.FloorToInt (time) + 1;
										//Call your fonction
										UpdateEverySecond ();
									} 

									if(downloadCallback != null)
										downloadCallback ((float)downloadProgress, downloadSpeed);

									downloadProgress = ((double)iExistLen * 100) / (double)iFileSize;

								}

								// If download progress is full
								if (downloadProgress > 99.97f) {
									downloadProgress = 100;
									downloadState = DownloadState.Complete;
								}
							}

						}
					}
				

			} catch(WebException e) {
				// If Protocol Error, might be the file has already been fully downloaded
				if (e.Status == WebExceptionStatus.ProtocolError) {
					Debug.Log ("Error. WebExceptionStatus : ProtocolError !");
				}

				// No connection establish, possibly no internet at the beginning
				if (e.Status == WebExceptionStatus.NameResolutionFailure) {
					Debug.Log ("Error. WebExceptionStatus : NameResolutionFailure !");
				}

				// Lost connection during download
				if (e.Status == WebExceptionStatus.ConnectFailure) {
					Debug.Log ("Error. WebExceptionStatus : ConnectFailure !");
				}

				Debug.Log ("Error. Check internet connection!  " + e.Message);
				
				downloadState = DownloadState.Fail;

                Debug.Log("Error. downloadState:  " + downloadState);
            }

        }

        // Try to establish connection again, to see if there is anything wrong
        // Make sure code is run on Main Thread
        yield return Ninja.JumpToUnity;
        Debug.Log("DownloadState  " + downloadState);
        if (downloadState == DownloadState.Fail)
		{
			Debug.Log ("DownloadState.Fail, attempting reset... ");
			Reset ();

			if(failCallback != null)
				failCallback ();
		}

		if (downloadState == DownloadState.Complete)
		{
			if (successCallback != null) {
				successCallback ();
			}
		}

//		if (downloadProgress < 99.99f) {
//			Reset ();
//		}
	}

	float GetTotalSizeFromHeader(WebHeaderCollection header)
	{
		float result = -1;

		try
		{
			string[] contentRangeVal = header.GetValues (7);
            string TotalSize = contentRangeVal[0];
			result = float.Parse (TotalSize);
		} catch(Exception e) {
			Debug.Log ("Exception! " + e.Message);
			return -1;
		}
	
		return result;
	}

	void OnDestroy()
	{
		Pause ();
		DisposeStreams ();
	}


	void DisposeStreams()
	{
		if (saveFileStream != null) {
			saveFileStream.Dispose ();
			saveFileStream = null;
		}
		if (smRespStream != null) {
			smRespStream.Dispose ();
			smRespStream = null;
		}
	}

	void UpdateEverySecond(){
		if (iExistLen != 0 && iFileSize != 0) {
			downloadSpeed = ((float)iExistLen - tmp_down)/1024f;
			tmp_down = (float)iExistLen;
		}
	}

	#region newly added

	public void Download (Video video)
	{
		ScreenLoading.instance.Play ();

		if (MainAllController.instance != null) {
			
			string authToken = MainAllController.instance.user.token;

			if (authToken != null){

				this.video = video;

				Networking.instance.GetVideoLinkRequest (video.videoInfo.id, authToken, OnGetLink,OnFailedGetStreamingLink);

			}
		}
	}

	public void Resume()
	{
		Debug.LogError (" ++++Resume()!");
		
		if (MainAllController.instance != null && Networking.instance != null) {
			string authToken = MainAllController.instance.user.token;

			if (authToken != null && video != null) {
				Networking.instance.GetVideoLinkRequest (video.videoInfo.id, authToken, OnGetLink, OnFailedGetStreamingLink);
			} else {
				Debug.LogError (" Resume() Exception ! ");
			}

		}
	}

	public void OnGetLink(GetLinkVideoResponse getLinkVideoResponse){
		try
		{
			if (MainAllController.instance != null) {
				path = MainAllController.instance.user.GetPath ();
			}

			string filepath = MainAllController.instance.user.GetPathToFile (video.videoInfo.id,video.videoInfo.video_name);

			// Create a directory that houses the video file
			if (!File.Exists (filepath)) {
				Directory.CreateDirectory(Path.Combine(path,video.videoInfo.id));
			}

			if (downloadState != DownloadState.Downloading) {

				// If download state is not downloading, kick start download sequence
				DownLoad (getLinkVideoResponse.link,filepath,OnGetDownloadCallback,OnSuccessDownloadCallback,OnFailDownloadCallback);

			} else {

			}

			// Set UI state to downloading
			//SetDownloadStateUI (DownloadState.Downloading);

		} catch (System.Exception e)
		{

		}
		finally {
			ScreenLoading.instance.Stop ();
		}
	}

	void OnFailedGetStreamingLink()
	{
		ScreenLoading.instance.Stop ();
	}

	public void OnGetDownloadCallback(float progress, float downloadSpeed){

	}

	public void OnSuccessDownloadCallback()
	{
		Notifications_DownloadCompleted();
		CheckDownloadComplete ();
	}

	public void OnFailDownloadCallback()
	{

	}

	#endregion

	#region Notifications 

	/// <summary>
	/// Notificationses the downloaded.
	/// </summary>
	private void Notifications_DownloadCompleted(){
		NotificationsDemo notificationsDemo = UnityEngine.Object.FindObjectOfType<NotificationsDemo>();
		SettingsMenu settingsMenu = UnityEngine.Object.FindObjectOfType<SettingsMenu>();

		if (notificationsDemo != null && settingsMenu.GetNotificationText() == settingsMenu.GetkeyTrueNotification()){
			notificationsDemo.ScheduleLocalNotification ("" + video.videoInfo.video_name, "Download Completed!" );
		}
	}

	#endregion

	private void CheckDownloadComplete()
	{
		if (downloadState == DownloadState.Complete || downloadProgress == 100) {

			// Kick event that this video has been downladed
			MainAllController.instance.Downloaded (video);

			// Destroy the this object too
			video = null;
			Destroy (this.gameObject);
		}

	}

}
