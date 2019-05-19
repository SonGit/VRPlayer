using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VRPlayerMenu : BasicMenuNavigation
{
	[Header("Components")]
	[SerializeField] private Button BtnRunVRPlayer;

	public event Action OnRunVRPlayer;

	protected override void Start ()
	{
		base.Start ();

        if (BtnRunVRPlayer != null)
        {
            BtnRunVRPlayer.onClick.AddListener(() =>
                {
                    if (MainAllController.instance != null)
                    {
                        MainAllController.instance.PlayButtonSound();
                    }

                    if (OnRunVRPlayer != null)
                    {
                        OnRunVRPlayer();
                    }
                });
        }

    }

}
