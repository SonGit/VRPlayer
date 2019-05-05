using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignUpPanel : BasicPanel
{
    [Header("Components")]
    [SerializeField] private Button BtnSignUp;
    [SerializeField] private Button BtnAlreadyAccount;

    public event Action OnAlreadyAccount;
    public event Action OnSignUp;

    protected override void Start()
    {
        base.Start();

        BtnAlreadyAccount.onClick.AddListener(() =>
        {
            if (OnAlreadyAccount != null)
            {
                OnAlreadyAccount();
            }
        });

        BtnSignUp.onClick.AddListener(() =>
        {
            if (OnSignUp != null)
            {
                OnSignUp();
            }
        });
    }
}