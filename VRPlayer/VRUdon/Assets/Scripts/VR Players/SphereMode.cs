using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VRUdon.VR
{
    public class SphereMode : VRMode
    {
        [SerializeField]
        private ApplyToMesh applyToMesh;

        // Start is called before the first frame update
        void Awake()
        {
            base.Awake();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void PackingTopBottom()
        {
            base.PackingTopBottom();
            UpdateMesh();
        }

        public override void PackingLeftRight()
        {
            base.PackingLeftRight();
            UpdateMesh();
        }

        public override void PackingNone()
        {
            base.PackingNone();
            UpdateMesh();
        }

        void UpdateMesh()
        {
            applyToMesh.ForceUpdate();
        }
    }

}
