using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UI_SlidePanel : MonoBehaviour
{
//	
//	private bool isShow;
//	private RectTransform rectTransform;
//	[SerializeField]
//	private GameObject touchArea;
//	public float time,begin,end;
//
//
//	public void ShowPanel(){
//		iTween.ValueTo(gameObject, 
//			iTween.Hash(
//				"from", rectTransform.anchoredPosition,
//				"to", new Vector2(end, rectTransform.anchoredPosition.y),
//				"time", time, 
//				"easetype", iTween.EaseType.linear,
//				"islocal",true,
//				"onupdatetarget", this.gameObject, 
//				"onupdate", "MovePos"));
//
//		touchArea.SetActive (true);
//	}
//
//	public void ClosePanel(){
//		iTween.ValueTo(gameObject, 
//			iTween.Hash(
//				"from", rectTransform.anchoredPosition,
//				"to", new Vector2(begin, rectTransform.anchoredPosition.y),
//				"time", time, 
//				"easetype", iTween.EaseType.linear,
//				"islocal",true,
//				"onupdatetarget", this.gameObject, 
//				"onupdate", "MovePos"));
//
//		touchArea.SetActive (false);
//	}
//	public void MovePos(Vector2 position){
//		rectTransform.anchoredPosition = position;
//	}
//
//    // Start is called before the first frame update
//    void Start()
//    {
//		rectTransform = this.GetComponent<RectTransform> ();
//    }
//
//    // Update is called once per frame
//    void Update()
//    {
//        
//    }
}
