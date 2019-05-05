using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_RatioManager : MonoBehaviour
{
	public static VR_RatioManager instance;


	private VR_ButtonRatio[] btns;
	public VR_ButtonRatio currentBtn;
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

	}

	void Init(){
		btns = GetComponentsInChildren<VR_ButtonRatio> ();
		if (btns.Length > 0) {

			foreach (VR_ButtonRatio btn in btns) {
				switch (btn.name) {
				case "4:3":
					btn.Init ();
					btn.OnInactive ();
					//btn.thisBtn.onClick.AddListener (vplayer.Ratio43);
					break;
				case "16:9":
					btn.Init ();
					btn.OnInactive ();
					//btn.thisBtn.onClick.AddListener (vplayer.Ratio169);
					break;
				case "1.85:1":
					btn.Init ();
					btn.OnInactive ();
					//btn.thisBtn.onClick.AddListener (vplayer.Ratio1851);
					break;
				case "Original":
					btn.Init ();
					btn.OnActive ();
					//btn.thisBtn.onClick.AddListener (vplayer.Ratio169);
					currentBtn = btn;
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
