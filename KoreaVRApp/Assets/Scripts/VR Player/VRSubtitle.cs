using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRSubtitle : MonoBehaviour
{
	[SerializeField]
	private GameObject _subtitleUIPrefab = null;

	public List<VRSubtitleUI> subtitleUIs = new List<VRSubtitleUI> ();

    // Start is called before the first frame update
    void Start()
    {
        
    }

	public void Setup(Subtitle[] subtitles, Video video)
	{
		foreach (Subtitle subtitle in subtitles) {

			GameObject go = (GameObject)Instantiate (_subtitleUIPrefab);
			VRSubtitleUI subtitleUI_VR = go.GetComponent<VRSubtitleUI> ();

			if (subtitleUI_VR != null) {
				subtitleUI_VR.Setup (subtitle, video);
				subtitleUIs.Add (subtitleUI_VR);
				go.transform.SetParent (this.transform, false);
				go.transform.localScale = Vector3.one;
			}
		}
	}

	public void RemoveAllSubtitleUI(){
		for (int i = 0; i < subtitleUIs.Count; i++) {
			Destroy (subtitleUIs[i].gameObject);
		}

		subtitleUIs = new List<VRSubtitleUI> ();
	}

	public void UserSelect(VRSubtitleUI subtitleUI_VR)
	{
		for (int i = 0; i < subtitleUIs.Count; i++) {
			if (subtitleUIs [i] == subtitleUI_VR) {
				subtitleUIs [i].OnSelected ();
			} else {
				subtitleUIs [i].OnUnselected ();
			}
		}
	}
}
