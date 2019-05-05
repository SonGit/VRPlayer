using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class VR_ButtonMode : VR_BasicButton
{
	public override void Init ()
	{
		base.Init ();
	}

	public override void OnClickBtn ()
	{
		if (VR_ModeManager.instance != null && VR_ModeManager.instance.currentBtn != null){
			base.OnClickBtn ();

			if (VR_ModeManager.instance.currentBtn != this) {
				this.OnActive ();
				VR_ModeManager.instance.currentBtn.OnInactive ();
				VR_ModeManager.instance.currentBtn = this;
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
