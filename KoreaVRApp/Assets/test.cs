using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using System.Diagnostics;
using UnityEditor;
using RenderHeads.Media.AVProVideo;
using CielaSpike;

public class test : MonoBehaviour
{
	public MediaPlayer mediaPlayer;

	public static test instance;

	void Awake()
	{
		instance = this;
	}

	public void GetFrame(string url,Texture2D _texture)
	{
		this.StartCoroutine (frame(url,_texture));
	}

	IEnumerator frame(string url,Texture2D _texture)
	{
		mediaPlayer.OpenVideoFromFile (MediaPlayer.FileLocation.AbsolutePathOrURL,url,false);
		mediaPlayer.ExtractFrameAsync (_texture, ProcessExtractedFrame, 0, false, 0);
		yield return null;
	}

	private void ProcessExtractedFrame(Texture2D texture)
	{

	}
}
