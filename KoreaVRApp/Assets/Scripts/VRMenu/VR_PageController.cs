using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_PageController : MonoBehaviour
{
	[SerializeField]
	private VR_PageButton[] pageBtns;

	void Awake()
	{
		pageBtns = this.GetComponentsInChildren<VR_PageButton> ();
	}

	int _currentTier = 0;

	public int currentTier
	{
		get
		{
			return _currentTier;
		}

		set {
			if (value >= 0) {
				_currentTier = value;
				Setup ();
			} else {
				Debug.Log ("Invalid Page Number");
			}

		}
	}

	void Update()
	{

	}

	public void FirstPage()
	{
		currentTier = 0;

		UnselectAll ();
		//pageBtns [0].Selected ();
		GoToPage (0);
	}

	public void FinalPage()
	{
		int pageMax = totalObjects / VR_BasicMenu.MAX_ITEM_PER_PAGE;
		currentTier = pageMax / 5;

		UnselectAll ();
	//	pageBtns [0].Selected ();
		GoToPage (pageMax);
	}

	public void NextPage()
	{
		int lowestPageNextTier = (currentTier + 1) * 5;
		int pageMax = totalObjects / VR_BasicMenu.MAX_ITEM_PER_PAGE;

		if (lowestPageNextTier <= pageMax) {
			currentTier++;
			UnselectAll ();
			pageBtns [0].Selected ();
            currentPage = pageBtns[0].pageNo;

        }
	}

	public void LastPage()
	{
		int highestPageNextTier = ((currentTier - 1) * 5) + 5;
		int pageMax = totalObjects / VR_BasicMenu.MAX_ITEM_PER_PAGE;

		if (highestPageNextTier == 0) {
			return;
		}

		if (highestPageNextTier <= pageMax) {
			currentTier--;
			UnselectAll ();
			pageBtns [pageBtns.Length - 1].Selected ();
            currentPage = pageBtns[pageBtns.Length - 1].pageNo;
        }

	}

	int totalObjects;

	public void Refresh(int totalObjects)
	{
		this.totalObjects = totalObjects;
		currentTier = 0;
		GoToPage (0);
	}

	public void RefreshLastPage(int totalObjects)
	{
		this.totalObjects = totalObjects;

		int pageMax = (totalObjects / VR_BasicMenu.MAX_ITEM_PER_PAGE);
  
		if (totalObjects % VR_BasicMenu.MAX_ITEM_PER_PAGE == 0) {
			pageMax -= 1;
		}

        Debug.Log("currentPage   " + currentPage + "  pageMax   " + pageMax + "   totalObjects  " + totalObjects);

        if(currentPage >= pageMax)
        {
            GoToPage(pageMax);
        }
        else
        {
            GoToPage(currentPage);
        }

	}

	public void Setup()
	{
		int pageMax = (totalObjects / VR_BasicMenu.MAX_ITEM_PER_PAGE);

		if (totalObjects % VR_BasicMenu.MAX_ITEM_PER_PAGE == 0) {
			pageMax -= 1;
		}

		int remainder = totalObjects - (pageMax * VR_BasicMenu.MAX_ITEM_PER_PAGE);

//		if (remainder > 0) {
//			pageMax++;
//		}

		for (int i = 0 ; i < pageBtns.Length; i++) {
			int pageNumber = i + (5 * currentTier);

			if (pageNumber > pageMax) {
				pageBtns [i].Disable ();
			} else {
				pageBtns [i].Setup (pageNumber);
			}

		}
	}

	int currentPage;
	public void GoToPage( int page )
	{
		if (page < 0) {
			page = 0;
		}
		if (page == 0) {
			currentTier = 0;
		} else {
			currentTier = page / 5;
		}

		foreach (VR_PageButton pageBtn in pageBtns) {
			if (pageBtn.pageNo == page) {
				pageBtn.Selected ();
			} else {
				pageBtn.Unselected ();
			}
		}
        currentPage = page;

    }

	void UnselectAll()
	{
		foreach (VR_PageButton pageBtn in pageBtns) {
				pageBtn.Unselected ();
		}
	}

	int closestNumber(int n, int m) 
	{ 
		// find the quotient 
		int q = n / m; 

		// 1st possible closest number 
		int n1 = m * q; 

		// 2nd possible closest number 
		int n2 = (n * m) > 0 ? (m * (q + 1)) : (m * (q - 1)); 

		// if true, then n1 is the required closest number 
		if (Mathf.Abs(n - n1) < Mathf.Abs(n - n2)) 
			return n1; 

		// else n2 is the required closest number     
		return n2;     
	} 
}
