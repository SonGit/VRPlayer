using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeadRecognition : MonoBehaviour
{
	private Camera camera;

	private Vector3 currentPosition;
	private Vector3 newPosition;
	private Vector3 diffirentPosition;
	private float time;
	public float timeCheck;
	public GameObject Setting;
	public float distance;


	void Start (){
		camera = GetComponent<Camera> ();
		ResetGesture ();

	}

	void Update (){
		
		if (camera != null) {
			currentPosition = camera.transform.eulerAngles;
		}

		if (!Setting.activeInHierarchy) {
			CheckMovement ();
		}

			ResetGesture ();
		
	}

	private void CheckMovement(){
		
		time += Time.deltaTime;
		if (time > timeCheck) {
			time = 0;
			distance = Vector3.Distance (newPosition,currentPosition);
			if (distance > 5f && distance < 20f ) {
				Setting.SetActive (true);
				Recenter ();
			}
		}
	}

	private void ResetGesture(){
		if (camera != null){
			newPosition = camera.transform.eulerAngles;
		}
	}

	private void Recenter() {
		#if !UNITY_EDITOR
		GvrCardboardHelpers.Recenter();
		#else
		if (GvrEditorEmulator.Instance != null) {
			GvrEditorEmulator.Instance.Recenter();
		}
		#endif  // !UNITY_EDITOR
	}
}
