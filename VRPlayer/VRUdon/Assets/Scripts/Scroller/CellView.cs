using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRUdon.VR
{
    public class CellView : EnhancedScrollerCellView
    {
        public RowCellView[] rowCellViews;
        /// <summary>
        /// This function just takes the Demo data and displays it
        /// </summary>
        /// <param name="data"></param>
        public void SetData(ref List<Video> data, int startingIndex)
        {
            // loop through the sub cells to display their data (or disable them if they are outside the bounds of the data)
            for (var i = 0; i < rowCellViews.Length; i++)
            {
                // if the sub cell is outside the bounds of the data, we pass null to the sub cell
                rowCellViews[i].SetData(startingIndex + i < data.Count ? data[startingIndex + i] : null);
            }
        }
    }
}

