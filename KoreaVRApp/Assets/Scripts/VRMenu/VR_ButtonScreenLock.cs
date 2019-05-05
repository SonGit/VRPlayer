using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_ButtonScreenLock : MonoBehaviour
{
	[SerializeField]
	private GameObject lockBtn;
	[SerializeField]
	private GameObject unlockBtn;

	private float lockcam;
	private bool _isActive;

	public bool isActive
	{
		get {
			return _isActive;
		}

		set {
			_isActive = value;

			if (_isActive) {
				lockBtn.SetActive (true);
				unlockBtn.SetActive (false);

				if (vrPlayerRef) {
					vrPlayerRef.ScreenUnlock ();
				}

			} else {
				lockBtn.SetActive (false);
				unlockBtn.SetActive (true);

				if (vrPlayerRef) {
					vrPlayerRef.ScreenLock ();
				}
			}
		}
	}

	[SerializeField]
	private VRPlayer vrPlayerRef;
	// Use this for initialization
	void Awake () {

	}

	public void OnClickLockBtn(bool value)
	{
		isActive = value;

	}
}
