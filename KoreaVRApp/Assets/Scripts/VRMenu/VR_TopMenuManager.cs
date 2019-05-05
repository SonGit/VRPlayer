using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_TopMenuManager : MonoBehaviour
{
	public static VR_TopMenuManager instance;


	private VR_TopMenuButton[] btns;
	public VR_TopMenuButton currentBtn;

	void Awake(){
		instance = this;
	}

	void Start()
	{
		Init ();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Init(){
		btns = GetComponentsInChildren<VR_TopMenuButton> ();
		if (btns.Length > 0) {

			foreach (VR_TopMenuButton btn in btns) {
				switch (btn.name) {
				case "Video":
					btn.Init ();
					btn.OnActive ();
					currentBtn = btn;
					break;
				case "Screen":
					btn.Init ();
					btn.OnInactive ();
					break;
//				case "2D":
//					btn.Init ();
//					btn.OnInactive ();
//					break;
				case "VideoList":
					btn.Init ();
					btn.OnInactive ();
					break;
				default:
					break;
				}
			}
		}

		else {
			Debug.LogError ("basicButtonMenus[] is mull");
		}
	}
}
