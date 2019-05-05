using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_MyvideoTitleManager : MonoBehaviour
{
	public static VR_MyvideoTitleManager instance;

	private VR_MyVideoTitleButton[] btns;
	public VR_MyVideoTitleButton currentBtn;

	[HideInInspector] public VR_MyVideoTitleButton VideoListBtn;
	[HideInInspector] public VR_MyVideoTitleButton favoriteBtn;

	void Awake(){
		instance = this;
	}

	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void VideoListTitle(){
		btns = GetComponentsInChildren<VR_MyVideoTitleButton> ();
		if (btns.Length > 0) {
			foreach (VR_MyVideoTitleButton btn in btns) {
				switch (btn.name) {
				case "VideoList":
					VideoListBtn = btn;
					VideoListBtn.Init ();
					currentBtn = VideoListBtn;
					VideoListBtn.OnActive ();
					break;
				case "Favorite":
					favoriteBtn = btn;
					favoriteBtn.Init ();
					favoriteBtn.OnInactive ();
					break;
				default:
					break;
				}
			}
		}

		else {
			Debug.LogError ("VR_NavMenuButton[] is mull");
		}
	}

	public void FavoriteTitle(){
		btns = GetComponentsInChildren<VR_MyVideoTitleButton> ();
		if (btns.Length > 0) {
			foreach (VR_MyVideoTitleButton btn in btns) {
				switch (btn.name) {
				case "VideoList":
					VideoListBtn = btn;
					VideoListBtn.Init ();
					VideoListBtn.OnInactive ();
					break;
				case "Favorite":
					favoriteBtn = btn;
					favoriteBtn.Init ();
					currentBtn = favoriteBtn;
					favoriteBtn.OnActive ();
					break;
				default:
					break;
				}
			}
		}

		else {
			Debug.LogError ("VR_NavMenuButton[] is mull");
		}
	}
}
