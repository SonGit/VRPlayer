using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_NavMenuManager : MonoBehaviour
{
	public static VR_NavMenuManager instance;

	public VR_NavMenuButton[] btns;
	public VR_NavMenuButton currentBtn;

	void Awake(){
		instance = this;
		btns = GetComponentsInChildren<VR_NavMenuButton> ();
	}

	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OnClick_PhoneVideoMenu()
	{
		foreach (VR_NavMenuButton btn in btns) {
			btn.Init ();
			if (btn is VR_PhoneVideoButton) {
				currentBtn = btn;
				btn.OnActive ();
			} else {
				btn.OnInactive ();
			}
		}
	}

	public void OnClick_DownloadMenu()
	{
		foreach (VR_NavMenuButton btn in btns) {
			btn.Init ();
			if (btn is VR_DownloadMenuButton) {
				currentBtn = btn;
				btn.OnActive ();
			} else {
				btn.OnInactive ();
			}
		}
	}

	public void OnClick_UserVideoMenu()
	{
		foreach (VR_NavMenuButton btn in btns) {
			btn.Init ();
			if (btn is VR_MyVideoButton) {
				currentBtn = btn;
				btn.OnActive ();
			} else {
				btn.OnInactive ();
			}
		}
	}

	public void OnClick_FavoriteVideoMenu()
	{
		foreach (VR_NavMenuButton btn in btns) {
			btn.Init ();
			if (btn is VR_MyVideoButton) {
				currentBtn = btn;
				btn.OnActive ();
			} else {
				btn.OnInactive ();
			}
		}
	}
}
