using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Newtonsoft.Json;
using EasyMobile;

public class LoginMenu : BasicMenuNavigation
{
	[Header("Components")]
	[SerializeField] private Button BtnLogin;
	[SerializeField] private Button BtnEnableAuto;
	[SerializeField] private Button BtnDisableAuto;

	public event Action OnLogin;
	public event Action OnSignUp;

	[SerializeField] private InputField login_UsernameField;
	[SerializeField] private InputField login_PasswordField;

	private bool isAutoLogin;

	protected override void Awake ()
	{
		
	}

	private void Update (){
		
	}

	protected override void Start()
	{
		base.Start();

		if (BtnLogin != null){
			BtnLogin.onClick.AddListener(() =>
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

		if (BtnEnableAuto != null){
			BtnEnableAuto.onClick.AddListener(() =>
				{
					if(MainAllController.instance != null){
						MainAllController.instance.PlayButtonSound ();
					}
				});
		}

		if (BtnDisableAuto != null){
			BtnDisableAuto.onClick.AddListener(() =>
				{
					if(MainAllController.instance != null){
						MainAllController.instance.PlayButtonSound ();
					}
				});
		}

		InitViewable ();
	}

	public void InitViewable(){
		EnableAutoViewable (true);
	}


	public string GetUsernameInput()
	{
		return login_UsernameField.text;
	}

	public void SetUsernameInput(string username)
	{
		login_UsernameField.text = username;
	}

	public string GetPasswordInput()
	{
		return login_PasswordField.text;
	}

	public void SetPasswordInput(string password)
	{
		login_PasswordField.text = password;
	}

	#region Login
	public void Login()
	{
		UpdateNetworkConnectionUI ();

		if (Networking.instance != null) {
			if (login_UsernameField.text != "" && login_PasswordField.text != "") {
				if (ScreenLoading.instance != null) {
					ScreenLoading.instance.Play ();
				}

				Networking.instance.LoginRequest (GetUsernameInput (), GetPasswordInput (), OnGetLogin, ErrorGetLoginCallback);
			} 
			else if (login_UsernameField.text == "" || (login_UsernameField.text == "" && login_PasswordField.text == "")) {
				//NativeUI.AlertPopup alert = NativeUI.Alert ("Notification!", "Enter your email");
				AndroidDialog.instance.showWarningDialog("Enter your email");
			} 
			else if (login_PasswordField.text == "") {
				//NativeUI.AlertPopup alert = NativeUI.Alert ("Notification!", "Enter your password");
				AndroidDialog.instance.showWarningDialog("Enter your password");
			}
		}
	}

	private void OnGetLogin(LoginRespone response){
		if (response.event_code == "201") {
			MainAllController.instance.LoginUser(GetUsernameInput(), response.auth_token);
		}
	}

	public void ErrorGetLoginCallback(){
		//NativeUI.AlertPopup alert = NativeUI.Alert("Notification!", "This email address does not exist");

		if (ScreenLoading.instance != null) {
			ScreenLoading.instance.Stop ();
		}

		ClearAllInfo ();
	}
	#endregion


	#region Logout

	public void Logout()
	{
		if (ScreenLoading.instance != null) {
			ScreenLoading.instance.Play ();
		}

		if (VR_MainMenu.instance != null) {
			VR_MainMenu.instance.ShowLoadingUI ();
		}

		if (Networking.instance != null) {
			if (MainAllController.instance.user != null) {
				Networking.instance.LogoutRequest (MainAllController.instance.user.token, LogoutCallback, ErrorLogoutCallback);
			} else {
				Debug.Log ("Error logging out!");
			}
		}
	}

	void LogoutCallback(LogoutRespone response)
	{
		if (response.event_code == "200") {
			MainAllController.instance.LogoutUser();
		}
	}

	void ErrorLogoutCallback()
	{
		//NativeUI.AlertPopup alert = NativeUI.Alert("Notification!", "Logout isn't correct!");

		if (ScreenLoading.instance != null) {
			ScreenLoading.instance.Stop ();
		}

		if (VR_MainMenu.instance != null) {
			VR_MainMenu.instance.HideLoadingUI ();
		}
	}
	#endregion

	public void EnableAutoViewable(bool visible)
	{
		if (visible) {
			if (BtnEnableAuto != null && BtnDisableAuto != null) {
				BtnEnableAuto.gameObject.SetActive (visible);
				BtnDisableAuto.gameObject.SetActive (!visible);
				isAutoLogin = visible;
			} else {
				Debug.LogError ("BtnEnableAuto - BtnDisableAuto null!");
			}
		} else {
			if (BtnEnableAuto != null && BtnDisableAuto != null) {
				BtnEnableAuto.gameObject.SetActive (visible);
				BtnDisableAuto.gameObject.SetActive (!visible);
				isAutoLogin = visible;
			} else {
				Debug.LogError ("BtnEnableAuto - BtnDisableAuto null!");
			}
		}

	}

	public bool AutoLoginEnable()
	{
		return isAutoLogin;
	}

	public void ClearAllInfo(){
		login_UsernameField.text = "";
		login_PasswordField.text = "";
	}
}
