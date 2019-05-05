using UnityEngine;
using UnityEngine.UI;
using CurvedUI;
using System.Collections;


public class VR_NodDetect : MonoBehaviour
{
	public SceneVR sceneVR;

	public float Threshold = 7;

	Quaternion itemRotation;
	Quaternion previousRotation;
	Vector3 angularVelocity;

	bool recentering;
	float timeCount = 0;

	void FixedUpdate()
	{
		itemRotation = Camera.main.transform.rotation;

		Quaternion deltaRotation = itemRotation * Quaternion.Inverse(previousRotation);

		previousRotation = itemRotation;

		float angle  = 0.0f;
		Vector3 axis = Vector3.zero;

		deltaRotation.ToAngleAxis(out angle, out axis);

		angle *= Mathf.Deg2Rad;

		angularVelocity = axis * angle * (1.0f / Time.deltaTime);

		//print (angularVelocity.magnitude);

		if (angularVelocity.magnitude > Threshold) {
			print ("angularVelocity  " + angularVelocity.magnitude);
			recentering = true;
		}

	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.T)){
			if (sceneVR != null) {
				sceneVR.ShowSetting ();
			}
		}

		if (recentering) {
			timeCount += Time.deltaTime;

			if (timeCount > 0.5f) {
				VR_Recenterer.instance.Recenter ();

				if (sceneVR != null) {
					sceneVR.ShowSetting ();
				}

				recentering = false;
				timeCount = 0;
			}
		}
	}

}