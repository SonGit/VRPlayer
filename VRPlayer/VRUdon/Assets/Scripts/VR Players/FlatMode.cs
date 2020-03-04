using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRUdon.VR
{
    public class FlatMode : VRMode
    {
        public RectTransform avproUGUI;

        [SerializeField]
        private GameObject cubemap;

        [SerializeField]
        private ApplyToMaterial applyToMaterial;

        // Start is called before the first frame update
        void Awake()
        {
            base.Awake();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public override void Show()
        {
            base.Show();
            cubemap.SetActive(false);

            avproUGUI.localPosition = new Vector3(-20, -200, -400);
        }

        public override void Hide()
        {
            base.Hide();
            cubemap.SetActive(true);

            avproUGUI.localPosition = new Vector3(10, -10, 0);
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
