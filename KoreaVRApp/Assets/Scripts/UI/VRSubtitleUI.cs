using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Net;
using System;

public class VRSubtitleUI : MonoBehaviour
{
	[SerializeField]
	private Text _languageLabel = null;

	[SerializeField]
	private RawImage _selectedImg;

	private Subtitle subtitle;

	private Video video;

	private VRSubtitle vrSubtitleManager;



    // Start is called before the first frame update
    void Start()
    {
		vrSubtitleManager = GetComponentInParent<VRSubtitle> ();

		if (_selectedImg != null) {
			_selectedImg.enabled = false;
		}

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void LoadSubTitle()
	{
		string urlSubtitle = string.Empty;

		if (video is UserVideo || video is FavoriteVideo) {
			
			urlSubtitle = MainAllController.instance.user.GetPathToSubtitle (video.videoInfo.id, subtitle.language);

			print ("Checking subtitle location " + urlSubtitle);

			// if the subtitle is downloaded, simply load it
			if (File.Exists (urlSubtitle)) {
				MainAllController.instance.LoadSubTitleVR (urlSubtitle);
			} else {
				Directory.CreateDirectory(Path.Combine( MainAllController.instance.user.GetPath () ,video.videoInfo.id));
				// or, attempt to download it again
				StartCoroutine(DownloadSubtitleAndLoad(urlSubtitle));
			}

			// UI stuffs	
			vrSubtitleManager.UserSelect (this);
		}

	}

	IEnumerator DownloadSubtitleAndLoad(string path)
	{
		using (WebClient client = new WebClient ()) {

			try
			{

				client.DownloadFileAsync(new System.Uri(subtitle.subtitle_link),path);

			}catch(System.Exception e) {

				Debug.LogError ("DownloadThumbnail Exception! " + e.Message);

			} 

			// Attemp to reset coroutine if client cannot connect 
			// Could be useful for when internet is lost during downloading
			if (!client.IsBusy) {
				this.StartCoroutine (DownloadSubtitleAndLoad (path));
				Debug.Log ("Resetting Download Thumbnail...................................");
				yield break;
			}

			// Wait until client has completed download
			while(client.IsBusy)
			{
				yield return new WaitForSeconds (.25f);
			}

			LoadSubTitle ();
		}
	}

	public void Setup(Subtitle subtitle, Video video)
	{
		_languageLabel.text = subtitle.language.ToUpper();
		this.subtitle = subtitle;
		this.video = video;
	}

	public void OnSelected()
	{
		if (_selectedImg != null) {
			_selectedImg.enabled = true;
		}
	}

	public void OnUnselected()
	{
		if (_selectedImg != null) {
			_selectedImg.enabled = false;
		}
	}
}
