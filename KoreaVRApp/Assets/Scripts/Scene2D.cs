using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Scene2D : AppScene
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public override void Show(BasicMenu lastMenu = null)
	{

		base.Show (lastMenu);
		StartCoroutine(LoadDevice("none"));
	}

	IEnumerator LoadDevice(string newDevice)
	{
		Screen.fullScreen = false;

		yield return new WaitForEndOfFrame();
		UnityEngine.XR.XRSettings.LoadDeviceByName(newDevice);
		yield return null;
		UnityEngine.XR.XRSettings.enabled = true;

		Screen.orientation = ScreenOrientation.Portrait;

		// Only render at half resolution
		// If target res is too low, ignore
		int targetWidth = MainAllController.instance.maxWidth / 2;
		int targetHeight = MainAllController.instance.maxHeight / 2;

		if (targetHeight > 1000) {
			Screen.orientation = ScreenOrientation.Portrait;
			yield return new WaitForSeconds (.25f);
			Screen.SetResolution (targetWidth,targetHeight,false);
			yield return new WaitForSeconds (.25f);
		}
			
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 50;


		Screen.orientation = ScreenOrientation.Portrait;
		Screen.SetResolution (720,1280,false);

		Debug.Log ("2D ++++Current Resolution  " + Screen.currentResolution.width + "  " + Screen.currentResolution.height);
//
//		yield return new WaitForEndOfFrame();
//		UnityEngine.XR.XRSettings.LoadDeviceByName(newDevice);
	}

	public override void Hide()
	{
		base.Hide ();
	}
}
