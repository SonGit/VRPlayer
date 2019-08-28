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
		StartCoroutine(SwitchTo2D());
	}

	IEnumerator LoadDevice(string newDevice)
	{
//		if(camera != null)
//			camera.enabled = false;
//
//		Screen.fullScreen = false;
//
//		yield return new WaitForEndOfFrame();
//		UnityEngine.XR.XRSettings.LoadDeviceByName(newDevice);
//		yield return null;
//		UnityEngine.XR.XRSettings.enabled = true;
//
//		Screen.orientation = ScreenOrientation.Portrait;
//
//		// Only render at half resolution
//		// If target res is too low, ignore
//		int targetWidth = MainAllController.instance.maxWidth / 2;
//		int targetHeight = MainAllController.instance.maxHeight / 2;
//
//		if (targetHeight > 1000) {
//			Screen.orientation = ScreenOrientation.Portrait;
//			yield return new WaitForSeconds (.25f);
//			Screen.SetResolution (targetWidth,targetHeight,false);
//			yield return new WaitForSeconds (.25f);
//		}
//			
//		QualitySettings.vSyncCount = 0;
//		Application.targetFrameRate = 50;
//
//
//		Screen.orientation = ScreenOrientation.Portrait;
//		Screen.SetResolution (720,1280,false);
//
//		Debug.Log ("2D ++++Current Resolution  " + Screen.currentResolution.width + "  " + Screen.currentResolution.height);
//
//		if(camera != null)
//			camera.enabled = true;
//
		yield return new WaitForEndOfFrame();
//		UnityEngine.XR.XRSettings.LoadDeviceByName(newDevice);
	}

	IEnumerator SwitchTo2D() {

        if (MainAllController.instance != null) {
			MainAllController.instance.ShowScreenSwitchSceneMode ();
		}

		if (GvrViewer.Instance.VRModeEnabled == true){
			GvrViewer.Instance.VRModeEnabled = false;
		}
			
		yield return new WaitForSeconds(0.25f);


        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 50;


        // Disable auto rotation, except for landscape left.
        Screen.orientation = ScreenOrientation.Portrait;

        yield return new WaitForSeconds(0.7f);

		if (MainAllController.instance != null){
			MainAllController.instance.HideVR_CloseButton ();
		}

		if (MainAllController.instance != null) {
			MainAllController.instance.HideScreenSwitchSceneMode ();
		}

        if (VRCrosshair != null)
        {
            VRCrosshair.SetActive(false);
        }
        else
        {
            Debug.Log("VRCrosshair is null!");
        }

        yield return new WaitForSeconds(1f);
    }

	public override void Hide()
	{
		base.Hide ();
	}
}
