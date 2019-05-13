using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemLanguageManager : MonoBehaviour
{
	public static SystemLanguageManager instance;

	[Header("---- EnglishLanguage ----")]

	#region 2D
	private string phoneVideoString_English = "Phone Video";
	private string myStorageString_English = "My Storage";
	private string myvideoString_English = "My Video";
	private string videoListString_English = "Video List";
	private string favoriteString_English = "Favorite";
	private string downloadString_English = "Download";
	private string inboxString_English = "Inbox";
	private string etcString_English = "Etc.";
	private string preferencesString_English = "Preferences";
	private string usageInformationString_English = "Usage Information";
	private string versionString_English = "Version";
	private string lOGINString_English = "LOGIN";
	private string autoLoginString_English = "Auto Login";
	private string networkString_English = "Unable to connect to network.\nPlease check your network connection and try again.";
	private string noVideosString_English = "No Videos";
	private string detailPageString_English = "Detail Page";
	private string streaming2DString_English = "2D Streaming";
	private string streaming3DString_English = "3D Streaming";
	private string watchin2DString_English = "Watch in 2D";
	private string watchin3DString_English = "Watch in 3D";
	private string informationString_English = "Information";
	private string contentIDString_English = "ContentID";
	private string releaseString_English = "Release";
	private string playtimeString_English = "Play time";
	private string genreString_English = "Genre";
	private string Go_to_inbox_after_downloadString_English = "Go to inbox after download";
	private string settingsString_English = "Settings";
	private string loginString_English = "Login";
	private string loginInformationString_English = "Login Information";
	private string keep_logged_inString_English = "Keep logged in";
	private string mobile_network_usage_notificationString_English = "Mobile network usage notification";
	private string confirm_when_using_mobile_network_dataString_English = "Confirm when using mobile network data";
	private string push_notification_settingsString_English = "Push notification settings";
	private string receive_PUSH_notificationString_English = "Receive PUSH notification";
	private string usable_capacityString_English = "Usable capacity";
	private string welcomeString_English = "Welcome";
	private string experience_realistic_virtual_realityString_English = "Experience realistic virtual reality realism with VRUdon";
	private string connect_your_mobile_phone_to_your_deviceString_English = "Connect your mobile phone to your device";
	private string run_vr_PlayerString_English = "RUN VR PLAYER";
	private string sign_in_completedString_English = "Sign-in completed";
	private string logged_outString_English = "Logged out";
	private string press_the_previous_button_again_to_exitString_English = "Press the previous button again to exit.";
	#endregion

	#region VR_ScreenCursor

	private string Hover_the_on_screenString_English = "Hover the on-screen cursor for 3 seconds to start the player";
	private string Precautions_AvoidString_English = "Precautions\nAvoid prolonged usage.If you experience any abnormal symptoms such as eye fatigue or motion sickness,\nstop using the device immediately";

	#endregion

	#region VR_MainMenu

	private string vr_PhoneVideoString_English = "Phone Video";
	private string vr_DownloadString_English = "Download";
	private string vr_MyVideoString_English = "My Video";
	private string vr_MyStorageString_English = "My Storage";
	private string vr_InboxString_English = "Inbox";
	private string vr_VideoListString_English = "VideoList";
	private string vr_FavoriteString_English = "Favorite";
	private string vr_LoginrequiredString_English = "Login required";
	private string vr_Move_to_loginString_English = "Move to login page?";
	private string vr_YesString_English = "Yes";
	private string vr_NoString_English = "No";
	private string vr_DeleterequiredString_English = "Delete required";
	private string vr_DeleteString_English = "Delete?";
	private string vr_Please_check_the_network_connectionString_English = "Please check the network connection";
	private string vr_NovideosString_English = "No Videos";
	private string vr_StreamingrequiredString_English = "Streaming required";
	private string vr_StreamingString_English = "Streaming?";

	#endregion

	#region VR_Settings

	private string vr_VideoString_English = "Video";
	private string vr_ScreenString_English = "Screen";
	private string vr_2DString_English = "2D";
	private string vr_ListString_English = "List";
	private string vr_CloseString_English = "Close";
	private string vr_SettingsString_English = "Settings";
	private string vr_ScreenLockString_English = "Screen Lock";
	private string vr_ModeString_English = "Mode";
	private string vr_FlatString_English = "Flat";
	private string vr_CinemaString_English = "Cinema";
	private string vr_AutoString_English = "Auto";
	private string vr_SizeString_English = "Size";
	private string vr_RatioString_English = "Ratio";
	private string vr_Are_you_sure_you_deviceString_English = "Are you sure you want to disconnect your phone from your VR device?";

	#endregion


	[Header("---- KoreanLanguage ----")]

	#region 2D
	private string phoneVideoString_Korean = "전화 비디오";
	private string myStorageString_Korean = "내 저장 공간";
	private string myvideoString_Korean = "내 비디오";
	private string videoListString_Korean = "동영상 목록";
	private string favoriteString_Korean = "즐겨 찾기";
	private string downloadString_Korean = "다운로드";
	private string inboxString_Korean = "받은 편지함";
	private string etcString_Korean = "기타.";
	private string preferencesString_Korean = "환경 설정";
	private string usageInformationString_Korean = "사용 정보";
	private string versionString_Korean = "번역";
	private string lOGINString_Korean = "로그인";
	private string autoLoginString_Korean = "자동 로그인";
	private string networkString_Korean = "네트워크에 연결할 수 없습니다.\n네트워크 연결을 확인하고 다시 시도하십시오.";
	private string noVideosString_Korean = "동영상 없음";
	private string detailPageString_Korean = "세부 정보 페이지";
	private string streaming2DString_Korean = "2D 스트리밍";
	private string streaming3DString_Korean = "3D 스트리밍";
	private string watchin2DString_Korean = "2D로보기";
	private string watchin3DString_Korean = "3D로보기";
	private string informationString_Korean = "정보";
	private string contentIDString_Korean = "콘텐츠 ID";
	private string releaseString_Korean = "해제";
	private string playtimeString_Korean = "플레이 시간";
	private string genreString_Korean = "유형";
	private string Go_to_inbox_after_downloadString_Korean = "다운로드 후받은 편지함으로 이동";
	private string settingsString_Korean = "설정";
	private string loginString_Korean = "로그인";
	private string loginInformationString_Korean = "로그인 정보";
	private string keep_logged_inString_Korean = "로그인 상태 유지";
	private string mobile_network_usage_notificationString_Korean = "모바일 네트워크 사용 알림";
	private string confirm_when_using_mobile_network_dataString_Korean = "모바일 네트워크 데이터 사용시 확인";
	private string push_notification_settingsString_Korean = "푸시 알림 설정";
	private string receive_PUSH_notificationString_Korean = "푸시 알림 수신";
	private string usable_capacityString_Korean = "사용 가능한 용량";
	private string welcomeString_Korean = "환영";
	private string experience_realistic_virtual_realityString_Korean = "VRUdon을 사용하여 사실적인 가상 현실 리얼리즘을 경험하십시오";
	private string connect_your_mobile_phone_to_your_deviceString_Korean = "휴대 전화를 기기에 연결하십시오";
	private string run_vr_PlayerString_Korean = "VR 플레이어 실행";
	private string sign_in_completedString_Korean = "로그인이 완료되었습니다";
	private string logged_outString_Korean = "로그 아웃 됨";
	private string press_the_previous_button_again_to_exitString_Korean = "이전 버튼을 다시 누르면 종료됩니다.";
	#endregion

	#region VR_ScreenCursor

	private string Hover_the_on_screenString_Korean = "화면 커서를 3 초 동안 가져 가서 플레이어를 시작하십시오.";
	private string Precautions_AvoidString_Korean = "예방 조치\n장기간 사용하지 마십시오.눈의 피로 나 멀미와 같은 비정상적인 증상이 나타나면,\n즉시 장치 사용을 중지하십시오.";

	#endregion

	#region VR_MainMenu

	private string vr_Left_PhoneVideoString_Korean = "전화 비디오";
	private string vr_Left_DownloadString_Korean = "다운로드";
	private string vr_Left_MyVideoString_Korean = "내 비디오";
	private string vr_Mid_MyStorageString_Korean = "내 저장 공간";
	private string vr_Mid_InboxString_Korean = "받은 편지함";
	private string vr_Mid_VideoListString_Korean = "동영상 목록";
	private string vr_Mid_FavoriteString_Korean = "인기 있는 말";
	private string vr_LoginrequiredString_Korean = "로그인 필요";
	private string vr_Move_to_loginString_Korean = "로그인 페이지로 이동 하시겠습니까?";
	private string vr_YesString_Korean = "예";
	private string vr_NoString_Korean = "아니";
	private string vr_DeleterequiredString_Korean = "삭제 필요";
	private string vr_DeleteString_Korean = "지우다?";
	private string vr_Please_check_the_network_connectionString_Korean = "네트워크 연결을 확인하십시오.";
	private string vr_NovideosString_Korean = "동영상 없음";
	private string vr_StreamingrequiredString_Korean = "스트리밍 요구됨";
	private string vr_StreamingString_Korean = "스트리밍?";

	#endregion

	#region VR_Settings

	private string vr_VideoString_Korean = "비디오";
	private string vr_ScreenString_Korean = "화면";
	private string vr_2DString_Korean = "2D";
	private string vr_ListString_Korean = "명부";
	private string vr_CloseString_Korean = "닫기";
	private string vr_SettingsString_Korean = "설정";
	private string vr_ScreenLockString_Korean = "화면 잠금";
	private string vr_ModeString_Korean = "방법";
	private string vr_FlatString_Korean = "플랫";
	private string vr_CinemaString_Korean = "영화";
	private string vr_AutoString_Korean = "자동";
	private string vr_SizeString_Korean = "크기";
	private string vr_RatioString_Korean = "비율";
	private string vr_Are_you_sure_you_deviceString_Korean = "VR 장치에서 전화기를 분리 하시겠습니까?";

	#endregion


	[Header("---- JapaneseLanguage ----")]

	#region 2D
	private string phoneVideoString_Japanese = "電話ビデオ";
	private string myStorageString_Japanese = "私のストレージ";
	private string myvideoString_Japanese = "私のビデオ";
	private string videoListString_Japanese = "ビデオリスト";
	private string favoriteString_Japanese = "気に入り";
	private string downloadString_Japanese = "ダウンロード";
	private string inboxString_Japanese = "受信トレイ";
	private string etcString_Japanese = "等.";
	private string preferencesString_Japanese = "設定";
	private string usageInformationString_Japanese = "利用情報";
	private string versionString_Japanese = "バージョン";
	private string lOGINString_Japanese = "ログイン";
	private string autoLoginString_Japanese = "自動ログイン";
	private string networkString_Japanese = "ネットワークに接続できません。\nネットワーク接続を確認してもう一度やり直してください。";
	private string noVideosString_Japanese = "動画なし";
	private string detailPageString_Japanese = "詳細ページ";
	private string streaming2DString_Japanese = "2Dストリーミング";
	private string streaming3DString_Japanese = "3Dストリーミング";
	private string watchin2DString_Japanese = "2Dで見る";
	private string watchin3DString_Japanese = "3Dで見る";
	private string informationString_Japanese = "情報";
	private string contentIDString_Japanese = "コンテンツID";
	private string releaseString_Japanese = "リリース";
	private string playtimeString_Japanese = "プレイタイム";
	private string genreString_Japanese = "ジャンル";
	private string Go_to_inbox_after_downloadString_Japanese = "ダウンロード後に受信トレイに移動する";
	private string settingsString_Japanese = "設定";
	private string loginString_Japanese = "ログイン";
	private string loginInformationString_Japanese = "ログイン情報";
	private string keep_logged_inString_Japanese = "ログインしたままにする";
	private string mobile_network_usage_notificationString_Japanese = "モバイルネットワーク利用通知";
	private string confirm_when_using_mobile_network_dataString_Japanese = "모モバイルネットワークデータを使用するときに確認する";
	private string push_notification_settingsString_Japanese = "プッシュ通知の設定";
	private string receive_PUSH_notificationString_Japanese = "プッシュ通知を受け取る";
	private string usable_capacityString_Japanese = "使用可能容量";
	private string welcomeString_Japanese = "ようこそ";
	private string experience_realistic_virtual_realityString_Japanese = "VRUdonでリアルなバーチャルリアリティリアリズムを体験";
	private string connect_your_mobile_phone_to_your_deviceString_Japanese = "携帯電話をデバイスに接続する";
	private string run_vr_PlayerString_Japanese = "VRプレーヤーを動かす";
	private string sign_in_completedString_Japanese = "サインイン完了";
	private string logged_outString_Japanese = "ログアウトしました";
	private string press_the_previous_button_again_to_exitString_Japanese = "前のボタンをもう一度押して終了します。";
	#endregion

	#region VR_ScreenCursor

	private string Hover_the_on_screenString_Japanese = "プレーヤーを起動するには、画面上のカーソルを3秒間移動します。";
	private string Precautions_AvoidString_Japanese = "注意事項\n長期間の使用を避ける.目の疲れや乗り物酔いなどの異常な症状がある場合は、\nすぐに使用を中止してください。";

	#endregion

	#region VR_MainMenu

	private string vr_Left_PhoneVideoString_Japanese = "電話ビデオ";
	private string vr_Left_DownloadString_Japanese = "ダウンロード";
	private string vr_Left_MyVideoString_Japanese = "私のビデオ";
	private string vr_Mid_MyStorageString_Japanese = "私のストレージ";
	private string vr_Mid_InboxString_Japanese = "受信トレイ";
	private string vr_Mid_VideoListString_Japanese = "動画リスト";
	private string vr_Mid_FavoriteString_Japanese = "気に入り";
	private string vr_LoginrequiredString_Japanese = "ログインが必要です";
	private string vr_Move_to_loginString_Japanese = "ログインページに移動しますか？";
	private string vr_YesString_Japanese = "はい";
	private string vr_NoString_Japanese = "いいえ";
	private string vr_DeleterequiredString_Japanese = "削除が必要です";
	private string vr_DeleteString_Japanese = "削除する?";
	private string vr_Please_check_the_network_connectionString_Japanese = "ネットワーク接続を確認してください";
	private string vr_NovideosString_Japanese = "動画なし";
	private string vr_StreamingrequiredString_Japanese = "ストリーミングが必要";
	private string vr_StreamingString_Japanese = "ストリーミング?";

	#endregion

	#region VR_Settings

	private string vr_VideoString_Japanese = "ビデオ";
	private string vr_ScreenString_Japanese = "画面";
	private string vr_2DString_Japanese = "2D";
	private string vr_ListString_Japanese = "リスト";
	private string vr_CloseString_Japanese = "閉じる";
	private string vr_SettingsString_Japanese = "設定";
	private string vr_ScreenLockString_Japanese = "スクリーンロック";
	private string vr_ModeString_Japanese = "モード";
	private string vr_FlatString_Japanese = "平ら";
	private string vr_CinemaString_Japanese = "シネマ";
	private string vr_AutoString_Japanese = "自動";
	private string vr_SizeString_Japanese = "サイズ";
	private string vr_RatioString_Japanese = "比率";
	private string vr_Are_you_sure_you_deviceString_Japanese = "あなたのVRデバイスからあなたの電話を切断してもよろしいですか？";

	#endregion


	[Header("---- ChineseLanguage ----")]

	#region 2D
	private string phoneVideoString_Chinese = "手机视频";
	private string myStorageString_Chinese = "我的存储";
	private string myvideoString_Chinese = "我的视频";
	private string videoListString_Chinese = "视频列表";
	private string favoriteString_Chinese = "喜爱";
	private string downloadString_Chinese = "下载";
	private string inboxString_Chinese = "收件箱";
	private string etcString_Chinese = "等等.";
	private string preferencesString_Chinese = "喜好";
	private string usageInformationString_Chinese = "使用信息";
	private string versionString_Chinese = "版";
	private string lOGINString_Chinese = "登錄";
	private string autoLoginString_Chinese = "Auto Login";
	private string networkString_Chinese = "無法連接到網絡。\n請檢查您的網絡連接，然後重試。";
	private string noVideosString_Chinese = "沒有視頻";
	private string detailPageString_Chinese = "細節頁面";
	private string streaming2DString_Chinese = "2D流媒體";
	private string streaming3DString_Chinese = "3D流媒體";
	private string watchin2DString_Chinese = "觀看2D";
	private string watchin3DString_Chinese = "觀看3D";
	private string informationString_Chinese = "信息";
	private string contentIDString_Chinese = "內容ID";
	private string releaseString_Chinese = "發布";
	private string playtimeString_Chinese = "上場時間";
	private string genreString_Chinese = "類型";
	private string Go_to_inbox_after_downloadString_Chinese = "下載後轉到收件箱";
	private string settingsString_Chinese = "設置";
	private string loginString_Chinese = "登錄";
	private string loginInformationString_Chinese = "登錄信息";
	private string keep_logged_inString_Chinese = "保持登錄狀態";
	private string mobile_network_usage_notificationString_Chinese = "移動網絡使用通知";
	private string confirm_when_using_mobile_network_dataString_Chinese = "使用移動網絡數據時確認";
	private string push_notification_settingsString_Chinese = "推送通知設置";
	private string receive_PUSH_notificationString_Chinese = "接收推送通知";
	private string usable_capacityString_Chinese = "可用容量";
	private string welcomeString_Chinese = "歡迎";
	private string experience_realistic_virtual_realityString_Chinese = "使用VRUdon體驗逼真的虛擬現實現實主義";
	private string connect_your_mobile_phone_to_your_deviceString_Chinese = "將手機連接到您的設備";
	private string run_vr_PlayerString_Chinese = "運行VR播放器";
	private string sign_in_completedString_Chinese = "登錄完成";
	private string logged_outString_Chinese = "登出";
	private string press_the_previous_button_again_to_exitString_Chinese = "再次按上一個按鈕退出。";
	#endregion

	#region VR_ScreenCursor

	private string Hover_the_on_screenString_Chinese = "將屏幕上的光標懸停3秒鐘以啟動播放器";
	private string Precautions_AvoidString_Chinese = "注意事項\n避免長時間使用。如果您出現任何異常症狀，如眼睛疲勞或暈車，\n立即停止使用該設備。";

	#endregion

	#region VR_MainMenu

	private string vr_Left_PhoneVideoString_Chinese = "電話ビデオ";
	private string vr_Left_DownloadString_Chinese = "ダウンロード";
	private string vr_Left_MyVideoString_Chinese = "私のビデオ";
	private string vr_Mid_MyStorageString_Chinese = "私のストレージ";
	private string vr_Mid_InboxString_Chinese = "受信トレイ";
	private string vr_Mid_VideoListString_Chinese = "動画リスト";
	private string vr_Mid_FavoriteString_Chinese = "気に入り";
	private string vr_LoginrequiredString_Chinese = "要求登錄";
	private string vr_Move_to_loginString_Chinese = "轉到登錄頁面？";
	private string vr_YesString_Chinese = "是";
	private string vr_NoString_Chinese = "沒有";
	private string vr_DeleterequiredString_Chinese = "需要刪除";
	private string vr_DeleteString_Chinese = "刪除?";
	private string vr_Please_check_the_network_connectionString_Chinese = "請檢查網絡連接";
	private string vr_NovideosString_Chinese = "沒有視頻";
	private string vr_StreamingrequiredString_Chinese = "需要流媒體";
	private string vr_StreamingString_Chinese = "流?";

	#endregion

	#region VR_Settings

	private string vr_VideoString_Chinese = "視頻";
	private string vr_ScreenString_Chinese = "屏幕";
	private string vr_2DString_Chinese = "2D";
	private string vr_ListString_Chinese = "名單";
	private string vr_CloseString_Chinese = "關";
	private string vr_SettingsString_Chinese = "設置";
	private string vr_ScreenLockString_Chinese = "屏幕鎖";
	private string vr_ModeString_Chinese = "模式";
	private string vr_FlatString_Chinese = "平面";
	private string vr_CinemaString_Chinese = "電影";
	private string vr_AutoString_Chinese = "汽車";
	private string vr_SizeString_Chinese = "尺寸";
	private string vr_RatioString_Chinese = "比";
	private string vr_Are_you_sure_you_deviceString_Chinese = "您確定要將手機與VR設備斷開連接嗎？";

	#endregion


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
	[SerializeField] private Text loginMenu_AutoLogin = null;
	[SerializeField] private Text loginMenu_Loginbnt = null;
	[SerializeField] private Text loginMenu_Network = null;

	[Header("---- StorageMenu ----")]
	[SerializeField] private Text storageMenu_Header = null;
	[SerializeField] private Text storageMenu_Novideos = null;
	[SerializeField] private Text storageMenu_Network = null;

	[Header("---- MyVideoMenu ----")]
	[SerializeField] private Text myVideoMenu_Header = null;
	[SerializeField] private Text myVideoMenu_Novideos = null;
	[SerializeField] private Text myVideoMenu_Network = null;

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
	[SerializeField] private Text detailMenu_Network = null;

	[Header("---- FavoriteMenu ----")]
	[SerializeField] private Text favoriteMenu_Header = null;
	[SerializeField] private Text favoriteMenu_Novideos = null;
	[SerializeField] private Text favoriteMenu_Network = null;

	[Header("---- DownloadMenu ----")]
	[SerializeField] private Text downloadMenu_Header = null;
	[SerializeField] private Text downloadMenu_Novideos = null;
	[SerializeField] private Text downloadMenu_Network = null;

	[Header("---- InboxMenu ----")]
	[SerializeField] private Text inboxMenu_Header = null;
	[SerializeField] private Text inboxMenu_Novideos = null;
	[SerializeField] private Text inboxMenu_Network = null;

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
	[SerializeField] private Text settingMenu_Network = null;

	[Header("---- VRPlayerMenu ----")]
	[SerializeField] private Text vrPlayerMenu_Welcome = null;
	[SerializeField] private Text vrPlayerMenu_Top = null;
	[SerializeField] private Text vrPlayerMenu_Bottom = null;
	[SerializeField] private Text vrPlayerMenu_RunVRBnt = null;

	[Header("---- AlertMenu ----")]
	[SerializeField] private Text alertMenu_Login = null;
	[SerializeField] private Text alertMenu_Logout = null;
	[SerializeField] private Text alertMenu_Exit = null;

	[Header("---- VR_ScreenCursor ----")]
	[SerializeField] private Text vr_ScreenCursor_3second = null;
	[SerializeField] private Text vr_ScreenCursor_Precautions = null;

	[Header("---- VR_MainMenu ----")]
	[SerializeField] private Text vr_MainMenu_Left_PhoneVideoTitle = null;
	[SerializeField] private Text vr_MainMenu_Left_DownloadTitle = null;
	[SerializeField] private Text vr_MainMenu_Left_MyVideoTitle = null;
	[SerializeField] private Text vr_MainMenu_Mid_MyStorageTitle = null;
	[SerializeField] private Text vr_MainMenu_Mid_InboxTitle = null;
	[SerializeField] private Text vr_MainMenu_Mid_VideoListTitle = null;
	[SerializeField] private Text vr_MainMenu_Mid_FavoriteTitle = null;

	[SerializeField] private Text vr_MainMenu_LoginAlertTitle = null;
	[SerializeField] private Text vr_MainMenu_LoginAlertContent = null;
	[SerializeField] private Text vr_MainMenu_LoginAlertYesButton = null;
	[SerializeField] private Text vr_MainMenu_LoginAlertNoButton = null;

	[SerializeField] private Text vr_MainMenu_VR_DeleteAlertTitle = null;
	[SerializeField] private Text vr_MainMenu_VR_DeleteAlertContent = null;
	[SerializeField] private Text vr_MainMenu_VR_DeleteAlertYesButton = null;
	[SerializeField] private Text vr_MainMenu_VR_DeleteAlertNoButton = null;

	[SerializeField] private Text vr_MainMenu_VR_NetworkConnection = null;

	[SerializeField] private Text vr_MainMenu_VR_Novideos = null;

	[SerializeField] private Text vr_MainMenu_VR_StreamingTitle = null;
	[SerializeField] private Text vr_MainMenu_VR_StreamingContent = null;
	[SerializeField] private Text vr_MainMenu_VR_StreamingYesButton = null;
	[SerializeField] private Text vr_MainMenu_VR_StreamingNoButton = null;

	[Header("---- VR_Settings ----")]

	[SerializeField] private Text vr_Settings_Top_VR_VideoTitle = null;
	[SerializeField] private Text vr_Settings_Top_VR_ScreenTitle = null;
	[SerializeField] private Text vr_Settings_Top_VR_2DTitle = null;
	[SerializeField] private Text vr_Settings_Top_VR_ListTitle = null;
	[SerializeField] private Text vr_Settings_Top_VR_CloseTitle = null;

	[SerializeField] private Text vr_Settings_Bottom_VideoSetting_SettingsTitle = null;
	[SerializeField] private Text vr_Settings_Bottom_VideoSetting_ScreenLockTitle = null;

	[SerializeField] private Text vr_Settings_Bottom_ScreenSetting_ModeSetting_ModeTitle = null;
	[SerializeField] private Text vr_Settings_Bottom_ScreenSetting_ModeSetting_FlatTitle = null;
	[SerializeField] private Text vr_Settings_Bottom_ScreenSetting_ModeSetting_CinemaTitle = null;
	[SerializeField] private Text vr_Settings_Bottom_ScreenSetting_ModeSetting_AutoTitle = null;
	[SerializeField] private Text vr_Settings_Bottom_ScreenSetting_Settings_SettingsTitle = null;
	[SerializeField] private Text vr_Settings_Bottom_ScreenSetting_Settings_FlatTitle = null;
	[SerializeField] private Text vr_Settings_Bottom_ScreenSetting_Size_sizeTitle = null;
	[SerializeField] private Text vr_Settings_Bottom_ScreenSetting_Ratio_RatioTitle = null;

	[SerializeField] private Text vr_Settings_Bottom_Alert2DBnt_Alert2DTitle = null;
	[SerializeField] private Text vr_Settings_Bottom_Alert2DBnt_YesButton = null;
	[SerializeField] private Text vr_Settings_Bottom_Alert2DBnt_NoButton = null;

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

		switch (Application.systemLanguage) 
		{
		case SystemLanguage.English: //This checks if your computer's operating system is in the English language
			Debug.Log ("This system is in English............................................");

			SetEnglishLanguage ();
			break;
		
		case SystemLanguage.Korean: //Otherwise, if the system is Korean
			Debug.Log ("This system is in Korean.............................................");

			SetKoreaLanguage ();
			break;
		case SystemLanguage.Japanese: //Otherwise, if the system is Japanese
			Debug.Log ("This system is in Japanese...........................................");

			SetJapaneseLanguage ();
			break;
		case SystemLanguage.Chinese: //Otherwise, if the system is Chinese
			Debug.Log ("This system is in Chinese.............................................");

			SetChineseLanguage ();
			break;
		default:
			Debug.Log ("This system is in other language......................................");
			SetEnglishLanguage ();
			break;
		}
		
//		//This checks if your computer's operating system is in the English language
//		if (Application.systemLanguage == SystemLanguage.English) {
//			Debug.Log ("This system is in English............................................");
//
//			SetEnglishLanguage ();
//		}
//
//		//Otherwise, if the system is Korean
//		else if (Application.systemLanguage == SystemLanguage.Korean) {
//			Debug.Log ("This system is in Korean.............................................");
//
//			SetKoreaLanguage ();
//		}
//
//		//Otherwise, if the system is Japanese
//		else if (Application.systemLanguage == SystemLanguage.Japanese) {
//			Debug.Log ("This system is in Japanese...........................................");
//
//			SetJapaneseLanguage ();
//		}
//
//		//Otherwise, if the system is Chinese
//		else if (Application.systemLanguage == SystemLanguage.Chinese) {
//			Debug.Log ("This system is in Chinese.............................................");
//
//			SetChineseLanguage ();
//		} 
//
//		else {
//			Debug.Log ("This system is in other language......................................");
//			SetEnglishLanguage ();
//		}
	}

	#region EnglishLanguage

	private void SetEnglishLanguage_AccessMenu(){
		DisplayValueText (accessMenu_PhoneVideoTitle,phoneVideoString_English);
		DisplayValueText (accessMenu_MyStorageName,myStorageString_English);
		DisplayValueText (accessMenu_MyVideoTitle,myvideoString_English);
		DisplayValueText (accessMenu_Videolistname,videoListString_English);
		DisplayValueText (accessMenu_Favoritename,favoriteString_English);
		DisplayValueText (accessMenu_DownloadTitle,downloadString_English);
		DisplayValueText (accessMenu_DownloadName,downloadString_English);
		DisplayValueText (accessMenu_Inboxname,inboxString_English);
		DisplayValueText (accessMenu_EtcTitle,etcString_English);
		DisplayValueText (accessMenu_PreferencesName,preferencesString_English);
		DisplayValueText (accessMenu_UsageInformationName,usageInformationString_English);
		DisplayValueText (accessMenu_VersionName,versionString_English);
	}

	private void SetEnglishLanguage_LoginMenu(){
		DisplayValueText (loginMenu_Header,lOGINString_English);
		DisplayValueText (loginMenu_AutoLogin,autoLoginString_English);
		DisplayValueText (loginMenu_Loginbnt,lOGINString_English);
		DisplayValueText (loginMenu_Network,networkString_English);
	}

	private void SetEnglishLanguage_StorageMenu(){
		DisplayValueText (storageMenu_Header,myStorageString_English);
		DisplayValueText (storageMenu_Novideos,noVideosString_English);
		DisplayValueText (storageMenu_Network,networkString_English);
	}

	private void SetEnglishLanguage_MyVideoMenu(){
		DisplayValueText (myVideoMenu_Header,myvideoString_English);
		DisplayValueText (myVideoMenu_Novideos,noVideosString_English);
		DisplayValueText (myVideoMenu_Network,networkString_English);
	}

	private void SetEnglishLanguage_DetailMenu(){
		DisplayValueText (detailMenu_Header,detailPageString_English);
		DisplayValueText (detailMenu_UnfavoriteBnt,favoriteString_English);
		DisplayValueText (detailMenu_FavoriteBnt,favoriteString_English);
		DisplayValueText (detailMenu_HaventDownloadBnt,downloadString_English);
		DisplayValueText (detailMenu_Havent2DBnt,streaming2DString_English);
		DisplayValueText (detailMenu_Havent3DBnt,streaming3DString_English);
		DisplayValueText (detailMenu_Downloaded2DBnt,watchin2DString_English);
		DisplayValueText (detailMenu_Downloaded3DBnt,watchin3DString_English);
		DisplayValueText (detailMenu_InformationTitle,informationString_English);
		DisplayValueText (detailMenu_ID,contentIDString_English);
		DisplayValueText (detailMenu_Release,releaseString_English);
		DisplayValueText (detailMenu_Playtime,playtimeString_English);
		DisplayValueText (detailMenu_Genre,genreString_English);
		DisplayValueText (detailMenu_Network,networkString_English);
	}


	private void SetEnglishLanguage_FavoriteMenu(){
		DisplayValueText (favoriteMenu_Header,favoriteString_English);
		DisplayValueText (favoriteMenu_Novideos,noVideosString_English);
		DisplayValueText (favoriteMenu_Network,networkString_English);
	}

	private void SetEnglishLanguage_DownloadMenu(){
		DisplayValueText (downloadMenu_Header,downloadString_English);
		DisplayValueText (downloadMenu_Novideos,Go_to_inbox_after_downloadString_English);
		DisplayValueText (downloadMenu_Network,networkString_English);
	}

	private void SetEnglishLanguage_InboxMenu(){
		DisplayValueText (inboxMenu_Header,inboxString_English);
		DisplayValueText (inboxMenu_Novideos,noVideosString_English);
		DisplayValueText (inboxMenu_Network,networkString_English);
	}

	private void SetEnglishLanguage_SettingMenu(){
		DisplayValueText (settingMenu_Header,settingsString_English);
		DisplayValueText (settingMenu_LoginTitle,loginString_English);
		DisplayValueText (settingMenu_LoginInformation,loginInformationString_English);
		DisplayValueText (settingMenu_Keeploggedin,keep_logged_inString_English);
		DisplayValueText (settingMenu_PreferencesTitle,preferencesString_English);
		DisplayValueText (settingMenu_MobileNetwork,mobile_network_usage_notificationString_English);
		DisplayValueText (settingMenu_MobileNetworkNotice,confirm_when_using_mobile_network_dataString_English);
		DisplayValueText (settingMenu_PushNotificationSettings,push_notification_settingsString_English);
		DisplayValueText (settingMenu_PushNotificationSettingsNotice,receive_PUSH_notificationString_English);
		DisplayValueText (settingMenu_UsableCapacity,usable_capacityString_English);
		DisplayValueText (settingMenu_Network,networkString_English);
	}

	private void SetEnglishLanguage_VRPlayerMenu(){
		DisplayValueText (vrPlayerMenu_Welcome,welcomeString_English);
		DisplayValueText (vrPlayerMenu_Top,experience_realistic_virtual_realityString_English);
		DisplayValueText (vrPlayerMenu_Bottom,connect_your_mobile_phone_to_your_deviceString_English);
		DisplayValueText (vrPlayerMenu_RunVRBnt,run_vr_PlayerString_English);
	}

	private void SetEnglishLanguage_AlertMenu(){
		DisplayValueText (alertMenu_Login,sign_in_completedString_English);
		DisplayValueText (alertMenu_Logout,logged_outString_English);
		DisplayValueText (alertMenu_Exit,press_the_previous_button_again_to_exitString_English);
	}

	private void SetEnglishLanguage_VR_ScreenCursor(){
		DisplayValueText (vr_ScreenCursor_3second,Hover_the_on_screenString_English);
		DisplayValueText (vr_ScreenCursor_Precautions,Precautions_AvoidString_English);
	}

	private void SetEnglishLanguage_VR_MainMenu(){
		DisplayValueText (vr_MainMenu_Left_PhoneVideoTitle,vr_PhoneVideoString_English);
		DisplayValueText (vr_MainMenu_Left_DownloadTitle,vr_DownloadString_English);
		DisplayValueText (vr_MainMenu_Left_MyVideoTitle,vr_MyVideoString_English);

		DisplayValueText (vr_MainMenu_Mid_MyStorageTitle,vr_MyStorageString_English);
		DisplayValueText (vr_MainMenu_Mid_InboxTitle,vr_InboxString_English);
		DisplayValueText (vr_MainMenu_Mid_VideoListTitle,vr_VideoListString_English);
		DisplayValueText (vr_MainMenu_Mid_FavoriteTitle,vr_FavoriteString_English);

		DisplayValueText (vr_MainMenu_LoginAlertTitle,vr_LoginrequiredString_English);
		DisplayValueText (vr_MainMenu_LoginAlertContent,vr_Move_to_loginString_English);
		DisplayValueText (vr_MainMenu_LoginAlertYesButton,vr_YesString_English);
		DisplayValueText (vr_MainMenu_LoginAlertNoButton,vr_NoString_English);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertTitle,vr_DeleterequiredString_English);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertTitle,vr_DeleterequiredString_English);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertContent,vr_DeleteString_English);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertYesButton,vr_YesString_English);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertNoButton,vr_NoString_English);

		DisplayValueText (vr_MainMenu_VR_NetworkConnection,vr_Please_check_the_network_connectionString_English);

		DisplayValueText (vr_MainMenu_VR_Novideos,vr_NovideosString_English);

		DisplayValueText (vr_MainMenu_VR_StreamingTitle,vr_StreamingrequiredString_English);
		DisplayValueText (vr_MainMenu_VR_StreamingContent,vr_StreamingString_English);
		DisplayValueText (vr_MainMenu_VR_StreamingYesButton,vr_YesString_English);
		DisplayValueText (vr_MainMenu_VR_StreamingNoButton,vr_NoString_English);
	}

	private void SetEnglishLanguage_VR_Settings(){
		DisplayValueText (vr_Settings_Top_VR_VideoTitle,vr_VideoString_English);
		DisplayValueText (vr_Settings_Top_VR_ScreenTitle,vr_ScreenString_English);
		DisplayValueText (vr_Settings_Top_VR_2DTitle,vr_2DString_English);
		DisplayValueText (vr_Settings_Top_VR_ListTitle,vr_ListString_English);
		DisplayValueText (vr_Settings_Top_VR_CloseTitle,vr_CloseString_English);
		DisplayValueText (vr_Settings_Bottom_VideoSetting_SettingsTitle,vr_SettingsString_English);
		DisplayValueText (vr_Settings_Bottom_VideoSetting_ScreenLockTitle,vr_ScreenLockString_English);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_ModeSetting_ModeTitle,vr_ModeString_English);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_ModeSetting_FlatTitle,vr_FlatString_English);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_ModeSetting_CinemaTitle,vr_CinemaString_English);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_ModeSetting_AutoTitle,vr_AutoString_English);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_Settings_SettingsTitle,vr_SettingsString_English);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_Settings_FlatTitle,vr_FlatString_English);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_Size_sizeTitle,vr_SizeString_English);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_Ratio_RatioTitle,vr_RatioString_English);
		DisplayValueText (vr_Settings_Bottom_Alert2DBnt_Alert2DTitle,vr_Are_you_sure_you_deviceString_English);
		DisplayValueText (vr_Settings_Bottom_Alert2DBnt_YesButton,vr_YesString_English);
		DisplayValueText (vr_Settings_Bottom_Alert2DBnt_NoButton,vr_NoString_English);
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
		SetEnglishLanguage_VR_ScreenCursor ();
		SetEnglishLanguage_VR_MainMenu ();
		SetEnglishLanguage_VR_Settings ();
	}

	#endregion


	#region KoreaLanguage

	private void SetKoreanLanguage_AccessMenu(){
		DisplayValueText (accessMenu_PhoneVideoTitle,phoneVideoString_Korean);
		DisplayValueText (accessMenu_MyStorageName,myStorageString_Korean);
		DisplayValueText (accessMenu_MyVideoTitle,myvideoString_Korean);
		DisplayValueText (accessMenu_Videolistname,videoListString_Korean);
		DisplayValueText (accessMenu_Favoritename,favoriteString_Korean);
		DisplayValueText (accessMenu_DownloadTitle,downloadString_Korean);
		DisplayValueText (accessMenu_DownloadName,downloadString_Korean);
		DisplayValueText (accessMenu_Inboxname,inboxString_Korean);
		DisplayValueText (accessMenu_EtcTitle,etcString_Korean);
		DisplayValueText (accessMenu_PreferencesName,preferencesString_Korean);
		DisplayValueText (accessMenu_UsageInformationName,usageInformationString_Korean);
		DisplayValueText (accessMenu_VersionName,versionString_Korean);
	}

	private void SetKoreanLanguage_LoginMenu(){
		DisplayValueText (loginMenu_Header,lOGINString_Korean);
		DisplayValueText (loginMenu_AutoLogin,autoLoginString_Korean);
		DisplayValueText (loginMenu_Loginbnt,lOGINString_Korean);
		DisplayValueText (loginMenu_Network,networkString_Korean);
	}

	private void SetKoreanLanguage_StorageMenu(){
		DisplayValueText (storageMenu_Header,myStorageString_Korean);
		DisplayValueText (storageMenu_Novideos,noVideosString_Korean);
		DisplayValueText (storageMenu_Network,networkString_Korean);
	}

	private void SetKoreanLanguage_MyVideoMenu(){
		DisplayValueText (myVideoMenu_Header,myvideoString_Korean);
		DisplayValueText (myVideoMenu_Novideos,noVideosString_Korean);
		DisplayValueText (myVideoMenu_Network,networkString_Korean);
	}

	private void SetKoreanLanguage_DetailMenu(){
		DisplayValueText (detailMenu_Header,detailPageString_Korean);
		DisplayValueText (detailMenu_UnfavoriteBnt,favoriteString_Korean);
		DisplayValueText (detailMenu_FavoriteBnt,favoriteString_Korean);
		DisplayValueText (detailMenu_HaventDownloadBnt,downloadString_Korean);
		DisplayValueText (detailMenu_Havent2DBnt,streaming2DString_Korean);
		DisplayValueText (detailMenu_Havent3DBnt,streaming3DString_Korean);
		DisplayValueText (detailMenu_Downloaded2DBnt,watchin2DString_Korean);
		DisplayValueText (detailMenu_Downloaded3DBnt,watchin3DString_Korean);
		DisplayValueText (detailMenu_InformationTitle,informationString_Korean);
		DisplayValueText (detailMenu_ID,contentIDString_Korean);
		DisplayValueText (detailMenu_Release,releaseString_Korean);
		DisplayValueText (detailMenu_Playtime,playtimeString_Korean);
		DisplayValueText (detailMenu_Genre,genreString_Korean);
		DisplayValueText (detailMenu_Network,networkString_Korean);
	}


	private void SetKoreanLanguage_FavoriteMenu(){
		DisplayValueText (favoriteMenu_Header,favoriteString_Korean);
		DisplayValueText (favoriteMenu_Novideos,noVideosString_Korean);
		DisplayValueText (favoriteMenu_Network,networkString_Korean);
	}

	private void SetKoreanLanguage_DownloadMenu(){
		DisplayValueText (downloadMenu_Header,downloadString_Korean);
		DisplayValueText (downloadMenu_Novideos,Go_to_inbox_after_downloadString_Korean);
		DisplayValueText (downloadMenu_Network,networkString_Korean);
	}

	private void SetKoreanLanguage_InboxMenu(){
		DisplayValueText (inboxMenu_Header,inboxString_Korean);
		DisplayValueText (inboxMenu_Novideos,noVideosString_Korean);
		DisplayValueText (inboxMenu_Network,networkString_Korean);
	}

	private void SetKoreanLanguage_SettingMenu(){
		DisplayValueText (settingMenu_Header,settingsString_Korean);
		DisplayValueText (settingMenu_LoginTitle,loginString_Korean);
		DisplayValueText (settingMenu_LoginInformation,loginInformationString_Korean);
		DisplayValueText (settingMenu_Keeploggedin,keep_logged_inString_Korean);
		DisplayValueText (settingMenu_PreferencesTitle,preferencesString_Korean);
		DisplayValueText (settingMenu_MobileNetwork,mobile_network_usage_notificationString_Korean);
		DisplayValueText (settingMenu_MobileNetworkNotice,confirm_when_using_mobile_network_dataString_Korean);
		DisplayValueText (settingMenu_PushNotificationSettings,push_notification_settingsString_Korean);
		DisplayValueText (settingMenu_PushNotificationSettingsNotice,receive_PUSH_notificationString_Korean);
		DisplayValueText (settingMenu_UsableCapacity,usable_capacityString_Korean);
		DisplayValueText (settingMenu_Network,networkString_Korean);
	}

	private void SetKoreanLanguage_VRPlayerMenu(){
		DisplayValueText (vrPlayerMenu_Welcome,welcomeString_Korean);
		DisplayValueText (vrPlayerMenu_Top,experience_realistic_virtual_realityString_Korean);
		DisplayValueText (vrPlayerMenu_Bottom,connect_your_mobile_phone_to_your_deviceString_Korean);
		DisplayValueText (vrPlayerMenu_RunVRBnt,run_vr_PlayerString_Korean);
	}

	private void SetKoreanLanguage_AlertMenu(){
		DisplayValueText (alertMenu_Login,sign_in_completedString_Korean);
		DisplayValueText (alertMenu_Logout,logged_outString_Korean);
		DisplayValueText (alertMenu_Exit,press_the_previous_button_again_to_exitString_Korean);
	}

	private void SetKoreanLanguage_VR_ScreenCursor(){
		DisplayValueText (vr_ScreenCursor_3second,Hover_the_on_screenString_Korean);
		DisplayValueText (vr_ScreenCursor_Precautions,Precautions_AvoidString_Korean);
	}

	private void SetKoreanLanguage_VR_MainMenu(){
		DisplayValueText (vr_MainMenu_Left_PhoneVideoTitle,vr_Left_PhoneVideoString_Korean);
		DisplayValueText (vr_MainMenu_Left_DownloadTitle,vr_Left_DownloadString_Korean);
		DisplayValueText (vr_MainMenu_Left_MyVideoTitle,vr_Left_MyVideoString_Korean);

		DisplayValueText (vr_MainMenu_Mid_MyStorageTitle,vr_Mid_MyStorageString_Korean);
		DisplayValueText (vr_MainMenu_Mid_InboxTitle,vr_Mid_InboxString_Korean);
		DisplayValueText (vr_MainMenu_Mid_VideoListTitle,vr_Mid_VideoListString_Korean);
		DisplayValueText (vr_MainMenu_Mid_FavoriteTitle,vr_Mid_FavoriteString_Korean);

		DisplayValueText (vr_MainMenu_LoginAlertTitle,vr_LoginrequiredString_Korean);
		DisplayValueText (vr_MainMenu_LoginAlertContent,vr_Move_to_loginString_Korean);
		DisplayValueText (vr_MainMenu_LoginAlertYesButton,vr_YesString_Korean);
		DisplayValueText (vr_MainMenu_LoginAlertNoButton,vr_NoString_Korean);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertTitle,vr_DeleterequiredString_Korean);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertTitle,vr_DeleterequiredString_Korean);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertContent,vr_DeleteString_Korean);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertYesButton,vr_YesString_Korean);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertNoButton,vr_NoString_Korean);

		DisplayValueText (vr_MainMenu_VR_NetworkConnection,vr_Please_check_the_network_connectionString_Korean);

		DisplayValueText (vr_MainMenu_VR_Novideos,vr_NovideosString_Korean);

		DisplayValueText (vr_MainMenu_VR_StreamingTitle,vr_StreamingrequiredString_Korean);
		DisplayValueText (vr_MainMenu_VR_StreamingContent,vr_StreamingString_Korean);
		DisplayValueText (vr_MainMenu_VR_StreamingYesButton,vr_YesString_Korean);
		DisplayValueText (vr_MainMenu_VR_StreamingNoButton,vr_NoString_Korean);
	}

	private void SetKoreanLanguage_VR_Settings(){
		DisplayValueText (vr_Settings_Top_VR_VideoTitle,vr_VideoString_Korean);
		DisplayValueText (vr_Settings_Top_VR_ScreenTitle,vr_ScreenString_Korean);
		DisplayValueText (vr_Settings_Top_VR_2DTitle,vr_2DString_Korean);
		DisplayValueText (vr_Settings_Top_VR_ListTitle,vr_ListString_Korean);
		DisplayValueText (vr_Settings_Top_VR_CloseTitle,vr_CloseString_Korean);
		DisplayValueText (vr_Settings_Bottom_VideoSetting_SettingsTitle,vr_SettingsString_Korean);
		DisplayValueText (vr_Settings_Bottom_VideoSetting_ScreenLockTitle,vr_ScreenLockString_Korean);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_ModeSetting_ModeTitle,vr_ModeString_Korean);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_ModeSetting_FlatTitle,vr_FlatString_Korean);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_ModeSetting_CinemaTitle,vr_CinemaString_Korean);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_ModeSetting_AutoTitle,vr_AutoString_Korean);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_Settings_SettingsTitle,vr_SettingsString_Korean);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_Settings_FlatTitle,vr_FlatString_Korean);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_Size_sizeTitle,vr_SizeString_Korean);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_Ratio_RatioTitle,vr_RatioString_Korean);
		DisplayValueText (vr_Settings_Bottom_Alert2DBnt_Alert2DTitle,vr_Are_you_sure_you_deviceString_Korean);
		DisplayValueText (vr_Settings_Bottom_Alert2DBnt_YesButton,vr_YesString_Korean);
		DisplayValueText (vr_Settings_Bottom_Alert2DBnt_NoButton,vr_NoString_Korean);
	}

	private void SetKoreaLanguage(){
		SetKoreanLanguage_AccessMenu ();
		SetKoreanLanguage_LoginMenu ();
		SetKoreanLanguage_StorageMenu ();
		SetKoreanLanguage_MyVideoMenu ();
		SetKoreanLanguage_DetailMenu ();
		SetKoreanLanguage_FavoriteMenu ();
		SetKoreanLanguage_DownloadMenu ();
		SetKoreanLanguage_InboxMenu ();
		SetKoreanLanguage_SettingMenu ();
		SetKoreanLanguage_VRPlayerMenu ();
		SetKoreanLanguage_AlertMenu ();
		SetKoreanLanguage_VR_ScreenCursor ();
		SetKoreanLanguage_VR_MainMenu ();
		SetKoreanLanguage_VR_Settings ();
	}

	#endregion


	#region JapaneseLanguage

	private void SetJapaneseLanguage_AccessMenu(){
		DisplayValueText (accessMenu_PhoneVideoTitle,phoneVideoString_Japanese);
		DisplayValueText (accessMenu_MyStorageName,myStorageString_Japanese);
		DisplayValueText (accessMenu_MyVideoTitle,myvideoString_Japanese);
		DisplayValueText (accessMenu_Videolistname,videoListString_Japanese);
		DisplayValueText (accessMenu_Favoritename,favoriteString_Japanese);
		DisplayValueText (accessMenu_DownloadTitle,downloadString_Japanese);
		DisplayValueText (accessMenu_DownloadName,downloadString_Japanese);
		DisplayValueText (accessMenu_Inboxname,inboxString_Japanese);
		DisplayValueText (accessMenu_EtcTitle,etcString_Japanese);
		DisplayValueText (accessMenu_PreferencesName,preferencesString_Japanese);
		DisplayValueText (accessMenu_UsageInformationName,usageInformationString_Japanese);
		DisplayValueText (accessMenu_VersionName,versionString_Japanese);
	}

	private void SetJapaneseLanguage_LoginMenu(){
		DisplayValueText (loginMenu_Header,lOGINString_Japanese);
		DisplayValueText (loginMenu_AutoLogin,autoLoginString_Japanese);
		DisplayValueText (loginMenu_Loginbnt,lOGINString_Japanese);
		DisplayValueText (loginMenu_Network,networkString_Japanese);
	}

	private void SetJapaneseLanguage_StorageMenu(){
		DisplayValueText (storageMenu_Header,myStorageString_Japanese);
		DisplayValueText (storageMenu_Novideos,noVideosString_Japanese);
		DisplayValueText (storageMenu_Network,networkString_Japanese);
	}

	private void SetJapaneseLanguage_MyVideoMenu(){
		DisplayValueText (myVideoMenu_Header,myvideoString_Japanese);
		DisplayValueText (myVideoMenu_Novideos,noVideosString_Japanese);
		DisplayValueText (myVideoMenu_Network,networkString_Japanese);
	}

	private void SetJapaneseLanguage_DetailMenu(){
		DisplayValueText (detailMenu_Header,detailPageString_Japanese);
		DisplayValueText (detailMenu_UnfavoriteBnt,favoriteString_Japanese);
		DisplayValueText (detailMenu_FavoriteBnt,favoriteString_Japanese);
		DisplayValueText (detailMenu_HaventDownloadBnt,downloadString_Japanese);
		DisplayValueText (detailMenu_Havent2DBnt,streaming2DString_Japanese);
		DisplayValueText (detailMenu_Havent3DBnt,streaming3DString_Japanese);
		DisplayValueText (detailMenu_Downloaded2DBnt,watchin2DString_Japanese);
		DisplayValueText (detailMenu_Downloaded3DBnt,watchin3DString_Japanese);
		DisplayValueText (detailMenu_InformationTitle,informationString_Japanese);
		DisplayValueText (detailMenu_ID,contentIDString_Japanese);
		DisplayValueText (detailMenu_Release,releaseString_Japanese);
		DisplayValueText (detailMenu_Playtime,playtimeString_Japanese);
		DisplayValueText (detailMenu_Genre,genreString_Japanese);
		DisplayValueText (detailMenu_Network,networkString_Japanese);
	}


	private void SetJapaneseLanguage_FavoriteMenu(){
		DisplayValueText (favoriteMenu_Header,favoriteString_Japanese);
		DisplayValueText (favoriteMenu_Novideos,noVideosString_Japanese);
		DisplayValueText (favoriteMenu_Network,networkString_Japanese);
	}

	private void SetJapaneseLanguage_DownloadMenu(){
		DisplayValueText (downloadMenu_Header,downloadString_Japanese);
		DisplayValueText (downloadMenu_Novideos,Go_to_inbox_after_downloadString_Japanese);
		DisplayValueText (downloadMenu_Network,networkString_Japanese);
	}

	private void SetJapaneseLanguage_InboxMenu(){
		DisplayValueText (inboxMenu_Header,inboxString_Japanese);
		DisplayValueText (inboxMenu_Novideos,noVideosString_Japanese);
		DisplayValueText (inboxMenu_Network,networkString_Japanese);
	}

	private void SetJapaneseLanguage_SettingMenu(){
		DisplayValueText (settingMenu_Header,settingsString_Japanese);
		DisplayValueText (settingMenu_LoginTitle,loginString_Japanese);
		DisplayValueText (settingMenu_LoginInformation,loginInformationString_Japanese);
		DisplayValueText (settingMenu_Keeploggedin,keep_logged_inString_Japanese);
		DisplayValueText (settingMenu_PreferencesTitle,preferencesString_Japanese);
		DisplayValueText (settingMenu_MobileNetwork,mobile_network_usage_notificationString_Japanese);
		DisplayValueText (settingMenu_MobileNetworkNotice,confirm_when_using_mobile_network_dataString_Japanese);
		DisplayValueText (settingMenu_PushNotificationSettings,push_notification_settingsString_Japanese);
		DisplayValueText (settingMenu_PushNotificationSettingsNotice,receive_PUSH_notificationString_Japanese);
		DisplayValueText (settingMenu_UsableCapacity,usable_capacityString_Japanese);
		DisplayValueText (settingMenu_Network,networkString_Japanese);
	}

	private void SetJapaneseLanguage_VRPlayerMenu(){
		DisplayValueText (vrPlayerMenu_Welcome,welcomeString_Japanese);
		DisplayValueText (vrPlayerMenu_Top,experience_realistic_virtual_realityString_Japanese);
		DisplayValueText (vrPlayerMenu_Bottom,connect_your_mobile_phone_to_your_deviceString_Japanese);
		DisplayValueText (vrPlayerMenu_RunVRBnt,run_vr_PlayerString_Japanese);
	}

	private void SetJapaneseLanguage_AlertMenu(){
		DisplayValueText (alertMenu_Login,sign_in_completedString_Japanese);
		DisplayValueText (alertMenu_Logout,keep_logged_inString_Japanese);
		DisplayValueText (alertMenu_Exit,press_the_previous_button_again_to_exitString_Japanese);
	}

	private void SetJapaneseLanguage_VR_ScreenCursor(){
		DisplayValueText (vr_ScreenCursor_3second,Hover_the_on_screenString_Japanese);
		DisplayValueText (vr_ScreenCursor_Precautions,Precautions_AvoidString_Japanese);
	}

	private void SetJapaneseLanguage_VR_MainMenu(){
		DisplayValueText (vr_MainMenu_Left_PhoneVideoTitle,vr_Left_PhoneVideoString_Japanese);
		DisplayValueText (vr_MainMenu_Left_DownloadTitle,vr_Left_DownloadString_Japanese);
		DisplayValueText (vr_MainMenu_Left_MyVideoTitle,vr_Left_MyVideoString_Japanese);

		DisplayValueText (vr_MainMenu_Mid_MyStorageTitle,vr_Mid_MyStorageString_Japanese);
		DisplayValueText (vr_MainMenu_Mid_InboxTitle,vr_Mid_InboxString_Japanese);
		DisplayValueText (vr_MainMenu_Mid_VideoListTitle,vr_Mid_VideoListString_Japanese);
		DisplayValueText (vr_MainMenu_Mid_FavoriteTitle,vr_Mid_FavoriteString_Japanese);

		DisplayValueText (vr_MainMenu_LoginAlertTitle,vr_LoginrequiredString_Japanese);
		DisplayValueText (vr_MainMenu_LoginAlertContent,vr_Move_to_loginString_Japanese);
		DisplayValueText (vr_MainMenu_LoginAlertYesButton,vr_YesString_Japanese);
		DisplayValueText (vr_MainMenu_LoginAlertNoButton,vr_NoString_Japanese);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertTitle,vr_DeleterequiredString_Japanese);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertTitle,vr_DeleterequiredString_Japanese);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertContent,vr_DeleteString_Japanese);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertYesButton,vr_YesString_Japanese);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertNoButton,vr_NoString_Japanese);

		DisplayValueText (vr_MainMenu_VR_NetworkConnection,vr_Please_check_the_network_connectionString_Japanese);

		DisplayValueText (vr_MainMenu_VR_Novideos,vr_NovideosString_Japanese);

		DisplayValueText (vr_MainMenu_VR_StreamingTitle,vr_StreamingrequiredString_Japanese);
		DisplayValueText (vr_MainMenu_VR_StreamingContent,vr_StreamingString_Japanese);
		DisplayValueText (vr_MainMenu_VR_StreamingYesButton,vr_YesString_Japanese);
		DisplayValueText (vr_MainMenu_VR_StreamingNoButton,vr_NoString_Japanese);
	}

	private void SetJapaneseLanguage_VR_Settings(){
		DisplayValueText (vr_Settings_Top_VR_VideoTitle,vr_VideoString_Japanese);
		DisplayValueText (vr_Settings_Top_VR_ScreenTitle,vr_ScreenString_Japanese);
		DisplayValueText (vr_Settings_Top_VR_2DTitle,vr_2DString_Japanese);
		DisplayValueText (vr_Settings_Top_VR_ListTitle,vr_ListString_Japanese);
		DisplayValueText (vr_Settings_Top_VR_CloseTitle,vr_CloseString_Japanese);
		DisplayValueText (vr_Settings_Bottom_VideoSetting_SettingsTitle,vr_SettingsString_Japanese);
		DisplayValueText (vr_Settings_Bottom_VideoSetting_ScreenLockTitle,vr_ScreenLockString_Japanese);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_ModeSetting_ModeTitle,vr_ModeString_Japanese);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_ModeSetting_FlatTitle,vr_FlatString_Japanese);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_ModeSetting_CinemaTitle,vr_CinemaString_Japanese);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_ModeSetting_AutoTitle,vr_AutoString_Japanese);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_Settings_SettingsTitle,vr_SettingsString_Japanese);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_Settings_FlatTitle,vr_FlatString_Japanese);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_Size_sizeTitle,vr_SizeString_Japanese);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_Ratio_RatioTitle,vr_RatioString_Japanese);
		DisplayValueText (vr_Settings_Bottom_Alert2DBnt_Alert2DTitle,vr_Are_you_sure_you_deviceString_Japanese);
		DisplayValueText (vr_Settings_Bottom_Alert2DBnt_YesButton,vr_YesString_Japanese);
		DisplayValueText (vr_Settings_Bottom_Alert2DBnt_NoButton,vr_NoString_Japanese);
	}


	private void SetJapaneseLanguage(){
		SetJapaneseLanguage_AccessMenu ();
		SetJapaneseLanguage_LoginMenu ();
		SetJapaneseLanguage_StorageMenu ();
		SetJapaneseLanguage_MyVideoMenu ();
		SetJapaneseLanguage_DetailMenu ();
		SetJapaneseLanguage_FavoriteMenu ();
		SetJapaneseLanguage_DownloadMenu ();
		SetJapaneseLanguage_InboxMenu ();
		SetJapaneseLanguage_SettingMenu ();
		SetJapaneseLanguage_VRPlayerMenu ();
		SetJapaneseLanguage_AlertMenu ();
		SetJapaneseLanguage_VR_ScreenCursor ();
		SetJapaneseLanguage_VR_MainMenu ();
		SetJapaneseLanguage_VR_Settings ();
	}

	#endregion


	#region ChineseLanguage

	private void SetChineseLanguage_AccessMenu(){
		DisplayValueText (accessMenu_PhoneVideoTitle,phoneVideoString_Chinese);
		DisplayValueText (accessMenu_MyStorageName,myStorageString_Chinese);
		DisplayValueText (accessMenu_MyVideoTitle,myvideoString_Chinese);
		DisplayValueText (accessMenu_Videolistname,videoListString_Chinese);
		DisplayValueText (accessMenu_Favoritename,favoriteString_Chinese);
		DisplayValueText (accessMenu_DownloadTitle,downloadString_Chinese);
		DisplayValueText (accessMenu_DownloadName,downloadString_Chinese);
		DisplayValueText (accessMenu_Inboxname,inboxString_Chinese);
		DisplayValueText (accessMenu_EtcTitle,etcString_Chinese);
		DisplayValueText (accessMenu_PreferencesName,preferencesString_Chinese);
		DisplayValueText (accessMenu_UsageInformationName,usageInformationString_Chinese);
		DisplayValueText (accessMenu_VersionName,versionString_Chinese);
	}

	private void SetChineseLanguage_LoginMenu(){
		DisplayValueText (loginMenu_Header,lOGINString_Chinese);
		DisplayValueText (loginMenu_AutoLogin,autoLoginString_Chinese);
		DisplayValueText (loginMenu_Loginbnt,lOGINString_Chinese);
		DisplayValueText (loginMenu_Network,networkString_Chinese);
	}

	private void SetChineseLanguage_StorageMenu(){
		DisplayValueText (storageMenu_Header,myStorageString_Chinese);
		DisplayValueText (storageMenu_Novideos,noVideosString_Chinese);
		DisplayValueText (storageMenu_Network,networkString_Chinese);
	}

	private void SetChineseLanguage_MyVideoMenu(){
		DisplayValueText (myVideoMenu_Header,myvideoString_Chinese);
		DisplayValueText (myVideoMenu_Novideos,noVideosString_Chinese);
		DisplayValueText (myVideoMenu_Network,networkString_Chinese);
	}

	private void SetChineseLanguage_DetailMenu(){
		DisplayValueText (detailMenu_Header,detailPageString_Chinese);
		DisplayValueText (detailMenu_UnfavoriteBnt,favoriteString_Chinese);
		DisplayValueText (detailMenu_FavoriteBnt,favoriteString_Chinese);
		DisplayValueText (detailMenu_HaventDownloadBnt,downloadString_Chinese);
		DisplayValueText (detailMenu_Havent2DBnt,streaming2DString_Chinese);
		DisplayValueText (detailMenu_Havent3DBnt,streaming3DString_Chinese);
		DisplayValueText (detailMenu_Downloaded2DBnt,watchin2DString_Chinese);
		DisplayValueText (detailMenu_Downloaded3DBnt,watchin3DString_Chinese);
		DisplayValueText (detailMenu_InformationTitle,informationString_Chinese);
		DisplayValueText (detailMenu_ID,contentIDString_Chinese);
		DisplayValueText (detailMenu_Release,releaseString_Chinese);
		DisplayValueText (detailMenu_Playtime,playtimeString_Chinese);
		DisplayValueText (detailMenu_Genre,genreString_Chinese);
		DisplayValueText (detailMenu_Network,networkString_Chinese);
	}


	private void SetChineseLanguage_FavoriteMenu(){
		DisplayValueText (favoriteMenu_Header,favoriteString_Chinese);
		DisplayValueText (favoriteMenu_Novideos,noVideosString_Chinese);
		DisplayValueText (favoriteMenu_Network,networkString_Chinese);
	}

	private void SetChineseLanguage_DownloadMenu(){
		DisplayValueText (downloadMenu_Header,downloadString_Chinese);
		DisplayValueText (downloadMenu_Novideos,Go_to_inbox_after_downloadString_Chinese);
		DisplayValueText (downloadMenu_Network,networkString_Chinese);
	}

	private void SetChineseLanguage_InboxMenu(){
		DisplayValueText (inboxMenu_Header,inboxString_Chinese);
		DisplayValueText (inboxMenu_Novideos,noVideosString_Chinese);
		DisplayValueText (inboxMenu_Network,networkString_Chinese);
	}

	private void SetChineseLanguage_SettingMenu(){
		DisplayValueText (settingMenu_Header,settingsString_Chinese);
		DisplayValueText (settingMenu_LoginTitle,loginString_Chinese);
		DisplayValueText (settingMenu_LoginInformation,loginInformationString_Chinese);
		DisplayValueText (settingMenu_Keeploggedin,keep_logged_inString_Chinese);
		DisplayValueText (settingMenu_PreferencesTitle,preferencesString_Chinese);
		DisplayValueText (settingMenu_MobileNetwork,mobile_network_usage_notificationString_Chinese);
		DisplayValueText (settingMenu_MobileNetworkNotice,confirm_when_using_mobile_network_dataString_Chinese);
		DisplayValueText (settingMenu_PushNotificationSettings,push_notification_settingsString_Chinese);
		DisplayValueText (settingMenu_PushNotificationSettingsNotice,receive_PUSH_notificationString_Chinese);
		DisplayValueText (settingMenu_UsableCapacity,usable_capacityString_Chinese);
		DisplayValueText (settingMenu_Network,networkString_Chinese);
	}

	private void SetChineseLanguage_VRPlayerMenu(){
		DisplayValueText (vrPlayerMenu_Welcome,welcomeString_Chinese);
		DisplayValueText (vrPlayerMenu_Top,experience_realistic_virtual_realityString_Chinese);
		DisplayValueText (vrPlayerMenu_Bottom,connect_your_mobile_phone_to_your_deviceString_Chinese);
		DisplayValueText (vrPlayerMenu_RunVRBnt,run_vr_PlayerString_Chinese);
	}

	private void SetChineseLanguage_AlertMenu(){
		DisplayValueText (alertMenu_Login,sign_in_completedString_Chinese);
		DisplayValueText (alertMenu_Logout,logged_outString_Chinese);
		DisplayValueText (alertMenu_Exit,press_the_previous_button_again_to_exitString_Chinese);
	}

	private void SetChineseLanguage_VR_ScreenCursor(){
		DisplayValueText (vr_ScreenCursor_3second,Hover_the_on_screenString_Chinese);
		DisplayValueText (vr_ScreenCursor_Precautions,Precautions_AvoidString_Chinese);
	}

	private void SetChineseLanguage_VR_MainMenu(){
		DisplayValueText (vr_MainMenu_Left_PhoneVideoTitle,vr_Left_PhoneVideoString_Chinese);
		DisplayValueText (vr_MainMenu_Left_DownloadTitle,vr_Left_DownloadString_Chinese);
		DisplayValueText (vr_MainMenu_Left_MyVideoTitle,vr_Left_MyVideoString_Chinese);

		DisplayValueText (vr_MainMenu_Mid_MyStorageTitle,vr_Mid_MyStorageString_Chinese);
		DisplayValueText (vr_MainMenu_Mid_InboxTitle,vr_Mid_InboxString_Chinese);
		DisplayValueText (vr_MainMenu_Mid_VideoListTitle,vr_Mid_VideoListString_Chinese);
		DisplayValueText (vr_MainMenu_Mid_FavoriteTitle,vr_Mid_FavoriteString_Chinese);

		DisplayValueText (vr_MainMenu_LoginAlertTitle,vr_LoginrequiredString_Chinese);
		DisplayValueText (vr_MainMenu_LoginAlertContent,vr_Move_to_loginString_Chinese);
		DisplayValueText (vr_MainMenu_LoginAlertYesButton,vr_YesString_Chinese);
		DisplayValueText (vr_MainMenu_LoginAlertNoButton,vr_NoString_Chinese);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertTitle,vr_DeleterequiredString_Chinese);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertTitle,vr_DeleterequiredString_Chinese);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertContent,vr_DeleteString_Chinese);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertYesButton,vr_YesString_Chinese);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertNoButton,vr_NoString_Chinese);

		DisplayValueText (vr_MainMenu_VR_NetworkConnection,vr_Please_check_the_network_connectionString_Chinese);

		DisplayValueText (vr_MainMenu_VR_Novideos,vr_NovideosString_Chinese);

		DisplayValueText (vr_MainMenu_VR_StreamingTitle,vr_StreamingrequiredString_Chinese);
		DisplayValueText (vr_MainMenu_VR_StreamingContent,vr_StreamingString_Chinese);
		DisplayValueText (vr_MainMenu_VR_StreamingYesButton,vr_YesString_Chinese);
		DisplayValueText (vr_MainMenu_VR_StreamingNoButton,vr_NoString_Chinese);
	}

	private void SetChineseLanguage_VR_Settings(){
		DisplayValueText (vr_Settings_Top_VR_VideoTitle,vr_VideoString_Chinese);
		DisplayValueText (vr_Settings_Top_VR_ScreenTitle,vr_ScreenString_Chinese);
		DisplayValueText (vr_Settings_Top_VR_2DTitle,vr_2DString_Chinese);
		DisplayValueText (vr_Settings_Top_VR_ListTitle,vr_ListString_Chinese);
		DisplayValueText (vr_Settings_Top_VR_CloseTitle,vr_CloseString_Chinese);
		DisplayValueText (vr_Settings_Bottom_VideoSetting_SettingsTitle,vr_SettingsString_Chinese);
		DisplayValueText (vr_Settings_Bottom_VideoSetting_ScreenLockTitle,vr_ScreenLockString_Chinese);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_ModeSetting_ModeTitle,vr_ModeString_Chinese);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_ModeSetting_FlatTitle,vr_FlatString_Chinese);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_ModeSetting_CinemaTitle,vr_CinemaString_Chinese);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_ModeSetting_AutoTitle,vr_AutoString_Chinese);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_Settings_SettingsTitle,vr_SettingsString_Chinese);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_Settings_FlatTitle,vr_FlatString_Chinese);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_Size_sizeTitle,vr_SizeString_Chinese);
		DisplayValueText (vr_Settings_Bottom_ScreenSetting_Ratio_RatioTitle,vr_RatioString_Chinese);
		DisplayValueText (vr_Settings_Bottom_Alert2DBnt_Alert2DTitle,vr_Are_you_sure_you_deviceString_Chinese);
		DisplayValueText (vr_Settings_Bottom_Alert2DBnt_YesButton,vr_YesString_Chinese);
		DisplayValueText (vr_Settings_Bottom_Alert2DBnt_NoButton,vr_NoString_Chinese);
	}

	private void SetChineseLanguage(){
		SetChineseLanguage_AccessMenu ();
		SetChineseLanguage_LoginMenu ();
		SetChineseLanguage_StorageMenu ();
		SetChineseLanguage_MyVideoMenu ();
		SetChineseLanguage_DetailMenu ();
		SetChineseLanguage_FavoriteMenu ();
		SetChineseLanguage_DownloadMenu ();
		SetChineseLanguage_InboxMenu ();
		SetChineseLanguage_SettingMenu ();
		SetChineseLanguage_VRPlayerMenu ();
		SetChineseLanguage_AlertMenu ();
		SetChineseLanguage_VR_ScreenCursor ();
		SetChineseLanguage_VR_MainMenu ();
		SetChineseLanguage_VR_Settings ();
	}

	#endregion



	private void DisplayValueText(Text text, string value){
		if (text != null) {
			text.text = value;
		} else {
			Debug.Log( "null...........................");
		}
	}
}
