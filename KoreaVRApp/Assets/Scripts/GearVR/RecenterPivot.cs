using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecenterPivot : MonoBehaviour
{
//	private Camera camera;
//
//    // Start is called before the first frame update
//    void Start()
//    {
//		camera = transform.parent.GetComponentInChildren<Camera> ();
//    }
//
//    // Update is called once per frame
//    void Update()
//    {
//		if (camera != null) {
//			var projectedLookDirection = Vector3.ProjectOnPlane (camera.transform.forward, Vector3.up);
//			transform.rotation =  Quaternion.LookRotation (projectedLookDirection);
//		}
//    }
//
//	private void Recenter() {
//		#if !UNITY_EDITOR
//		GvrCardboardHelpers.Recenter();
//		#else
//		if (GvrEditorEmulator.Instance != null) {
//			GvrEditorEmulator.Instance.Recenter();
//		}
//		#endif  // !UNITY_EDITOR
//	}
//
//	public void RecenterEvent(BaseEventData eventData) {
//		// Only trigger on left input button, which maps to
//		// Daydream controller TouchPadButton and Trigger buttons.
//		PointerEventData ped = eventData as PointerEventData;
//		if (ped != null) {
//			if (ped.button != PointerEventData.InputButton.Left) {
//				return;
//			}
//		}
//
//		Recenter ();
//	}
}
