using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VR_RecenterPanel : MonoBehaviour
{
	[SerializeField] private GameObject root;
	private VR_NodDetect vr_NodDetect;

	void Start()
	{
		vr_NodDetect = UnityEngine.Object.FindObjectOfType<VR_NodDetect>();
	}

	// Update is called once per frame
	void Update()
	{
	}
		
	public delegate void OnRecenterCallback();
	OnRecenterCallback callback;

	public void Show(OnRecenterCallback callback = null)
	{
		if (vr_NodDetect != null){
			vr_NodDetect.enabled = false;
		}

		if (MainAllController.instance != null){
			MainAllController.instance.IsShowRecenterPanel = true;
		}
			
		Show ();

		if (callback != null) {
			this.callback = callback;
			StartCoroutine (RecenterDelay(.5f));
		}

	}

	IEnumerator RecenterDelay(float delay)
	{
		yield return new WaitForSeconds (delay);
		VR_Recenterer.instance.Recenter ();
	}

	public void OnClickRecenter()
	{
		Debug.Log ("OnClickRecenter>>>>>");

		VR_Recenterer.instance.Recenter ();

		if (callback != null) {
			callback ();
		}

		Hide ();

		if (vr_NodDetect != null){
			vr_NodDetect.enabled = true;
		}
	}

	void Show()
	{
		if (root != null){
			root.SetActive(true);
			this.enabled = true;
		}
	}

	public void Hide()
	{
		if (root != null){
			root.SetActive(false);
			this.enabled = false;
		}
		callback = null;
	}

	public void RecenterBtn()
	{
		VR_Recenterer.instance.Recenter ();
	}
		
}
