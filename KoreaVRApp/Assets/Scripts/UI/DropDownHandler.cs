using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDownHandler : MonoBehaviour
{
	public BasicMenu basicMenu;
	//public ScrollListController scrollListController;
    // Start is called before the first frame update
    void Start()
    {
		basicMenu = this.GetComponentInParent<BasicMenu> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void DropDownInput (int input){
		if (input == 0) {
			if (basicMenu != null) {
				
				basicMenu.SortByDate ();

			}

			if(MainAllController.instance != null){
				MainAllController.instance.PlayButtonSound ();
			}
		}
		if (input == 1) {
			if (basicMenu != null) {
				
				basicMenu.SortByName ();

			}

			if(MainAllController.instance != null){
				MainAllController.instance.PlayButtonSound ();
			}
		}
		if (input == 2) {
			if (basicMenu != null) {
				
				basicMenu.SortBySize ();

			}

			if(MainAllController.instance != null){
				MainAllController.instance.PlayButtonSound ();
			}
		}
	
	}
}
