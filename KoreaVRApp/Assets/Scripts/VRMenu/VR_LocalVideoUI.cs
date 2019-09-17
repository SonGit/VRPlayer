﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class VR_LocalVideoUI : LocalVideoUI
{
	
	public override void Setup(Video video)
	{
		if (root != null){
			root.gameObject.SetActive(video != null);
		}
			
		if (video != null)
		{
			this.video = video;
			this.videoTitle.text = Path.GetFileNameWithoutExtension ((video as LocalVideo).videoURL); // Exeption "type .mp4"
			this.videoLength_videoSize.text = MakeLengthString ();
            //videoImage.texture = null;

        #if !UNITY_EDITOR
            loadThumbnail = true;
        #endif

        }

    }
		

	public override void PlayIn3D ()
	{
		if (MainAllController.instance != null){
			MainAllController.instance.Play3D (video, this);
		}
	}


	public override void Update ()
	{
		if (loadThumbnail)
		{
			StartCoroutine(LoadThumbnail());
			loadThumbnail = false;
		}
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

		while (!gotThumbnail) {
			
			Debug.Log ("VR Looking at path: " + path);

			if (File.Exists (path)) {
				Debug.Log ("VR Found thumbnail at" + path);
                LoadThumbnail (path);
				gotThumbnail = true;

				yield break;
			}

			yield return new WaitForSeconds (.5f);
		}

	}

	public override void OnLoadedThumbnail()
	{
		videoImage.texture = thumbnailTexture;
		//thumbnailTexture = null;
		Debug.Log("------------------DONE VR");
	}

}
 