using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertMenu : MonoBehaviour
{
	[SerializeField] private GameObject root;

	[Header("Components")]
	[SerializeField] private GameObject alertLogin;
	[SerializeField] private GameObject alertLogout;
	[SerializeField] private GameObject alertExit;

	private float _delayCount = 0;
	private float _delayTime = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
		CheckDisableAlert ();
    }

	public void LoginAlert()
	{
		if (root != null && !root.activeSelf && alertLogin != null && alertLogout != null && alertExit != null) {
			root.SetActive (true);
			alertLogin.gameObject.SetActive (true);
			alertLogout.gameObject.SetActive (false);
			alertExit.gameObject.SetActive (false);
		}
	}

	public void LogoutAlert()
	{
		if (root != null && !root.activeSelf && alertLogin != null && alertLogout != null && alertExit != null) {
			root.SetActive(true);
			alertLogout.gameObject.SetActive (true);
			alertLogin.gameObject.SetActive (false);
			alertExit.gameObject.SetActive (false);
		}
	}

	public void ExitAlert()
	{
		if (root != null && !root.activeSelf && alertLogin != null && alertLogout != null && alertExit != null) {
			root.SetActive(true);
			alertExit.gameObject.SetActive (true);
			alertLogout.gameObject.SetActive (false);
			alertLogin.gameObject.SetActive (false);
		}
	}

	private void CheckDisableAlert(){

		if (root.activeSelf && root != null) {
			_delayCount += Time.deltaTime;
			if (_delayCount >= _delayTime) {
				_delayCount = 0;
				root.SetActive (false);
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
		}
	}
}
