using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VRUdon.VR
{
    public class Brightness : MonoBehaviour
    {
        public RawImage filter;

        Color currentColor;

        void Awake()
        {
            MessageDispatcher.AddListener(GameEvent.setBrightness, SetBrightness);
        }

        public void SetBrightness(IMessage rMessage)
        {
            if (filter != null)
            {
                currentColor = filter.color;
                currentColor.a = Mathf.Abs(1 - (float)rMessage.Data);
                filter.color = currentColor;
            }
        }
    }

}
