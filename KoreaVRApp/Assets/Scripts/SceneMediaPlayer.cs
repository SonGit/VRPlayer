using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMediaPlayer : AppScene
{
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
        UnityEngine.XR.XRSettings.enabled = false;
        yield return new WaitForEndOfFrame();
        UnityEngine.XR.XRSettings.LoadDeviceByName(newDevice);
        yield return null;

        Screen.orientation = ScreenOrientation.LandscapeLeft;

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
    }

    public void PlayFromURL(Video video)
    {

    }
}
