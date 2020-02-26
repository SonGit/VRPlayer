using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VRUdon.VR
{
    public class SizeSlider : MonoBehaviour
    {
        [SerializeField]
        private Slider sizeSlider;

        [SerializeField]
        private Text sizeAmountLabel;

        public void Increase()
        {
            sizeSlider.value += 0.05f;
        }

        public void Decrease()
        {
            sizeSlider.value -= 0.05f;
        }

        private void Update()
        {
            if(sizeAmountLabel)
            {
                sizeAmountLabel.text = sizeSlider.value.ToString();
            }
        }

        public void OnSliderChanged(float value)
        {
            if (sizeSlider)
            {
                MessageDispatcher.SendMessageData(GameEvent.setSize, value);
            }
        }
    }
}

