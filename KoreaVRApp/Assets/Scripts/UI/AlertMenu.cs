using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertMenu : MonoBehaviour
{
	private Canvas _canvas;

	[Header("Components")]
	[SerializeField] private GameObject alertLogin;
	[SerializeField] private GameObject alertLogout;
	[SerializeField] private GameObject alertExit;
	[SerializeField] private GameObject alertPurchase;

	private float _delayCount = 0;
	private float _delayTime = 2.2f;

    // Start is called before the first frame update
    void Start()
    {
		if (_canvas == null) {
			_canvas = transform.GetComponentInParent<Canvas> ();
		}
    }

    // Update is called once per frame
    void Update()
    {
		CheckDisableAlert ();
    }

	public void LoginAlert()
	{
		if (_canvas != null && !_canvas.enabled) {
			_canvas.enabled = true;
			SetActive (true,alertLogin);
			SetActive (false,alertLogout);
			SetActive (false,alertExit);
			SetActive (false,alertPurchase);
		}
	}

	public void LogoutAlert()
	{
		if (_canvas != null && !_canvas.enabled) {
			_canvas.enabled = true;
			SetActive (true,alertLogout);
			SetActive (false,alertLogin);
			SetActive (false,alertExit);
			SetActive (false,alertPurchase);
		}
	}

	public void ExitAlert()
	{
		if (_canvas != null && !_canvas.enabled) {
			_canvas.enabled = true;
			SetActive (true,alertExit);
			SetActive (false,alertLogout);
			SetActive (false,alertLogin);
			SetActive (false,alertPurchase);
		}
	}

	public void PurchaseAlert()
	{
		if (_canvas != null && !_canvas.enabled) {
			_canvas.enabled = true;

			SetActive (true,alertPurchase);
			SetActive (false,alertExit);
			SetActive (false,alertLogout);
			SetActive (false,alertLogin);
		}
	}

	private void CheckDisableAlert(){

		if (_canvas.enabled && _canvas != null) {
			_delayCount += Time.deltaTime;
			if (_delayCount >= _delayTime) {
				_delayCount = 0;
				_canvas.enabled = false;
			} else {
				if (_delayCount > 0.25f && alertExit.gameObject.activeSelf && alertExit != null){
					ApplicationExit ();
				}
			}
		}
	}

	private void ApplicationExit (){
		if (Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit ();
			Debug.Log("Application Quit.........................................................");
		}
	}

	private void SetActive(bool value, GameObject obj){
		if (obj != null){
			obj.SetActive (value);
		}
	}
}
