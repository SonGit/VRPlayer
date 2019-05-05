using System.Collections;
using PullToRefresh;
using UnityEngine;
using UnityEngine.EventSystems;

public class RefreshLoading : MonoBehaviour, IDropHandler
{
    private UIRefreshControl m_UIRefreshControl;
	private BasicMenu basicMenu;
    private void Start()
    {
		m_UIRefreshControl = this.GetComponent<UIRefreshControl> ();

		basicMenu = this.GetComponentInParent<BasicMenu> ();
		print ("basic " + basicMenu);

        // Register callback
        // This registration is possible even from Inspector.
        m_UIRefreshControl.OnRefresh.AddListener(RefreshLoadingUI);
    }

	#region RefreshLoadingUI
    private void RefreshLoadingUI()
    {
		EndLoadingUI();
    }

	private void EndLoadingUI()
    {
        // Instead of data acquisition.

        // Call EndRefreshing() when refresh is over.
		if (m_UIRefreshControl != null) {
			m_UIRefreshControl.EndRefreshing ();
		} else {
			Debug.LogError ("m_UIRefreshControl null");
		}
    }
	#endregion

	#region RefreshData
	private void RefreshData()
	{
		basicMenu.Refresh ();
		print ("REFRESH");
	}

	/// <summary>
	/// Hander on scroller drop pull
	/// </summary>
	public void OnDrop (PointerEventData eventData) {
		bool isPulled = m_UIRefreshControl.IsPulled;
		if (isPulled){
			RefreshData ();
		}
	}
	#endregion
}
