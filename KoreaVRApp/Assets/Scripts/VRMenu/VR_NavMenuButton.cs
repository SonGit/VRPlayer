using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class VR_NavMenuButton : VR_BasicButton
{
	[SerializeField]
	protected VR_TopMenuTitleManager vr_MenuTitleManager;

    public VR_FlashButton _button;

    public Image _selectedImage;

    public Image _unSelectedImage;

    protected virtual void Start(){
		vr_MenuTitleManager = UnityEngine.Object.FindObjectOfType<VR_TopMenuTitleManager>();
        _button = this.GetComponent<VR_FlashButton>();
    }
	// Update is called once per frame
	void Update()
	{

	}

	public override void Init ()
	{
		base.Init ();
	}

	public override void OnClickBtn ()
	{
		if (VR_MainMenu.instance != null){
			bool isConnect = VR_MainMenu.instance.CheckNetworkConnection ();
			if (isConnect) {
				if (MainAllController.instance.HasUserLoggedIn ()) {
					if (VR_NavMenuManager.instance != null && VR_NavMenuManager.instance.currentBtn != null) {
						base.OnClickBtn ();

						if (VR_NavMenuManager.instance.currentBtn != this) {
							VR_NavMenuManager.instance.currentBtn.OnInactive ();
							VR_NavMenuManager.instance.currentBtn = this;
							this.OnActive ();
						}
					}
				}
			}
		}
	}

	public override void OnActive ()
	{
		base.OnActive ();

		if(vr_MenuTitleManager == null)
		{
			vr_MenuTitleManager = UnityEngine.Object.FindObjectOfType<VR_TopMenuTitleManager>();
		}

		if (vr_MenuTitleManager != null) {
			vr_MenuTitleManager.TitleViewable ();
		}

    }

	public override void OnInactive ()
	{
		base.OnInactive ();

    }
}