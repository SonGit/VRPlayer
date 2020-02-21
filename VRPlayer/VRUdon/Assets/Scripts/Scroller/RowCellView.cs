using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

namespace VRUdon.VR
{
    public class RowCellView : MonoBehaviour
    {
        public GameObject container;

        /// <summary>
        /// References to Components
        /// </summary>
        [SerializeField] private Button DownloadButton;
        [SerializeField] private Button FavoriteButton;
        [SerializeField] private Button UnfavoriteButton;
        [SerializeField] private LoadingIcon LoadingIcon;
        [SerializeField] private RawImage thumbnail;
        [SerializeField] private Text text;

        /// <summary>
        /// Local References
        /// </summary>
        private Video video;
        private Texture2D thumbnailTexture;

        public void On()
        {
            print("Test1");
        }
        public void Test2()
        {
            print("Test2");
        }

        /// <summary>
        /// This function just takes the Demo data and displays it
        /// </summary>
        /// <param name="data"></param>
        public void SetData(Video data)
        {
            // this cell was outside the range of the data, so we disable the container.
            // Note: We could have disable the cell gameobject instead of a child container,
            // but that can cause problems if you are trying to get components (disabled objects are ignored).
            container.SetActive(data != null);

            if (data != null)
            {
                // Release thumbnail texture
                thumbnail.texture = null;

                // Remember the current displayed video
                video = data;

                //If Video is a local video
                if (video is LocalVideo)
                {
                    LocalVideo d = video as LocalVideo;
                    // set the text if the cell is inside the data range
                    text.text = d.videoName;

                    FavoriteButton.gameObject.SetActive(false);
                    UnfavoriteButton.gameObject.SetActive(false);
                }

                // If Video us a UserVideo
                if(video is UserVideo)
                {
                    // Check if thumbnail exists in cache (temp folder)...
                    if(!IsThumbnailExists(video.videoInfo.id))
                    {
                        //... if not, start download thumbnail
                        StartCoroutine(DownloadThumbnail(video.videoInfo.thumbnail_link));
                    }else
                    {
                        //... if exists, loading straight from path
                        LoadThumbnail(video.videoInfo.id);
                    }

                    // set the text if the cell is inside the data range
                    text.text = video.videoInfo.video_name;

                    if(video.videoInfo.favorited)
                    {
                        FavoriteButton.gameObject.SetActive(true);
                        UnfavoriteButton.gameObject.SetActive(false);
                    }
                    else
                    {
                        FavoriteButton.gameObject.SetActive(false);
                        UnfavoriteButton.gameObject.SetActive(true);
                    }

                    print(video.videoInfo.video_name + " favorited " + video.videoInfo.favorited);
                }
            }
        }

        // Check if thumbnail exists at this path
        private bool IsThumbnailExists(string video_id)
        {
            if(File.Exists(User.GetPathToVideoThumbnail(video_id)))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        // Cache for downloaded thumbnail binary data
        private byte[] thumbnailRaw;
        /// <summary>
        /// Downloads the thumbnail.
        /// </summary>
        /// <param name="url">URL.</param>
        protected IEnumerator DownloadThumbnail(string url)
        {
            // Release
            thumbnailRaw = null;

            yield return new WaitForSeconds(.25f);

            using (WebClient client = new WebClient())
            {
                try
                {
                    // Try to download from URL
                    client.DownloadDataCompleted += DownloadThumbnailDataCompleted;
                    client.DownloadDataAsync(new System.Uri(url));
                }
                catch (Exception e)
                {
                    Debug.LogError("DownloadThumbnail Exception! " + e.Message);
                }
            }
        }

        /// <summary>
        /// Event called when downloaded thumbnail is completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DownloadThumbnailDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            thumbnailRaw = e.Result;

            // Try to write data to temp folder
            File.WriteAllBytes(User.GetPathToVideoThumbnail(video.videoInfo.id), thumbnailRaw);

            // Release texture
            if (thumbnailTexture == null)
            {
                thumbnailTexture = new Texture2D(4, 4, TextureFormat.RGB565, false);
            }

            // Load data to texture
            thumbnailTexture.LoadImage(thumbnailRaw);

            // Assign texture to thumbnail object
            //thumbnail.texture = thumbnailTexture;

            print("Thumbnail downloaded to " + User.GetPathToVideoThumbnail(video.videoInfo.id));
        }

        /// <summary>
        /// Load thumbnail from path, locally
        /// </summary>
        /// <param name="video_id"></param>
        void LoadThumbnail(string video_id)
        {
            try
            {
                // Read texture
                byte[] fileData = File.ReadAllBytes(User.GetPathToVideoThumbnail(video_id));

                // Release texture
                if (thumbnailTexture == null)
                {
                    thumbnailTexture = new Texture2D(4, 4, TextureFormat.RGB565, false);
                }
                // Load data to texture
                thumbnailTexture.LoadImage(fileData);

                // Optional
                thumbnailTexture.name = video.videoInfo.id;

                // Assign texture to thumbnail object
                //thumbnail.texture = thumbnailTexture;
            }
            catch (Exception e)
            {
                Debug.Log("Exception!  " + e.Message);
            }
            finally
            {
         
            }
        }

    }
}
