using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
	private bool isShow;
	private RectTransform rectTransform;
	public CanvasGroup darken;


	public void MovePos(Vector3 position){
		rectTransform.localPosition = position;
	}

	// Start is called before the first frame update
	void Start()
	{
		rectTransform = this.GetComponent<RectTransform> ();
	}

	void Update()
	{
		if (darken.alpha == 0) {
			return;
		} else {
			rectTransform.localPosition = new Vector3 (rectTransform.localPosition.x, rectTransform.localPosition.y, 150*(darken.alpha/1));
		}
	}
}
