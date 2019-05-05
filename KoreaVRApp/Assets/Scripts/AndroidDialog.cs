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

	public void showLoginDialog(string message, ConfirmCallback callback,string confirmMessage = "Confirm",string cancelMessage = "Cancel")
	{
		#if UNITY_ANDROID //&& !UNITY_EDITOR
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

				AndroidJavaObject dialog = alertDialogBuilder.Call<AndroidJavaObject>("create");

				dialog.Call("show");
			}
		)
		);
		#endif
	}

	public void showWarningDialog(string message)
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

				AndroidJavaObject dialog = alertDialogBuilder.Call<AndroidJavaObject>("create");

				dialog.Call("show");
			}
		)
		);
		#endif
	}
		
}
