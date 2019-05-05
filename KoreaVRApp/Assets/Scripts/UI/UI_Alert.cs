using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using EasyMobile;
using EasyMobile.Demo;

public class UI_Alert : MonoBehaviour
{
    // Start is called before the first frame update

	public Text testing;

	public void ShowAlert()
	{
		NativeUI.AlertPopup alert = NativeUI.ShowTwoButtonAlert("Confirm Your Delete", "Are You Sure?.", "Yes", "No");

		if (alert != null)
			alert.OnComplete += OnAlertCompleteHandler;
	}

	void OnAlertCompleteHandler(int buttonIndex)
	{

		switch (buttonIndex)
		{
		case 0:
			// Button 1 was clicked
			testing.text = "Yes";
			break;
		case 1:
			// Button 2 was clicked
			testing.text = "No";
			break;
		default:
			break;
		}
	}

	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
