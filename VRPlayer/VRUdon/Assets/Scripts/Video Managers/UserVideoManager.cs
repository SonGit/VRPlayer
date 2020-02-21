using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRUdon.VR
{
    public struct UserVideoPlayload
    {
        public List<Video> userVideos;

        public List<Video> favoriteVideos;

        public UserVideoPlayload(List<Video> userVideos, List<Video> favoriteVideos)
        {
            this.userVideos = userVideos;
            this.favoriteVideos = favoriteVideos;
        }
    }

    public class UserVideoManager : MonoBehaviour
    {
        // Reference to logged in user
        private User user;

        private List<Video> userVideos = new List<Video>();

        private List<Video> favoriteVideos = new List<Video>();

        void Awake()
        {
            MessageDispatcher.AddListener(GameEvent.userLoggedIn, OnUserLoggedIn);
            MessageDispatcher.AddListener(GameEvent.showUserVideoScreen, OnLoadUserVideo);
            MessageDispatcher.AddListener(GameEvent.showFavoriteUserVideoScreen, OnLoadFavoriteVideo);
        }
        void OnUserLoggedIn(IMessage rMessage)
        {
            if(rMessage.Data is User)
            {
                user = rMessage.Data as User;
            }
        }

        void OnLoadUserVideo(IMessage rMessage)
        {
            // Wipe the screen first
            MessageDispatcher.SendMessageData(GameEvent.InitScreen, null);
            // Get User Video List
            Networking.instance.GetUserVideoRequest(user.auth_token, OnGetUserVideoList);
        }

        /// <summary>
        /// Process user video data from server
        /// </summary>
        /// <param name="videoList"></param>
        void OnGetUserVideoList(Video_Info[] videoList)
        {
            userVideos = new List<Video>();

            for (int i = 0; i < videoList.Length; i++)
            {
                UserVideo userVideo = new UserVideo(videoList[i]);
                userVideos.Add(userVideo);
            }

            // Also get favorite videos , so we know which video is favorited
            Networking.instance.GetFavoriteVideoRequest(user.auth_token, OnGetFavoriteVideoList);
        }

        /// <summary>
        /// Process user favorite video data from server
        /// </summary>
        /// <param name="videoList"></param>
        void OnGetFavoriteVideoList(Video_Info[] videoList)
        {
            favoriteVideos = new List<Video>();

            for (int i = 0; i < videoList.Length; i++)
            {
                UserVideo favoriteVideo = new UserVideo(videoList[i]);
                favoriteVideos.Add(favoriteVideo);

                // Cross-reference with userVideos to see which user video is favorited
                for (int j = 0; j < userVideos.Count; j++)
                {
                    if (userVideos[j].videoInfo.id == favoriteVideo.videoInfo.id)
                    {
                        userVideos[j].videoInfo.favorited = true;
                    }
                }

            }

            // Once everything is done, send out the payload
            MessageDispatcher.SendMessageData(GameEvent.sendVideos, userVideos, EnumMessageDelay.NEXT_UPDATE);
        }

        /// <summary>
        /// Process user favorite video data from server
        /// Send favorite videos only
        /// </summary>
        /// <param name="videoList"></param>
        void OnGetFavoriteVideoList2(Video_Info[] videoList)
        {
            favoriteVideos = new List<Video>();

            for (int i = 0; i < videoList.Length; i++)
            {
                UserVideo favoriteVideo = new UserVideo(videoList[i]);
                favoriteVideos.Add(favoriteVideo);

                // Cross-reference with userVideos to see which user video is favorited
                for (int j = 0; j < userVideos.Count; j++)
                {
                    if (userVideos[j].videoInfo.id == favoriteVideo.videoInfo.id)
                    {
                        userVideos[j].videoInfo.favorited = true;
                    }
                }

            }

            // Once everything is done, send out the payload
            MessageDispatcher.SendMessageData(GameEvent.sendVideos, favoriteVideos, EnumMessageDelay.NEXT_UPDATE);
        }

        void OnLoadFavoriteVideo(IMessage rMessage)
        {
            // Wipe the screen first
            MessageDispatcher.SendMessageData(GameEvent.InitScreen, null);
            // Get Fav User Video List
            Networking.instance.GetFavoriteVideoRequest(user.auth_token, OnGetFavoriteVideoList2);
        }

    }

}
