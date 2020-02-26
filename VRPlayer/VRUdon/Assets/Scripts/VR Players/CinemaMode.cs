using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRUdon.VR
{
    public class CinemaMode : VRMode
    {
        public RectTransform avproUGUI;
        // Start is called before the first frame update
        void Awake()
        {
            base.Awake();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void Ratio_43(float scale)
        {
            avproUGUI.sizeDelta = new Vector2(1280 * scale, 962 * scale);
        }

        public override void Ratio_169(float scale)
        {
            avproUGUI.sizeDelta = new Vector2(1280 * scale, 768 * scale);
        }

        public override void Ratio_1851(float scale)
        {
            avproUGUI.sizeDelta = new Vector2(1280 * scale, 692 * scale);
        }

    }

}
