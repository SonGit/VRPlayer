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

	private float _delayCount = 0;
	private float _delayTime = 2.5f;

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
		if (_canvas != null && !_canvas.enabled && alertLogin != null && alertLogout != null && alertExit != null) {
			_canvas.enabled = true;
			alertLogin.gameObject.SetActive (true);
			alertLogout.gameObject.SetActive (false);
			alertExit.gameObject.SetActive (false);
		}
	}

	public void LogoutAlert()
	{
		if (_canvas != null && !_canvas.enabled && alertLogin != null && alertLogout != null && alertExit != null) {
			_canvas.enabled = true;
			alertLogout.gameObject.SetActive (true);
			alertLogin.gameObject.SetActive (false);
			alertExit.gameObject.SetActive (false);
		}
	}

	public void ExitAlert()
	{
		if (_canvas != null && !_canvas.enabled && alertLogin != null && alertLogout != null && alertExit != null) {
			_canvas.enabled = true;
			alertExit.gameObject.SetActive (true);
			alertLogout.gameObject.SetActive (false);
			alertLogin.gameObject.SetActive (false);
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
}
