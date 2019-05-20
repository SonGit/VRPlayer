using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Scene2D : AppScene
{
	public Camera camera;
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
		if(camera != null)
		camera.enabled = false;


		yield return new WaitForEndOfFrame();
		UnityEngine.XR.XRSettings.LoadDeviceByName(newDevice);
		yield return null;
		UnityEngine.XR.XRSettings.enabled = false;

		//Screen.orientation = ScreenOrientation.Portrait;

//		int targetWidth = MainAllController.instance.maxWidth / 2;
//		int targetHeight = MainAllController.instance.maxHeight / 2;
//
//		if (targetHeight > 1000) {
//			Debug.Log ("++++ 2D last " + Screen.currentResolution.width + "  " + Screen.currentResolution.height);
//			Screen.orientation = ScreenOrientation.Portrait;
//			yield return new WaitForSeconds (.25f);
//			Screen.SetResolution (targetWidth,targetHeight,false);
//			yield return new WaitForSeconds (.25f);
//			Debug.Log ("++++ 2D current " + Screen.currentResolution.width + "  " + Screen.currentResolution.height);
//		}
//
		if(camera != null)
		camera.enabled = true;

		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 50;

//		Debug.Log ("++++ " + Screen.currentResolution.width + "  " + Screen.currentResolution.height);
//		Screen.orientation = ScreenOrientation.Portrait;
//		Screen.SetResolution (720,1280,false);
//
//		yield return new WaitForEndOfFrame();
//		UnityEngine.XR.XRSettings.LoadDeviceByName(newDevice);
	}

	public override void Hide()
	{
		base.Hide ();
	}
}
