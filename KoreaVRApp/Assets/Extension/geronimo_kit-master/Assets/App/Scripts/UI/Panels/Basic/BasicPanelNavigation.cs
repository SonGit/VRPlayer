using System;
using UnityEngine;
using UnityEngine.UI;

public class BasicPanelNavigation : BasicPanel
{
    [SerializeField] private Button BtnBack;

    public event Action OnBack;

    protected override void Start()
    {
        base.Start();

        BtnBack.onClick.AddListener(() =>
        {
            if (OnBack != null)
            {

                OnBack();
            }
        });
    }
}