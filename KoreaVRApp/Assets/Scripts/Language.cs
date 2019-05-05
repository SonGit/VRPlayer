using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Language : MonoBehaviour
{
	
	public TextMeshProUGUI MyStorageLabel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown (KeyCode.A)) {
			English ();
		}

		if (Input.GetKeyDown (KeyCode.B)) {
			Korean ();
		}
    }

	void English()
	{
		MyStorageLabel.text = " My Storage";
	}

	void Korean()
	{
		MyStorageLabel.text = " Korean";
	}
}
