using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : BasicPanelNavigation
{
    [Header("Components")]
    [SerializeField] private Toggle TglApp;
    [SerializeField] private Toggle TglCamera;
    [SerializeField] private Toggle TglNetwork;
    [SerializeField] private Toggle TglOther;
    [SerializeField] private GameObject App;
    [SerializeField] private GameObject Camera;
    [SerializeField] private GameObject Network;
    [SerializeField] private GameObject Other;
    [SerializeField] private GameObject Container;

    [Header("Parts")]
    [SerializeField] private SettingsPart PartApp;

    [Header("Prefabs")]
    [SerializeField] private GameObject ParamButton;

    private List<ParamButton> _lstParamButton;

    protected override void Start()
    {
        base.Start();

        _lstParamButton = new List<ParamButton>();

        TglApp.onValueChanged.AddListener((value) =>
        {
            if (!value) return;

            DisableParams();

            App.SetActive(true);
            Camera.SetActive(false);
            Network.SetActive(false);
            Other.SetActive(false);
        });

        TglCamera.onValueChanged.AddListener((value) =>
        {
            if (!value) return;

            DisableParams();

            Camera.SetActive(true);
            App.SetActive(false);
            Network.SetActive(false);
            Other.SetActive(false);
        });

        TglNetwork.onValueChanged.AddListener((value) =>
        {
            if (!value) return;

            DisableParams();

            Network.SetActive(true);
            Camera.SetActive(false);
            App.SetActive(false);
            Other.SetActive(false);
        });

        TglOther.onValueChanged.AddListener((value) =>
        {
            if (!value) return;

            DisableParams();

            Other.SetActive(true);
            Camera.SetActive(false);
            Network.SetActive(false);
            App.SetActive(false);
        });

        Init();
    }

    private void Init()
    {
        PartApp.OnClick += PartApp_OnClick;
    }

    public void DisableParams()
    {
        Container.SetActive(false);
        DestroyElmnt();
    }

    private void PartApp_OnClick(ESettingsType type)
    {
        print("type: " + type);

        DestroyElmnt();

        var data = LocalCore.GetDataFromJsonFromPath<ParamModel>(type.ToString(), "Settings").ToList();
        Container.SetActive(true);
        InitParams(data, type);
    }

    private void InitParams(IEnumerable<ParamModel> data, ESettingsType type)
    {
        foreach (var model in data)
        {
            var param = Instantiate(ParamButton) as GameObject;
            param.transform.SetParent(Container.transform, false);
            param.transform.localScale = Vector3.one;
            param.transform.SetAsFirstSibling();

            var button = param.GetComponent<ParamButton>();
            button.Init(model, type);

            button.OnClick += Button_OnClick;

            _lstParamButton.Add(button);
        }
    }

    private void Button_OnClick(string paramName, int paramId, ESettingsType type)
    {
        Container.SetActive(false);
        PartApp.Init(paramName, type);
    }

    private void DestroyElmnt()
    {
        if (_lstParamButton != null)
        {
            foreach (var elmnt in _lstParamButton)
            {
                Destroy(elmnt.gameObject);
            }
            _lstParamButton.Clear();
        }
    }
}