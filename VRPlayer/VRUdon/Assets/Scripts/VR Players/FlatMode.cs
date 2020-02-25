using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRUdon.VR
{
    public class FlatMode : VRMode
    {
        public GameObject Cubemap;
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
            Cubemap.SetActive(false);
        }

        public override void Hide()
        {
            base.Hide();
            Cubemap.SetActive(true);
        }
    }
}
