using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoButton : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] private GameObject contentObj;
	[SerializeField] private GameObject downObj;
	[SerializeField] private GameObject upObj;

	private InfoMenu infoMenu;

    // Start is called before the first frame update
    void Start()
    {
		Init ();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Init(){
		HideContent ();

		ShowDownObj ();

		HideUpObj ();
	}

	public void OnClickBtn(){

		if(MainAllController.instance != null){
			MainAllController.instance.PlayButtonSound ();
		}

		if (infoMenu == null){
			infoMenu = UnityEngine.Object.FindObjectOfType<InfoMenu> ();
		}

		foreach (InfoButton infoButton in infoMenu.infoButtons) {
			if (infoButton.contentObj == this.contentObj) {
				if (!contentObj.activeSelf) {
					ShowContent ();
					HideDownObj ();
					ShowUpObj ();
				} else {
					HideContent ();
					ShowDownObj ();
					HideUpObj ();
				}
			} else {
				infoButton.Init ();
			}
		}


	}

	private void ShowContent(){
		if (contentObj != null) {
			contentObj.SetActive (true);
		}
	}

	private void HideContent(){
		if (contentObj != null) {
			contentObj.SetActive (false);
		}
	}


	private void ShowDownObj(){
		if (downObj != null) {
			downObj.SetActive (true);
		}
	}

	private void HideDownObj(){
		if (downObj != null) {
			downObj.SetActive (false);
		}
	}

	private void ShowUpObj(){
		if (upObj != null) {
			upObj.SetActive (true);
		}
	}

	private void HideUpObj(){
		if (upObj != null) {
			upObj.SetActive (false);
		}
	}

}
