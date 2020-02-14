using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRUdon.VR
{
    public enum AlertType
    {
        LOGIN_ALERT,
        DELETE_ALERT,
        NETWORK_ALERT,
        NO_VIDEO_ALERT,
        PURCHASE_ALERT,
        SENSOR_ALERT,
        STREAMING_ALERT,
        CAPACITY_ALERT
    }

    public class AlertController : MonoBehaviour
    {
        [SerializeField]
        private Alert[] alertPrefabs;

        [SerializeField]
        private Transform AlertRoot;

        // Start is called before the first frame update
        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

