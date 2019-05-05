using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class VR_ButtonSettings : VR_BasicButton
{
	public override void Init ()
	{
		base.Init ();
	}

	public override void OnClickBtn ()
	{
		if (VR_SettingsManager.instance != null && VR_SettingsManager.instance.currentBtn != null){
			base.OnClickBtn ();

			if (VR_SettingsManager.instance.currentBtn != this) {
				this.OnActive ();
				VR_SettingsManager.instance.currentBtn.OnInactive ();
				VR_SettingsManager.instance.currentBtn = this;
			}
		}

	}

	public override void OnActive ()
	{
		base.OnActive ();
	}

	public override void OnInactive ()
	{
		base.OnInactive ();
	}
}
