using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class SensorMenu : BasicMenuNavigation
{
	[Header("Component")]
	[SerializeField] private Image countdownImg;
	[SerializeField] private Image pointImg;
	[SerializeField] private Text countdownNumber;
	[SerializeField] private Button sensorBnt;
	private float countdownTime;

	protected override void Awake ()
	{
		base.Awake ();
	}

	protected override void Start ()
	{
		base.Start ();

		Init ();
	}

	void Update()
	{
		PlayCountdown ();
	}

	public void Init(){
		ReSetCountdownInfo ();

		SetActive (pointImg.gameObject,true);
		SetActive (countdownImg.gameObject,false);
		SetActive (sensorBnt.gameObject,true);
	}

	public void ClickSensorBnt(){
		if(MainAllController.instance != null){
			MainAllController.instance.PlayButtonSound ();
		}

		SetActive (pointImg.gameObject,false);
		SetActive (countdownImg.gameObject,true);
		SetActive (sensorBnt.gameObject,false);
	}

	public void SetActive(GameObject obj, bool value)
	{
		if (obj != null) {
			obj.SetActive(value);
		} else {
			Debug.Log ("obj is null!");
		}
	}

	private void PlayCountdown (){
		if (countdownImg != null && countdownImg.gameObject.activeSelf) {
			CheckAccelerometerInput ();
		}
	}

	bool isCountdown;
	bool isWaitTime;

	private void CheckAccelerometerInput(){
		Vector3 dir = Vector3.zero;
		dir.x = -Input.acceleration.y;
		dir.z = Input.acceleration.x;

		if (dir.sqrMagnitude < 0.001f) {
			if (isWaitTime){
				isWaitTime = false;
				StartCoroutine (WaitTime());
			}

			if (isCountdown){

				countdownTime -= Time.deltaTime;

				if (countdownTime <= 0) {
					if (MainAllController.instance != null) {
						MainAllController.instance.SensorMenuMenu_OnSkip ();
					}
				}
			}
		}else {
			ReSetCountdownInfo ();
		}

		countdownNumber.text = "" + (int)countdownTime;
		countdownImg.fillAmount = (int)countdownTime * 0.2f;

	}

	private IEnumerator WaitTime (){
		yield return new WaitForSeconds(0.5f);
		isCountdown = true;
	}

	private void ReSetCountdownInfo(){
		countdownTime = 5;
		isWaitTime = true;
		isCountdown = false;
	}

//		if (-Input.acceleration.z > 0.99f ) {
//			countdownTime -= Time.deltaTime;
//			if (countdownTime <= 0) {
//				if (MainAllController.instance != null) {
//					MainAllController.instance.SensorMenuMenu_OnSkip ();
//				}
//			}
//		} else {
//			countdownTime = 5;
//		}
}
