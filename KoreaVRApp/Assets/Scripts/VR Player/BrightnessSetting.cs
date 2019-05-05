using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessSetting : MonoBehaviour
{
	[SerializeField]
	private RawImage filter;

	Color currentColor;

	public void OnSliderValueChanged(float value)
	{
		if (filter != null) {
			currentColor = filter.color;
			currentColor.a = Mathf.Abs(1 - value);
			filter.color = currentColor;
		}
	}
}
