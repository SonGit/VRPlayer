using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AppScene : MonoBehaviour
{
	[SerializeField]
	protected GameObject root;

    public GameObject Camera2D;
    public GameObject CameraVR;

    /// <summary>
    /// Show the menu
    /// </summary>
    /// <param name="lastMenu">Last menu user was at.</param>
    public virtual void Show (BasicMenu lastMenu = null)
	{
		root.gameObject.SetActive (true);
	}

	public virtual void Hide ()
	{
		root.gameObject.SetActive (false);
	}
}
