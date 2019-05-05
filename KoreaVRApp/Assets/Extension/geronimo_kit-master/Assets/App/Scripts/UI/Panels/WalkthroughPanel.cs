using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalkthroughPanel : BasicPanel
{
    [Header("Components")]
    [SerializeField] private Button BtnGetStarted;

    public event Action OnClick;

    protected override void Start()
    {
        base.Start();

        BtnGetStarted.onClick.AddListener(() =>
        {
            if (OnClick != null)
            {
                OnClick();
            }
        });
    }
}