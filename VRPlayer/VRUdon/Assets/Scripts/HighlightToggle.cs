using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighlightToggle : Toggle
{
    public float _timeCountdown = 1;

    private bool _PointerEnter;

    private float _timeStart;

    private float _timeCount;

    private float _percent;

    public string eventStrinsg = "";

    public Graphic _text;

    public bool _loopable = false;

    /// <summary>
    /// sprite when active
    /// </summary>
    public Sprite _onSprite;

    /// <summary>
    /// sprite when inactive
    /// </summary>
    public Sprite _offSprite;

    public string _message;

    public string _messageOff;

    // Start is called before the first frame update
    void Start()
    {
        isOn = false;
        onValueChanged.AddListener(delegate {
            ValueChanged();
        });
    }
    public void ValueChanged()
    {
        // Change sprite according to active state
        if (isOn)
        {
            if (_onSprite)
            {
                image.sprite = _onSprite;
            }

        }
        else
        {
            if (_offSprite)
            {
                MessageDispatcher.SendMessageData(_messageOff, null);
                image.sprite = _offSprite;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_PointerEnter)
        {

            _timeCount += Time.deltaTime;

            _percent = ((_timeCount * 100) / _timeStart);

            if (_percent >= 100)
            {

                if (_loopable)
                {
                    _timeCount = 0;
                }
                else
                {
                    OnExit();
                }

                if (!string.IsNullOrWhiteSpace(_message))
                {
                    MessageDispatcher.SendMessageData(_message, null);
                }

                ExecuteEvents.Execute(gameObject, _pointerEvt, ExecuteEvents.pointerClickHandler);

            }

        }
    }

    public void OnEnter()
    {
        _PointerEnter = true;
        Reset();
        StartCoroutine(Blinking());
    }

    public void OnExit()
    {
        StopAllCoroutines();
        _PointerEnter = false;
        Reset();
    }

    IEnumerator Blinking()
    {
        while (_PointerEnter)
        {

            if (targetGraphic != null)
            {

                colorStart = new Color(targetGraphic.color.r, targetGraphic.color.g, targetGraphic.color.b, 1);
                colorEnd = new Color(targetGraphic.color.r, targetGraphic.color.g, targetGraphic.color.b, 0);

                if (_text != null)
                {
                    textColorStart = new Color(_text.color.r, _text.color.g, _text.color.b, 1);
                    textColorEnd = new Color(_text.color.r, _text.color.g, _text.color.b, 0);
                }

                Off();
                yield return new WaitForSeconds(.2f);
                On();
                yield return new WaitForSeconds(.2f);

                Off();
                yield return new WaitForSeconds(.1f);
                On();
                yield return new WaitForSeconds(.1f);
                Off();

                while (true)
                {
                    On();
                    yield return new WaitForSeconds(.05f);
                    Off();
                    yield return new WaitForSeconds(.05f);
                }

            }

            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    PointerEventData _pointerEvt;

    public override void OnPointerExit(PointerEventData pointerEventData)
    {
        base.OnPointerExit(pointerEventData);
        OnExit();
    }

    public override void OnPointerEnter(PointerEventData pointerEventData)
    {
        base.OnPointerEnter(pointerEventData);
        _pointerEvt = pointerEventData;
        OnEnter();
    }

    Color colorStart;
    Color colorEnd;

    Color textColorStart;
    Color textColorEnd;

    void On()
    {
        if (targetGraphic != null)
            targetGraphic.color = colorStart;

        if (_text != null)
            _text.color = textColorStart;
    }

    void Off()
    {
        if (targetGraphic != null)
            targetGraphic.color = colorEnd;

        if (_text != null)
            _text.color = textColorEnd;
    }

    void Reset()
    {
        _timeStart = _timeCountdown;
        _timeCount = 0;

        if (targetGraphic != null)
            targetGraphic.color = new Color(targetGraphic.color.r, targetGraphic.color.g, targetGraphic.color.b, 1);

        if (_text != null)
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 1);
    }
}
