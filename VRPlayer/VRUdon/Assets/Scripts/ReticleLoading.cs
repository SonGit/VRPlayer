﻿using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticleLoading : MonoBehaviour
{
    [SerializeField]
    private Transform loadingIcon;

    [SerializeField]
    private Text reticleLabel;

    [SerializeField]
    private float loadTime = 2;

    [SerializeField]
    private float loadTimeCount = 0;

    [SerializeField]
    private bool isLoading;

    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    void Awake()
    {
        MessageDispatcher.AddListener(GameEvent.nodEvent, StartLoading);
    }

    private void Start()
    {
        loadingIcon.gameObject.SetActive(false);
        Hide();
    }

    void StartLoading(IMessage rMessage)
    {
        isLoading = true;

        loadTimeCount = 0;

        Show();
    }

    // Update is called once per frame
    void Update()
    {
        if(isLoading)
        {
            loadingIcon.localEulerAngles += new Vector3(0,0,1 * speed) ;

            loadTimeCount += Time.deltaTime;

            if (loadTimeCount > loadTime)
            {
                isLoading = false;
                Hide();

                MessageDispatcher.SendMessageData(GameEvent.recenterEvent, null);
            }
        }
    }

    void Show()
    {
        loadingIcon.gameObject.SetActive(true);
        reticleLabel.enabled = true;
    }
    void Hide()
    {
        loadingIcon.gameObject.SetActive(false);
        reticleLabel.enabled = false;
    }
}
