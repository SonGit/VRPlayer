using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMenu : BasicMenu
{
    [Header("Components")]
    [SerializeField] private Button BtnGetStarted;

    public event Action OnClick;

    protected override void Start()
    {
        base.Start();

        BtnGetStarted.onClick.AddListener(() =>
        {
			if(MainAllController.instance != null){
				MainAllController.instance.PlayButtonSound ();
			}

            if (OnClick != null)
            {
                OnClick();
            }
        });
    }
}