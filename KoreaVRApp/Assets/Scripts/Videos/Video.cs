using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System;
using System.Net;

[System.Serializable]
public class Video_Info
{
    public Subtitle[] subtitles { get; set; }

    public string thumbnail_link { get; set; }

    public string video_name { get; set; }

    public string description { get; set; }

    public string date { get; set; }

    public string[] tag { get; set; }

    public int length { get; set; }

	public string genre { get; set; }

	public string actor { get; set; }

    public string id { get; set; }

    public long size { get; set; }

    public DateTime dateTime
    {
        get {
            string formatString = "yyyy-MM-dd HH:mm:ss";
            DateTime dt = default(DateTime);
            try
            {
                dt =  DateTime.ParseExact(date,formatString,null);
            }
            catch(Exception e) {
                Debug.LogError ("DateTime format Invalid!  " + e.Message);
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
        get {
            return _info;
        }

        set {
            _info = value;
        }
    }


//    private FileInfo _fileInfo;
//    public FileInfo fileInfo
//    {
//        get {
//            return _fileInfo;
//        }
//
//        set {
//            _fileInfo = value;
//        }
//    }

    private float _lastTimeMs = 0;
    public float lastTimeMs
    {
        get {
            return _lastTimeMs;
        }

        set {
            _lastTimeMs = value;
        }
    }
		
    public UnityWebRequest www;
    IEnumerator GetText() {
        www = UnityWebRequest.Get("https://www.avr-creative.com/SmectaGame/GetVideos.txt");
        www.SetRequestHeader("User-Agent", "runscope/0.1");
        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
//            GetVideoResponse getvideoJSON = JsonConvert.DeserializeObject<GetVideoResponse> (www.downloadHandler.text);
//            video_info[] info = getvideoJSON.video_list;
//            Subtitle[] sub = info[0].subtitle_list;
//            print (info[1].description);
//            print (sub[1].language);
        }
    }

    GetUserVideoResponse GetVideos(string auth_token){
        //check auth_token valid from server
        {
            return null;
        }
        if (auth_token != null) {
            return JsonConvert.DeserializeObject<GetUserVideoResponse> (www.downloadHandler.text);
        }
    }

    VideoInfoResponse GetVideosInfo(){
        return null;
    }

    public bool isPartial()
    {
        string pathToVideo = MainAllController.instance.user.GetPathToFile (videoInfo.id,videoInfo.video_name);
        if (File.Exists (pathToVideo)) {
            return true;
        } else {
            return false;
        }
    }

    public bool isDownloaded()
    {
        string pathToVideo = MainAllController.instance.user.GetPathToFile (videoInfo.id,videoInfo.video_name);
        if (File.Exists (pathToVideo)) {

            FileInfo fileInfo = new FileInfo (pathToVideo);

            if (fileInfo != null) {
                Debug.Log ("videoInfo.id    "+videoInfo.id+"     fileInfo.Length " + fileInfo.Length + "   videoInfo.size   " +  videoInfo.size);
                if (fileInfo.Length >= videoInfo.size) {
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }

        } else {
            return false;
        }
    }
        
}


public class LocalVideo: Video
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

            }else
            {
                throw new ArgumentException("Reason", "param name");
            }
        }
        catch(Exception e)
        {

        }

    }

    public LocalVideo(string videoName,string videoURL,string videoLength,string videoSize, string videoDate)
    {
        this.videoName = videoName;
        this.videoURL = videoURL;

		videoInfo = new Video_Info ();
		videoInfo.id = videoName;

		try
		{
			// IOS return size in bit. We need byte
			this.videoSize = (Convert.ToInt64(videoSize))/8;

		} catch(Exception e) {
			
			Debug.Log ("LocalVideo videoSize exception! " + e.Message);
			this.videoSize = (long)0;
		}

		try
		{
			// 2018-03-27 07:22:32 +0000
			string[] elements = videoDate.Split(' ');
			string input = elements[0] + " " + elements[1];
			this.videoDate = DateTime.ParseExact(input, "yyyy-MM-dd HH:mm:ss",null);

		} catch(Exception e) {

			Debug.Log ("LocalVideo videoDate exception! " + e.Message);

		}
			
		try
		{
			
			// seconds --> ms
			this.videoLength = TimeSpan.FromSeconds( Convert.ToDouble(videoLength) ).TotalMilliseconds;

		} catch(Exception e) {

			Debug.Log ("LocalVideo videoDate exception! " + e.Message);

		}
    }

}


public class UserVideo: Video
{
    public UserVideo(Video_Info video_info)
    {
        this.videoInfo = video_info;
    }



    public void DownloadSubtitle()
    {
        string filepath = String.Empty;

        foreach (Subtitle subtitle in videoInfo.subtitles) {
            
            filepath = MainAllController.instance.user.GetPathToSubtitle (videoInfo.id, subtitle.language);

            if (!File.Exists (filepath)) {
                WebClient client = new WebClient();
                client.DownloadFileAsync (new Uri(subtitle.subtitle_link), filepath);
            }

        }
    }
}



public class RecommendVideo: Video
{
    public RecommendVideo()
    {

    }
}

public class FavoriteVideo: Video
{
    public FavoriteVideo(Video_Info video_info)
    {
        this.videoInfo = video_info;
    }
        
}
