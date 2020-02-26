using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VRUdon.VR
{
    public class BrightnessSlider : MonoBehaviour
    {
        public Slider slider;

        // Start is called before the first frame update
        IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            // slider.value = (slider.maxValue + slider.minValue) / 2;
            slider.value = slider.maxValue;
            MessageDispatcher.SendMessageData(GameEvent.setBrightness, slider.value);
        }

        public void OnValueChanged(float d)
        {
            MessageDispatcher.SendMessageData(GameEvent.setBrightness, d);
        }

        public void Increase()
        {
            slider.value = slider.value + 0.1f;
        }

        public void Decrease()
        {
            slider.value = slider.value - 0.1f;
        }
    }
}

