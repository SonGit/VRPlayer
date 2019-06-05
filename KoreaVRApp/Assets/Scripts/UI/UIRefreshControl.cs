﻿using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PullToRefresh
{
    /*
    MIT License

    Copyright (c) 2018 kiepng

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
    */

    public class UIRefreshControl : MonoBehaviour
    {
        [Serializable] public class RefreshControlEvent : UnityEvent {}

        [SerializeField] private float m_PullDistanceRequiredRefresh;
		[SerializeField] private Image m_PullLoading;
        [SerializeField] private float m_LoadingSpeed = 200f;
        [SerializeField] RefreshControlEvent m_OnRefresh = new RefreshControlEvent();

		private ScrollRect m_ScrollRect;
        private float m_InitialPosition;
        private float m_Progress;
        private bool m_IsPulled;
        private bool m_IsRefreshing;
        private Vector2 m_PositionStop;
        private IScrollable m_ScrollView;
		private Color color;

        /// <summary>
        /// Progress until refreshing begins. (0-1)
        /// </summary>
        public float Progress
        {
            get { return m_Progress; }
        }

        /// <summary>
        /// Refreshing?
        /// </summary>
        public bool IsRefreshing
        {
            get { return m_IsRefreshing; }
        }

		/// <summary>
		/// Pulled?
		/// </summary>
		public bool IsPulled
		{
			get { return m_IsPulled; }
		}

        /// <summary>
        /// Callback executed when pulled more than required distance.
        /// </summary>
        public RefreshControlEvent OnRefresh
        {
            get { return m_OnRefresh; }
            set { m_OnRefresh = value; }
        }

        /// <summary>
        /// Call When Refresh is End.
        /// </summary>
        public void EndRefreshing()
        {
            m_IsPulled = false;
            m_IsRefreshing = false;
        }

        private void Start()
        {
			m_ScrollRect = this.GetComponent<ScrollRect> ();
			m_ScrollRect.verticalNormalizedPosition = 1;
			m_InitialPosition = 0;
            m_PositionStop = new Vector2(m_ScrollRect.content.anchoredPosition.x, m_InitialPosition - m_PullDistanceRequiredRefresh);
            m_ScrollView = m_ScrollRect.GetComponent<IScrollable>();
            m_ScrollRect.onValueChanged.AddListener(OnScroll);

			GetColor_alpha (color, m_PullLoading, 0f);
        }

        private void LateUpdate()
        {
            if (!m_IsPulled)
            {
                return;
            }

            

//            if (!m_IsRefreshing)
//            {
//                return;
//            }
//
//            m_ScrollRect.content.anchoredPosition = m_PositionStop;
        }

        private void OnScroll(Vector2 normalizedPosition)
        {

			var distance = m_InitialPosition - GetContentAnchoredPosition();

            if (distance < 0.4f)
            {
				LoadingUI (distance,0.0f,0);
                return;
            }

            OnPull(distance);
        }

        private void OnPull(float distance)
        {
			if (Math.Abs (distance) > 30f) {
				LoadingUI (distance,0.5f,0.0062f);
			} else {
				//m_PullLoading.transform.localEulerAngles = new Vector3 (0,180,0);
				LoadingUI (distance,0.0f,0);
			}

				
            if (m_IsRefreshing && Math.Abs(distance) < 1f)
            {
                m_IsRefreshing = false;
            }

            if (m_IsPulled && m_ScrollView.Dragging)
            {
				if (Math.Abs (distance) > m_PullDistanceRequiredRefresh) {
					LoadingUI (distance,1.0f,0.0062f);
				}else{
					m_IsPulled = false;
				}

                return;
            }

            if (m_IsPulled && !m_ScrollView.Dragging)
            {
                m_IsRefreshing = true;
                m_OnRefresh.Invoke();
            }

            m_Progress = distance / m_PullDistanceRequiredRefresh;

            if (m_Progress < 1f)
            {
                return;
            }

            m_Progress = 0f;
            m_IsPulled = true;
        }

        private float GetContentAnchoredPosition()
        {
            if (m_ScrollRect.vertical && !m_ScrollRect.horizontal)
            {
                return m_ScrollRect.content.anchoredPosition.y;
            }

            Debug.LogError("PullToRefresh works vertical scroll.");
            return -1f;
        }
			
		private void LoadingUI(float distance, float alpha, float fillAmount){
			m_PullLoading.fillAmount = distance * fillAmount;
			GetColor_alpha (color, m_PullLoading, alpha);
			//m_PullLoading.transform.Rotate (Vector3.forward * Time.deltaTime * distance * 0.6f);
		}

		private void GetColor_alpha(Color color, Image image, float alpha){
			color = image.color;
			color.a = alpha;
			image.color = color;
		}
    }
}
