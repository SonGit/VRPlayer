using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropDownSubtitle2D : Dropdown, IPointerEnterHandler
{
	private MediaPlayerMenu mediaPlayerMenu;

	// Start is called before the first frame update
	void Start()
	{
		mediaPlayerMenu = this.GetComponentInParent<MediaPlayerMenu> ();
	}

	public override void OnPointerClick (PointerEventData eventData)
	{
		base.OnPointerClick (eventData);

		if (mediaPlayerMenu != null){
			mediaPlayerMenu.isShowDropDown = true;
			mediaPlayerMenu._delayCount = 0;
		}

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

	public override void OnSelect (BaseEventData eventData)
	{
		base.OnSelect (eventData);

		if (mediaPlayerMenu != null){
			mediaPlayerMenu.isShowDropDown = false;
			mediaPlayerMenu._delayCount = 0;
		}
	}

	void Update(){
		if (mediaPlayerMenu.isSetDropdownValue){
			value = 0;
			mediaPlayerMenu.isSetDropdownValue = false;
		}
	}
}
