using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class UI_Testing : MonoBehaviour
{

	//Process touch for panel display on if the touch is less than this threshold.
	private float leftEdge = Screen.width * 0.25f;

	//Minimum swipe distance for showing/hiding the panel.
	float swipeDistance = 10f;


	float startXPos;
	bool processTouch = false;
	bool isExpanded = false;
	public Animation panelAnimation;
	public TextMeshProUGUI a;


	void Update(){
		if (Input.touchCount > 0) {
			
			Panel (Input.GetTouch (0));
		}
		else
		a.text = "NOPEEEE";
	}




	void Panel (Touch touch)
	{
		switch (touch.phase) {
		case TouchPhase.Began:
			//Get the start position of touch.

			startXPos = touch.position.x;
			a.text = startXPos.ToString ();
			Debug.Log(startXPos);
			//Check if we need to process this touch for showing panel.
			if (startXPos < leftEdge) {
				processTouch = true;
			}
			break;
		case TouchPhase.Ended:
			if (processTouch) {
				//Determine how far the finger was swiped.
				float deltaX = touch.position.x - startXPos;
				a.text += "      " + deltaX.ToString();

				if(isExpanded && deltaX < (-swipeDistance))
				{

					panelAnimation.CrossFade("SlideOut");
					isExpanded = false;
				} 
				else if(!isExpanded && deltaX > swipeDistance) 
				{
					panelAnimation.CrossFade("SlideIn");
					isExpanded = true;
				}

				startXPos = 0f;
				processTouch = false;
			}
			break;
		default:
			return;
		}
	}
}