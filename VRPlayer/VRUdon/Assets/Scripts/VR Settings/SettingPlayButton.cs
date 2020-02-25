using com.ootii.Messages;
using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VRUdon.VR
{
    public class SettingPlayButton : MonoBehaviour
    {
        [SerializeField]
        private MediaPlayer mediaPlayer;

        [SerializeField]
        private Toggle toggle;

        private void Awake()
        {
            MessageDispatcher.AddListener(GameEvent.pauseVideo, OnPauseVideo);
            MessageDispatcher.AddListener(GameEvent.resumeVideo, OnResumeVideo);
        }

        /// <summary>
        /// Reset 
        /// </summary>
        /// <param name="rMessage"></param>
        void OnPauseVideo(IMessage rMessage)
        {
            print("OnPauseVideo");
            mediaPlayer.Control.Pause();
            //MessageDispatcher.SendMessageData(GameEvent.goToPage, 0);
        }

        /// <summary>
        /// Reset 
        /// </summary>
        /// <param name="rMessage"></param>
        void OnResumeVideo(IMessage rMessage)
        {
            print("OnResumeVideo");
            mediaPlayer.Control.Play();
            //MessageDispatcher.SendMessageData(GameEvent.goToPage, 0);
        }
        // Start is called before the first frame update
        void Start()
        {
            if(mediaPlayer == null)
            {
                mediaPlayer = GameObject.FindObjectOfType<MediaPlayer>();
            }

            if (toggle == null)
            {
                toggle = this.GetComponent<Toggle>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            toggle.isOn = !mediaPlayer.Control.IsPlaying();
        }


    }

}
