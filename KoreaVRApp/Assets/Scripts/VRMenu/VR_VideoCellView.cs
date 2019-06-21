using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedUI.EnhancedScroller;
using EnhancedUI;
using System;

public class VR_VideoCellView : EnhancedScrollerCellView
{
	public VideoUI[] videoUIs;

	/// <summary>
	/// This function just takes the Demo data and displays it
	/// </summary>
	/// <param name="data"></param>
	public void SetData(List<Video> videos, int currentPage)
	{
		//Debug.Log ("videos.Count---------------:     " + videos.Count);

		int start = currentPage * VR_BasicMenu.MAX_ITEM_PER_PAGE;

		// loop through the sub cells to display their data (or disable them if they are outside the bounds of the data)
		for (int i = 0; i < videoUIs.Length; i++)
		{
			videoUIs[i].Setup(start + i < videos.Count ? videos[start + i] : null);
		}

	}
}
