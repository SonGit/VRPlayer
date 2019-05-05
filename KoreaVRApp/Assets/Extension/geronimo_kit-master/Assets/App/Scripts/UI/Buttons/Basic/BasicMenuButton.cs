using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BasicMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler 
{
    public Image ImgLine;
    public Text TxtButton;

    private float _widthButton;
    private float _hightButton;
    private float _widthLine;
    private float _hightLine;

    private float _velocity = 0.0F;
    private float _timeAnim = 0.1F;
    private bool _enter = false;
    private bool _exit = false;

    public event Action OnClick;

    protected virtual void Start()
    {
        _widthButton = GetComponent<RectTransform>().rect.width;
        _hightButton = GetComponent<RectTransform>().rect.height;
        _widthLine = ImgLine.GetComponent<RectTransform>().rect.width;
        _hightLine = ImgLine.GetComponent<RectTransform>().rect.height;
    }

    private void Update()
    {
        if (_enter)
        {
            GoAnim();
        }

        if (_exit)
        {
            BackAnim();
        }
    }

    public void InitialState()
    {
        _enter = false;
        _exit = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_WEBGL
        _enter = true;
        _exit = false;
#endif
    }

    public void OnPointerExit(PointerEventData eventData)
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_WEBGL
        _enter = false;
        _exit = true;
#endif
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _enter = true;
        _exit = false;

        StartCoroutine(OnClickAsync());
    }

    private void GoAnim()
    {
        var targetLine = ImgLine.GetComponent<RectTransform>().rect.width;
        var width =  Mathf.SmoothDamp(targetLine, _widthButton, ref _velocity, _timeAnim);

        ImgLine.rectTransform.sizeDelta = new Vector2(width, _hightButton);
    }

    private void BackAnim()
    {
        var targetLine = ImgLine.GetComponent<RectTransform>().rect.width;
        var width = Mathf.SmoothDamp(targetLine, _widthLine, ref _velocity, _timeAnim);

        ImgLine.rectTransform.sizeDelta = new Vector2(width, _hightLine);
    }

    private IEnumerator OnClickAsync()
    {
        print("Click: " + gameObject.name);

        yield return new WaitForSeconds(_timeAnim + 0.12f);
        if (OnClick != null) { OnClick(); }
    }

    private void OnEnable()
    {
        _enter = false;
        _exit = true;
    }
}