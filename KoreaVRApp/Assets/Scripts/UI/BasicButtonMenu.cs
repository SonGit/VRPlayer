using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BasicButtonMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler 
{
    public event Action OnClick;

    protected virtual void Start()
    {
      
    }

    private void Update()
    {
      
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_WEBGL
      
#endif
    }

    public void OnPointerExit(PointerEventData eventData)
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_WEBGL
        
#endif
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickAsync();
    }

	private void OnClickAsync()
    {
        print("Click: " + gameObject.name);
        if (OnClick != null) { OnClick(); }
    }

    private void OnEnable()
    {
       
    }
}