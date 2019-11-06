using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using RenderHeads.Media.AVProVideo.Demos;

public class VR_SettingMenu : MonoBehaviour
{
	[SerializeField] private GameObject Root;

	public GameObject VideoSetting;
	public GameObject ScreenSetting;
	public GameObject alert2DBnt;
	public VCR vcr;
	public float _delayCount = 0;
	public float _delayTime = 0.5f;
	[SerializeField] private RawImage BG;

	private EventSystem eventSystem;
	private SceneVR sceneVR;

    public Camera cameraSubtitle;

    private bool _isShowSetting;

    public bool isShowSetting
    {
        get
        {
            return _isShowSetting;
        }

        set
        {
            _isShowSetting = value;
        }
    }

    void Awake(){
	}

	void Start()
    {
		eventSystem = Object.FindObjectOfType<EventSystem> ();
		sceneVR = Object.FindObjectOfType<SceneVR> ();
		vcr = Object.FindObjectOfType<VCR> ();
    }

    // Update is called once per frame
    void Update()
    {
//#if UNITY_ANDROID && !UNITY_EDITOR
//		CheckOOB ();
//#endif

        if(!isInTouchZone && touchzone.gameObject.activeInHierarchy)
        CheckOOB();

        if(Input.GetKeyDown(KeyCode.Q))
        {
            ShowSetting();
        }
    }

	public void CloseButton_OnClick(){
		HideSetting ();
		if (sceneVR != null){
			sceneVR.HideProgressBar ();
		}
	}

	public void PlayButton_OnClick(){
		HideSetting ();
		if (sceneVR != null){
			sceneVR.HideProgressBar ();
		}
	}

	public void ShowSetting(){
		if (!Root.activeSelf){
			Root.SetActive (true);
            touchzone.gameObject.SetActive(true);
            _isShowSetting = true;

            if (VR_TopMenuManager.instance != null){
				VR_TopMenuManager.instance.Init ();
			}

			ShowVideoSetting ();

			// Force pause video when setting is shown
			if (vcr != null) {
				vcr.OnPauseButton ();
			} else {
				vcr = Object.FindObjectOfType<VCR> ();
				if (vcr != null) {
					vcr.OnPauseButton ();
				}
			}

			if (BG != null) {
				BG.enabled = true;
			} else {
				Debug.LogError ("VR_SettingMenu BG is null!");
			}

            if (cameraSubtitle != null)
            {
                //cameraSubtitle.enabled = false;
            }
        }


	}

	public void HideSetting(){
		if (Root.activeSelf){
			Root.SetActive (false);
            touchzone.gameObject.SetActive(false);
        }

        _isShowSetting = false;

        // Force resuyme video when setting is shown
        if (vcr != null) {
			vcr.OnPlayButton ();
		} else {
			vcr = Object.FindObjectOfType<VCR> ();
			if (vcr != null) {
				vcr.OnPlayButton ();
			}

		}

		if (BG != null) {
			BG.enabled = false;
		}else {
			Debug.LogError ("VR_SettingMenu BG is null!");
		}

        if (cameraSubtitle != null)
        {
            //cameraSubtitle.enabled = true;
        }

    }

	#region VideoSetting
	public void ShowVideoSetting(){
		VideoSetting.SetActive (true);
		HideScreenSetting ();
		HideAlert2DBnt ();
	}

	public void HideVideoSetting(){
		if (VideoSetting.activeSelf) {
			VideoSetting.SetActive (false);
		}
	}
	#endregion


	#region ScreenSetting
	public void ShowScreenSetting(){
		if (!ScreenSetting.activeSelf) {
			ScreenSetting.SetActive (true);
			HideVideoSetting ();
			HideAlert2DBnt ();
		}
	}

	public void HideScreenSetting(){
		if (ScreenSetting.activeSelf) {
			ScreenSetting.SetActive (false);
		}
	}
	#endregion

	#region Alert2DBnt
	public void ShowAlert2DBnt(){
		if (alert2DBnt != null ) {
			alert2DBnt.SetActive (true);
		}else{
			Debug.LogError ("Null................");
		}
	}

	public void HideAlert2DBnt(){
		if (alert2DBnt != null) {
			alert2DBnt.SetActive (false);
		}else{
			Debug.LogError ("Null................");
		}
	}
	#endregion

	public void CheckOOB()
	{

        _delayCount += Time.deltaTime;

        if (_delayCount >= _delayTime)
        {
            HideSetting();
            if (sceneVR != null)
            {
                sceneVR.HideProgressBar();
            }
            _delayCount = 0;
            print("Hiding>>>>>");
        }

  //      if (eventSystem == null) {
		//	Debug.Log ("Event System not found!");
		//	return;
		//}

		//if (Root.activeSelf) {
		//	if (!eventSystem.IsPointerOverGameObject ()) {
		//		_delayCount += Time.deltaTime;
  //              print("--------------------------------------------");
  //              if (_delayCount >= _delayTime && Root.activeSelf) {
		//			HideSetting ();
		//			if (sceneVR != null){
		//				sceneVR.HideProgressBar ();
		//			}
		//			_delayCount = 0;
		//			print ("Hiding>>>>>");
		//		}

		//	} else {
		//		_delayCount = 0;
		//	}
		//}

	}

	public void ClickYesButton_2D(){
		if (MainAllController.instance != null){
			MainAllController.instance.ModeVR_OnMediaPlayerMenu ();
		}
	}

    public Transform touchzone;
    bool isInTouchZone;

    public void OnTEnter()
    {
        print("OnTEnter");
        isInTouchZone = true;
        _delayCount = 0;
    }

    public void OnTOut()
    {
        print("OnTOut");
        isInTouchZone = false;
    }
}
