using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo;
using UnityEngine.UI;
using System.IO;

public class VRPlayer : MonoBehaviour
{
	[Tooltip("Drag VR 360 Sphere + UI Handle object here")]
	[SerializeField]
	private Transform VR360Handle;

	[Tooltip("Drag the 360 cubemap sphere here")]
	[SerializeField]
	private Transform Cubemap;

	[Tooltip("Current VR Mode")]
	public VRMode currentMode;

	[Tooltip("All available VR modes")]
	[SerializeField]
	private VRMode[] vrModes;

	[Tooltip("Default VR mode. VR Player will use this mode on load")]
	[SerializeField]
	private VRMode defaultMode;

	[SerializeField]
	private VRSubtitle vrSubtitle;

	[SerializeField]
	private Slider seekSlider;

//	[SerializeField]
//	private Slider brightnessSlider;


	public List<Video> playedVideoList = new List<Video>();

	private Video currentVideo;

	[SerializeField]
	private Text videoNameLabel;

	[SerializeField]
	private Text _totalTimeLabel;

    // Start is called before the first frame update

	private SubtitlesUGUI[] subtitlesUGUIs;

	public AspectRatio aspectRatio;
	public float screenSize = 1.2f;

	public static VRPlayer instance;

	void Awake()
	{
		instance = this;
	}

	void Start()
    {
		vrModes = Object.FindObjectsOfType<VRMode> ();
		subtitlesUGUIs = Object.FindObjectsOfType<SubtitlesUGUI> ();

		DefaultMode ();

//		if (brightnessSlider != null) {
//			brightnessSlider.value = GetBrightness();
//		} else {
//			Debug.Log ("brightnessSlider not assigned");
//		}

		//SetupSubtitle ();

		//ScreenUnlock ();

		// Default is Ratio 16:9
		//Ratio169 ();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown (KeyCode.Q)) {
			AutoVR ();
		}
    }

	public void ClearTextSubTitle(){
		if (subtitlesUGUIs != null && subtitlesUGUIs.Length > 0){
			for (int i = 0; i < subtitlesUGUIs.Length; i++) {
				subtitlesUGUIs[i]._text.text = string.Empty;
			}
		}
	}

	/// <summary>
	/// Play video based on the specified url.
	/// Can be used for streaming
	/// </summary>
	/// <param name="url">URL.</param>
	public void Play(Video video, VideoUI videoUI)
	{
		float resumeMs = 0;
		// Attemp to resume video from last time
		for (int i = 0; i < playedVideoList.Count; i++) {
			if (video.videoInfo.id == playedVideoList [i].videoInfo.id) {
				resumeMs = playedVideoList [i].lastTimeMs;
			}
		}

		// Play the Video and resume to last time
		if (currentMode != null) {
			
			currentMode.Show ();
			currentMode.Play (video,resumeMs);

			currentVideo = video;

		} else {
			Debug.Log ("No VR Mode present!");
		}

		SetVideoNameLabel (video);

		if (videoUI != null && _totalTimeLabel != null){
			_totalTimeLabel.text = videoUI.MakeLengthString ();
		}
	}

	public void Stream(Video video, VideoUI videoUI, string url)
	{
		if (currentMode != null) {
			
			if (seekSlider != null) {
				seekSlider.value = 0;
			} else {
				Debug.Log ("seekSlider is null!");
			}

			currentMode.Show ();
			currentMode.PlayStream (video,url);
		} else {
			Debug.Log ("No VR Mode present!");
		}

		SetVideoNameLabel (video);

		if (videoUI != null && _totalTimeLabel != null){
			_totalTimeLabel.text = videoUI.MakeLengthString ();
		}
	}

	public void LoadSubTitles(string url)
	{
		if (currentMode != null) {
			currentMode.LoadSubTitles (url);
		} else {
			Debug.Log ("No VR Mode present!");
		}
	}

	public void DisableSubtitles()
	{
		if (currentMode != null) {
			currentMode.DisableSubtitles ();
		} else {
			Debug.Log ("No VR Mode present!");
		}
	}

	#region Change Mode
	public void StereoMode()
	{
		foreach (VRMode mode in vrModes) {

			if (mode is StereoMode) {
				mode.Show ();
				currentMode = mode;
			} else {
				mode.Hide ();
			}
		}

		if (lockingScreen) {
			ScreenLock ();
		} else {
			ScreenUnlock ();
		}

		RetainScreenlock ();
		SetCubemapVisibility (false);
	}

	public void CinemaMode()
	{
		foreach (VRMode mode in vrModes) {

			if (mode is CinemaMode) {
				mode.Show ();
				currentMode = mode;
			} else {
				mode.Hide ();
			}
		}

		if (lockingScreen) {
			ScreenLock ();
		} else {
			ScreenUnlock ();
		}

		RetainScreenlock ();
		SetCubemapVisibility (true);
	}

	public void SphereMode()
	{
		foreach (VRMode mode in vrModes) {

			if (mode is SphereMode) {
				mode.Show ();
				currentMode = mode;
			} else {
				mode.Hide ();
			}
		}

		if (lockingScreen) {
			ScreenLock ();
		} else {
			ScreenUnlock ();
		}

		RetainScreenlock ();
		SetCubemapVisibility (false);
	}

	public void FlatMode()
	{
		foreach (VRMode mode in vrModes) {

			if (mode is FlatMode) {
				mode.Show ();
				currentMode = mode;
			} else {
				mode.Hide ();
			}
		}

		RetainScreenlock ();
		SetCubemapVisibility (false);
	}

	void RetainScreenlock()
	{
		if (lockingScreen) {
			ScreenLock ();
		} else {
			ScreenUnlock ();
		}
	}

	void SetCubemapVisibility(bool value)
	{
		Cubemap.GetComponent<MeshRenderer> ().enabled = value;
	}

	/// <summary>
	/// Default mode
	/// </summary>
	public void DefaultMode()
	{
		foreach (VRMode mode in vrModes) {
			mode.Hide ();
		}

		if (defaultMode != null) {
			currentMode = defaultMode;
			defaultMode.Show ();

			if (defaultMode is FlatMode || defaultMode is CinemaMode) {
				RatioOriginal ();
			}
		}
	}
	#endregion


	#region Stereo Setting
	public void PackingTopBottom()
	{
		if (currentMode == null) {
			Debug.Log ("Current mode is null!");
			return;
		}
		currentMode.PackingTopBottom ();
	}

	public void PackingLeftRight()
	{
		if (currentMode == null) {
			Debug.Log ("Current mode is null!");
			return;
		}
		currentMode.PackingLeftRight ();
	}

	public void PackingNone()
	{
		if (currentMode == null) {
			Debug.Log ("Current mode is null!");
			return;
		}
		currentMode.PackingNone ();
	}
	#endregion

	#region Scale Setting
	public void SetSize(float scale)
	{
		scale = scale / 10;
		if (currentMode == null) {
			Debug.Log ("Current mode is null!");
			return;
		}

		if (currentMode is CinemaMode || currentMode is FlatMode) {
			currentMode.SetSize (scale);
		}
	}
	public void Ratio43()
	{
		if (currentMode != null) {
			if (currentMode is CinemaMode || currentMode is FlatMode) {
				currentMode.Ratio43 ();
				aspectRatio = AspectRatio.RATIO_43;
			} 		
		}
	}
	public void Ratio1851()
	{

		if (currentMode != null) {
			if (currentMode is CinemaMode || currentMode is FlatMode) {
				currentMode.Ratio1851 ();
				aspectRatio = AspectRatio.RATIO_1851;
			}	
		}
	}

	public void Ratio169()
	{
		if (currentMode != null) {
			if (currentMode is CinemaMode || currentMode is FlatMode) {
				currentMode.Ratio169 ();
				aspectRatio = AspectRatio.RATIO_169;
			}
		}
	}

	public void RatioOriginal()
	{
		if (currentMode != null) {
			if (currentMode is FlatMode) {
				currentMode.RatioOriginal ();
				aspectRatio = AspectRatio.ORIGINAL;
			}
			if (currentMode is CinemaMode ) {
				currentMode.Ratio2351 ();
				aspectRatio = AspectRatio.RATIO_2351;
			}
		}
	}


	#endregion

	#region Brightness Setting

	/// <summary>
	/// Ask for setting writing permission on Android
	/// </summary>
	/// <returns><c>true</c> if this instance has write settings permission; otherwise, <c>false</c>.</returns>
	private bool HasWriteSettingsPermission() { 
		#if UNITY_ANDROID  && !UNITY_EDITOR
		var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"); 
		var system = new AndroidJavaClass("android.provider.Settings$System"); 
		if (!system.CallStatic<bool>("canWrite", activity)) 
		{ var settings = new AndroidJavaClass("android.provider.Settings"); 
			var uri = new AndroidJavaClass("android.net.Uri"); 
			var intent = new AndroidJavaObject("android.content.Intent", settings.GetStatic<string>("ACTION_MANAGE_WRITE_SETTINGS")); 
			intent.Call<AndroidJavaObject>("setData", uri.CallStatic<AndroidJavaObject>("parse", "package:" + activity.Call<string>("getPackageName")));
			activity.Call("startActivityForResult", intent, 1); 
			return false; 
		} 
		return true;
		#endif 

		return false;
	}

	/// <summary>
	/// Sets the brightness on Android.
	/// </summary>
	/// <param name="brightnessVal">Brightness value.</param>
	public void SetBrightness(float brightnessVal)
	{
		if (!HasWriteSettingsPermission() ) {
			Debug.Log ("You do not have writing permission");
			return;
		}

		var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
		activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
			var window = activity.Call<AndroidJavaObject>("getWindow");
			var lp = window.Call<AndroidJavaObject>("getAttributes");
			lp.Set("screenBrightness", brightnessVal);
			window.Call("setAttributes", lp);

			int b = 1 + (int)(Mathf.Min(1, Mathf.Max(0, brightnessVal)) * 254f); // 1 <= b <= 255
			var system = new AndroidJavaClass("android.provider.Settings$System");
			var contentResolver = activity.Call<AndroidJavaObject>("getContentResolver");
			//system.CallStatic("putInt", contentResolver, system.CallStatic<string>("screen_brightness_mode"), 0);
			system.CallStatic<bool>("putInt", contentResolver, system.GetStatic<string>("SCREEN_BRIGHTNESS_MODE"), 0);
			system.CallStatic<bool>("putInt", contentResolver, system.GetStatic<string>("SCREEN_BRIGHTNESS"), b);
		}));
	}

	/// <summary>
	/// Get Brightness Value
	/// </summary>
	/// <returns>The brightness.</returns>
	private float GetBrightness() {
		if (!HasWriteSettingsPermission() ) {
			Debug.Log ("You do not have writing permission");
			return 0.7f;
		}
		var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
		var system = new AndroidJavaClass("android.provider.Settings$System");
		var contentResolver = activity.Call<AndroidJavaObject>("getContentResolver");
		var b = system.CallStatic<int>("getInt", contentResolver, system.GetStatic<string>("SCREEN_BRIGHTNESS"));
		return b;
	}

	#endregion

	#region Subtitle Setting

	public void SetupSubtitle(Video video)
	{

		if (!vrSubtitle) {
			vrSubtitle = GameObject.FindObjectOfType<VRSubtitle> ();
		}
			
		if(vrSubtitle){
			vrSubtitle.ResetVRSubtitle_ButtonUI ();
			vrSubtitle.Setup (video.videoInfo.subtitles, video);
		}

	}

	public void RemoveAllSubtitle()
	{
		if(vrSubtitle){
			vrSubtitle.RemoveAllSubtitleUI ();
		}
	}

	public void InitSubtitle(){
		RemoveAllSubtitle ();
		if(MainAllController.instance != null){
			MainAllController.instance.DisableSubtitleVR ();
		}
	}

	#endregion

	bool lockingScreen;

	public void ScreenLock()
	{
		if (Cubemap != null && VR_Recenterer.instance != null) {
			
			VR_Recenterer.instance.Recenter ();

			currentMode.MakeMeshChildOf (Cubemap);
			Cubemap.SetParent (Camera.main.transform);

			lockingScreen = true;

		} else {
			Debug.LogError ("ScreenLock Exception!");
		}
	}

	public void ScreenUnlock()
	{
		if (Cubemap != null && VR_Recenterer.instance != null) {
			
			VR_Recenterer.instance.Recenter ();

			currentMode.MakeMeshChildOf (transform);
			Cubemap.SetParent (transform);

			lockingScreen = false;

		}else {
			Debug.LogError ("ScreenUnlock Exception!");
		}
	}

	void OnDisable()
	{
		if (currentVideo != null) {
			
			try
			{
                // When Video IsFinished -> Rewind Video
                if (currentMode.GetMediaPlayer().Control.IsFinished())
                {
                    currentMode.GetMediaPlayer().Control.Rewind();

                    Debug.Log("Rewind Video.....................");
                }

                // Remember the time for resuming
                currentVideo.lastTimeMs = currentMode.GetMediaPlayer ().Control.GetCurrentTimeMs ();

				bool updated = false;
				for(int i = 0 ; i < playedVideoList.Count ; i ++)
				{
					if(playedVideoList[i].videoInfo.id == currentVideo.videoInfo.id)
					{
						playedVideoList[i] = currentVideo;
						updated = true;
						break;
					}
				}

				// add the list if there is no update
				if(!updated)
				{
					playedVideoList.Add (currentVideo);
				}

			}catch (System.Exception e) 
			{

			}


		} else {
			Debug.LogError ("currentVideo is null!");
		}

		if (currentMode != null) {
			currentMode.CloseVideo ();
			currentMode.Hide ();
		}

	}

	public void AutoVR()
	{
		if (currentMode == null) {
			
			DefaultMode ();

		} else {

			int videoWidth = currentMode.GetMediaPlayer ().Info.GetVideoWidth ();
			int videoHeight = currentMode.GetMediaPlayer ().Info.GetVideoHeight ();
			Debug.Log ("videoWidth " + videoWidth + "   videoHeight " + videoHeight);
			if (videoWidth == 0 || videoHeight == 0) {
				Debug.Log ("Error AutoVR! Width/Height is zero!");
				return;
			}

			float aspectRatio = videoWidth / videoHeight;

			// if aspect ratio = 1, high chance that the video is 180 
			if (aspectRatio == 1) {

				StereoMode ();

				PackingTopBottom ();

				if (VR_SettingsManager.instance != null){
					VR_SettingsManager.instance.TB1Btn ();
				}

			}

			// if aspect ratio = 2, high chance that the video is 360
			if (aspectRatio == 2) {

				SphereMode ();

				PackingLeftRight ();

                if (VR_SettingsManager.instance != null)
                {
                    VR_SettingsManager.instance.LR1Btn();
                }

			}
        }
	}

	void SetVideoNameLabel(Video video)
	{
		if (videoNameLabel != null) {
			if (video is LocalVideo) {
				videoNameLabel.text = Path.GetFileNameWithoutExtension ((video as LocalVideo).videoURL); // Exeption "type .mp4"
			} else {
				videoNameLabel.text = video.videoInfo.video_name;
			}

			videoNameLabel.enabled = false;
			videoNameLabel.enabled = true;

		} else {
			Debug.Log ("videoNameLabel not assigned!");
		}
	}

}
