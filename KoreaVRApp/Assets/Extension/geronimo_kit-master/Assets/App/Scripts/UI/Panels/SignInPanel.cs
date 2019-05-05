using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignInPanel : BasicPanel
{
    [Header("Components")]
    [SerializeField] private Button BtnLogin;
    [SerializeField] private Button BtnCreate;
    [SerializeField] private Button BtnForgot;

    public event Action OnLogin;
    public event Action OnCreate;
    public event Action OnForgot;

    protected override void Start()
    {
        base.Start();

        BtnLogin.onClick.AddListener(() =>
        {
            if (OnLogin != null)
            {
                OnLogin();
            }
        });

        BtnCreate.onClick.AddListener(() =>
        {
            if (OnCreate != null)
            {
                OnCreate();
            }
        });

        BtnForgot.onClick.AddListener(() =>
        {
            if (OnForgot != null)
            {
                OnForgot();
            }
        });
    }
}