using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class VRSlider : Slider,IPointerEnterHandler
{
    private float timeCount = 0;

	public bool hover = false;

	PointerEventData _pointerEvt;

	public override void OnPointerExit(PointerEventData pointerEventData)
	{
		base.OnPointerExit (pointerEventData);
		hover = false;
        Reset();
    }
		
	public override void OnPointerEnter(PointerEventData pointerEventData)
	{
		base.OnPointerEnter (pointerEventData);

		_pointerEvt = pointerEventData;
		_pointerEvt.pointerPressRaycast = _pointerEvt.pointerCurrentRaycast;

        hover = true;
        Reset();
    }

	void Update()
	{
		if (hover) {

            if(timeCount < 1.15f)
            {
                timeCount += Time.deltaTime;
            }else
            {      
                ExecuteEvents.Execute(gameObject, _pointerEvt, ExecuteEvents.dragHandler);
            }

		}
	}

    void Reset()
    {
        timeCount = 0;
    }
		
}
