using System;
using UnityEngine;
using UnityEngine.UI;

public class BasicMenuNavigation : BasicMenu
{
	[SerializeField] private Button btnOnBack;
	[SerializeField] private Button btnOnVR;

    public event Action OnBack;
	public event Action OnVR;

    protected override void Start()
    {
        base.Start();

		if (btnOnBack != null){
			btnOnBack.onClick.AddListener(() =>
				{
					if(MainAllController.instance != null){
						MainAllController.instance.PlayButtonSound ();
					}

					if (OnBack != null)
					{
						OnBack();
					}
				});
		}
			
		if (btnOnVR != null){
			btnOnVR.onClick.AddListener(() =>
				{
					if(MainAllController.instance != null){
						MainAllController.instance.PlayButtonSound ();
					}

					if (OnVR != null)
					{
						OnVR();
					}
				});
		}

    }
}