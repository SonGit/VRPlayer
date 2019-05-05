using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_GazeManager : MonoBehaviour
{
    // Start is called before the first frame update
	private CanvasGroup _gaze;


	private VRPlayer _vrplayer;
	[SerializeField]
	private GameObject RootSetting;
    void Start()
    {
		_gaze = GetComponent<CanvasGroup> ();
		_vrplayer = FindObjectOfType<VRPlayer> ();
    }

    // Update is called once per frame
    void Update ()
	{
		if (_vrplayer != null && !(_vrplayer.currentMode is CinemaMode)) {
			if (!RootSetting.activeInHierarchy) {
				_gaze.alpha = 0;
			} else {
				_gaze.alpha = 1;
			}
		} else {
			_gaze.alpha = 1;
		}
	}
}
