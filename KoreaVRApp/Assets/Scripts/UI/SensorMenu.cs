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

	float accelerometerUpdateInterval = 1.0f / 60.0f;
	// The greater the value of LowPassKernelWidthInSeconds, the slower the
	// filtered value will converge towards current input sample (and vice versa).
	float lowPassKernelWidthInSeconds = 1.0f;
	// This next parameter is initialized to 2.0 per Apple's recommendation,
	// or at least according to Brady! ;)
	float shakeDetectionThreshold = 0f;

	float lowPassFilterFactor;
	Vector3 lowPassValue;

	protected override void Awake ()
	{
		base.Awake ();
	}

	protected override void Start ()
	{
		base.Start ();

		if (sensorBnt != null){
			sensorBnt.onClick.AddListener(() =>
				{
					if(MainAllController.instance != null){
						MainAllController.instance.PlayButtonSound ();
					}
				});
		}

		lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
		lowPassValue = Input.acceleration;

		SensorMenuInit ();
	}

	void Update()
	{
		PlayCountdown ();
	}


	public void SensorMenuInit(){
		ReSetCountdownInfo ();

		SetActive (pointImg.gameObject,true);
		SetActive (countdownImg.gameObject,false);
		SetActive (sensorBnt.gameObject,true);
	}

	public void ClickSensorBnt(){
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

	private void CheckAccelerometerInput(){
//		Vector3 dir = Vector3.zero;
//		dir.x = -Input.acceleration.y;
//		dir.z = Input.acceleration.x;

		Vector3 acceleration = Input.acceleration;
		lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
		Vector3 deltaAcceleration = acceleration - lowPassValue;
		float magnitude = Mathf.Round (deltaAcceleration.magnitude * 10f) / 10f;

		Debug.Log("deltaAcceleration" + magnitude);

		if (magnitude == shakeDetectionThreshold) {

			countdownTime -= Time.deltaTime;

			if (countdownTime <= 0) {
				if (MainAllController.instance != null) {
					MainAllController.instance.SensorMenuMenu_OnSkip ();
				}
			}
		}else {
			ReSetCountdownInfo ();
		}

		countdownNumber.text = "" + (int)countdownTime;
		countdownImg.fillAmount = (int)countdownTime * 0.2f;

	}

	private void ReSetCountdownInfo(){
		countdownTime = 5;
	}


}
