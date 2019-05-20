using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VR_FlashButton : Button,IPointerEnterHandler
{
	
	public float _timeCountdown = 1;

	private bool _PointerEnter;

	private float _timeStart;

	private float _timeCount;

	private float _percent;

    public Graphic _text;

    public bool _loopable = false;

    // Start is called before the first frame update
    void Start()
    {
     
    }


    // Update is called once per frame
    void Update()
    {
		if (_PointerEnter) {

			_timeCount += Time.deltaTime;

			_percent = ((_timeCount * 100) / _timeStart);

			if (_percent >= 100) {
				
                if(_loopable)
                {
                    _timeCount = 0;
                }
                else
                {
                    OnExit();
                }


				ExecuteEvents.Execute(gameObject, _pointerEvt, ExecuteEvents.pointerClickHandler);

			}

		}
    }

	public void OnEnter()
	{
		_PointerEnter = true;
        Reset();
        StartCoroutine (Blinking());
	}

	public void OnExit()
	{
		StopAllCoroutines ();
		_PointerEnter = false;
        Reset();
    }



	IEnumerator Blinking()
	{
		while (_PointerEnter) {

			if (targetGraphic != null) {

				colorStart = new Color (targetGraphic.color.r,targetGraphic.color.g,targetGraphic.color.b,1);
				colorEnd = new Color (targetGraphic.color.r,targetGraphic.color.g,targetGraphic.color.b,0);

                if(_text != null)
                {
                    textColorStart = new Color(_text.color.r, _text.color.g, _text.color.b, 1);
                    textColorEnd = new Color(_text.color.r, _text.color.g, _text.color.b, 0);
                }

                Off();
				yield return new WaitForSeconds (.2f);
                On();
				yield return new WaitForSeconds (.2f);

                Off();
                yield return new WaitForSeconds (.1f);
                On();
                yield return new WaitForSeconds (.1f);
                Off();

                while (true) {
                    On();
                    yield return new WaitForSeconds (.05f);
                    Off();
                    yield return new WaitForSeconds (.05f);
				}

			}
	
			yield return new WaitForEndOfFrame ();
		}
		yield return null;
	}

	PointerEventData _pointerEvt;

	public override void OnPointerExit(PointerEventData pointerEventData)
	{
		base.OnPointerExit (pointerEventData);
		OnExit ();
	}

	public override void OnPointerEnter(PointerEventData pointerEventData)
	{
		base.OnPointerEnter (pointerEventData);
		_pointerEvt = pointerEventData;
		OnEnter ();
	}

    Color colorStart;
    Color colorEnd;

    Color textColorStart;
    Color textColorEnd;

    void On()
    {
        if(targetGraphic != null)
        targetGraphic.color = colorStart;

        if(_text != null)
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

        if(targetGraphic != null)
        targetGraphic.color = new Color(targetGraphic.color.r, targetGraphic.color.g, targetGraphic.color.b, 1);

        if (_text != null)
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 1);
    }
}
