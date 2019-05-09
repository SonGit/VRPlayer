using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using CielaSpike;
using UnityEngine.Android;

public class LocalVideoManager : MonoBehaviour
{
    public List<Video> localVideos = new List<Video>();

    public static LocalVideoManager instance;

    private bool m_IsDisplayLocalVideo;

    public delegate void SuccessCallback();

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
		//LoadLocal_ios ("IMG_0188.MOV@file:///var/mobile/Media/DCIM/100APPLE/IMG_0188.MOV@159.67666666666668@208430086@2018-03-27 07:22:32 +0000");
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// DisplayBookmartVideo?
    /// </summary>
    public bool IsDisplayLocalVideo
    {
        get { return m_IsDisplayLocalVideo; }
        set { m_IsDisplayLocalVideo = value; }
    }

    public void Load(SuccessCallback callback = null)
    {
		//Reset count
		localVideos = new List<Video>();

        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead)) {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
            Debug.Log(" RequestUserPermission ExternalStorageRead");
            StartCoroutine(LoadProgress(callback));
        } else {
            StartCoroutine(LoadProgress(callback));
        }
        
    }

    IEnumerator LoadProgress(SuccessCallback callback = null)
    {
#if UNITY_ANDROID
		while(!Permission.HasUserAuthorizedPermission (Permission.ExternalStorageRead))
		{
			yield return new WaitForSeconds (.5f);
		}
#endif

        List<string> AllFolders = new List<string>();
		string origin = "D:\\Video";
#if UNITY_EDITOR

        try
        {
			var tempFolders = Directory.GetDirectories(origin);

			foreach(var folder in tempFolders)
			{
				string fileName = Path.GetFileName(folder);

				AllFolders.Add(folder);
			}

			for(int i = 0 ; i< AllFolders.Count ; i ++)
			{
				var paths = GetFileList("*.mp4",AllFolders[i]);
				foreach(var path in paths)
				{
					LocalVideo localVideo = new LocalVideo(path);
					localVideos.Add(localVideo);
				}
			}

        }
        catch (System.Exception e)
        {
            Debug.Log("LocalVideoManager Exception! " + e.Message);
        }
        finally
        {

            if (callback != null)
            {
                callback();
            }
        }

#endif

        // Enumerate all files in Android local storage, ignoring Android folder #if UNITY_ANDROID  && !UNITY_EDITOR
		#if UNITY_ANDROID && !UNITY_EDITOR
		try
		{
			var plugin = new AndroidJavaClass ("com.example.unityplugin.PluginClass");
			var unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			var unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
			var unityContext = unityActivity.Call<AndroidJavaObject>("getApplicationContext");

			string data =  plugin.CallStatic<string> ("getAllMedia",unityContext);
		Debug.Log("getAllMedia  " + data);
            LoadLocal(data);

		}
		catch(System.Exception e)
		{
			Debug.Log("LocalVideoManager Exception! " + e.Message);
		} finally
		{

			if (callback != null) {
				callback ();
			}
		}

	#endif

#if UNITY_IOS
        try
        {

            string urlString = SwiftForUnity.getURLAndBuildThumbnail();
            Debug.Log("UNITY_IOS LOCAL   " + urlString);
            LoadLocal_ios(urlString);

        }
        catch(System.Exception e)
        {
            Debug.Log("LocalVideoManager Exception! " + e.Message);
        } finally
        {

            if (callback != null) {
                callback ();
            }
        }

#endif

        yield return new WaitForEndOfFrame();
    }

    // Remove videos that have been deleted by user
    void RemoveInvalidVideos()
    {
		#if UNITY_ANDROID && !UNITY_EDITOR
        if (localVideos != null) {
            for (int i = localVideos.Count - 1; i > -1; i--)
            {
				if (! File.Exists((localVideos[i] as LocalVideo).videoURL))
                {
                    localVideos.RemoveAt(i);
                }
            }
        }
		#endif
    }

    bool CheckDuplicate(string path)
    {
        for (int i = 0; i < localVideos.Count; i++) {
			if (path == (localVideos[i] as LocalVideo).videoURL) {
                return true;
            }
        }
        return false;
    }

    void UpdateVideo(string path)
    {
        for (int i = 0; i < localVideos.Count; i++) {
			if (path == (localVideos[i] as LocalVideo).videoURL) {
                localVideos[i] = new LocalVideo(path);
                return;
            }
        }
    }

    public List<Video> GetAllLocalVideos()
    {
        return localVideos;
    }

    public static IEnumerable<string> GetFileList(string fileSearchPattern, string rootFolderPath)
    {
        Queue<string> pending = new Queue<string>();
        pending.Enqueue(rootFolderPath);
        string[] tmp;
        while (pending.Count > 0)
        {
            rootFolderPath = pending.Dequeue();
            try
            {
                tmp = Directory.GetFiles(rootFolderPath, fileSearchPattern);
            }
            catch (System.UnauthorizedAccessException)
            {
                continue;
            }
            for (int i = 0; i < tmp.Length; i++)
            {
                yield return tmp[i];
            }
            tmp = Directory.GetDirectories(rootFolderPath);
            for (int i = 0; i < tmp.Length; i++)
            {
                pending.Enqueue(tmp[i]);
            }
        }
    }
    public string testData;
    private void LoadLocal(string data)
    {
        try
        {
            // Init vars
            string[] videos = data.Split(',');

            string[] element;
            string videoPath = string.Empty;
            long videoLengthMs = 0;



            foreach (string video in videos)
            {
                if (video != null && video != string.Empty)
                {
                    element = video.Split('@');

                    videoPath = element[0];

                    videoLengthMs = (long)System.Convert.ToDouble(element[1]);

                    if (videoPath != null && videoPath != string.Empty)
                    {
                        Debug.Log("making new local " + videoPath);

                        LocalVideo newVideo = null;

                        if (File.Exists(videoPath))
                        {
                            newVideo = new LocalVideo(videoPath);
                        }

                        if (newVideo != null)
                        {
                            newVideo.videoInfo.length = (int)(videoLengthMs);

                            localVideos.Add(newVideo);
                        }


                    }else
                    {
                        Debug.Log("NOT making new local " + videoPath);
                    }

                }
            }

        }
        catch (System.Exception e)
        {
            Debug.Log("LoadLocal Exception! " + e.Message);
        }

		RemoveInvalidVideos();
    }

    /// <summary>
    /// Loading local from ios is different....
    /// Format: videoName@URL@length@size@date
    /// </summary>
    /// <param name="data">Data.</param>
    private void LoadLocal_ios(string data)
    {
        try
        {
            // Init vars
            string[] videos = data.Split(',');

            string[] element;
            string videoPath = string.Empty;
            long videoLengthMs = 0;

            RemoveInvalidVideos();

            foreach (string video in videos)
            {
                if (video != null && video != string.Empty)
                {
                    element = video.Split('@');

                    /// Format: videoName@URL@length@size@date
                    /// 
                    string videoName = element[0];

                    string videoURL = element[1];

                    string videoLength = element[2];

                    string videoSize = element[3];

                    string videoDate = element[4];

					if (videoName != null && videoName != string.Empty)
                    {
                        Debug.Log("videoName  " + videoName + " videoURL  "+ videoURL + " videoLength  " + videoLength + " videoSize  " + videoSize + " videoDate  " + videoDate);
						LocalVideo newVideo = new LocalVideo(videoName,videoURL,videoLength,videoSize,videoDate);

                        if (newVideo != null)
                        {
                            localVideos.Add(newVideo);
                            Debug.Log("MADE new local " + videoPath);
                        }


                    }
                    else
                    {
                        Debug.Log("NOT making new local " + videoPath);
                    }

                }
            }

        }
        catch (System.Exception e)
        {
            Debug.Log("LoadLocal Exception! " + e.Message);
        }
    }


//	public Dictionary<string,Texture2D> dictionary = new Dictionary<string, Texture2D>();
//
//	public Texture2D GetThumbnailFromCache(string url)
//	{
//		if (dictionary.ContainsKey (url)) {
//			return dictionary [url];
//		} else {
//			return null;
//		}
//	}
//
//	public void AddThumbnailToCache(string url,Texture2D tex)
//	{
//		Debug.Log ("Adding ThumbnailToCache   " + url );
//		if (!dictionary.ContainsKey (url)) {
//			dictionary.Add (url,tex);
//			Debug.Log ("Added ThumbnailToCache   " );
//		}
//	}
}
