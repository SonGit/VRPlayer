using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreadUnblocker : MonoBehaviour
{
	public static ThreadUnblocker instance;
    // Start is called before the first frame update
    void Awake()
    {
		instance = this;
    }

//	public void Unblock()
//	{
//		VideoDownloader[] videoUIs = Object.FindObjectsOfType<VideoDownloader> ();
//		foreach (VideoDownloader downloader in videoUIs) {
//			downloader.StopThread ();
//		}
//	}
//
//	public void Return()
//	{
//		VideoDownloader[] videoUIs = Object.FindObjectsOfType<VideoDownloader> ();
//		foreach (VideoDownloader downloader in videoUIs) {
//			downloader.Reset ();
//		}
//	}
		
}
