using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderBackground : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
	public bool hover;

	PointerEventData _pointerEvt;

	Slider slider;

	public VRSlider dragDrop;
    // Start is called before the first frame update
    void Start()
    {
		slider = this.GetComponent<Slider> ();

    }

    // Update is called once per frame
    void Update()
    {
//		if (hover) {
//
//			Debug.DrawLine (Camera.main.transform.position,Camera.main.transform.position + Camera.main.transform.forward * 100,Color.blue);
////			print (_pointerEvt.delta);
//
//			float xVal = _pointerEvt.delta.x;
//
//
//			slider.OnDrag (_pointerEvt);
//
//		}
    }


	public void OnPointerEnter(PointerEventData pointerEventData)
	{
		ExecuteEvents.Execute(gameObject, pointerEventData, ExecuteEvents.pointerClickHandler);
		//Output to console the GameObject's name and the following message
		print("click");
//		hover = true;
//		_pointerEvt = pointerEventData;
		//_pointerEvt.pointerDrag = this.gameObject;
		//ExecuteEvents.Execute(gameObject, _pointerEvt, ExecuteEvents.dragHandler);
		//slider.
//		slider.OnInitializePotentialDrag(_pointerEvt);
//		slider.Select ();
		//Debug.Log("Cursor Entering " + name + " GameObject");

	}

	//Detect when Cursor leaves the GameObject
	public void OnPointerExit(PointerEventData pointerEventData)
	{
		//Output the following message with the GameObject's name
//		Debug.Log("Cursor Exiting " + name + " GameObject");
//		hover = false;
//		_pointerEvt.pointerDrag = this.gameObject;
		//slider.OnDeselect (_pointerEvt);
	//	ExecuteEvents.Execute(gameObject, _pointerEvt, ExecuteEvents.dropHandler);


	}

	//Detect when Cursor leaves the GameObject
	public void OnPointerClick(PointerEventData pointerEventData)
	{
		//Output the following message with the GameObject's name
		Debug.Log("Cursor Clicking " + name + " GameObject");

	}

	public void Test()
	{
		print ("TEST");
	}
}
