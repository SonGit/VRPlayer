using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRUdon.VR
{
    public class ModeManager : MonoBehaviour
    {
        [SerializeField]
        private VRMode[] modes;

        [SerializeField]
        private VRMode currentMode;

        void Awake()
        {
            modes = this.GetComponentsInChildren<VRMode>();

            MessageDispatcher.AddListener(GameEvent.showFlatVR, ShowFlat);
            MessageDispatcher.AddListener(GameEvent.showCinemaVR, ShowCinema);
            MessageDispatcher.AddListener(GameEvent.showStereoVR, ShowStereo);
            MessageDispatcher.AddListener(GameEvent.showSphereVR, ShowSphere);

            MessageDispatcher.AddListener(GameEvent.unlockScreen, UnlockScreen);
            MessageDispatcher.AddListener(GameEvent.lockScreen, LockScreen);
        }

        private void Start()
        {
            ShowCinema(null);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                MessageDispatcher.SendMessageData(GameEvent.showFlatVR, null);
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                MessageDispatcher.SendMessageData(GameEvent.showCinemaVR, null);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                MessageDispatcher.SendMessageData(GameEvent.showStereoVR, null);
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                MessageDispatcher.SendMessageData(GameEvent.showSphereVR, null);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                MessageDispatcher.SendMessageData(GameEvent.lockScreen, null);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                MessageDispatcher.SendMessageData(GameEvent.unlockScreen, null);
            }
        }

        void ShowFlat(IMessage rMessage)
        {
            TurnOffAll();

            foreach (VRMode mode in modes)
            {
                if(mode is FlatMode)
                {
                    mode.Show();
                    currentMode = mode;
                }
            }
        }

        void ShowCinema(IMessage rMessage)
        {
            TurnOffAll();

            foreach (VRMode mode in modes)
            {
                if (mode is CinemaMode)
                {
                    mode.Show();
                    currentMode = mode;
                }
            }
        }
        void ShowStereo(IMessage rMessage)
        {
            TurnOffAll();

            foreach (VRMode mode in modes)
            {
                if (mode is StereoMode)
                {
                    mode.Show();
                    currentMode = mode;
                }
            }
        }
        void ShowSphere(IMessage rMessage)
        {
            TurnOffAll();

            foreach (VRMode mode in modes)
            {
                if (mode is SphereMode)
                {
                    mode.Show();
                    currentMode = mode;
                }
            }
        }

        void LockScreen(IMessage rMessage)
        {
            currentMode.ScreenLock();
        }

        void UnlockScreen(IMessage rMessage)
        {
            currentMode.ScreenUnlock();
        }

        void TurnOffAll()
        {
            foreach(VRMode mode in modes)
            {
                mode.Hide();
            }
        }
    }
}
