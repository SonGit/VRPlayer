using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSensor : AppScene
{
	public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
		
    }

    public override void Show(BasicMenu lastMenu = null)
    {
        base.Show(lastMenu);
        StartCoroutine(LoadDevice("none"));
    }

    IEnumerator LoadDevice(string newDevice)
    {
		if(camera != null)
			camera.enabled = false;

		if (UnityEngine.XR.XRSettings.enabled) {
			UnityEngine.XR.XRSettings.enabled = false;
			yield return new WaitForEndOfFrame();
			UnityEngine.XR.XRSettings.LoadDeviceByName(newDevice);
			yield return null;
		}

		Screen.orientation = ScreenOrientation.LandscapeLeft;

		if(camera != null)
			camera.enabled = true;

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;

		yield return null;
    }
}
