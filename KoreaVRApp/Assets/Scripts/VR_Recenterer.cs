using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_Recenterer : MonoBehaviour
{
	public Transform VRCamHandle;  // drag the VR cam handle here in the inspector

	public Transform VRSphere;

	public static VR_Recenterer instance;

    // Start is called before the first frame update
    void Awake()
    {
		instance = this;
    }
		
	public void Recenter()
	{
		VRSphere.transform.position = VRCamHandle.transform.position;
		VRSphere.transform.eulerAngles = VRCamHandle.transform.eulerAngles;
	}

	public void ResetRotation()
	{
		VRSphere.transform.localPosition = Vector3.zero;
		VRCamHandle.transform.localPosition = Vector3.zero;

		VRSphere.transform.localEulerAngles = Vector3.zero;
		VRCamHandle.transform.localEulerAngles = Vector3.zero;

		Recenter ();
	}
}
