using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRUdon.VR
{
    public abstract class Alert : MonoBehaviour
    {
        public void Hide()
        {
            Destroy(gameObject);
        }
    }
}
