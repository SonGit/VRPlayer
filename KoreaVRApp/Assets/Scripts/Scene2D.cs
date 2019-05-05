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
		Screen.fullScreen = false;
		base.Show (lastMenu);
		StartCoroutine(LoadDevice("none"));
	}

	IEnumerator LoadDevice(string newDevice)
	{
		yield return new WaitForEndOfFrame();
		UnityEngine.XR.XRSettings.LoadDeviceByName(newDevice);
	}

	public override void Hide()
	{
		base.Hide ();
	}
}
