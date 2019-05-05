using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_ButtonSlider : MonoBehaviour
{
	[SerializeField]
	VRSlider _slider;

	public void OnMinusBtn()
	{
		_slider.value -= (_slider.maxValue - _slider.minValue)/10f;
	}

	public void OnPlusBtn()
	{
		_slider.value += (_slider.maxValue - _slider.minValue) /10f;
	}
}
