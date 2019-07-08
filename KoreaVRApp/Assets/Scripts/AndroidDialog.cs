using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EasyMobile;

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

			AndroidDialog.instance.dialog = null;
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

			AndroidDialog.instance.dialog = null;
		}
	}

	private class DismissListener: AndroidJavaProxy
	{
		private AndroidDialog mDialog;

		public DismissListener(AndroidDialog d) : base("android.content.DialogInterface$OnClickListener")
		{
			mDialog = d;
		}

		public void onClick(AndroidJavaObject obj, int value)
		{
			mDialog.mYesPressed = false;
			mDialog.mNoPressed = true;

			AndroidDialog.instance.dialog = null;
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

	public void showLoginDialog(string message, ConfirmCallback callback,string confirmMessage,string cancelMessage,bool remember = true)
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

		object[] paramObject = new object[2];
		paramObject[0] = activity;
		paramObject[1] = 16974394; //Holo Light theme

		activity.Call("runOnUiThread",new AndroidJavaRunnable(() => 
			{
				AndroidJavaObject alertDialogBuilder = new AndroidJavaObject("android/app/AlertDialog$Builder",paramObject);

				if (SystemLanguageManager.instance != null){
					if (SystemLanguageManager.instance.IsEnglishLanguage){
						alertDialogBuilder.Call<AndroidJavaObject>("setTitle","Notification");
					}

					if (SystemLanguageManager.instance.IsKoreanLanguage){
						alertDialogBuilder.Call<AndroidJavaObject>("setTitle","공고");
					}

					if (SystemLanguageManager.instance.IsJapaneseLanguage){
						alertDialogBuilder.Call<AndroidJavaObject>("setTitle","お知らせ");
					}

					if (SystemLanguageManager.instance.IsChineseLanguage){
						alertDialogBuilder.Call<AndroidJavaObject>("setTitle","通知");
					}

					if (SystemLanguageManager.instance.IsOtherLanguage){
						alertDialogBuilder.Call<AndroidJavaObject>("setTitle","Notification");
					}
				}

				alertDialogBuilder.Call<AndroidJavaObject>("setMessage",message);

				alertDialogBuilder.Call<AndroidJavaObject>("setCancelable",false);

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

#if UNITY_IOS && !UNITY_EDITOR

            var alert = NativeUI.ShowTwoButtonAlert("Notification", message, confirmMessage, cancelMessage);
            if (alert != null)
            {
                alert.OnComplete += button =>
                {
                    if (button != 0)
                        return;

                     if(callback != null)
                        {
                            callback();
                         }
                };
            }

#endif
    }

    public void showWarningDialog(string message,bool remember = true)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

		object[] paramObject = new object[2];
		paramObject[0] = activity;
		paramObject[1] = 16974394; //Holo Light theme

		activity.Call("runOnUiThread",new AndroidJavaRunnable(() => 
			{
				AndroidJavaObject alertDialogBuilder = new AndroidJavaObject("android/app/AlertDialog$Builder",paramObject);

				if (SystemLanguageManager.instance != null){
					if (SystemLanguageManager.instance.IsEnglishLanguage){
						alertDialogBuilder.Call<AndroidJavaObject>("setTitle","Notification");
					}

					if (SystemLanguageManager.instance.IsKoreanLanguage){
						alertDialogBuilder.Call<AndroidJavaObject>("setTitle","공고");
					}

					if (SystemLanguageManager.instance.IsJapaneseLanguage){
						alertDialogBuilder.Call<AndroidJavaObject>("setTitle","お知らせ");
					}

					if (SystemLanguageManager.instance.IsChineseLanguage){
						alertDialogBuilder.Call<AndroidJavaObject>("setTitle","通知");
					}

					if (SystemLanguageManager.instance.IsOtherLanguage){
						alertDialogBuilder.Call<AndroidJavaObject>("setTitle","Notification");
					}
				}

				alertDialogBuilder.Call<AndroidJavaObject>("setMessage",message);

				alertDialogBuilder.Call<AndroidJavaObject>("setCancelable",false);

				alertDialogBuilder.Call<AndroidJavaObject>("setNegativeButton","OK",new NegativeButtonListener(this));

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

#if UNITY_IOS && !UNITY_EDITOR

        var alert = NativeUI.Alert("Notification", message, "Close");

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
			if (dialog != null) {
				if (lastDialog == 1) {
					showLoginDialog (lastMessage,lastCallback,lastConfirmMessage,lastCancelMessage);
				}

				if (lastDialog == 2) {
					showWarningDialog (lastMessage);
				}
			}

		}

	}
		
}
