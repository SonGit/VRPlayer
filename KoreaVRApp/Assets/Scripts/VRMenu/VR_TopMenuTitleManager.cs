using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_TopMenuTitleManager : MonoBehaviour
{
	private VR_BasicButton[] vr_TopMenuTitles;
	public VR_BasicButton currentBtn;
	[HideInInspector] public VR_BasicButton phoneVideoTitle;
	[HideInInspector] public VR_BasicButton downloadTitle;
	[HideInInspector] public VR_BasicButton myVideoTitle;

	void Awake(){
		Init ();
	}

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void Init(){
		vr_TopMenuTitles = GetComponentsInChildren<VR_BasicButton> ();
		if (vr_TopMenuTitles.Length > 0) {

			foreach (VR_BasicButton vr_TopMenuTitle in vr_TopMenuTitles) {
				switch (vr_TopMenuTitle.name) {
				case "PhoneVideo":
					phoneVideoTitle = vr_TopMenuTitle;
					break;
				case "Download":
					downloadTitle = vr_TopMenuTitle;
					break;
				case "MyVideo":
					myVideoTitle = vr_TopMenuTitle;
					break;
				default:
					break;
				}
			}
		}

		else {
			Debug.LogError ("vr_MainMenuTitles[] is mull");
		}
	}

	public void TitleViewable()
	{
		if (VR_NavMenuManager.instance != null) {
			foreach (VR_NavMenuButton btn in VR_NavMenuManager.instance.btns) {
				if (btn is VR_PhoneVideoButton && VR_NavMenuManager.instance.currentBtn == btn) {
					phoneVideoTitle.SetActive (true);
					downloadTitle.SetActive (false);
					myVideoTitle.SetActive (false);
				}else if(btn is VR_DownloadMenuButton && VR_NavMenuManager.instance.currentBtn == btn){
					phoneVideoTitle.SetActive (false);
					downloadTitle.SetActive (true);
					myVideoTitle.SetActive (false);
				}else if(btn is VR_MyVideoButton && VR_NavMenuManager.instance.currentBtn == btn){
					phoneVideoTitle.SetActive (false);
					downloadTitle.SetActive (false);
					myVideoTitle.SetActive (true);
				}
			}

		}
	}
}
