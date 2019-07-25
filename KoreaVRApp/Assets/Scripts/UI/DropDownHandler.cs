using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortName
{
	public string name;

	public SortName (string newName){
		name = newName;
	}
}

public class DropDownHandler : MonoBehaviour
{
	public BasicMenu basicMenu;
	private DropDownSort2D dropDownSort2D;

	private List<SortName> sortNameLists = new List<SortName> ();
	//public ScrollListController scrollListController;
    // Start is called before the first frame update
    void Start()
    {
		basicMenu = this.GetComponentInParent<BasicMenu> ();
		dropDownSort2D = this.GetComponent<DropDownSort2D> ();
		LanguageViewable ();
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

	public void LanguageViewable(){

		switch (Application.systemLanguage) 
		{
		case SystemLanguage.English: //This checks if your computer's operating system is in the English language
			Debug.Log ("This system is in English............................................");

			SetupSortNameList_English ();
			break;
		case SystemLanguage.Korean: //Otherwise, if the system is Korean
			Debug.Log ("This system is in Korean.............................................");

			SetupSortNameList_Korean ();
			break;
		case SystemLanguage.Japanese: //Otherwise, if the system is Japanese
			Debug.Log ("This system is in Japanese...........................................");

			SetupSortNameList_Japanese ();
			break;
		case SystemLanguage.Chinese: //Otherwise, if the system is Chinese
			Debug.Log ("This system is in Chinese.............................................");

			SetupSortNameList_Chinese ();
			break;
		case SystemLanguage.ChineseSimplified: //Otherwise, if the system is Chinese
			Debug.Log ("This system is in Chinese.............................................");

			SetupSortNameList_Chinese ();
			break;
		case SystemLanguage.ChineseTraditional: //Otherwise, if the system is Chinese
			Debug.Log ("This system is in Chinese.............................................");

			SetupSortNameList_Chinese ();
			break;
		default:
			Debug.Log ("This system is in other language......................................");

			SetupSortNameList_English ();
			break;
		}

		SetupDropdownOptions ();
	}

	private void SetupSortNameList_English(){
		sortNameLists = new List<SortName> ();
		sortNameLists.Add(new SortName("Sort by Date"));
		sortNameLists.Add(new SortName("Sort by Name"));
		sortNameLists.Add(new SortName("Sort by Size"));
	}

	private void SetupSortNameList_Korean(){
		sortNameLists = new List<SortName> ();
		sortNameLists.Add(new SortName("날짜순 정렬"));
		sortNameLists.Add(new SortName("이름순 정렬"));
		sortNameLists.Add(new SortName("크기로 정렬"));
	}

	private void SetupSortNameList_Japanese(){
		sortNameLists = new List<SortName> ();
		sortNameLists.Add(new SortName("日付けで並び替え"));
		sortNameLists.Add(new SortName("名前順"));
		sortNameLists.Add(new SortName("サイズで並べ替え"));
	}

	private void SetupSortNameList_Chinese(){
		sortNameLists = new List<SortName> ();
		sortNameLists.Add(new SortName("按日期排序"));
		sortNameLists.Add(new SortName("按名稱分類"));
		sortNameLists.Add(new SortName("按大小排序"));
	}

	#region Dropdown
	private void SetupDropdownOptions(){

		if (dropDownSort2D != null){
			dropDownSort2D.ClearOptions ();
		}

		List<Dropdown.OptionData> flagItems = new List<Dropdown.OptionData> ();

		foreach (SortName sortName in sortNameLists) {
			string flagName = sortName.name;

			var flagOption = new Dropdown.OptionData (flagName); 
			flagItems.Add (flagOption);
		}

		if (dropDownSort2D != null) {
			dropDownSort2D.AddOptions (flagItems);
		}
	}
	#endregion
}
