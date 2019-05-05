using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecoveryPanel : BasicPanel
{
    [Header("Components")]
    [SerializeField] private Button BtnSend;
    [SerializeField] private Button BtnAlreadyAccount;

    public event Action OnSend;
    public event Action OnAlreadyAccount;

    protected override void Start()
    {
        base.Start();

        BtnSend.onClick.AddListener(() =>
        {
            if (OnSend != null)
            {
                OnSend();
            }
        });

        BtnAlreadyAccount.onClick.AddListener(() =>
        {
            if (OnAlreadyAccount != null)
            {
                OnAlreadyAccount();
            }
        });
    }
}