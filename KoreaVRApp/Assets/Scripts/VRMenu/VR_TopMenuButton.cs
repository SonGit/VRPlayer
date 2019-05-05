using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class VR_TopMenuButton : VR_BasicButton
{

    public VR_FlashButton _button;

    public Image _selectedImage;

    public Image _unSelectedImage;

    public override void Init ()
	{
		base.Init ();
        _button = this.GetComponent<VR_FlashButton>();
    }


	public override void OnClickBtn ()
	{
		if (VR_TopMenuManager.instance != null && VR_TopMenuManager.instance.currentBtn != null){
			base.OnClickBtn ();

			if (VR_ModeManager.instance.currentBtn != this) {
				VR_TopMenuManager.instance.currentBtn.OnInactive ();
				this.OnActive ();
				VR_TopMenuManager.instance.currentBtn = this;
			}
		}
	}

	public override void OnActive ()
	{
		base.OnActive ();

        _button.targetGraphic = _selectedImage;

        _selectedImage.enabled = true;
        _unSelectedImage.enabled = false;
    }

	public override void OnInactive ()
	{
		base.OnInactive ();

        _button.targetGraphic = _unSelectedImage;

        _selectedImage.enabled = false;
        _unSelectedImage.enabled = true;
    }
}