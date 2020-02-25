using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VRUdon.VR
{
    public class VideoName : MonoBehaviour
    {
        [SerializeField]
        private MediaPlayer mediaPlayer;

        [SerializeField]
        private Text VideoNameLabel;

        // Start is called before the first frame update
        void Start()
        {
            if (mediaPlayer == null)
            {
                mediaPlayer = GameObject.FindObjectOfType<MediaPlayer>();
            }
        }
        string[] videoPath;
        // Update is called once per frame
        void Update()
        {
            if(VideoNameLabel && mediaPlayer)
            {
                videoPath = mediaPlayer.m_VideoPath.Split('/');
                VideoNameLabel.text = videoPath[videoPath.Length - 1];
            }
        }
    }
}
