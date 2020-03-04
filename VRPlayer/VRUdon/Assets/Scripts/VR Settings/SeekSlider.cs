using RenderHeads.Media.AVProVideo;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VRUdon.VR
{
    public class SeekSlider : MonoBehaviour
    {
        [SerializeField]
        private MediaPlayer mediaPlayer;

        [SerializeField]
        private float _setVideoSeekSliderValue;

        [SerializeField]
        private Slider _videoSeekSlider;

        [SerializeField]
        private Text _currentTimeLabel;

        [SerializeField]
        private Text _totalTimeLabel;

        // Start is called before the first frame update
        void Start()
        {
            if (mediaPlayer == null)
            {
                mediaPlayer = GameObject.FindObjectOfType<MediaPlayer>();
            }

            if (_videoSeekSlider == null)
            {
                _videoSeekSlider = GetComponent<Slider>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (mediaPlayer && mediaPlayer.Info != null && mediaPlayer.Info.GetDurationMs() > 0f)
            {
                float time = mediaPlayer.Control.GetCurrentTimeMs();
                float duration = mediaPlayer.Info.GetDurationMs();
                float d = Mathf.Clamp(time / duration, 0.0f, 1.0f);

                // Debug.Log(string.Format("time: {0}, duration: {1}, d: {2}", time, duration, d));

                _setVideoSeekSliderValue = d;
                _videoSeekSlider.value = d;

                SetCurrentTimeLabel(time);
            }
        }

        public void OnSliderChanged(float value)
        {
            if (mediaPlayer && _videoSeekSlider && _videoSeekSlider.value != _setVideoSeekSliderValue)
            {
                mediaPlayer.Control.Seek(value * mediaPlayer.Info.GetDurationMs());
                _setVideoSeekSliderValue = value;
            }
        }

        TimeSpan ts;
        /// <summary>
        /// Sets the current time label in VR Setting prefab.
        /// </summary>
        /// <param name="time">Time.</param>
        void SetCurrentTimeLabel(float time)
        {
            if (_currentTimeLabel)
            {
                ts = TimeSpan.FromMilliseconds(time);
                _currentTimeLabel.text = String.Format("{0:00}", ts.Hours) + ":" + String.Format("{0:00}", ts.Minutes) + ":" + String.Format("{0:00}", ts.Seconds); 

                ts = TimeSpan.FromMilliseconds(mediaPlayer.Info.GetDurationMs());
                _totalTimeLabel.text = String.Format("{0:00}", ts.Hours) + ":" + String.Format("{0:00}", ts.Minutes) + ":" + String.Format("{0:00}", ts.Seconds);
            }
        }
    }

}
