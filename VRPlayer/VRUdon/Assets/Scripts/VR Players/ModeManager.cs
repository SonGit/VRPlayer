using com.ootii.Messages;
using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VRUdon.VR
{
    public enum ASPECT_RATIO
    {
        RATIO_43,
        RATIO_169,
        RATIO_1851,
        RATIO_ORIGINAL,
    }

    public class ModeManager : MonoBehaviour
    {
        [SerializeField]
        private VRMode[] modes;

        [SerializeField]
        private VRMode currentMode;

        [SerializeField]
        private ASPECT_RATIO currentRatio;

        [SerializeField]
        private MediaPlayer mediaPlayer;

        void Awake()
        {
            modes = this.GetComponentsInChildren<VRMode>();

            MessageDispatcher.AddListener(GameEvent.showFlatVR, ShowFlat);
            MessageDispatcher.AddListener(GameEvent.showCinemaVR, ShowCinema);
            MessageDispatcher.AddListener(GameEvent.showStereoVR, ShowStereo);
            MessageDispatcher.AddListener(GameEvent.showSphereVR, ShowSphere);
            MessageDispatcher.AddListener(GameEvent.showAutoVR, ShowAutoVR);

            MessageDispatcher.AddListener(GameEvent.setSize, SetSize);
            MessageDispatcher.AddListener(GameEvent.setRatio43, SetRatio_43);
            MessageDispatcher.AddListener(GameEvent.setRatio169, SetRatio_169);
            MessageDispatcher.AddListener(GameEvent.setRatio1851, SetRatio_1851);
            MessageDispatcher.AddListener(GameEvent.setRatioOriginal, SetRatio_169);

            MessageDispatcher.AddListener(GameEvent.packingLR, PackingLR);
            MessageDispatcher.AddListener(GameEvent.packingTB, PackingTB);
            MessageDispatcher.AddListener(GameEvent.packingNone, PackingNone);

            MessageDispatcher.AddListener(GameEvent.unlockScreen, UnlockScreen);
            MessageDispatcher.AddListener(GameEvent.lockScreen, LockScreen);
        }

        IEnumerator Start()
        {
            mediaPlayer = GameObject.FindObjectOfType<MediaPlayer>();

            yield return new WaitForEndOfFrame();
            MessageDispatcher.SendMessageData(GameEvent.showCinemaVR, null);
            // Quick and dirty way to activate Packing top/bottom and also activate the toggle
            //Toggle toggle = GameObject.Find("Cinema Mode Toggle").GetComponent<Toggle>();
            //toggle.isOn = true;
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
        void ShowAutoVR(IMessage rMessage)
        {
            AutoVR();
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

        public float currentScale = 1;

        void SetSize(IMessage rMessage)
        {
            currentScale = (float)rMessage.Data;

            switch (currentRatio)
            {
                case ASPECT_RATIO.RATIO_43:
                    currentMode.Ratio_43(currentScale);
                    break;
                case ASPECT_RATIO.RATIO_169:
                    currentMode.Ratio_169(currentScale);
                    break;
                case ASPECT_RATIO.RATIO_1851:
                    currentMode.Ratio_1851(currentScale);
                    break;
                case ASPECT_RATIO.RATIO_ORIGINAL:
                    currentMode.Ratio_169(currentScale);
                    break;
            }
        }

        void SetRatio_43(IMessage rMessage)
        {
            if(currentMode is FlatMode || currentMode is CinemaMode)
            {
                currentMode.Ratio_43(currentScale);
                currentRatio = ASPECT_RATIO.RATIO_43;
            }
        }

        void SetRatio_169(IMessage rMessage)
        {
            if (currentMode is FlatMode || currentMode is CinemaMode)
            {
                currentMode.Ratio_169(currentScale);
                currentRatio = ASPECT_RATIO.RATIO_169;
            }
        }

        void PackingTB(IMessage rMessage)
        {
            currentMode.PackingTopBottom();
        }

        void PackingLR(IMessage rMessage)
        {
            currentMode.PackingLeftRight();
        }
        void PackingNone(IMessage rMessage)
        {
            currentMode.PackingNone();
        }

        void SetRatio_1851(IMessage rMessage)
        {
            if (currentMode is FlatMode || currentMode is CinemaMode)
            {
                currentMode.Ratio_1851(currentScale);
                currentRatio = ASPECT_RATIO.RATIO_1851;
            }
        }

        void SetRatio_Original(IMessage rMessage)
        {
            currentMode.ScreenUnlock();
        }

        void AutoVR()
        {
            if (currentMode == null)
            {
                ShowCinema(null);
            }
            else
            {

                int videoWidth = mediaPlayer.Info.GetVideoWidth();
                int videoHeight = mediaPlayer.Info.GetVideoHeight();

                Debug.Log("videoWidth " + videoWidth + "   videoHeight " + videoHeight);
                if (videoWidth == 0 || videoHeight == 0)
                {
                    Debug.Log("Error AutoVR! Width/Height is zero!");
                    return;
                }

                float aspectRatio = videoWidth / videoHeight;

                // if aspect ratio = 1, high chance that the video is 180 
                if (aspectRatio == 1)
                {
                    Debug.Log("the video is 180 ");

                    // Send command to switch to 180 VR
                    MessageDispatcher.SendMessageData(GameEvent.showStereoVR, null);

                    // Quick and dirty way to activate Packing top/bottom and also activate the toggle
                    Toggle toggle = GameObject.Find("12TB Toggle").GetComponent<Toggle>();
                    toggle.isOn = true;
                }

                // if aspect ratio = 2, high chance that the video is 360
                if (aspectRatio == 2)
                {
                    Debug.Log("the video is 360 ");
                    // Send command to switch to 360 VR
                    MessageDispatcher.SendMessageData(GameEvent.showSphereVR, null);

                    // Quick and dirty way to activate Packing left/right and also activate the toggle
                    Toggle toggle = GameObject.Find("12LR Toggle").GetComponent<Toggle>();
                    toggle.isOn = true;
                }
            }
        }
    }
}
