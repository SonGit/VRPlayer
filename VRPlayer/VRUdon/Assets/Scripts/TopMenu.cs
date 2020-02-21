using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace VRUdon.VR
{
    public class TopMenu : MonoBehaviour
    {
        public ToggleGroup MyStorage;
        public ToggleGroup Download;
        public ToggleGroup MyVideo;

        public Toggle MyStorageDefaultToggle;
        public Toggle DownloadDefaultToggle;
        public Toggle MyVideoDefaultToggle;

        private void Awake()
        {
            MessageDispatcher.AddListener(GameEvent.openLocalVideoScreen, OpenLocalVideoScreen);
            MessageDispatcher.AddListener(GameEvent.openDownloadVideoScreen, OpenDownloadVideoScreen);
            MessageDispatcher.AddListener(GameEvent.openMyVideoScreen, OpenMyVideoScreen);
        }

        void OpenLocalVideoScreen(IMessage rMessage)
        {
            SetAllOff();

            MyStorage.gameObject.SetActive(true);

            MyStorage.SetAllTogglesOff();

            MyStorageDefaultToggle.isOn = true;
            MyStorage.NotifyToggleOn(MyStorageDefaultToggle);
        }

        void OpenDownloadVideoScreen(IMessage rMessage)
        {
            SetAllOff();

            Download.gameObject.SetActive(true);

            Download.SetAllTogglesOff();

            DownloadDefaultToggle.isOn = true;
            Download.NotifyToggleOn(DownloadDefaultToggle);
        }

        void OpenMyVideoScreen(IMessage rMessage)
        {
            SetAllOff();

            MyVideo.gameObject.SetActive(true);

            MyVideo.SetAllTogglesOff();

            MyVideoDefaultToggle.isOn = true;
            MyVideo.NotifyToggleOn(MyVideoDefaultToggle);
        }

        void SetAllOff()
        {
            MyStorage.gameObject.SetActive(false);
            Download.gameObject.SetActive(false);
            MyVideo.gameObject.SetActive(false);
        }

        void ActivateFirstToggle(ToggleGroup group)
        {
            var toggle = group.ActiveToggles().First();

            if (toggle)
            {
                toggle.Select();
            }

        }
    }
}

