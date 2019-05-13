using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AndroidDialog : MonoBehaviour
{
	const int ButtonWidth = 256;
	const int ButtonHeight = 64;

	private bool mYesPressed;
	private bool mNoPressed;

	public static AndroidDialog instance;

	void Awake()
	{
		instance = this;
	}


	#if UNITY_ANDROID

	private class PositiveButtonListener: AndroidJavaProxy
	{
		private AndroidDialog mDialog;
        private ConfirmCallback callback;
        public PositiveButtonListener(AndroidDialog d, ConfirmCallback callback) : base("android.content.DialogInterface$OnClickListener")
		{
			mDialog = d;
            this.callback = callback;

        }
		public void onClick(AndroidJavaObject obj, int value)
		{
			mDialog.mYesPressed = true;
			mDialog.mNoPressed = false;

			if (callback != null) {
                callback();
			}
		}
	}

	private class NegativeButtonListener: AndroidJavaProxy
	{
		private AndroidDialog mDialog;

		public NegativeButtonListener(AndroidDialog d) : base("android.content.DialogInterface$OnClickListener")
		{
			mDialog = d;
		}

		public void onClick(AndroidJavaObject obj, int value)
		{
			mDialog.mYesPressed = false;
			mDialog.mNoPressed = true;
		}
	}
	#endif

	public delegate void ConfirmCallback();
	public delegate void CancelCallback();

	AndroidJavaObject dialog;
	string lastMessage;
	ConfirmCallback lastCallback;
	string lastConfirmMessage;
	string lastCancelMessage;

	int lastDialog;

	public void showLoginDialog(string message, ConfirmCallback callback,string confirmMessage = "Confirm",string cancelMessage = "Cancel",bool remember = true)
	{
		#if UNITY_ANDROID
		AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

		object[] paramObject = new object[2];
		paramObject[0] = activity;
		paramObject[1] = 16974394; //Holo Light theme

		activity.Call("runOnUiThread",new AndroidJavaRunnable(() => 
			{
				AndroidJavaObject alertDialogBuilder = new AndroidJavaObject("android/app/AlertDialog$Builder",paramObject);

				alertDialogBuilder.Call<AndroidJavaObject>("setTitle","Notification");

				alertDialogBuilder.Call<AndroidJavaObject>("setMessage",message);

				alertDialogBuilder.Call<AndroidJavaObject>("setCancelable",true);

				alertDialogBuilder.Call<AndroidJavaObject>("setPositiveButton",confirmMessage,new PositiveButtonListener(this, callback));

				alertDialogBuilder.Call<AndroidJavaObject>("setNegativeButton",cancelMessage,new NegativeButtonListener(this));

				dialog = alertDialogBuilder.Call<AndroidJavaObject>("create");

				dialog.Call("show");
				

		lastMessage = message;
		lastCallback = callback;
		lastConfirmMessage = confirmMessage;
		lastCancelMessage = cancelMessage;

				if(remember)
				{
					lastDialog = 1;
				}else
				{
					lastDialog = -1;
				}

			}
		)
		);
		#endif
	}

	public void showWarningDialog(string message,bool remember = true)
	{
		#if UNITY_ANDROID 
		AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

		object[] paramObject = new object[2];
		paramObject[0] = activity;
		paramObject[1] = 16974394; //Holo Light theme

		activity.Call("runOnUiThread",new AndroidJavaRunnable(() => 
			{
				AndroidJavaObject alertDialogBuilder = new AndroidJavaObject("android/app/AlertDialog$Builder",paramObject);

				alertDialogBuilder.Call<AndroidJavaObject>("setMessage",message);

				alertDialogBuilder.Call<AndroidJavaObject>("setCancelable",true);

				dialog = alertDialogBuilder.Call<AndroidJavaObject>("create");

				dialog.Call("show");

				lastMessage = message;

				if(remember)
				{
					lastDialog = 2;
				}else
				{
					lastDialog = -1;
				}
			}
		)
		);

		#endif
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			if (dialog != null) {
				dialog.Call("dismiss");
			}
		}
		else
		{
			if (lastDialog == 1) {
				showLoginDialog (lastMessage,lastCallback,lastConfirmMessage,lastCancelMessage,false);
			}

			if (lastDialog == 2) {
				showWarningDialog (lastMessage,false);
			}

		}

	}
		
}
