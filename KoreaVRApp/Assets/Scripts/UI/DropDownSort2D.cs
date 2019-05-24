using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropDownSort2D : Dropdown, IPointerEnterHandler
{

	// Start is called before the first frame update
	void Start()
	{
		
	}

	public override void OnPointerClick (PointerEventData eventData)
	{
		base.OnPointerClick (eventData);

		if(MainAllController.instance != null){
			MainAllController.instance.PlayButtonSound ();
		}
	}

	protected override void DestroyBlocker (GameObject blocker)
	{
		base.DestroyBlocker (blocker);

		if(MainAllController.instance != null){
			MainAllController.instance.PlayButtonSound ();
		}
	}
}
