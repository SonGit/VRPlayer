
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class VR_LocalVideoUI : LocalVideoUI
{
	
	public override void Setup(Video currentlocalVideo)
	{
		this.video = currentlocalVideo;
		this.videoTitle.text = (video as LocalVideo).videoName;
		this.videoLength.text = MakeLengthString ();

		#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
		StartCoroutine(LoadThumbnail ());
		#endif

	}
		

	public override void PlayIn3D ()
	{
		if (MainAllController.instance != null){
			MainAllController.instance.Play3D (video);
		}
	}


	void Update()
	{
		UIAnimation ();
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

	}

	public override void OnLoadedThumbnail()
	{
		videoImage.texture = thumbnailTexture;
		StopAllCoroutines();
		Debug.Log("------------------DONE");
	}


}
 