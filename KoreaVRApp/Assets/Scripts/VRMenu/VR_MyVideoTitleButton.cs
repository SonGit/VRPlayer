using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class VR_MyVideoTitleButton : VR_BasicButton
{

	public override void Init ()
	{
		base.Init ();
	}

	public override void OnClickBtn ()
	{
		base.OnClickBtn ();

		if (VR_MyvideoTitleManager.instance != null && VR_MyvideoTitleManager.instance.currentBtn != null){
			if (VR_MyvideoTitleManager.instance.currentBtn != this) {
				VR_MyvideoTitleManager.instance.currentBtn.OnInactive ();
				VR_MyvideoTitleManager.instance.currentBtn = this;
				this.OnActive ();
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
