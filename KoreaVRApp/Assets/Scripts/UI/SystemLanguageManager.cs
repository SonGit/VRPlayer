using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemLanguageManager : MonoBehaviour
{
	public static SystemLanguageManager instance;

	[Header("---- AccessMenu ----")]
	[SerializeField] private Text accessMenu_PhoneVideoTitle = null;
	[SerializeField] private Text accessMenu_MyStorageName = null;

	[SerializeField] private Text accessMenu_MyVideoTitle = null;
	[SerializeField] private Text accessMenu_Videolistname = null;
	[SerializeField] private Text accessMenu_Favoritename = null;

	[SerializeField] private Text accessMenu_DownloadTitle = null;
	[SerializeField] private Text accessMenu_DownloadName = null;
	[SerializeField] private Text accessMenu_Inboxname = null;

	[SerializeField] private Text accessMenu_EtcTitle = null;
	[SerializeField] private Text accessMenu_PreferencesName = null;
	[SerializeField] private Text accessMenu_UsageInformationName = null;
	[SerializeField] private Text accessMenu_VersionName = null;

	[Header("---- LoginMenu ----")]
	[SerializeField] private Text loginMenu_Header = null;
	[SerializeField] private Text loginMenu_UsernamePlacehold = null;
	[SerializeField] private Text loginMenu_PasswordPlacehold  = null;
	[SerializeField] private Text loginMenu_AutoLogin = null;
	[SerializeField] private Text loginMenu_Loginbnt = null;

	[Header("---- StorageMenu ----")]
	[SerializeField] private Text storageMenu_Header = null;

	[Header("---- MyVideoMenu ----")]
	[SerializeField] private Text myVideoMenu_Header = null;

	[Header("---- DetailMenu ----")]
	[SerializeField] private Text detailMenu_Header = null;
	[SerializeField] private Text detailMenu_UnfavoriteBnt = null;
	[SerializeField] private Text detailMenu_FavoriteBnt = null;
	[SerializeField] private Text detailMenu_HaventDownloadBnt = null;
	[SerializeField] private Text detailMenu_Havent2DBnt = null;
	[SerializeField] private Text detailMenu_Havent3DBnt = null;
	[SerializeField] private Text detailMenu_Downloaded2DBnt = null;
	[SerializeField] private Text detailMenu_Downloaded3DBnt = null;
	[SerializeField] private Text detailMenu_InformationTitle = null;
	[SerializeField] private Text detailMenu_ID = null;
	[SerializeField] private Text detailMenu_Release = null;
	[SerializeField] private Text detailMenu_Playtime = null;
	[SerializeField] private Text detailMenu_Genre = null;

	[Header("---- FavoriteMenu ----")]
	[SerializeField] private Text favoriteMenu_Header = null;

	[Header("---- DownloadMenu ----")]
	[SerializeField] private Text downloadMenu_Header = null;

	[Header("---- InboxMenu ----")]
	[SerializeField] private Text inboxMenu_Header = null;

	[Header("---- SettingMenu ----")]
	[SerializeField] private Text settingMenu_Header = null;
	[SerializeField] private Text settingMenu_LoginTitle = null;
	[SerializeField] private Text settingMenu_LoginInformation = null;
	[SerializeField] private Text settingMenu_Keeploggedin = null;
	[SerializeField] private Text settingMenu_PreferencesTitle = null;
	[SerializeField] private Text settingMenu_MobileNetwork = null;
	[SerializeField] private Text settingMenu_MobileNetworkNotice = null;
	[SerializeField] private Text settingMenu_PushNotificationSettings = null;
	[SerializeField] private Text settingMenu_PushNotificationSettingsNotice = null;
	[SerializeField] private Text settingMenu_UsableCapacity = null;

	[Header("---- VRPlayerMenu ----")]
	[SerializeField] private Text vrPlayerMenu_Welcome = null;
	[SerializeField] private Text vrPlayerMenu_Top = null;
	[SerializeField] private Text vrPlayerMenu_Bottom = null;
	[SerializeField] private Text vrPlayerMenu_RunVRBnt = null;

	[Header("---- AlertMenu ----")]
	[SerializeField] private Text alertMenu_Login = null;
	[SerializeField] private Text alertMenu_Logout = null;
	[SerializeField] private Text alertMenu_Exit = null;

	void Awake(){
		instance = this;

		LanguageViewable ();
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		
    }

	public void LanguageViewable(){
		
		//This checks if your computer's operating system is in the English language
		if (Application.systemLanguage == SystemLanguage.English) {
			Debug.Log ("This system is in English............................................");

			SetEnglishLanguage ();
		}

		//Otherwise, if the system is Japanese
		else if (Application.systemLanguage == SystemLanguage.Japanese) {
			Debug.Log ("This system is in Japanese...........................................");
		}

		//Otherwise, if the system is Korean
		else if (Application.systemLanguage == SystemLanguage.Korean) {
			Debug.Log ("This system is in Korean.............................................");

			SetKoreaLanguage ();
		}

		//Otherwise, if the system is Chinese
		else if (Application.systemLanguage == SystemLanguage.Chinese) {
			Debug.Log ("This system is in Chinese.............................................");
		} 

		else {
			Debug.Log ("This system is in other language......................................");
			SetEnglishLanguage ();
		}
	}

	#region EnglishLanguage

	private void SetEnglishLanguage_AccessMenu(){
		DisplayValueText (accessMenu_PhoneVideoTitle,"Phone Video");
		DisplayValueText (accessMenu_MyStorageName,"My Storage");
		DisplayValueText (accessMenu_MyVideoTitle,"My Video");
		DisplayValueText (accessMenu_Videolistname,"Video List");
		DisplayValueText (accessMenu_Favoritename,"Favorite");
		DisplayValueText (accessMenu_DownloadTitle,"Download");
		DisplayValueText (accessMenu_DownloadName,"Download");
		DisplayValueText (accessMenu_Inboxname,"Inbox");
		DisplayValueText (accessMenu_EtcTitle,"Etc.");
		DisplayValueText (accessMenu_PreferencesName,"Preferences");
		DisplayValueText (accessMenu_UsageInformationName,"Usage Information");
		DisplayValueText (accessMenu_VersionName,"Version");
	}

	private void SetEnglishLanguage_LoginMenu(){
		DisplayValueText (loginMenu_Header,"LOGIN");
		DisplayValueText (loginMenu_UsernamePlacehold,"Email");
		DisplayValueText (loginMenu_PasswordPlacehold,"Password");
		DisplayValueText (loginMenu_AutoLogin,"Auto Login");
		DisplayValueText (loginMenu_Loginbnt,"LOGIN");
	}

	private void SetEnglishLanguage_StorageMenu(){
		DisplayValueText (storageMenu_Header,"My Storage");
	}

	private void SetEnglishLanguage_MyVideoMenu(){
		DisplayValueText (myVideoMenu_Header,"My Video");
	}

	private void SetEnglishLanguage_DetailMenu(){
		DisplayValueText (detailMenu_Header,"Detail Page");
		DisplayValueText (detailMenu_UnfavoriteBnt,"Favorite");
		DisplayValueText (detailMenu_FavoriteBnt,"Favorite");
		DisplayValueText (detailMenu_HaventDownloadBnt,"Download");
		DisplayValueText (detailMenu_Havent2DBnt,"2D Streaming");
		DisplayValueText (detailMenu_Havent3DBnt,"3D Streaming");
		DisplayValueText (detailMenu_Downloaded2DBnt,"Watch in 2D");
		DisplayValueText (detailMenu_Downloaded3DBnt,"Watch in 3D");
		DisplayValueText (detailMenu_InformationTitle,"Information");
		DisplayValueText (detailMenu_ID,"ContentID");
		DisplayValueText (detailMenu_Release,"Release");
		DisplayValueText (detailMenu_Playtime,"Play time");
		DisplayValueText (detailMenu_Genre,"Genre");
	}


	private void SetEnglishLanguage_FavoriteMenu(){
		DisplayValueText (favoriteMenu_Header,"Favorite");
	}

	private void SetEnglishLanguage_DownloadMenu(){
		DisplayValueText (downloadMenu_Header,"Download");
	}

	private void SetEnglishLanguage_InboxMenu(){
		DisplayValueText (inboxMenu_Header,"Inbox");
	}

	private void SetEnglishLanguage_SettingMenu(){
		DisplayValueText (settingMenu_Header,"Settings");
		DisplayValueText (settingMenu_LoginTitle,"Login");
		DisplayValueText (settingMenu_LoginInformation,"Login Information");
		DisplayValueText (settingMenu_Keeploggedin,"Keep logged in");
		DisplayValueText (settingMenu_PreferencesTitle,"Preferences");
		DisplayValueText (settingMenu_MobileNetwork,"Mobile network usage notification");
		DisplayValueText (settingMenu_MobileNetworkNotice,"Confirm when using mobile network data");
		DisplayValueText (settingMenu_PushNotificationSettings,"Push notification settings");
		DisplayValueText (settingMenu_PushNotificationSettingsNotice,"Receive PUSH notification");
		DisplayValueText (settingMenu_UsableCapacity,"Usable capacity");
	}

	private void SetEnglishLanguage_VRPlayerMenu(){
		DisplayValueText (vrPlayerMenu_Welcome,"Welcome");
		DisplayValueText (vrPlayerMenu_Top,"Experience realistic virtual reality realism\nwith\nVR FETISH WOMEN");
		DisplayValueText (vrPlayerMenu_Bottom,"Connect your mobile phone to your device");
		DisplayValueText (vrPlayerMenu_RunVRBnt,"RUN VR PLAYER");
	}

	private void SetEnglishLanguage_AlertMenu(){
		DisplayValueText (alertMenu_Login,"Sign-in completed");
		DisplayValueText (alertMenu_Logout,"Logged out");
		DisplayValueText (alertMenu_Exit,"Press the previous button again to exit.");
	}

	private void SetEnglishLanguage(){
		SetEnglishLanguage_AccessMenu ();
		SetEnglishLanguage_LoginMenu ();
		SetEnglishLanguage_StorageMenu ();
		SetEnglishLanguage_MyVideoMenu ();
		SetEnglishLanguage_DetailMenu ();
		SetEnglishLanguage_FavoriteMenu ();
		SetEnglishLanguage_DownloadMenu ();
		SetEnglishLanguage_InboxMenu ();
		SetEnglishLanguage_SettingMenu ();
		SetEnglishLanguage_VRPlayerMenu ();
		SetEnglishLanguage_AlertMenu ();
	}

	#endregion


	#region KoreaLanguage

	private void SetKoreaLanguage_AccessMenu(){
		DisplayValueText (accessMenu_PhoneVideoTitle,"전화 비디오");
		DisplayValueText (accessMenu_MyStorageName,"내 저장 공간");
		DisplayValueText (accessMenu_MyVideoTitle,"내 비디오");
		DisplayValueText (accessMenu_Videolistname,"동영상 목록");
		DisplayValueText (accessMenu_Favoritename,"인기 있는 말");
		DisplayValueText (accessMenu_DownloadTitle,"다운로드");
		DisplayValueText (accessMenu_DownloadName,"다운로드");
		DisplayValueText (accessMenu_Inboxname,"받은 편지함");
		DisplayValueText (accessMenu_EtcTitle,"기타.");
		DisplayValueText (accessMenu_PreferencesName,"환경 설정");
		DisplayValueText (accessMenu_UsageInformationName,"사용 정보");
		DisplayValueText (accessMenu_VersionName,"번역");
	}

	private void SetKoreaLanguage_LoginMenu(){
		DisplayValueText (loginMenu_Header,"로그인");
		DisplayValueText (loginMenu_UsernamePlacehold,"이메일");
		DisplayValueText (loginMenu_PasswordPlacehold,"암호");
		DisplayValueText (loginMenu_AutoLogin,"자동 로그인");
		DisplayValueText (loginMenu_Loginbnt,"로그인");
	}

	private void SetKoreaLanguage_StorageMenu(){
		DisplayValueText (storageMenu_Header,"내 저장 공간");
	}

	private void SetKoreaLanguage_MyVideoMenu(){
		DisplayValueText (myVideoMenu_Header,"내 비디오");
	}

	private void SetKoreaLanguage_DetailMenu(){
		DisplayValueText (detailMenu_Header,"세부 정보 페이지");
		DisplayValueText (detailMenu_UnfavoriteBnt,"인기 있는 말");
		DisplayValueText (detailMenu_FavoriteBnt,"인기 있는 말");
		DisplayValueText (detailMenu_HaventDownloadBnt,"다운로드");
		DisplayValueText (detailMenu_Havent2DBnt,"2D 스트리밍");
		DisplayValueText (detailMenu_Havent3DBnt,"3D 스트리밍");
		DisplayValueText (detailMenu_Downloaded2DBnt,"2D로보기");
		DisplayValueText (detailMenu_Downloaded3DBnt,"3D로보기");
		DisplayValueText (detailMenu_InformationTitle,"정보");
		DisplayValueText (detailMenu_ID,"콘텐츠 ID");
		DisplayValueText (detailMenu_Release,"해제");
		DisplayValueText (detailMenu_Playtime,"플레이 시간");
		DisplayValueText (detailMenu_Genre,"유형");
	}


	private void SetKoreaLanguage_FavoriteMenu(){
		DisplayValueText (favoriteMenu_Header,"인기 있는 말");
	}

	private void SetKoreaLanguage_DownloadMenu(){
		DisplayValueText (downloadMenu_Header,"다운로드");
	}

	private void SetKoreaLanguage_InboxMenu(){
		DisplayValueText (inboxMenu_Header,"받은 편지함");
	}

	private void SetKoreaLanguage_SettingMenu(){
		DisplayValueText (settingMenu_Header,"설정");
		DisplayValueText (settingMenu_LoginTitle,"로그인");
		DisplayValueText (settingMenu_LoginInformation,"로그인 정보");
		DisplayValueText (settingMenu_Keeploggedin,"로그인 상태 유지");
		DisplayValueText (settingMenu_PreferencesTitle,"환경 설정");
		DisplayValueText (settingMenu_MobileNetwork,"모바일 네트워크 사용 알림");
		DisplayValueText (settingMenu_MobileNetworkNotice,"모바일 네트워크 데이터 사용시 확인");
		DisplayValueText (settingMenu_PushNotificationSettings,"푸시 알림 설정");
		DisplayValueText (settingMenu_PushNotificationSettingsNotice,"PUSH 알림 수신");
		DisplayValueText (settingMenu_UsableCapacity,"사용 가능한 용량");
	}

	private void SetKoreaLanguage_VRPlayerMenu(){
		DisplayValueText (vrPlayerMenu_Welcome,"환영");
		DisplayValueText (vrPlayerMenu_Top,"사실적인 가상 현실 리얼리즘을 경험하십시오\n와\nVR 공상 여자");
		DisplayValueText (vrPlayerMenu_Bottom,"휴대 전화를 기기에 연결하십시오\n달린 후");
		DisplayValueText (vrPlayerMenu_RunVRBnt,"VR 플레이어 실행");
	}

	private void SetKoreaLanguage_AlertMenu(){
		DisplayValueText (alertMenu_Login,"로그인이 완료되었습니다");
		DisplayValueText (alertMenu_Logout,"로그 아웃 됨");
		DisplayValueText (alertMenu_Exit,"이전 버튼을 다시 누르면 종료됩니다.");
	}

	private void SetKoreaLanguage(){
		SetKoreaLanguage_AccessMenu ();
		SetKoreaLanguage_LoginMenu ();
		SetKoreaLanguage_StorageMenu ();
		SetKoreaLanguage_MyVideoMenu ();
		SetKoreaLanguage_DetailMenu ();
		SetKoreaLanguage_FavoriteMenu ();
		SetKoreaLanguage_DownloadMenu ();
		SetKoreaLanguage_InboxMenu ();
		SetKoreaLanguage_SettingMenu ();
		SetKoreaLanguage_VRPlayerMenu ();
		SetKoreaLanguage_AlertMenu ();
	}

	#endregion


	private void DisplayValueText(Text text, string value){
		if (text != null) {
			text.text = value;
		} else {
			Debug.Log(text.name + "null...........................");
		}
	}
}
