using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRUdon.VR;

namespace VRUdon.VR
{
    public class PageController : MonoBehaviour
    {
        // Amount of video to renderper page
        public static int videoPerPage = 8;

        // Maximum number of page a list of video can have
        public int totalPossiblePageNumber = 0;

        // Current page
        public int currentPage = 0;

        // References to page buttons
        public PageButton[] pageButtons;

        // Reference to video current rendering
        List<Video> videos;

        // Start is called before the first frame update
        void Awake()
        {
            MessageDispatcher.AddListener(GameEvent.setPageNumber, SetPageNumber);
            MessageDispatcher.AddListener(GameEvent.goToPage, OnReceivePageNumber);
        }

        /// <summary>
        /// On go to page, remember what page user is visiting
        /// </summary>
        /// <param name="rMessage"></param>
        void OnReceivePageNumber(IMessage rMessage)
        {
            currentPage = (int)rMessage.Data;
        }

        private void Start()
        {
            pageButtons = this.GetComponentsInChildren<PageButton>();
        }

        void SetPageNumber(IMessage rMessage)
        {
            // Remember the list of currently rendered videos 
            videos = ((List<Video>)rMessage.Data);

            // Start page 
            // Because pages are broken into segments of 5, ie: page 6 can have start page of 5, endpage of 10
            int startpage = (currentPage / 5 ) * 5;

            totalPossiblePageNumber = Mathf.CeilToInt( (videos.Count / (float)videoPerPage) );

            // Setting page number info on page buttons
            for (int i = 0; i < pageButtons.Length; i++)
            {
                // If startpage is within range, render page number
                if(startpage < totalPossiblePageNumber)
                {
                    pageButtons[i].SetData(startpage++);
                }else
                {
                    // if not, simply disable the button
                    pageButtons[i].NoData();
                }

                // If page is currenly on, activate its toggle
                if(pageButtons[i].page == currentPage)
                {
                    pageButtons[i].Activate();
                }
            }

        }

        /// <summary>
        /// Forward to next segment of videos
        /// Unity Event, call by button
        /// </summary>
        public void Forward()
        {
            // Current segment
            int currentSegment = (currentPage * videoPerPage) / 40;

            // Next segment
            int nextSegment = currentSegment + 1;

            // Index to start at
            int expectedIndex = (nextSegment * 40) + 1;

            // If index is within video count range, go to first page of segment
            if (videos.Count > expectedIndex)
            {
                currentPage = (expectedIndex / 8);

                MessageDispatcher.SendMessageData(GameEvent.goToPage, currentPage);
            }
        }
        /// <summary>
        /// Backward, similar to Forward
        /// </summary>
        public void Backward()
        {
            int currentSegment = (currentPage * videoPerPage) / 40;

            int nextSegment = currentSegment - 1;

            int expectedIndex = (nextSegment * 40) + 1;

            if(expectedIndex > 0)
            {
                currentPage = (expectedIndex / videoPerPage);
                MessageDispatcher.SendMessageData(GameEvent.goToPage, currentPage);
            }

        }

        /// <summary>
        /// Forward to last segment
        /// </summary>
        public void FastFoward()
        {
            // Current segment
            int highestSegment = (videos.Count / 40);

            // Index to start at
            int expectedIndex = (highestSegment * 40) + 1;

            // If index is within video count range, go to first page of segment
            if (videos.Count > expectedIndex)
            {
                currentPage = (expectedIndex / 8);

                MessageDispatcher.SendMessageData(GameEvent.goToPage, currentPage);
            }
        }

        /// <summary>
        /// Backward to last segment
        /// </summary>
        public void FastBackward()
        {
            currentPage = 0;
            MessageDispatcher.SendMessageData(GameEvent.goToPage, currentPage);
        }
    }

}
