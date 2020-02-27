using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRUdon.VR
{
    public class VRSettings : MonoBehaviour
    {
        public GameObject VideoSettingPanel;

        public GameObject ScreenSettingPanel;

        // Start is called before the first frame update
        void Awake()
        {
            MessageDispatcher.AddListener(GameEvent.openVideoSetting, OpenVideoSetting);
            MessageDispatcher.AddListener(GameEvent.openScreenSetting, OpenScreenSetting);
            MessageDispatcher.AddListener(GameEvent.gazingAtNothing, OnGazeAtNothing);

            MessageDispatcher.AddListener(GameEvent.recenterEvent, OnOpen);
            MessageDispatcher.AddListener(GameEvent.openVRSetting, OnOpen);
            MessageDispatcher.AddListener(GameEvent.closeVRSetting, OnClose);
        }

        void OpenVideoSetting(IMessage rMessage)
        {
            VideoSettingPanel.SetActive(true);
            ScreenSettingPanel.SetActive(false);
        }

        void OpenScreenSetting(IMessage rMessage)
        {
            VideoSettingPanel.SetActive(false);
            ScreenSettingPanel.SetActive(true);
        }

        void OnGazeAtNothing(IMessage rMessage)
        {
            gameObject.SetActive(false);
        }

        void OnClose(IMessage rMessage)
        {
            Close();
        }

        void OnOpen(IMessage rMessage)
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}

