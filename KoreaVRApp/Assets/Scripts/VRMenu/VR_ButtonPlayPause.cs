using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_ButtonPlayPause : MonoBehaviour
{
	[SerializeField]
	private GameObject playBtn;
	[SerializeField]
	private GameObject pauseBtn;

	private bool _isActive;

	public bool isActive
	{
		get {
			return _isActive;
		}

		set {
			_isActive = value;

			if (_isActive) {
				playBtn.SetActive (true);
				pauseBtn.SetActive (false);
			} else {
				playBtn.SetActive (false);
				pauseBtn.SetActive (true);
			}
		}
	}

	// Use this for initialization
	void Start () {

	}

	public void OnClickPlayBtn(bool value)
	{
		isActive = value;
	}
}
