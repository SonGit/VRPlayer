using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class MediaPlayerMenu : BasicMenuNavigation,IPointerDownHandler, IPointerUpHandler
{
	[Header("---- MediaPlayer ----")]
	[SerializeField]
	private MediaPlayer mediaPlayer;
	[SerializeField]
	private Button playBnt;
	[SerializeField]
	private Button pauseBnt;
	[SerializeField]
	private GameObject allUI;
	[SerializeField]
	private Slider videoSeekSlider;
	[SerializeField]
	private Text _currentTimeLabel;
	[SerializeField]
	private Text _totalTimeLabel;
	[SerializeField]
	private RectTransform _bufferedSliderRect;
	[SerializeField]
	private SubTitle2D subTitle2D;

	[SerializeField]
	private GameObject _subtitleLandscape;
	[SerializeField]
	private GameObject _subtitlePortrait;
	[SerializeField]
	private GameObject _buttonContainer;
    [SerializeField]
    private GameObject _videoFinishUI;
    [SerializeField]
    private Button _menuBnt;
    [SerializeField]
    private Button _replayVideoBnt;

    private Image _bufferedSliderImage;
	private float _setVideoSeekSliderValue;
	private Video currentVideo;
    private bool isVideoFinish;

	public float _delayCount = 0;
	public float _delayTime = 3f;
	public bool isShowDropDown;
	public bool isSetDropdownValue;

    protected override void Awake ()
	{
		base.Awake ();

		if (mediaPlayer == null){
			mediaPlayer = MainAllController.instance.mediaPlayer;
		}
	}

	protected override void Start ()
	{
		base.Start ();

		if (_bufferedSliderRect != null)
		{
			_bufferedSliderImage = _bufferedSliderRect.GetComponent<Image>();
		}

		if (playBnt != null){
			playBnt.onClick.AddListener(() =>
				{
					if(MainAllController.instance != null){
						MainAllController.instance.PlayButtonSound ();
					}
				});
		}

		if (pauseBnt != null){
			pauseBnt.onClick.AddListener(() =>
				{
					if(MainAllController.instance != null){
						MainAllController.instance.PlayButtonSound ();
					}
				});
		}

        if (_menuBnt != null)
        {
            _menuBnt.onClick.AddListener(() =>
            {
                if (MainAllController.instance != null)
                {
                    MainAllController.instance.PlayButtonSound();
                }
            });
        }

        if (_replayVideoBnt != null)
        {
            _replayVideoBnt.onClick.AddListener(() =>
            {
                if (MainAllController.instance != null)
                {
                    MainAllController.instance.PlayButtonSound();
                }
            });
        }
    }

	void Update()
	{
        CheckIsVideoFinish();

		DisableAllUI ();

		if (mediaPlayer == null){
			mediaPlayer = MainAllController.instance.mediaPlayer;
		}

		if (mediaPlayer != null && mediaPlayer.Info != null && mediaPlayer.Info.GetDurationMs() > 0f)
		{
			float time = mediaPlayer.Control.GetCurrentTimeMs();
			float duration = mediaPlayer.Info.GetDurationMs();
			float d = Mathf.Clamp(time / duration, 0.0f, 1.0f);


			_setVideoSeekSliderValue = d;
			videoSeekSlider.value = d;

			if (_bufferedSliderRect != null)
			{
				if (mediaPlayer.Control.IsBuffering())
				{
					float t1 = 0f;
					float t2 = mediaPlayer.Control.GetBufferingProgress();
					if (t2 <= 0f)
					{
						if (mediaPlayer.Control.GetBufferedTimeRangeCount() > 0)
						{
							mediaPlayer.Control.GetBufferedTimeRange(0, ref t1, ref t2);
							t1 /= mediaPlayer.Info.GetDurationMs();
							t2 /= mediaPlayer.Info.GetDurationMs();
						}
					}

					Vector2 anchorMin = Vector2.zero;
					Vector2 anchorMax = Vector2.one;

					if (_bufferedSliderImage != null &&
						_bufferedSliderImage.type == Image.Type.Filled)
					{
						_bufferedSliderImage.fillAmount = d;
					}
					else
					{   
						anchorMin[0] = t1;   
						anchorMax[0] = t2;
					}

					_bufferedSliderRect.anchorMin = anchorMin;
					_bufferedSliderRect.anchorMax = anchorMax;
				}
			}

			SetCurrentTimeLabel (time);
		}			
	}

	TimeSpan ts;
	/// <summary>
	/// Sets the current time label in VR Setting prefab.
	/// </summary>
	/// <param name="time">Time.</param>
	void SetCurrentTimeLabel(float time)
	{
		if (_currentTimeLabel != null) {
			ts = TimeSpan.FromMilliseconds(time);
			_currentTimeLabel.text = String.Format("{0:00}", ts.Hours) + ":" + String.Format("{0:00}", ts.Minutes) + ":" + String.Format("{0:00}", ts.Seconds);  ;
		}
	}

	#region PlayVieo

	public void Play(Video video, VideoUI videoUI, float resumeMs = 0)
	{
		
		if (mediaPlayer == null){
			mediaPlayer = MainAllController.instance.mediaPlayer;
		}

        //mediaPlayer.m_StereoPacking =  StereoPacking.None;

        //		#if UNITY_ANDROID
        //		if (mediaPlayer.PlatformOptionsAndroid.videoApi == Android.VideoApi.ExoPlayer) {
        //			mediaPlayer.PlatformOptionsAndroid.videoApi = Android.VideoApi.MediaPlayer;
        //		}
        //		#endif

        SetActiveObj(allUI, false);
        SetActiveObj(_buttonContainer, false);

        SetActiveObj(pauseBnt.gameObject, true);
        SetActiveObj(playBnt.gameObject, false);

        if (videoUI != null && _totalTimeLabel != null){
			_totalTimeLabel.text = videoUI.MakeLengthString ();
		}
			
		// Attemp to resume video from last time
		//for (int i = 0; i < vrPlayer.playedVideoList.Count; i++) {
			//if (video.videoInfo.id == vrPlayer.playedVideoList [i].videoInfo.id) {
				//resumeMs = vrPlayer.playedVideoList [i].lastTimeMs;
			//}
		//}

		string url = string.Empty;

		if (video is LocalVideo) {
			url = (video as LocalVideo).videoURL;
			InitSubtitle ();
			if (subTitle2D != null) {
				subTitle2D.SetViewrDropdown (false);
			}
		} else {
			url = MainAllController.instance.user.GetPathToFile (video.videoInfo.id, video.videoInfo.video_name);
//			if (currentVideo != null && currentVideo.videoInfo.id == video.videoInfo.id) {
//				Debug.Log ("Not Setup Subtitle");
//				isSetDropdownValue = false;
//			} else {
//				
//			}

			InitSubtitle ();
			SetupSubtitle (video);
			if (subTitle2D != null) {
				subTitle2D.SetViewrDropdown (true);
			}
			isSetDropdownValue = true;

			Debug.Log ("Setup Subtitle");

		}

		if (url != string.Empty) {

			mediaPlayer.OpenVideoFromFile (MediaPlayer.FileLocation.AbsolutePathOrURL, url, true);

			// Resume to last play time
			mediaPlayer.Control.SeekFast (resumeMs);

			Debug.LogError ("Play2D.......................................................................");

		} else {
			Debug.LogError ("Empty URL, cannot play video!");
		}

		currentVideo = video;

		OnRectTransformDimensionsChange ();

        DisableVideoFinishUI();

        isVideoFinish = false;
    }

	public void Streaming(Video video, VideoUI videoUI, string urlStreaming){

        SetActiveObj(allUI, false);
        SetActiveObj(_buttonContainer, false);

        SetActiveObj(pauseBnt.gameObject, true);
        SetActiveObj(playBnt.gameObject, false);

        if (videoUI != null && _totalTimeLabel != null){
			_totalTimeLabel.text = videoUI.MakeLengthString ();
		}

		if (video is LocalVideo) {
			InitSubtitle ();
			if (subTitle2D != null) {
				subTitle2D.SetViewrDropdown (false);
			}
		} else {
//			if (currentVideo != null && currentVideo.videoInfo.id == video.videoInfo.id) {
//				Debug.Log ("Not Setup Subtitle");
//				isSetDropdownValue = false;
//			} else {
//				
//
//				Debug.Log ("Setup Subtitle");
//			}

			InitSubtitle ();
			SetupSubtitle (video);
			if (subTitle2D != null) {
				subTitle2D.SetViewrDropdown (true);
			}
			isSetDropdownValue = true;
			Debug.Log ("Setup Subtitle");

		}

		if (urlStreaming != null){
			mediaPlayer.OpenVideoFromFile (MediaPlayer.FileLocation.AbsolutePathOrURL, urlStreaming, true);
			Debug.LogError ("Streaming2D.......................................................................");
		}

		currentVideo = video;

		OnRectTransformDimensionsChange ();

        DisableVideoFinishUI();

        isVideoFinish = false;
    }



	public void pause(){
		_delayCount = 0;

		if (mediaPlayer != null){
			mediaPlayer.Control.Pause ();
		}

        SetActiveObj(pauseBnt.gameObject, false);
        SetActiveObj(playBnt.gameObject, true);
    }

	public void Play(){
		_delayCount = 0;

		if (mediaPlayer != null){
			mediaPlayer.Control.Play ();
		}

        SetActiveObj(pauseBnt.gameObject, true);
        SetActiveObj(playBnt.gameObject, false);

    }

	public void OnPointerDown(PointerEventData e) {
        if (mediaPlayer != null && !mediaPlayer.Control.IsFinished())
        {
            Debug.Log("OnPointerDown");
            _delayCount = 0;
        }
	}

	public void OnPointerUp(PointerEventData e) {
        if (mediaPlayer != null && !mediaPlayer.Control.IsFinished())
        {
            Debug.Log("OnPointerUp");
            if (allUI != null)
            {
                if (allUI.activeSelf)
                {
                    SetActiveObj(allUI, false);
                    SetActiveObj(_buttonContainer, false);
                }
                else
                {
                    SetActiveObj(allUI, true);
                    SetActiveObj(_buttonContainer, true);
                }
            }
        }
	}

	public void OnVideoSeekSlider()
	{
		if (mediaPlayer && videoSeekSlider && videoSeekSlider.value != _setVideoSeekSliderValue)
		{
			mediaPlayer.Control.SeekFast(videoSeekSlider.value * mediaPlayer.Info.GetDurationMs());
			_delayCount = 0;
		}
	}

	public void Resume()
	{
		if (mediaPlayer != null){
			// Remember the time for resuming
			currentVideo.lastTimeMs = mediaPlayer.Control.GetCurrentTimeMs ();
		}
	}

	public void CloseVideo()
	{
		if (mediaPlayer != null){
			mediaPlayer.CloseVideo ();
		}
	}

	public void Play2D_3D()
	{
		if (MainAllController.instance != null){
			MainAllController.instance.Play2D_3D ();
		}
	}

	private void DisableAllUI(){

		if (allUI.activeSelf && !isShowDropDown) {
			_delayCount += Time.deltaTime;
			if (_delayCount >= _delayTime) {
                SetActiveObj(allUI, false);
                SetActiveObj(_buttonContainer, false);
                _delayCount = 0;
			}

		}
	}

	#endregion



	#region SubTitle

	public virtual void LoadSubTitles(string url)
	{
		mediaPlayer.EnableSubtitles (MediaPlayer.FileLocation.AbsolutePathOrURL, url);
	}

	public virtual void DisableSubtitles()
	{
		mediaPlayer.DisableSubtitles ();
		if (subTitle2D != null) {
			subTitle2D.ClearTextSubTitle ();
		}
	}

	public void SetupSubtitle(Video video)
	{
		if (!subTitle2D) {
			subTitle2D = GameObject.FindObjectOfType<SubTitle2D> ();
		}

		if(subTitle2D){
			subTitle2D.Setup (video.videoInfo.subtitles, video);
		}
	}

	void InitSubtitle(){
		RemoveAllSubtitleUI ();
		DisableSubtitles ();
	}

	public void RemoveAllSubtitleUI()
	{
		if (subTitle2D != null) {
			subTitle2D.RemoveAllSubtitleUI ();
		}
	}
		
	#endregion

	protected void OnRectTransformDimensionsChange()
	{
		Debug.Log("RectTransformDimensionsChange firing on " + Screen.orientation + " width ." + Screen.width + " heighty ." + Screen.height);

		if (Screen.width < Screen.height) {
            SetActiveObj(_subtitleLandscape, false);
            SetActiveObj(_subtitlePortrait, true);
		} else {
            SetActiveObj(_subtitleLandscape, true);
            SetActiveObj(_subtitlePortrait, false);
        }

//		if (_subtitleLandscape != null && _subtitlePortrait != null) {
//			switch (Screen.orientation) {
//
//			case ScreenOrientation.Portrait:
//				_subtitleLandscape.SetActive (true);
//				_subtitlePortrait.SetActive (false);
//				break;
//			case ScreenOrientation.Landscape:
//				_subtitleLandscape.SetActive (false);
//				_subtitlePortrait.SetActive (true);
//				break;
//			}
//		} else {
//			Debug.Log ("_subtitleLandscape/_subtitlePortrait not assigned!");
//		}
	}

    #region FinishVideo

    public void MediaPlayerMenu_OnBack() {
        if (MainAllController.instance != null)
        {
            MainAllController.instance.MediaPlayerMenu_OnBack();
        }
    }

    public void MediaPlayerMenu_ReplayVideo()
    {
        if (mediaPlayer != null)
        {
            mediaPlayer.Control.Rewind();
        }

        DisableVideoFinishUI();
        StartCoroutine(WaitIsVideoFinish());
    }

    private void EnableVideoFinishUI() {
        SetActiveObj(_videoFinishUI, true);
    }

    private void DisableVideoFinishUI()
    {
        SetActiveObj(_videoFinishUI,false);
    }

    private void CheckIsVideoFinish()
    {
        if (mediaPlayer != null && mediaPlayer.Control.IsFinished() && !isVideoFinish)
        {
            isVideoFinish = true;
            EnableVideoFinishUI();
            SetActiveObj(allUI, false);
            SetActiveObj(_buttonContainer, false);
        }
    }

    #endregion

    private void SetActiveObj(GameObject obj, bool value)
    {
        if (obj != null) {
            obj.SetActive(value);
        }
    }

    IEnumerator WaitIsVideoFinish() {
        yield return new WaitForSeconds(0.4f);
        isVideoFinish = false;
    }

}
