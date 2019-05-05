using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDownHandler_SubTitle2D : MonoBehaviour
{
	private SubTitle2D subTitle2D;

    // Start is called before the first frame update
    void Start()
    {
		subTitle2D = this.GetComponentInParent<SubTitle2D> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void DropDownInput (int input){

		if (subTitle2D != null){
			for (int i = 0; i < subTitle2D.subtitleUIList.Count; i++) {
				if (input == i && input == 0){
					subTitle2D.subtitleUIList [i].DisableSubTitle ();
				}

				if (input == i && input > 0){
					subTitle2D.subtitleUIList [i].LoadSubTitle ();
				}
			}
		}
	}
}
