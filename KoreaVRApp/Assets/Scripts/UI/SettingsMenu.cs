using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleDiskUtils;

public class SettingsMenu : BasicMenuNavigation
{
	[Header("Components")]
	[SerializeField] private Button btnLogin;
	[SerializeField] private Button btnLogout;
	[SerializeField] private Text usernameText;
	[SerializeField] private Text useableCapacityLabel;

	public event Action OnLogin;
	public event Action OnLogout;

	[Header("KeepLogin")]
	[SerializeField] private Button btnOffKeepLoggin;
	[SerializeField] private Button btnOnKeepLoggin;
	private string isOnKeepLogin = "f";
	private string keyTrueKeepLogin = "t";
	private string keyFalseKeepLogin = "f";

	[Header("Notification")]
	[SerializeField] private Button btnOffNotification;
	[SerializeField] private Button btnOnNotification;
	private string isOnNotification = "t";
	private string keyTrueNotification = "t";
	private string keyFalseNotification = "f";

	[Header("Mobie Network")]
	[SerializeField] private Button btnOffMobieNetwork;
	[SerializeField] private Button btnOnMobieNetwork;
	private string isOnMobieNetwork = "t";
	private string keyTrueMobieNetwork = "t";
	private string keyFalseMobieNetwork = "f";

	protected override void Start ()
	{
		base.Start ();

		// Event click Button
		if (btnLogin != null){
			btnLogin.onClick.AddListener(() =>
				{
					if(MainAllController.instance != null){
						MainAllController.instance.PlayButtonSound ();
					}

					if (OnLogin != null)
					{
						OnLogin();
					}
				});
		}

		if (btnLogout != null){
			btnLogout.onClick.AddListener(() =>
				{
					if(MainAllController.instance != null){
						MainAllController.instance.PlayButtonSound ();
					}

					if (OnLogout != null)
					{
						OnLogout();
					}
				});
		}

		if (btnOffKeepLoggin != null){
			btnOffKeepLoggin.onClick.AddListener(() =>
				{
					if(MainAllController.instance != null){
						MainAllController.instance.PlayButtonSound ();
					}
				});
		}

		if (btnOnKeepLoggin != null){
			btnOnKeepLoggin.onClick.AddListener(() =>
				{
					if(MainAllController.instance != null){
						MainAllController.instance.PlayButtonSound ();
					}
				});
		}

		if (btnOffNotification != null){
			btnOffNotification.onClick.AddListener(() =>
				{
					if(MainAllController.instance != null){
						MainAllController.instance.PlayButtonSound ();
					}
				});
		}

		if (btnOnNotification != null){
			btnOnNotification.onClick.AddListener(() =>
				{
					if(MainAllController.instance != null){
						MainAllController.instance.PlayButtonSound ();
					}
				});
		}

		if (btnOffMobieNetwork != null){
			btnOffMobieNetwork.onClick.AddListener(() =>
				{
					if(MainAllController.instance != null){
						MainAllController.instance.PlayButtonSound ();
					}
				});
		}

		if (btnOnMobieNetwork != null){
			btnOnMobieNetwork.onClick.AddListener(() =>
				{
					if(MainAllController.instance != null){
						MainAllController.instance.PlayButtonSound ();
					}
				});
		}
		// Event click Button
		OnLogout +=Reset;
	}

	void Reset()
	{
		usernameText.text = "";
	}

	public void InitViewable(){

		//Notification
		if (isOnNotification == keyTrueNotification) {
			NotificationViewable (true);
		} else {
			NotificationViewable (false);
		}
		//Notification

		//Mobie network
		if (isOnMobieNetwork == keyTrueMobieNetwork) {
			EnableMobieNetwork (true);
		} else {
			DisableMobieNetwork (false);
		}
		//Mobie network
		
		// HasUserLoggedIn ???
		if (MainAllController.instance != null && !MainAllController.instance.HasUserLoggedIn ()) {
			// Viewable button
			LogoutViewable (false);
			LoginViewable (true);
			KeepLogginViewable (false);
			// Viewable button

			//Interactable Button
			OffKeepLogginInteractable(false);
			//Interactable Button

			Setusername ("");
		}else{
			LogoutViewable(true);
			LoginViewable(false);

			//Interactable Button
			OffKeepLogginInteractable(true);
			//Interactable Button

			if (isOnKeepLogin == keyTrueKeepLogin) {
				KeepLogginViewable (true);
			} else {
				KeepLogginViewable (false);
			}

			Setusername (MainAllController.instance.GetUserNameInput());
		}
	}

	#region Viewable Button

	public void LogoutViewable(bool visible)
	{
		if (btnLogout != null) {
			btnLogout.gameObject.SetActive (visible);
		}
		else {
			Debug.LogError ("btnLogout null!");
		}
	}

	public void LoginViewable(bool visible)
	{
		if (btnLogin != null) {
			btnLogin.gameObject.SetActive (visible);
		}
		else {
			Debug.LogError ("btnLogout null!");
		}
	}

	public void KeepLogginViewable(bool visible)
	{
		if (visible) {
			if (btnOnKeepLoggin != null && btnOffKeepLoggin != null) {
				btnOnKeepLoggin.gameObject.SetActive (visible);
				btnOffKeepLoggin.gameObject.SetActive (!visible);
				isOnKeepLogin = keyTrueKeepLogin;
			} else {
				Debug.LogError ("btnKeepLoggin null!");
			}
		} else {
			if (btnOnKeepLoggin != null && btnOffKeepLoggin != null) {
				btnOnKeepLoggin.gameObject.SetActive (visible);
				btnOffKeepLoggin.gameObject.SetActive (!visible);
				isOnKeepLogin = keyFalseKeepLogin;
			}
			else {
				Debug.LogError ("btnKeepLoggin null!");
			}
		}
	}

	public void NotificationViewable(bool visible)
	{
		if (visible) {
			if (btnOnNotification != null && btnOffNotification != null) {
				btnOnNotification.gameObject.SetActive (visible);
				btnOffNotification.gameObject.SetActive (!visible);
				isOnNotification = keyTrueNotification;
			} else {
				Debug.LogError ("btnNotification null!");
			}
		} else {
			if (btnOnNotification != null && btnOffNotification != null) {
				btnOnNotification.gameObject.SetActive (visible);
				btnOffNotification.gameObject.SetActive (!visible);
				isOnNotification = keyFalseNotification;
			}
			else {
				Debug.LogError ("btnNotification null!");
			}
		}
	}

	bool mobieNetworkvisible;

	public void MobieNetworkViewable(bool visible)
	{
		mobieNetworkvisible = visible;
		if (mobieNetworkvisible) {
			EnableMobieNetwork (mobieNetworkvisible);
		} else {
			#if !UNITY_EDITOR
			AndroidDialog.instance.showLoginDialog ("Allow data network access Using your 3G / LTE connection may incur additional data charges", OnAlertMobieNetworkComplete);
			#endif

			#if UNITY_EDITOR
			DisableMobieNetwork (mobieNetworkvisible);
			#endif
		}
	}
	 
	private void OnAlertMobieNetworkComplete(){
		DisableMobieNetwork (mobieNetworkvisible);
	}

	private void DisableMobieNetwork(bool visible){
		if (btnOnMobieNetwork != null && btnOffMobieNetwork != null) {
			btnOnMobieNetwork.gameObject.SetActive (visible);
			btnOffMobieNetwork.gameObject.SetActive (!visible);
			isOnMobieNetwork = keyFalseMobieNetwork;
		}
		else {
			Debug.LogError ("btnMobieNetwork null!");
		}
	}

	private void EnableMobieNetwork(bool visible){
		if (btnOnMobieNetwork != null && btnOffMobieNetwork != null) {
			btnOnMobieNetwork.gameObject.SetActive (visible);
			btnOffMobieNetwork.gameObject.SetActive (!visible);
			isOnMobieNetwork = keyTrueMobieNetwork;
		} else {
			Debug.LogError ("btnMobieNetwork null!");
		}
	}

	#endregion


	#region Interactable Button

	public void OffKeepLogginInteractable(bool visible)
	{
		if (btnOffKeepLoggin != null) {
			btnOffKeepLoggin.interactable = visible;
		}
		else {
			Debug.LogError ("btnOffKeepLoggin null!");
		}
	}

	#endregion

	#region KeepLogin

	public string GetKeepLoginText()
	{
		return isOnKeepLogin;
	}

	public void SetKeepLoginText(string visible)
	{
		isOnKeepLogin = visible;
	}

	public string GetkeyTrueKeepLogin()
	{
		return keyTrueKeepLogin;
	}

	public string GetkeyFalseKeepLogin()
	{
		return keyFalseKeepLogin;
	}

	#endregion

	#region Notification
	public void SetNotificationText(string visible)
	{
		isOnNotification = visible;
	}

	public string GetNotificationText()
	{
		return isOnNotification;
	}

	public string GetkeyTrueNotification()
	{
		return keyTrueNotification;
	}

	public string GetkeyFalseNotification()
	{
		return keyFalseNotification;
	}

	#endregion

	#region MobieNetwork
	public void SetMobieNetworkText(string visible)
	{
		isOnMobieNetwork = visible;
	}

	public string GetMobieNetworkText()
	{
		return isOnMobieNetwork;
	}

	public string GetkeyTrueMobieNetwork()
	{
		return keyTrueMobieNetwork;
	}

	public string GetkeyFalseMobieNetwork()
	{
		return keyFalseMobieNetwork;
	}

	#endregion

	public string Getusername()
	{
		return usernameText.text;
	}

	public void Setusername(string username)
	{
		usernameText.text = username;
	}

	void OnEnable()
	{
		//int offfset = 651;
		if (useableCapacityLabel != null) 
		{
			int space = DiskUtils.CheckAvailableSpace ();

			if (space >= 1000) {
				float number = (float)space / 1000;
				useableCapacityLabel.text = number.ToString("F2") + " GB";
			} else {
				useableCapacityLabel.text = space + " MB";
			}
	
//			Debug.Log ("DiskUtils.CheckTotalSpace() " + DiskUtils.CheckTotalSpace());
//			Debug.Log ("DiskUtils.CheckBusySpace() " + DiskUtils.CheckBusySpace());
//			Debug.Log ("DiskUtils.CheckAvailableSpace() " + DiskUtils.CheckAvailableSpace());
		}
	}
}
