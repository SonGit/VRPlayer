using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using CielaSpike;
using System.Threading;
using System;
using System.Net;
using UnityEngine.Networking;


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
//	private System.Net.HttpWebRequest hwRq;
//	private System.Net.HttpWebResponse hwRes;
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
			}

		}

		// Try to establish connection again, to see if there is anything wrong
		// Make sure code is run on Main Thread
		yield return Ninja.JumpToUnity;

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
			string TotalSize =  (contentRangeVal [0].Split ('/'))[1];
			result = float.Parse (TotalSize);
		} catch(Exception e) {
			Debug.Log ("Exception! " + e.Message);
			return -1;
		}
	
		return result;
	}

	void OnDisable()
	{
//		Pause ();
//		DisposeStreams ();
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
			downloadSpeed = ((float)iExistLen - tmp_down)/1048576f;
			tmp_down = (float)iExistLen;
		}
	}

}
