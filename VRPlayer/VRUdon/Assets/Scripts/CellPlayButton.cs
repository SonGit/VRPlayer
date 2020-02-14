using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VRUdon.VR
{
    public class CellPlayButton : MonoBehaviour
    {
        public bool isCursorEnter = false;

        public Button playButton;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            playButton.gameObject.SetActive(isCursorEnter);
        }

        public void OnCursorEnter()
        {
            isCursorEnter = true;
        }

        public void OnCursorExit()
        {
            isCursorEnter = false;
        }
    }
}
