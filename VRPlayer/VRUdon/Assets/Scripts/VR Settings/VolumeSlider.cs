using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VRUdon.VR
{
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField]
        private MediaPlayer mediaPlayer;

        [SerializeField]
        private Slider volumeSlider;

        [SerializeField]
        private float volumePrev;


        // Start is called before the first frame update
        void Start()
        {
            if (mediaPlayer == null)
            {
                mediaPlayer = GameObject.FindObjectOfType<MediaPlayer>();
            }

            if (volumeSlider == null)
            {
                volumeSlider = GetComponentInChildren<Slider>();
            }
        }

        public void Increase()
        {
            volumeSlider.value += 0.1f;
        }

        public void Decrease()
        {
            volumeSlider.value -= 0.1f;
        }

        public void OnSliderChanged(float value)
        {
            if (mediaPlayer && volumeSlider && volumeSlider.value != volumePrev)
            {
                volumePrev = value;

                mediaPlayer.Control.SetVolume(volumeSlider.value);
            }
        }
    }

}
