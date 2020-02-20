using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace VRUdon.VR
{
    public class User : MonoBehaviour
    {
        public List<Video> userVideos = new List<Video>();

        public string auth_token;

        public string username;

        string test_id = "user1@email.com";

        // Start is called before the first frame update
        void Start()
        {
            CreateTempFolder();
            Networking.instance.LoginRequest(test_id, "pass", OnGetLogin);
        }

        private void OnGetLogin(LoginRespone response)
        {
            if (response.event_code == "200")
            {
                auth_token = response.auth_token;
                username = test_id;

                Networking.instance.GetUserVideoRequest(auth_token, OnGetUserVideoList);
            }
        }
        void OnGetUserVideoList(Video_Info[] videoList)
        {
            userVideos = new List<Video>();

            for (int i = 0; i < videoList.Length; i++)
            {
                UserVideo userVideo = new UserVideo(videoList[i]);
                userVideos.Add(userVideo);
            }

            MessageDispatcher.SendMessageData(GameEvent.sendVideos, userVideos, EnumMessageDelay.NEXT_UPDATE);
        }

        public string GetPathToFile(string videoID, string filename)
        {
            return Path.Combine(GetPath() + "/" + videoID, filename + ".mp4");
        }

        public string GetPathToSubtitle(string videoID, string filename)
        {
            return Path.Combine(GetPath() + "/" + videoID, filename + ".srt");
        }

        public string GetPathToVideoFolder(string videoID)
        {
            return GetPath() + "/" + videoID;
        }

        public static string GetPathToVideoThumbnail(string videoID)
        {
            return Path.GetFullPath(Application.persistentDataPath) + "/" + "temp" + "/" + videoID;
        }

        public string GetPath()
        {
            string path = string.Empty;
#if UNITY_ANDROID && !UNITY_EDITOR
		try
		{
		using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
		using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
		{
		path = jo.Call<AndroidJavaObject>("getDir", "pFiles", 0).Call<string>("getAbsolutePath") + "/" + username;
		}
		}
		}
		catch (System.Exception e)
		{
		Debug.LogWarning(e.ToString());
		}
#else
            path = Path.GetFullPath(Application.persistentDataPath) + "/" + username;
#endif

            return path;
        }

        private void CreateTempFolder()
        {
            string path = Path.GetFullPath(Application.persistentDataPath) + "/" + "temp";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }

}
