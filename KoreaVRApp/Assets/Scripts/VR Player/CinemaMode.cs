using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemaMode : VRMode
{
	void Awake()
	{
		base.Init ();
	}
    // Start is called before the first frame update
    void Start()
    {
		//DefaultSize ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    bool first = false;
	public override void Show()
	{
		base.Show ();
		mediaPlayer.VideoLayoutMapping = RenderHeads.Media.AVProVideo.VideoMapping.Unknown;
		applyToMesh.ForceUpdate ();

        if(first)
        {
            ResumeRatio();
        }else
        {
            first = true;
        }

	}
}
