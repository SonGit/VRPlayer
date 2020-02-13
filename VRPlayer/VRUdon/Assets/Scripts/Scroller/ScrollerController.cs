using com.ootii.Messages;
using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRUdon.VR;

public class ScrollerController : MonoBehaviour, IEnhancedScrollerDelegate
{
    /// <summary>
    /// Internal representation of our data. Note that the scroller will never see
    /// this, so it separates the data from the layout using MVC principles.
    /// </summary>
    private List<Video> _data;

    private List<Video> _allVideos;

    /// <summary>
    /// This is our scroller we will be a delegate for
    /// </summary>
    public EnhancedScroller scroller;

    /// <summary>
    /// This will be the prefab of each cell in our scroller. The cell view will
    /// hold references to each row sub cell
    /// </summary>
    public EnhancedScrollerCellView cellViewPrefab;

    public int numberOfCellsPerRow = 4;

    void Awake()
    {
        MessageDispatcher.AddListener(GameEvent.sendVideos, Receive);
        MessageDispatcher.AddListener(GameEvent.goToPage, GetDataAtPage);
    }

    /// <summary>
    /// Upon receive videos to render
    /// </summary>
    /// <param name="rMessage"></param>
    void Receive(IMessage rMessage)
    {
        // set up the videos
        _allVideos = ((List<Video>)rMessage.Data);

        // Get The first 8 videos to render
        _data = _allVideos.GetRange(0,8);

        // load in a large set of data
        LoadData();

        // Inform the page controller to setup page numbers UI
        MessageDispatcher.SendMessageData(GameEvent.setPageNumber, rMessage.Data);
    }

    /// <summary>
    /// For the VR pagination. Read PageController.cs
    /// each page contains 8 videos
    /// </summary>
    /// <param name="rMessage"></param>
    void GetDataAtPage(IMessage rMessage)
    {
        // Get the page number from event
        int page = (int)rMessage.Data;

        // Start index of data to get
        int currentPageMinIndex = page * 8;

        // Max index of data to get 
        int currentPageMaxIndex = currentPageMinIndex + 8; // 8 because each page contains 8 videos

        // If the page index range is within the videos length, then take out 8 videos of that range
        if (_allVideos.Count > currentPageMaxIndex)
        {
            _data = _allVideos.GetRange(page * 8, 8);
        }
        else
        {
            // If not, only take out the remainder 
            // this is to prevent out of index array exception
            _data = _allVideos.GetRange(page * 8, _allVideos.Count - currentPageMinIndex);
        }

        // load in a large set of data
        LoadData();

        // Inform the page controller to setup page numbers UI
        MessageDispatcher.SendMessageData(GameEvent.setPageNumber, _allVideos);
    }

    /// <summary>
    /// Be sure to set up your references to the scroller after the Awake function. The 
    /// scroller does some internal configuration in its own Awake function. If you need to
    /// do this in the Awake function, you can set up the script order through the Unity editor.
    /// In this case, be sure to set the EnhancedScroller's script before your delegate.
    /// 
    /// In this example, we are calling our initializations in the delegate's Start function,
    /// but it could have been done later, perhaps in the Update function.
    /// </summary>
    void Start()
    {
        // tell the scroller that this script will be its delegate
        scroller.Delegate = this;
    }

    /// <summary>
    /// Populates the data with a lot of records
    /// </summary>
    private void LoadData()
    {
        // tell the scroller to reload now that we have the data
        scroller.ReloadData();
    }

    #region EnhancedScroller Handlers

    /// <summary>
    /// This tells the scroller the number of cells that should have room allocated.
    /// For this example, the count is the number of data elements divided by the number of cells per row (rounded up using Mathf.CeilToInt)
    /// </summary>
    /// <param name="scroller">The scroller that is requesting the data size</param>
    /// <returns>The number of cells</returns>
    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        if(_data == null)
        {
            return 0;
        }

        return Mathf.CeilToInt((float)_data.Count / (float)numberOfCellsPerRow);
    }

    /// <summary>
    /// This tells the scroller what the size of a given cell will be. Cells can be any size and do not have
    /// to be uniform. For vertical scrollers the cell size will be the height. For horizontal scrollers the
    /// cell size will be the width.
    /// </summary>
    /// <param name="scroller">The scroller requesting the cell size</param>
    /// <param name="dataIndex">The index of the data that the scroller is requesting</param>
    /// <returns>The size of the cell</returns>
    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 100f;
    }

    /// <summary>
    /// Gets the cell to be displayed. You can have numerous cell types, allowing variety in your list.
    /// Some examples of this would be headers, footers, and other grouping cells.
    /// </summary>
    /// <param name="scroller">The scroller requesting the cell</param>
    /// <param name="dataIndex">The index of the data that the scroller is requesting</param>
    /// <param name="cellIndex">The index of the list. This will likely be different from the dataIndex if the scroller is looping</param>
    /// <returns>The cell for the scroller to use</returns>
    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        // first, we get a cell from the scroller by passing a prefab.
        // if the scroller finds one it can recycle it will do so, otherwise
        // it will create a new cell.
        CellView cellView = scroller.GetCellView(cellViewPrefab) as CellView;

        cellView.name = "Cell " + (dataIndex * numberOfCellsPerRow).ToString() + " to " + ((dataIndex * numberOfCellsPerRow) + numberOfCellsPerRow - 1).ToString();

        // pass in a reference to our data set with the offset for this cell
        cellView.SetData(ref _data, dataIndex * numberOfCellsPerRow);

        // return the cell to the scroller
        return cellView;
    }

    #endregion
}
