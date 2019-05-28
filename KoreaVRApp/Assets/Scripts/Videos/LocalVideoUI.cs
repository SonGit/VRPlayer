using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using CielaSpike;
using EasyMobile;
using System.IO;

public class LocalVideoUI: VideoUI
{
	[SerializeField] protected RawImage videoImage;
	[SerializeField] protected Text videoTitle;
	[SerializeField] protected Text videoLength;
	[SerializeField] protected Text videoSize;

	// Use this for initialization
	void Start () 
	{
       
	}

	public override void Setup(Video currentlocalVideo)
	{
		base.Setup (currentlocalVideo);

		videoSize.text = ((video as LocalVideo).videoSize / 1024f / 1024f).ToString("0.0") + " MB";
		videoTitle.text = (video as LocalVideo).videoName;

		videoLength.text = MakeLengthString ();

		#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
		StartCoroutine(LoadThumbnail ());
		#endif

    }

    IEnumerator LoadThumbnail()
    {
        bool gotThumbnail = false;

		string path = Application.persistentDataPath + "/localTemp/" + (video as LocalVideo).videoName;

		#if UNITY_ANDROID
		path = Application.persistentDataPath + "/" + (video as LocalVideo).videoName;
		#endif

		#if UNITY_IOS
		path = Application.persistentDataPath + "/localTemp/" + (video as LocalVideo).videoName;
		#endif

		//Texture2D texture = LocalVideoManager.instance.GetThumbnailFromCache (path);
		Texture2D texture = null;

		if (texture == null) {
			Debug.Log ("Looking at path: " + path);
			while (!gotThumbnail) {

				if (File.Exists (path)) {
					Debug.Log ("Found thumbnail at" + path);
					LoadThumbnail (path);
					gotThumbnail = true;

					yield break;
				}

				yield return new WaitForSeconds (.5f);
			}
		} else {
			Debug.Log ("Texture found in cache, loading.....");
		}

       
    }
		
	/// <summary>
	/// Call Android plugin to retrieve media list
	/// Syntax: {Media_name}@{Media_length_in_ms} , {Media_name2}@{Media_length_in_ms2}
	/// </summary>
	/// 
	private void LoadThumbnail_Threaded(string path)
	{
		plugin = new AndroidJavaClass ("com.example.unityplugin.PluginClass");

		this.StartCoroutine (LoadThumbnail_async(path));
	}

	AndroidJavaClass plugin;
	AndroidJavaClass unityClass;
	AndroidJavaObject unityActivity;
	AndroidJavaObject unityContext;

	IEnumerator LoadThumbnail_async(string path)
	{
		//AndroidJNI.AttachCurrentThread();

		byte[] pixelData;

//		using (var thumbnail = plugin.CallStatic<AndroidJavaObject> ("getThumbnail", path)) {
//
//			if (!thumbnail.Call<bool>("isLoaded")) {
//				Debug.LogError("NatShare Error: Failed to get thumbnail for video at path: "+path);
//				yield break;
//			}
//			var width = thumbnail.Get<int>("width");
//			var height = thumbnail.Get<int>("height");
//
//			using (var pixelBuffer = thumbnail.Get<AndroidJavaObject> ("pixelBuffer")) {
//				using (var array = pixelBuffer.Call<AndroidJavaObject>("array")) {
//					pixelData = AndroidJNI.FromByteArray(array.GetRawObject());
//				}
//			}
//
//			AndroidJNI.DetachCurrentThread ();
//
//			yield return Ninja.JumpToUnity;
//
//			thumbnailTexture = new Texture2D(width, height, TextureFormat.RGB565, false); // Weird texture format IMO
//			thumbnailTexture.LoadRawTextureData(pixelData);
//			thumbnailTexture.Apply ();
//			videoImage.texture = thumbnailTexture;
//			Debug.Log ("------------------DONE");
//		}


		yield return new WaitForEndOfFrame ();
	}


	#region NativeUI AlertPopup	
	/// <summary>
	/// Gets the alert when not loggin.
	/// </summary>
	public override void GetAlertDelete ()
	{
		base.GetAlertDelete ();
	}

	public override void OnAlertDeleteComplete ()
	{
        base.OnAlertDeleteComplete();
	}

    #endregion

    public override void OnLoadedThumbnail()
    {
        videoImage.texture = thumbnailTexture;
        StopAllCoroutines();
        Debug.Log("------------------DONE");
    }

	public override void RefreshCellView()
	{
		Setup(StorageMenu.instance.getVideoAtIndex(dataIndex));
	}
}
