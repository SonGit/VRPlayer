﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR;

public class SwitchToDeviceManager : MonoBehaviour
{
	public static SwitchToDeviceManager instance;

	void Awake()
	{
		instance = this;
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public IEnumerator SwitchToVR() {
		// Device names are lowercase, as returned by `XRSettings.supportedDevices`.
		string desiredDevice = "Cardboard";

		// Some VR Devices do not support reloading when already active, see
		// https://docs.unity3d.com/ScriptReference/XR.XRSettings.LoadDeviceByName.html
		if (String.Compare(XRSettings.loadedDeviceName, desiredDevice, true) != 0) {
			XRSettings.LoadDeviceByName(desiredDevice);

			// Must wait one frame after calling `XRSettings.LoadDeviceByName()`.
			yield return null;
		}

		// Now it's ok to enable VR mode.
		XRSettings.enabled = true;
	}


	IEnumerator SwitchTo2D() {
		// Empty string loads the "None" device.
		XRSettings.LoadDeviceByName("");

		// Must wait one frame after calling `XRSettings.LoadDeviceByName()`.
		yield return null;

		// Not needed, since loading the None (`""`) device takes care of this.
		// XRSettings.enabled = false;

		// Restore 2D camera settings.
		ResetCameras();
	}

	// Resets camera transform and settings on all enabled eye cameras.
	void ResetCameras() {
		// Camera looping logic copied from GvrEditorEmulator.cs
		for (int i = 0; i < Camera.allCameras.Length; i++) {
			Camera cam = Camera.allCameras[i];
			if (cam.enabled && cam.stereoTargetEye != StereoTargetEyeMask.None) {

				// Reset local position.
				// Only required if you change the camera's local position while in 2D mode.
				cam.transform.localPosition = Vector3.zero;

				// Reset local rotation.
				// Only required if you change the camera's local rotation while in 2D mode.
				cam.transform.localRotation = Quaternion.identity;

				// No longer needed, see issue github.com/googlevr/gvr-unity-sdk/issues/628.
				// cam.ResetAspect();

				// No need to reset `fieldOfView`, since it's reset automatically.
			}
		}
	}
}
