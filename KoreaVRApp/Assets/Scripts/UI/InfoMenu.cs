using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoMenu : BasicMenuNavigation
{
	[SerializeField] private InfoButton[] _infoButtons;

	private bool isShowInfoMenu;

	public InfoButton[] infoButtons
	{
		get{ return _infoButtons; }
		set { _infoButtons = value; }
	}

	protected override void Awake ()
	{

	}

    // Start is called before the first frame update
	protected override void Start ()
	{
		base.Start ();

		_infoButtons = this.GetComponentsInChildren<InfoButton> ();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public bool IsShowInfoMenu
	{
		get { return isShowInfoMenu; }
		set { isShowInfoMenu = value; }
	}

	public override void Init ()
	{
		foreach (InfoButton infoButton in infoButtons) {
			infoButton.Init ();
		}
	}
}
