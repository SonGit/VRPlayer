using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class VR_BasicButton : MonoBehaviour
{
    [SerializeField]
	protected Image Selected;
    [SerializeField]
    protected Image Unselect;
    [SerializeField]
    protected Text title;

	protected event Action clickBtn;

	public Button thisBtn;

	[SerializeField] 
	protected Color activeColor;
	[SerializeField] 
	protected Color inactiveColor;


	void Start(){
		
	}
	// Update is called once per frame
	void Update()
	{

	}

	public virtual void OnClickBtn(){
		
	}

	public virtual void OnActive(){
		if (Selected != null && Unselect != null) {
            thisBtn.targetGraphic = Selected;

            Selected.enabled = true;
			Unselect.enabled = false;
		} else {
			Debug.Log ("nulllllllllllllllllll");
		}

		if (title != null) {
			title.color = activeColor;
		}else {
			Debug.Log ("nulllllllllllllllllll");
		}
	}


	public virtual void OnInactive(){
		if (Selected != null && Unselect != null) {
            thisBtn.targetGraphic = Unselect;
            Selected.enabled = false;
			Unselect.enabled = true;
		}

		if (title != null) {
			title.color = inactiveColor;
		}
	}

	public virtual void Init (){
		thisBtn = GetComponent<Button> ();
		thisBtn.onClick.AddListener (OnClickBtn);
		title = GetComponentInChildren<Text> ();
		Image[] img = GetComponentsInChildren<Image> ();

		if (img.Length > 0) {
			foreach (Image im in img) {
				switch (im.name) {
				case "Selected":
					Selected = im;
					break;
				case "Unselect":
					Unselect = im;
					break;
				default:
					break;
				}
			}
		} else {
			Debug.LogError ("Selected or Unselect null");
		}
	}

	/// <summary>
	/// Sets the active.
	/// </summary>
	/// <param name="obj">Object.</param>
	/// <param name="value">If set to <c>true</c> value.</param>
	public void SetActive(bool value)
	{
		if (this.gameObject != null) {
			this.gameObject .SetActive(value);
		}
	}
}
