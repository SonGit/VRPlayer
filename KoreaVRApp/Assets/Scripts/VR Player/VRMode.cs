using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo;

public enum AspectRatio
{
	NONE,
	RATIO_43,
	RATIO_169,
	RATIO_1851,
}

public abstract class VRMode : MonoBehaviour
{
	[SerializeField]
	private GameObject root;

	[SerializeField]
	protected MediaPlayer mediaPlayer;

	[SerializeField]
	protected ApplyToMesh applyToMesh;

	protected virtual void Init()
    {
        applyToMesh = this.GetComponentInChildren<ApplyToMesh> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public virtual void Play(Video video,float resumeMs = 0 )
	{
		string url = string.Empty;

		if (video is LocalVideo) {
			url = (video as LocalVideo).videoURL;
		} else {
			url = MainAllController.instance.user.GetPathToFile (video.videoInfo.id,video.videoInfo.video_name);
		}

		if (url != string.Empty) {
			
			mediaPlayer.OpenVideoFromFile (MediaPlayer.FileLocation.AbsolutePathOrURL, url, true);

			// Resume to last play time
			mediaPlayer.Control.SeekFast (resumeMs);

		} else {
			Debug.LogError ("Empty URL, cannot play video!");
		}

	}

	public virtual void PlayStream(Video video,string url)
	{
		#if UNITY_ANDROID  && !UNITY_EDITOR
		mediaPlayer.OpenVideoFromFile (MediaPlayer.FileLocation.AbsolutePathOrURL, url, true);
		#endif 
		// Reset
		//mediaPlayer.Control.SeekFast (0);
		#if UNITY_EDITOR
		mediaPlayer.OpenVideoFromFile (MediaPlayer.FileLocation.AbsolutePathOrURL, url, true);
		#endif 
	}

	public virtual void LoadSubTitles(string url)
	{
		mediaPlayer.EnableSubtitles (MediaPlayer.FileLocation.AbsolutePathOrURL, url);
	}

	public virtual void DisableSubtitles()
	{
		mediaPlayer.DisableSubtitles ();
	}

	public void CloseVideo()
	{
		if (mediaPlayer != null)
			mediaPlayer.CloseVideo ();
	}

	public virtual void Show()
	{
		VR_Recenterer.instance.Recenter ();

		if (root != null) {
			root.SetActive (true);
		}
		if (applyToMesh != null) {
			applyToMesh.gameObject.SetActive (true);
		}
	}

	public virtual void Hide()
	{
		if (root != null) {
			root.SetActive (false);
		}

		if (applyToMesh != null) {
			applyToMesh.gameObject.SetActive (false);
		}
			
	}

	public MediaPlayer GetMediaPlayer()
	{
		return mediaPlayer;
	}

	public void PackingTopBottom()
	{
		if (mediaPlayer == null || applyToMesh == null) {
			Debug.Log ("Current mode is null!");
			return;
		}
		mediaPlayer.m_StereoPacking =  StereoPacking.TopBottom;
		applyToMesh.ForceUpdate ();

		//Update UI
		//VR_SettingsManager.instance.TB1Btn();
	}

	public void PackingLeftRight()
	{
		if (mediaPlayer == null || applyToMesh == null) {
			Debug.Log ("Current mode is null!");
			return;
		}
		mediaPlayer.m_StereoPacking =  StereoPacking.LeftRight;
		applyToMesh.ForceUpdate ();

		//Update UI
		//VR_SettingsManager.instance.LR1Btn();
	}

	public void PackingNone()
	{
		if (mediaPlayer == null || applyToMesh == null) {
			Debug.Log ("Current mode is null!");
			return;
		}
		mediaPlayer.m_StereoPacking =  StereoPacking.None;
		applyToMesh.ForceUpdate ();

		//Update UI
		//VR_SettingsManager.instance.FlatModeBtn();
	}

	float currentScale = 0;

	public void SetSize(float scale)
	{
		if (scale > 2)
			scale = 2;
		if (scale < .5f)
			scale = .5f;

		if (scale > 1) {
			scale = scale - 1;
		} else {
			scale = -(Mathf.Abs (1 - scale));
		}

		currentScale = scale * 9f;

		switch (VRPlayer.instance.aspectRatio) {

		case AspectRatio.RATIO_43:
			Ratio43 ();
			break;
		case AspectRatio.RATIO_169:
			Ratio169 ();
			break;
		case AspectRatio.RATIO_1851:
			Ratio1851 ();
			break;
		}

		VRPlayer.instance.screenSize = currentScale;
	}

	public void DefaultSize()
	{
		applyToMesh.transform.localScale = ratio169;
	}

	protected void ResumeRatio()
	{
		SetSize (VRPlayer.instance.screenSize);
	}

	Vector3 ratio43 = new Vector3(14.5f,10.8777f,1f);
	Vector3 ratio169 = new Vector3(14.6f,8.213f,1f);
	Vector3 ratio1851 = new Vector3(14.6f,8.918f,1f);

	public void Ratio43()
	{
		float scaleX = ratio43.x + currentScale;
		float scaleY = scaleX / 1.333f;
		applyToMesh.transform.localScale = new Vector3 (scaleX,scaleY,0);
	}

	public void Ratio169()
	{
		float scaleX = ratio169.x + currentScale;
		float scaleY = scaleX / 1.777777f;
		applyToMesh.transform.localScale = new Vector3 (scaleX,scaleY,0);
	}

	public void Ratio1851()
	{
		float scaleX = ratio1851.x + currentScale;
		float scaleY = scaleX / 1.85f;
			applyToMesh.transform.localScale = new Vector3 (scaleX,scaleY,0);
	}
		
	// Force applyToMesh to be child of a transform
	// useful for Screenlock/Unlock function
	public void MakeMeshChildOf(Transform t)
	{
		if (applyToMesh != null) {
			applyToMesh.transform.SetParent (t);
		} else {
			Debug.LogError ("MakeMeshChildOf Exception : ApplyToMesh is null");
		}
	}

	// Force applyToMesh to be child of the root
	public void ReturnMeshAsChild()
	{
		if (applyToMesh != null) {
			applyToMesh.transform.SetParent (root.transform);
		} else {
			Debug.LogError ("ReturnMeshAsChild Exception : ApplyToMesh is null");
		}
	}
}
