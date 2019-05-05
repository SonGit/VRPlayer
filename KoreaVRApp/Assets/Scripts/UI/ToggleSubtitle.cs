using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToggleSubtitle : Toggle, IPointerEnterHandler
{
	public PointerEventData eventData;

	public override void OnPointerClick (PointerEventData eventData)
	{
		base.OnPointerClick (eventData);
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
		this.eventData = eventData;
	}
}
