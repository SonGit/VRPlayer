using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_MenuManager : MonoBehaviour
{
	public static VR_MenuManager instance;
	public GameObject MainMenu;
	public GameObject SettingScreen;
	public GameObject SettingVideo;


	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}

	public void ShowMenu(){
		if (MainMenu.activeInHierarchy == false) {
			MainMenu.SetActive (true);
		}
	}

	public void ShowSettingScreen(){
		if (SettingScreen.activeInHierarchy == false) {
			SettingScreen.SetActive (true);
		}
	}

	public void ShowSettingVideo(){
		SettingVideo.SetActive (true);
	}

	public void HideMenu(){
		MainMenu.SetActive (false);
	}

	public void HideSettingScreen(){
		SettingScreen.SetActive (false);
	}

	public void HideSettingVideo(){
		SettingVideo.SetActive (false);
	}
}
