using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RenderHeads.Media.AVProVideo;

public class SubTitle2D : MonoBehaviour
{
	[SerializeField]
	private GameObject _subtitle2DUIPrefab = null;

	[SerializeField]
	private DropDownSubtitle2D dropDownSubtitle2D = null;

	public List<SubtitleUI2D> subtitleUIList = new List<SubtitleUI2D> ();

	private SubtitlesUGUI[] subtitlesUGUIs;

	private Subtitle none = new Subtitle{
		language = "NONE",
		subtitle_link = "",
	};

	// Start is called before the first frame update
	void Start()
	{
		subtitlesUGUIs = Object.FindObjectsOfType<SubtitlesUGUI> ();

	}



	public void Setup(Subtitle[] subtitles, Video video)
	{
		// None Prefabs

		//string pathNone = "Prefabs/SubLanguage" + "/" + "NONE";
		//GameObject objNone = LoadObject (pathNone);

		GameObject noneObj = (GameObject)Instantiate (_subtitle2DUIPrefab);
		SubtitleUI2D subtitleUI2DNone = noneObj.GetComponent<SubtitleUI2D> ();

		if (subtitleUI2DNone != null){
			subtitleUIList.Add (subtitleUI2DNone);
			subtitleUI2DNone.Setup(none, video);
			noneObj.transform.SetParent (this.transform, false);
		}
		// None Prefabs

		// Language Prefabs
		for (int i = 0; i < subtitles.Length; i++) {

			//string path = "Prefabs/SubLanguage" + "/" + subtitles[i].language;
			//GameObject objLanguage = LoadObject (path);

			GameObject go = (GameObject)Instantiate (_subtitle2DUIPrefab);
			SubtitleUI2D subtitleUI2D = go.GetComponent<SubtitleUI2D> ();

			if (subtitleUI2D != null){
				subtitleUI2D.Setup(subtitles[i], video);
				subtitleUIList.Add (subtitleUI2D);
				go.transform.SetParent (this.transform, false);
			}

		}
		// Language Prefabs

		SetupDropdownOptions ();
	}

	public void RemoveAllSubtitleUI(){
		for (int i = 0; i < subtitleUIList.Count; i++) {
			Destroy (subtitleUIList[i].gameObject);
		}
		
		subtitleUIList = new List<SubtitleUI2D> ();

		InitDropdown ();
	}

	public void ClearTextSubTitle(){
		if (subtitlesUGUIs != null && subtitlesUGUIs.Length > 0){
			for (int i = 0; i < subtitlesUGUIs.Length; i++) {
				subtitlesUGUIs[i]._text.text = string.Empty;
				Debug.Log ("Clear all text");
			}
		}
	}

	// Load gameObject from resources
	public GameObject LoadObject(string path)
	{
		if (path != null) {
			return (Resources.Load (path, typeof(GameObject))) as GameObject;
		} else {
			print ("NULL");
			return null;
		}

	}

	#region Dropdown
	private void SetupDropdownOptions(){

		if (dropDownSubtitle2D != null){
			dropDownSubtitle2D.ClearOptions ();
		}
			
		List<Dropdown.OptionData> flagItems = new List<Dropdown.OptionData> ();

		foreach (var subtitleUI in subtitleUIList) {
			string flagName = subtitleUI.name;
			int dot = subtitleUI.name.IndexOf ('.');
			if (dot >= 0){
				flagName = flagName.Substring (dot + 1);
			}
			var flagOption = new Dropdown.OptionData (flagName); 
			flagItems.Add (flagOption);
		}

		if (dropDownSubtitle2D != null) {
			dropDownSubtitle2D.AddOptions (flagItems);
		}
	}

	private void InitDropdown(){
		if (dropDownSubtitle2D != null){
			dropDownSubtitle2D.ClearOptions ();
		}
	}

	public void SetViewrDropdown(bool value){
		if (dropDownSubtitle2D != null){
			dropDownSubtitle2D.gameObject.SetActive (value);
		}
	}
	#endregion
}
