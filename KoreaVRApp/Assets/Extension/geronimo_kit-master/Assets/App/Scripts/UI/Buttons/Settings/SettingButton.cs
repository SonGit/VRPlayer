using System;
using UnityEngine;
using UnityEngine.UI;

public enum ESettingsType
{
    FontSize = 0,
    FontCustom = 1,
    Language = 2,
    Theme = 3,
}

public class SettingButton : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]private Text TxtSetting;
    public ESettingsType Type;

    private Button _btnSetting;

    public event Action<ESettingsType> OnClick;
         
    private void Start()
    {
        _btnSetting = GetComponent<Button>();

        if (_btnSetting != null)
        {
            _btnSetting.onClick.AddListener(() =>
            {
                if (OnClick != null)
                {
                    OnClick(Type);
                }
            });
        }
    }

    public void SetText(string paramName)
    {
        if (TxtSetting != null)
        {
            TxtSetting.text = paramName;
        }
    }
}