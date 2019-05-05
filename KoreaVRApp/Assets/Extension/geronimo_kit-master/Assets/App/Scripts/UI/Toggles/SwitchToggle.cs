using UnityEngine;
using UnityEngine.UI;

public class SwitchToggle : MonoBehaviour
{
    public Image ImgBackground;
    public Image ImgOff;
    public Image ImgOn;

    private Toggle _tglSwitch;
    private Color _color;

    private void Start()
    {
        _tglSwitch = GetComponent<Toggle>();
        _color =  ImgBackground.color;

        if (_tglSwitch != null)
        {
            if (_tglSwitch.isOn)
            {
                ImgBackground.color = Color.green;
            }

            _tglSwitch.onValueChanged.AddListener((value) =>
            {
                if (value)
                {
                    ImgBackground.color = Color.green;
                    ImgOff.gameObject.SetActive(false);
                    ImgOn.gameObject.SetActive(true);
                }
                else
                {
                    ImgBackground.color = _color;
                    ImgOn.gameObject.SetActive(false);
                    ImgOff.gameObject.SetActive(true);
                }
            });
        }
    }
}