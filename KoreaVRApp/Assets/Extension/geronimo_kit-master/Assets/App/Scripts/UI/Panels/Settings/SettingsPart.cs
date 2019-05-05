using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPart : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SettingButton[] SettingsButton;

    public event Action<ESettingsType> OnClick;

    private void Start()
    {
        foreach (var button in SettingsButton)
        {
            button.OnClick += Button_OnClick;
        }
    }

    public void Init(string paramName, ESettingsType type)
    {
        foreach (var button in SettingsButton)
        {
            if (button.Type == type)
            {
                button.SetText(paramName);
            }
        }
    }

    private void Button_OnClick(ESettingsType type)
    {
        if (OnClick != null)
        {
            OnClick(type);
        }
    }
}