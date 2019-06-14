using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShotUI : VideoUI
{
	[SerializeField] protected RawImage screenShot_image = null;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SetupScreenShot(string url)
	{
		if (screenShotTexture == null) {
			screenShotTexture = new Texture2D(4, 4, TextureFormat.DXT1, false);
		}

		if (screenShot_image.texture == null) {
			CheckAndDownloadScreenShot(url);
		} else {
			StopLoadingScreen ();
		}

	}

	public override void OnLoadedScreenShot ()
	{
		screenShot_image.texture = screenShotTexture;
		StopLoadingScreen ();
	}


}
