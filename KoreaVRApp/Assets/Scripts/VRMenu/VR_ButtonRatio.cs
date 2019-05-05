using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class VR_ButtonRatio : VR_BasicButton
{

	public override void Init ()
	{
		base.Init ();
	}

	public override void OnClickBtn ()
	{
		if (VR_RatioManager.instance != null && VR_RatioManager.instance.currentBtn != null){
			base.OnClickBtn ();

			if (VR_RatioManager.instance.currentBtn != this) {
				this.OnActive ();
				VR_RatioManager.instance.currentBtn.OnInactive ();
				VR_RatioManager.instance.currentBtn = this;
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
