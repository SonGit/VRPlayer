using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRUdon.VR
{
    public class VRSettingTopMenu : MonoBehaviour
    {
        public GameObject VideoSettingPanel;

        public GameObject ScreenSettingPanel;

        // Start is called before the first frame update
        void Awake()
        {
            MessageDispatcher.AddListener(GameEvent.openVideoSetting, OpenVideoSetting);
            MessageDispatcher.AddListener(GameEvent.openScreenSetting, OpenScreenSetting);
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
    }
}

