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

    private static LocalVideoManager _instance;

    public static LocalVideoManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<LocalVideoManager>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("LocalVideoManager");
                    _instance = container.AddComponent<LocalVideoManager>();
                }
            }

            return _instance;
        }
    }

    private bool m_IsDisplayLocalVideo;

    public delegate void SuccessCallback();

    void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
		//LoadLocal_ios ("img_0001.mov@file:///var/mobile/media/dcim/100apple/img_0001.mov@0.3016666666666667@561028@2017-01-08 04:44:10 +0000,img_0002.mov@file:///var/mobile/media/dcim/100apple/img_0002.mov@0.4583333333333333@951262@2017-01-11 18:23:55 +0000,img_0018.mov@file:///var/mobile/media/dcim/100apple/img_0018.mov@16.496666666666666@32978471@2017-04-05 06:46:43 +0000,img_0023.mov@file:///var/mobile/media/dcim/100apple/img_0023.mov@31.695@68015837@2017-04-05 06:52:41 +0000,img_0090.mov@file:///var/mobile/media/dcim/100apple/img_0090.mov@72.73166666666667@147768648@2017-09-22 08:44:11 +0000,img_0026.mov@file:///var/mobile/media/dcim/100apple/img_0026.mov@0.5666666666666667@924888@2017-04-15 04:12:58 +0000,img_0151.mov@file:///var/mobile/media/dcim/100apple/img_0151.mov@9.875@21881348@2018-02-27 09:47:27 +0000,img_0084.mov@file:///var/mobile/media/dcim/100apple/img_0084.mov@14.655@26282721@2017-09-01 04:33:58 +0000,img_0031.mov@file:///var/mobile/media/dcim/100apple/img_0031.mov@44.03666666666667@94189995@2017-04-22 18:40:22 +0000,img_0085.mov@file:///var/mobile/media/dcim/100apple/img_0085.mov@90.86333333333333@186561703@2017-09-01 04:34:14 +0000,img_0082.mov@file:///var/mobile/media/dcim/100apple/img_0082.mov@8.14@15296428@2017-09-01 04:30:58 +0000,img_0032.mov@file:///var/mobile/media/dcim/100apple/img_0032.mov@50.77@108844182@2017-04-22 18:42:52 +0000,img_0152.mov@file:///var/mobile/media/dcim/100apple/img_0152.mov@2.735@6308300@2018-02-27 09:47:44 +0000,img_0188.mov@file:///var/mobile/media/dcim/100apple/img_0188.mov@159.67666666666668@208430086@2018-03-27 07:22:32 +0000,img_0083.mov@file:///var/mobile/media/dcim/100apple/img_0083.mov@147.39833333333334@300194546@2017-09-01 04:31:13 +0000,img_0020.mov@file:///var/mobile/media/dcim/100apple/img_0020.mov@16.331666666666667@35592350@2017-04-05 06:47:39 +0000,img_0153.mov@file:///var/mobile/media/dcim/100apple/img_0153.mov@264.37@566711402@2018-02-27 09:47:58 +0000,img_0294.mov@file:///var/mobile/media/dcim/100apple/img_0294.mov@3.6033333333333335@7361146@2019-04-18 09:34:02 +0000,img_0021.mov@file:///var/mobile/media/dcim/100apple/img_0021.mov@46.608333333333334@97694520@2017-04-05 06:48:52 +0000,");
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

			StorageMenu.instance.FastRefresh();

        }
        catch (System.Exception e)
        {
            Debug.Log("LoadLocal Exception! " + e.Message);
        }
    }

    public void BuildLocalVideo(string data)
    {
//		localVideos = new List<Video>();
//		LoadLocal_ios (data);
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
