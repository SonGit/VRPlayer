using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_SettingSize : MonoBehaviour
{

	[SerializeField] 
	private VRSlider _slider;

	[SerializeField] 
	private Text _amount;

	[SerializeField] 
	private Button minus;
	[SerializeField] 
	private Button plus;
	private VRPlayer _vrplayer;
    void Start()
    {
		Init ();
    }

    // Update is called once per frame
    void Update()
    {
		if (_vrplayer.currentMode is CinemaMode || _vrplayer.currentMode is FlatMode) {
			_slider.interactable = true;
			minus.interactable = true;
			plus.interactable = true;
		} else {
			_slider.interactable = false;
			minus.interactable = false;
			plus.interactable = false;
		}

		_amount.text = (_slider.value/10).ToString ("0.0");
    }

	void Init()
	{
		_slider = GetComponentInChildren<VRSlider> ();
		_amount = GetComponentInChildren<Text> ();
		_vrplayer = FindObjectOfType<VRPlayer> ();
	}

	public void OnMinusBtn()
	{
		_slider.value -= 0.1f;
	}

	public void OnPlusBtn()
	{
		_slider.value += 0.1f;
	}
}
