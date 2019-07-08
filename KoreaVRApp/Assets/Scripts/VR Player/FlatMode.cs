using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatMode : VRMode
{
	[SerializeField]
	private Camera skyboxCamera;

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

	public override void Show()
	{
		base.Show ();
		mediaPlayer.VideoLayoutMapping = RenderHeads.Media.AVProVideo.VideoMapping.Unknown;
		applyToMesh.ForceUpdate ();

		if (VRPlayer.instance.aspectRatio == AspectRatio.RATIO_2351) {
			VRPlayer.instance.aspectRatio = AspectRatio.ORIGINAL;
		}

		ResumeRatio ();
	}

	public override void Hide()
	{
		base.Hide ();
	}
		
}
