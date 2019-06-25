using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMediaPlayer : AppScene
{
    // Start is called before the first frame update
    void Start()
    {
		Screen.orientation = ScreenOrientation.AutoRotation;
    }

    public override void Show(BasicMenu lastMenu = null)
    {
        base.Show(lastMenu);
        StartCoroutine(LoadDevice("none"));
    }

    IEnumerator LoadDevice(string newDevice)
    {
		if (MainAllController.instance != null) {
			MainAllController.instance.ShowScreenSwitchSceneMode ();
		}

		GvrViewer.Instance.VRModeEnabled = false;

		yield return new WaitForSeconds(0.25f);

		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 50;

//		if (UnityEngine.XR.XRSettings.enabled) {
//			UnityEngine.XR.XRSettings.enabled = false;
//			yield return new WaitForEndOfFrame();
//			UnityEngine.XR.XRSettings.LoadDeviceByName(newDevice);
//			yield return null;
//		}

        Screen.orientation = ScreenOrientation.AutoRotation;

		yield return new WaitForSeconds(0.5f);

		if (MainAllController.instance != null){
			MainAllController.instance.HideVR_CloseButton ();
		}

		if (MainAllController.instance != null) {
			MainAllController.instance.HideScreenSwitchSceneMode ();
		}

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;

        if (VRCrosshair != null)
        {
            VRCrosshair.SetActive(false);
        }
        else
        {
            Debug.Log("VRCrosshair is null!");
        }

        yield return null;
    }

    public void PlayFromURL(Video video)
    {

    }
}
