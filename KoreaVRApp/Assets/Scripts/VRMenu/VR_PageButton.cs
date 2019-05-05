using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_PageButton : MonoBehaviour
{
	[SerializeField]
	private Text label;

	[SerializeField]
	public int pageNo;

	[SerializeField]
	private Button button;

	[SerializeField]
	private Image selectImg;

	[SerializeField]
	private Image unselectImg;

    // Start is called before the first frame update
    void Awake()
    {
		label = this.GetComponentInChildren<Text> ();
		button = this.GetComponent<Button> ();
    }

	public void Setup(int pageNo)
	{
		this.pageNo = pageNo;
		label.text = this.pageNo + 1 + "";
		button.enabled = true;
	}

	public void OnClick()
	{
		VR_PageController pageController = this.GetComponentInParent<VR_PageController> ();

		if (pageController != null) {
			pageController.GoToPage (pageNo);
		}
	}

	public void Selected()
	{
		selectImg.enabled = true;
		unselectImg.enabled = false;

		VR_MainMenu.instance.GoToPage (pageNo);
	}
		
	public void Unselected()
	{
		selectImg.enabled = false;
		unselectImg.enabled = true;
	}

	public void Disable()
	{
		pageNo = -1;
		label.text = "";
		Unselected ();
		button.enabled = false;
	}
}
