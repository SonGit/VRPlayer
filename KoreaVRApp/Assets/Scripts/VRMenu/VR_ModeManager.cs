using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_ModeManager : MonoBehaviour
{
	public static VR_ModeManager instance;


	private VR_ButtonMode[] btns;
	public VR_ButtonMode currentBtn;
	private VRPlayer vplayer;
    
	void Awake(){
		instance = this;
	}

	void Start()
    {
		vplayer = UnityEngine.Object.FindObjectOfType<VRPlayer>();
		Init ();

    }

    // Update is called once per frame
    void Update()
    {
//		if (Input.GetKeyDown(KeyCode.Q)){
//			vplayer.FlatMode ();
//		}
//		if (Input.GetKeyDown(KeyCode.W)){
//			vplayer.CinemaMode ();
//		}
//		if (Input.GetKeyDown(KeyCode.E)){
//			vplayer.StereoMode();
//		}
//		if (Input.GetKeyDown(KeyCode.R)){
//			vplayer.SphereMode();
//		}
    }

	public void Init(){
		btns = GetComponentsInChildren<VR_ButtonMode> ();
		if (btns.Length > 0) {
			foreach (VR_ButtonMode btn in btns) {
				switch (btn.name) {
				case "Flat":
					btn.Init ();
					btn.OnInactive ();
					//btn.thisBtn.onClick.AddListener (vplayer.FlatMode);
					break;
				case "Cinema":
					btn.Init ();
					btn.OnActive ();
					//btn.thisBtn.onClick.AddListener (vplayer.CinemaMode);
					currentBtn = btn;
					break;
				case "180*":
					btn.Init ();
					btn.OnInactive ();
					//btn.thisBtn.onClick.AddListener (vplayer.StereoMode);
					break;
				case "360*":
					btn.Init ();
					btn.OnInactive ();
					//btn.thisBtn.onClick.AddListener (vplayer.SphereMode);
					break;
				case "Auto":
					btn.Init ();
					btn.OnInactive ();
					//btn.thisBtn.onClick.AddListener (vplayer.AutoVR);
					break;
				default:
					break;
				}
			}
		}

		 else {
			Debug.LogError ("basicButtonMenus[] Null");
		}
	}
}
