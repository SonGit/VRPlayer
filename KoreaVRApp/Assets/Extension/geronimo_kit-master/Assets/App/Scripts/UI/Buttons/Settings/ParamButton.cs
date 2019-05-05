using System;
using UnityEngine;
using UnityEngine.UI;

public class ParamButton : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Button BtnParam;
    [SerializeField] private Text TxtParam;

    public ESettingsType Type;

    private string _paramName;
    private int _paramId;

    public event Action<string, int, ESettingsType> OnClick;

    private void Start()
    {
        BtnParam.onClick.AddListener(() =>
        {
            if (OnClick != null)
            {
                OnClick(_paramName, _paramId, Type);
            }
        });
    }

    public void Init(ParamModel model, ESettingsType type)
    {
        _paramName = model.param_name;
        _paramId = model.param_id;
        Type = type;

        TxtParam.text = _paramName;
    }
}