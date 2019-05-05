using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VR_ScreenCursorManager : MonoBehaviour
{
	[SerializeField] private GameObject root;
	private EventSystem eventSystem;

	void Start()
	{
		eventSystem = Object.FindObjectOfType<EventSystem> ();
	}

	// Update is called once per frame
	void Update()
	{
		CheckOOB ();
	}


	public void CheckOOB()
	{
		if (eventSystem == null) {
			Debug.Log ("Event System not found!");
			return;
		}

		if (!eventSystem.IsPointerOverGameObject ()) {
			VR_Recenterer.instance.Recenter ();
			Debug.Log ("Recenter>>>>>");
		}
	}



	public void SetActive(bool value)
	{
		VR_Recenterer.instance.Recenter ();

		if (root != null){
			root.SetActive(value);
			this.enabled = value;
		}
	}

	void OnEnable()
	{
		if (VR_Recenterer.instance != null){
			VR_Recenterer.instance.Recenter ();
		}
	}
		
}
