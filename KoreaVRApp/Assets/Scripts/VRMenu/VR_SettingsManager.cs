using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_SettingsManager : MonoBehaviour
{
	public static VR_SettingsManager instance;


	private VR_ButtonSettings[] btns;
	public VR_ButtonSettings currentBtn;
    
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
		btns = GetComponentsInChildren <VR_ButtonSettings> ();
		if (btns.Length > 0) {
			foreach (VR_ButtonSettings btn in btns) {
				switch (btn.name) {
				case "Flat":
					btn.Init ();
					btn.OnActive ();
					currentBtn = btn;
					break;
				case "btn1-2LR":
					btn.Init ();
					btn.OnInactive ();
					break;
				case "btn2-1LR":
					btn.Init ();
					btn.OnInactive ();
					break;
				case "btn1-2TB":
					btn.Init ();
					btn.OnInactive ();
					break;
				case "btn2-1TB":
					btn.Init ();
					btn.OnInactive ();
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

    public void FlatModeBtn()
	{
		SetActiveBtn ("Flat");
	}

	public void LR1Btn()
	{
		SetActiveBtn ("btn1-2LR");
	}

	public void LR2Btn()
	{
		SetActiveBtn ("btn2-2LR");
	}

	public void TB1Btn()
	{
		SetActiveBtn ("btn1-2TB");
	}

	public void TB2Btn()
	{
		SetActiveBtn ("btn2-2TB");
	}

	void SetActiveBtn(string btnName)
	{
	    if (btns.Length > 0)
        {
			foreach (VR_ButtonSettings btn in btns)
            {
			    if (btn.name == btnName) {
				    btn.OnActive ();
                    currentBtn = btn;
			    } else {
				    btn.OnInactive ();
			    }

		    }
	    }

	}

}
