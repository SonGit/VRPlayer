using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class UnityDeeplinks : MonoBehaviour {

	#if UNITY_IOS
	[DllImport("__Internal")]
	private static extern void UnityDeeplinks_init(string gameObject = null, string deeplinkMethod = null);
	#endif

	void Awake()
	{
		//onDeeplink ("myapp://mtest&7db99e997329ab08801bd58cb2161bde6291ef03");
	}

	// Use this for initialization
	void Start () {
		#if UNITY_IOS
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			UnityDeeplinks_init(gameObject.name);
		}
		#endif

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onDeeplink(string deeplink) {
		Debug.Log ("onDeeplink deeplink " + deeplink);
		StartCoroutine (Delay(deeplink));
	}

	IEnumerator Delay(string deeplink)
	{
		Debug.Log ("onDeeplink Delay");
		yield return new WaitForEndOfFrame ();

		try
		{
			string processed = deeplink.Replace ("myapp://","");

			string[] parts = processed.Split ('&');

			string username = parts [0];

			string token = parts [1];


			MainAllController.instance.LogoutUser ();

			MainAllController.instance.OpenLoginMenuFromVR ();

			MainAllController.instance.LoginUser (username,token);

			Debug.Log("onDeeplink received "+ "username " + username + "   token " + token);

		} catch (System.Exception e) {

			Debug.LogError ("onDeeplink exception! " + e.Message);

		}
	}


}
