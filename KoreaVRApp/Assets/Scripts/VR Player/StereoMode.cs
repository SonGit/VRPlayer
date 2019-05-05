using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StereoMode : VRMode
{
    // Start is called before the first frame update
	void Awake()
	{
		base.Init ();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public override void Show()
	{
		base.Show ();
		mediaPlayer.VideoLayoutMapping = RenderHeads.Media.AVProVideo.VideoMapping.EquiRectangular180;
		applyToMesh.ForceUpdate ();
	}
}
