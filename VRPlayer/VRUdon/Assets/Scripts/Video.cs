using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System;
using System.Net;

namespace VRUdon.VR
{
    [System.Serializable]
    public class Video_Info
    {

        public string thumbnail_link { get; set; }

        public string[] screenShot_links { get; set; }

        public string video_name { get; set; }

        public string description { get; set; }

        public string date { get; set; }

        public string[] tag { get; set; }

        public int length { get; set; }

        public string genre { get; set; }

        public string actor { get; set; }

        public string id { get; set; }

        public long size { get; set; }

        public string status { get; set; }

        public DateTime dateTime
        {
            get
            {
                string formatString = "yyyy-MM-dd HH:mm:ss";
                DateTime dt = default(DateTime);
                try
                {
                    dt = DateTime.ParseExact(date, formatString, null);
                }
                catch (Exception e)
                {
                    Debug.LogError("DateTime format Invalid!  " + e.Message);
                }

                return dt;
            }
        }

    }


    public class Video
    {

        private Video_Info _info;
        public Video_Info videoInfo
        {
            get
            {
                return _info;
            }

            set
            {
                _info = value;
            }
        }

        private float _lastTimeMs = 0;
        public float lastTimeMs
        {
            get
            {
                return _lastTimeMs;
            }

            set
            {
                _lastTimeMs = value;
            }
        }


    }


    public class LocalVideo : Video
    {

        public string videoName;

        public string videoURL;

        public long videoSize;

        public DateTime videoDate;

        public double videoLength;

        public LocalVideo(string url)
        {
            try
            {
                if (File.Exists(url))
                {

                    FileInfo fileInfo = new FileInfo(url);

                    videoName = fileInfo.Name;

                    videoURL = fileInfo.FullName;

                    videoSize = fileInfo.Length;

                    videoDate = fileInfo.CreationTime;

                    videoInfo = new Video_Info();
                    videoInfo.id = fileInfo.FullName;

                }
                else
                {
                    throw new ArgumentException("Reason", "param name");
                }
            }
            catch (Exception e)
            {

            }

        }

        public LocalVideo(string videoName, string videoURL, string videoLength, string videoSize, string videoDate)
        {
            this.videoName = videoName;
            this.videoURL = videoURL;

            videoInfo = new Video_Info();
            videoInfo.id = videoName;

            try
            {
                // IOS return size in bit. We need byte
                this.videoSize = (Convert.ToInt64(videoSize)) / 8;

            }
            catch (Exception e)
            {

                Debug.Log("LocalVideo videoSize exception! " + e.Message);
                this.videoSize = (long)0;
            }

            try
            {
                // 2018-03-27 07:22:32 +0000
                string[] elements = videoDate.Split(' ');
                string input = elements[0] + " " + elements[1];
                this.videoDate = DateTime.ParseExact(input, "yyyy-MM-dd HH:mm:ss", null);

            }
            catch (Exception e)
            {

                Debug.Log("LocalVideo videoDate exception! " + e.Message);

            }

            try
            {

                // seconds --> ms
                this.videoLength = TimeSpan.FromSeconds(Convert.ToDouble(videoLength)).TotalMilliseconds;

            }
            catch (Exception e)
            {

                Debug.Log("LocalVideo videoDate exception! " + e.Message);

            }
        }

    }


    public class UserVideo : Video
    {
        public UserVideo(Video_Info video_info)
        {
            this.videoInfo = video_info;
        }



        public void DownloadSubtitle()
        {
            string filepath = String.Empty;
        }
    }



    public class RecommendVideo : Video
    {
        public RecommendVideo()
        {

        }
    }

    public class FavoriteVideo : Video
    {
        public FavoriteVideo(Video_Info video_info)
        {
            this.videoInfo = video_info;
        }

    }
}
