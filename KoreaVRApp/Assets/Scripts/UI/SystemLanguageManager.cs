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
	private string phoneVideoTitleString_English = "PHONE VIDEO";
	private string myStorageString_English = "My Storage";
	private string myStorageTitleString_English = "MY STORAGE";
	private string myvideoString_English = "My Video";
	private string myvideoTitleString_English = "AFFILIATED VIDEO";
	private string vrUDONVideoListString_English = "VR UDON Video List";
	private string affiliatedVideoListString_English = "Affiliated Video List";
	private string favoriteString_English = "Favorites";
	private string favoriteTitleString_English = "FAVORITES";
	private string downloadString_English = "Download";
	private string downloadTitleString_English = "DOWNLOAD";
	private string inboxString_English = "Inbox";
	private string inboxTitleString_English = "INBOX";
	private string etcString_English = "Etc";
	private string preferencesString_English = "Preferences";
	private string howtouseString_English = "How to use";
	private string howtouseTitleString_English = "HOW TO USE";
	private string versionString_English = "Version";
	private string lOGINString_English = "LOGIN";
	private string autoLoginString_English = "Auto Login";
	private string networkString_English = "Unable to connect to network.\nPlease check your network connection and try again.";
	private string noVideosString_English = "No Videos";
	private string detailPageString_English = "Detail Page";
	private string detailPageTitleString_English = "DETAIL PAGE";
	private string streaming2DString_English = "2D Streaming";
	private string streaming3DString_English = "3D Streaming";
	private string watchin2DString_English = "Watch in 2D";
	private string watchin3DString_English = "Watch in 3D";
	private string informationString_English = "Basic Information";
	private string registration_dateString_English = "Registration";
	private string modelString_English = "Model";
	private string playtimeString_English = "Play time";
	private string genreString_English = "Genre";
	private string Go_to_inbox_after_downloadString_English = "Go to inbox after download";
	private string settingsString_English = "Settings";
	private string settingsTitleString_English = "SETTINGS";
	private string loginString_English = "Login";
	private string loginInformationString_English = "Login Information";
	private string keep_logged_inString_English = "Keep logged in";
	private string mobile_network_usage_notificationString_English = "Mobile network usage notification";
	private string confirm_when_using_mobile_network_dataString_English = "Confirm when using mobile network data";
	private string push_notification_settingsString_English = "Push notification settings";
	private string receive_PUSH_notificationString_English = "Receive PUSH notification";
	private string usable_capacityString_English = "Usable capacity";
	private string welcomeString_English = "Welcome";
	private string experience_realistic_virtual_realityString_English = "Experience realistic virtual reality realism with VR UDON";
	private string connect_your_mobile_phone_to_your_deviceString_English = "Connect your mobile phone to your device";
	private string run_vr_PlayerString_English = "RUN VR PLAYER";
	private string sign_in_completedString_English = "Sign-in completed";
	private string logged_outString_English = "Logged out";
	private string press_the_previous_button_again_to_exitString_English = "Press the previous button again to exit";
	private string calibrate_SensorString_English = "Calibrate Sensor for 5 seconds for optimization\nPlace the device on a flat surface and press the Start button\nDo not move the device until calibration completes\nSensor calibration is excuted when the application starts for the first time";
	private string screenShotString_English = "Screen Shot";
	private string VR_headset_or_cardboard_is_not_readyString_English = "VR headset or cardboard is not ready";
	private string After_playing_VR_movieString_English = "After playing VR movie, shake to open the screen setting window";
	private string Switch_from_2D_mode_to_VR_modeString_English = "Switch from 2D mode to VR mode";
	private string add_to_FavoritesString_English = "Add to Favorites";
	private string this_content_is_available_after_purchaseString_English = "This content is available after purchase";
	#endregion

	#region VR_ScreenCursor

	private string Hover_the_on_screenString_English = "Hover the on-screen cursor for 3 seconds to start the player";
	private string Precautions_AvoidString_English = "Precautions\nAvoid prolonged usage.If you experience any abnormal symptoms such as eye fatigue or motion sickness,\nstop using the device immediately";

	#endregion

	#region VR_MainMenu

	private string vr_PhoneVideoString_English = "PHONE VIDEO";
	private string vr_DownloadString_English = "DOWNLOAD";
	private string vr_MyVideoString_English = "MY VIDEO";
	private string vr_MyStorageString_English = "MY STORAGE";
	private string vr_InboxString_English = "INBOX";
	private string vr_VideoListString_English = "VIDEOLIST";
	private string vr_FavoriteString_English = "FAVORITES";
	private string vr_LoginrequiredString_English = "Login required";
	private string vr_Move_to_loginString_English = "Move to login page?";
	private string vr_YesString_English = "Yes";
	private string vr_NoString_English = "No";
	private string vr_DeleterequiredString_English = "Delete required";
	private string vr_DeleteString_English = "Are you sure?";
	private string vr_Please_check_the_network_connectionString_English = "Please check the network connection";
	private string vr_NovideosString_English = "No Videos";
	private string vr_StreamingrequiredString_English = "Streaming required";
	private string vr_StreamingString_English = "Streaming?";
	private string vr_Application_will_beString_English = "Application will be teminated when you move to sensor calibrating page. Continue?";
	private string vr_Sensor_calibrationString_English = "Sensor calibration";
	private string vr_Usable_capacity_is_not_availableString_English = "Usable capacity is not available";
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
	private string phoneVideoString_Korean = "폰비디오";
	private string myStorageString_Korean = "내 저장 공간";
	private string myvideoString_Korean = "내 비디오";
	private string myvideoTitleString_Korean = "제휴 동영상 목록";
	private string vrUDONVideoListString_Korean = "VR UDON 동영상 목록";
	private string affiliatedVideoListString_Korean = "제휴 동영상 목록";
	private string favoriteString_Korean = "즐겨찾기";
	private string downloadString_Korean = "다운로드";
	private string inboxString_Korean = "받은 파일함";
	private string etcString_Korean = "기타";
	private string preferencesString_Korean = "환경 설정";
	private string howtouseString_Korean = "이용 안내";
	private string howtouseTitleString_Korean = "이용 안내";
	private string versionString_Korean = "버전";
	private string lOGINString_Korean = "로그인";
	private string autoLoginString_Korean = "자동 로그인";
	private string networkString_Korean = "네트워크에 연결할 수 없습니다.\n네트워크 연결을 확인하고 다시 시도하십시오.";
	private string noVideosString_Korean = "동영상 없음";
	private string detailPageString_Korean = "상세페이지";
	private string streaming2DString_Korean = "2D 스트리밍";
	private string streaming3DString_Korean = "3D 스트리밍";
	private string watchin2DString_Korean = "2D로보기";
	private string watchin3DString_Korean = "3D로보기";
	private string informationString_Korean = "기본정보";
	private string registration_dateString_Korean = "등록일";
	private string modelString_Korean = "모 델";
	private string playtimeString_Korean = "재생시간"; //"감 독";
	private string genreString_Korean = "장 르";
	private string Go_to_inbox_after_downloadString_Korean = "다운로드 받은 파일은 받은 파일함으로 이동";
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
	private string experience_realistic_virtual_realityString_Korean = "VR UDON 으로 짜릿하고 실감나는 가상현실을 체험하세요";
	private string connect_your_mobile_phone_to_your_deviceString_Korean = "휴대 전화를 기기에 연결하십시오";
	private string run_vr_PlayerString_Korean = "VR 플레이어 실행";
	private string sign_in_completedString_Korean = "로그인이 완료되었습니다";
	private string logged_outString_Korean = "로그 아웃 됨";
	private string press_the_previous_button_again_to_exitString_Korean = "이전 버튼을 다시 누르면 종료됩니다.";
	private string calibrate_SensorString_Korean = "최적화를 위해 센서를 5 초 동안 교정하십시오\n장치를 평평한 바닥에 놓고 시작 단추를 누릅니다\n보정이 완료 될 때까지 장치를 움직이지 마십시오\n센서 보정은 어플리케이션이 처음 시작할 때 실행됩니다";
	private string screenShotString_Korean = "스크린 샷";
	private string VR_headset_or_cardboard_is_not_readyString_Korean = "VR 헤드셋 또는 카드보드가 준비 안 된 경우";
	private string After_playing_VR_movieString_Korean = "VR 영상 재생 후 스마트폰을 위아래로 살짝 흔들어 화면 설정창을 연다";
	private string Switch_from_2D_mode_to_VR_modeString_Korean = "2D 모드에서 VR 모드로 전환해서 감상할 수 있습니다";
	private string add_to_FavoritesString_Korean = "즐겨찾기에 추가";
	private string this_content_is_available_after_purchaseString_Korean = "이 콘텐츠는 구매 후 사용 가능합니다";
	#endregion

	#region VR_ScreenCursor

	private string Hover_the_on_screenString_Korean = "화면 커서를 3 초 동안 가져 가서 플레이어를 시작하십시오.";
	private string Precautions_AvoidString_Korean = "예방 조치\n장기간 사용하지 마십시오.눈의 피로 나 멀미와 같은 비정상적인 증상이 나타나면,\n즉시 장치 사용을 중지하십시오.";

	#endregion

	#region VR_MainMenu

	private string vr_Left_PhoneVideoString_Korean = "폰비디오";
	private string vr_Left_DownloadString_Korean = "다운로드";
	private string vr_Left_MyVideoString_Korean = "내 비디오";
	private string vr_Mid_MyStorageString_Korean = "내 저장 공간";
	private string vr_Mid_InboxString_Korean = "받은 파일함";
	private string vr_Mid_VideoListString_Korean = "동영상 목록";
	private string vr_Mid_FavoriteString_Korean = "즐겨찾기";
	private string vr_LoginrequiredString_Korean = "로그인 필요";
	private string vr_Move_to_loginString_Korean = "로그인 페이지로 이동 하시겠습니까?";
	private string vr_YesString_Korean = "예";
	private string vr_NoString_Korean = "아니오";
	private string vr_DeleterequiredString_Korean = "삭제 필요";
	private string vr_DeleteString_Korean = "삭제하시겠습니까?";
	private string vr_Please_check_the_network_connectionString_Korean = "네트워크 연결을 확인하십시오.";
	private string vr_NovideosString_Korean = "동영상 없음";
	private string vr_StreamingrequiredString_Korean = "스트리밍 요구됨";
	private string vr_StreamingString_Korean = "스트리밍?";
	private string vr_Application_will_beString_Korean = "센서보정 페이지로 이동하게 되면 영상이 종료됩니다. 계속 하시겠습니까?";
	private string vr_Sensor_calibrationString_Korean = "센서 보정";
	private string vr_Usable_capacity_is_not_availableString_Korean = "사용 가능한 용량을 사용할 수 없습니다";
	#endregion

	#region VR_Settings

	private string vr_VideoString_Korean = "비디오";
	private string vr_ScreenString_Korean = "화면";
	private string vr_2DString_Korean = "2D";
	private string vr_ListString_Korean = "목록";
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
	private string myvideoTitleString_Japanese = "提携ビデオ";
	private string vrUDONVideoListString_Japanese = "VR UDONビデオリスト";
	private string affiliatedVideoListString_Japanese = "所属ビデオリスト";
	private string favoriteString_Japanese = "気に入り";
	private string downloadString_Japanese = "ダウンロード";
	private string inboxString_Japanese = "受信トレイ";
	private string etcString_Japanese = "等";
	private string preferencesString_Japanese = "設定";
	private string howtouseString_Japanese = "使い方";
	private string howtouseTitleString_Japanese = "使い方";
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
	private string informationString_Japanese = "基本情報";
	private string registration_dateString_Japanese = "登録日";
	private string modelString_Japanese = "モデル";
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
	private string calibrate_SensorString_Japanese = "最適化のために5秒間センサーを校正\n平らな場所にデバイスを置き、スタートボタンを押します\n校正が完了するまで装置を動かさないでください\nアプリケーションの初回起動時にセンサーキャリブレーションが実行されます";
	private string screenShotString_Japanese = "スクリーンショット";
	private string VR_headset_or_cardboard_is_not_readyString_Japanese = "VRヘッドセットまたは段ボールの準備ができていません";
	private string After_playing_VR_movieString_Japanese = "VR動画を再生した後、振って画面設定ウィンドウを開く";
	private string Switch_from_2D_mode_to_VR_modeString_Japanese = "2DモードからVRモードに切り替える";
	private string add_to_FavoritesString_Japanese = "お気に入りに追加";
	private string this_content_is_available_after_purchaseString_Japanese = "このコンテンツは購入後に利用可能です";
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
	private string vr_DeleteString_Japanese = "本気ですか？";
	private string vr_Please_check_the_network_connectionString_Japanese = "ネットワーク接続を確認してください";
	private string vr_NovideosString_Japanese = "動画なし";
	private string vr_StreamingrequiredString_Japanese = "ストリーミングが必要";
	private string vr_StreamingString_Japanese = "ストリーミング?";
	private string vr_Application_will_beString_Japanese = "センサー校正ページに移動するとアプリケーションは終了します。 持続する？";
	private string vr_Sensor_calibrationString_Japanese = "センサー校正";
	private string vr_Usable_capacity_is_not_availableString_Japanese = "使用可能容量がありません";
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
	private string myvideoTitleString_Chinese = "附屬視頻";
	private string vrUDONVideoListString_Chinese = "VR UDON視頻列表";
	private string affiliatedVideoListString_Chinese = "附屬視頻列表";
	private string favoriteString_Chinese = "喜爱";
	private string downloadString_Chinese = "下载";
	private string inboxString_Chinese = "收件箱";
	private string etcString_Chinese = "等等";
	private string preferencesString_Chinese = "喜好";
	private string howtouseString_Chinese = "如何使用";
	private string howtouseTitleString_Chinese = "如何使用";
	private string versionString_Chinese = "版";
	private string lOGINString_Chinese = "登錄";
	private string autoLoginString_Chinese = "自動登錄";
	private string networkString_Chinese = "無法連接到網絡。\n請檢查您的網絡連接，然後重試。";
	private string noVideosString_Chinese = "沒有視頻";
	private string detailPageString_Chinese = "細節頁面";
	private string streaming2DString_Chinese = "2D流媒體";
	private string streaming3DString_Chinese = "3D流媒體";
	private string watchin2DString_Chinese = "觀看2D";
	private string watchin3DString_Chinese = "觀看3D";
	private string informationString_Chinese = "基本信息";
	private string registration_dateString_Chinese = "登記日期";
	private string modelString_Chinese = "模型";
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
	private string calibrate_SensorString_Chinese = "校準傳感器5秒鐘以進行優化\n將設備放在平坦的表面上，然後按開始按鈕\n在校準完成之前，請勿移動設備\n第一次啟動應用程序時執行傳感器校準";
	private string screenShotString_Chinese = "屏幕截圖";
	private string VR_headset_or_cardboard_is_not_readyString_Chinese = "VR耳機或紙板尚未就緒";
	private string After_playing_VR_movieString_Chinese = "播放VR影片後，搖動以打開屏幕設置窗口";
	private string Switch_from_2D_mode_to_VR_modeString_Chinese = "從2D模式切換到VR模式";
	private string add_to_FavoritesString_Chinese = "添加到收藏夾";
	private string this_content_is_available_after_purchaseString_Chinese = "購買後即可獲得此內容";
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
	private string vr_DeleteString_Chinese = "你確定嗎?";
	private string vr_Please_check_the_network_connectionString_Chinese = "請檢查網絡連接";
	private string vr_NovideosString_Chinese = "沒有視頻";
	private string vr_StreamingrequiredString_Chinese = "需要流媒體";
	private string vr_StreamingString_Chinese = "流?";
	private string vr_Application_will_beString_Chinese = "移動到傳感器校準頁面時，應用程序將終止。 繼續？";
	private string vr_Sensor_calibrationString_Chinese = "傳感器校準";
	private string vr_Usable_capacity_is_not_availableString_Chinese = "可用容量不可用";
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
	[SerializeField] private Text accessMenu_UdonVideoname = null;
	[SerializeField] private Text accessMenu_AffiliatedVideoname = null;
	[SerializeField] private Text accessMenu_Favoritename = null;

	[SerializeField] private Text accessMenu_DownloadTitle = null;
	[SerializeField] private Text accessMenu_DownloadName = null;
	[SerializeField] private Text accessMenu_Inboxname = null;

	[SerializeField] private Text accessMenu_EtcTitle = null;
	[SerializeField] private Text accessMenu_PreferencesName = null;
	[SerializeField] private Text accessMenu_HowtouseName = null;
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
	//[SerializeField] private Text detailMenu_InformationTitle = null;
	[SerializeField] private Text detailMenu_ModelTitle = null;
	[SerializeField] private Text detailMenu_GenreTitle = null;
	[SerializeField] private Text detailMenu_PlaytimeTitle = null;
	[SerializeField] private Text detailMenu_RegistrationdateTitle = null;
	[SerializeField] private Text detailMenu_Network = null;
	[SerializeField] private Text detailMenu_ScreenShotTitle = null;

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

	[Header("---- InfoMenu ----")]
	[SerializeField] private Text infoMenu_Header = null;

	[Header("---- VRPlayerMenu ----")]
	[SerializeField] private Text vrPlayerMenu_Welcome = null;
	[SerializeField] private Text vrPlayerMenu_Top = null;
	[SerializeField] private Text vrPlayerMenu_Bottom = null;
	[SerializeField] private Text vrPlayerMenu_BottomBoxTitle = null;
	[SerializeField] private Text vrPlayerMenu_BottomBox1 = null;
	[SerializeField] private Text vrPlayerMenu_BottomBox2 = null;
	[SerializeField] private Text vrPlayerMenu_RunVRBnt = null;

	[Header("---- AlertMenu ----")]
	[SerializeField] private Text alertMenu_Login = null;
	[SerializeField] private Text alertMenu_Logout = null;
	[SerializeField] private Text alertMenu_Exit = null;
	[SerializeField] private Text alertMenu_purchase = null;

	[Header("---- SensorMenu ----")]
	[SerializeField] private Text sensorMenu_SensorContent = null;

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

	[SerializeField] private Text vr_MainMenu_VR_SensorTitle = null;
	[SerializeField] private Text vr_MainMenu_VR_SensorContent = null;
	[SerializeField] private Text vr_MainMenu_VR_SensorYesButton = null;
	[SerializeField] private Text vr_MainMenu_VR_SensorNoButton = null;

	[SerializeField] private Text vr_MainMenu_VR_UsablecapacityAlert = null;

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


	private bool isEnglishLanguage;
	private bool isKoreanLanguage;
	private bool isJapaneseLanguage;
	private bool isChineseLanguage;
	private bool isOtherLanguage;

	public bool IsEnglishLanguage
	{
		get { return isEnglishLanguage; }
		set { isEnglishLanguage = value; }
	}

	public bool IsKoreanLanguage
	{
		get { return isKoreanLanguage; }
		set { isKoreanLanguage = value; }
	}

	public bool IsJapaneseLanguage
	{
		get { return isJapaneseLanguage; }
		set { isJapaneseLanguage = value; }
	}

	public bool IsChineseLanguage
	{
		get { return isChineseLanguage; }
		set { isChineseLanguage = value; }
	}

	public bool IsOtherLanguage
	{
		get { return isOtherLanguage; }
		set { isOtherLanguage = value; }
	}

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
			IsEnglishLanguage = true;
			break;
		
		case SystemLanguage.Korean: //Otherwise, if the system is Korean
			Debug.Log ("This system is in Korean.............................................");

			SetKoreaLanguage ();
			IsKoreanLanguage = true;
			break;
		case SystemLanguage.Japanese: //Otherwise, if the system is Japanese
			Debug.Log ("This system is in Japanese...........................................");

			SetJapaneseLanguage ();
			IsJapaneseLanguage = true;
			break;
		case SystemLanguage.Chinese: //Otherwise, if the system is Chinese
			Debug.Log ("This system is in Chinese.............................................");

			SetChineseLanguage ();
			IsChineseLanguage = true;
			break;
		case SystemLanguage.ChineseSimplified: //Otherwise, if the system is Chinese
			Debug.Log ("This system is in Chinese.............................................");

			SetChineseLanguage ();
			IsChineseLanguage = true;
			break;
		case SystemLanguage.ChineseTraditional: //Otherwise, if the system is Chinese
			Debug.Log ("This system is in Chinese.............................................");

			SetChineseLanguage ();
			IsChineseLanguage = true;
			break;
		default:
			Debug.Log ("This system is in other language......................................");
			SetEnglishLanguage ();
			IsOtherLanguage = true;
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
		DisplayValueText (accessMenu_UdonVideoname,vrUDONVideoListString_English);
		DisplayValueText (accessMenu_AffiliatedVideoname,affiliatedVideoListString_English);
		DisplayValueText (accessMenu_Favoritename,favoriteString_English);
		DisplayValueText (accessMenu_DownloadTitle,downloadString_English);
		DisplayValueText (accessMenu_DownloadName,downloadString_English);
		DisplayValueText (accessMenu_Inboxname,inboxString_English);
		DisplayValueText (accessMenu_EtcTitle,etcString_English);
		DisplayValueText (accessMenu_PreferencesName,preferencesString_English);
		DisplayValueText (accessMenu_HowtouseName,howtouseString_English);
		DisplayValueText (accessMenu_VersionName,versionString_English);
	}

	private void SetEnglishLanguage_LoginMenu(){
		DisplayValueText (loginMenu_Header,lOGINString_English);
		DisplayValueText (loginMenu_AutoLogin,autoLoginString_English);
		DisplayValueText (loginMenu_Loginbnt,lOGINString_English);
		DisplayValueText (loginMenu_Network,networkString_English);
	}

	private void SetEnglishLanguage_StorageMenu(){
		DisplayValueText (storageMenu_Header,myStorageTitleString_English);
		DisplayValueText (storageMenu_Novideos,noVideosString_English);
		DisplayValueText (storageMenu_Network,networkString_English);
	}

	private void SetEnglishLanguage_MyVideoMenu(){
		DisplayValueText (myVideoMenu_Header,myvideoTitleString_English);
		DisplayValueText (myVideoMenu_Novideos,noVideosString_English);
		DisplayValueText (myVideoMenu_Network,networkString_English);
	}

	private void SetEnglishLanguage_DetailMenu(){
		DisplayValueText (detailMenu_Header,detailPageTitleString_English);
		DisplayValueText (detailMenu_UnfavoriteBnt,add_to_FavoritesString_English);
		DisplayValueText (detailMenu_FavoriteBnt,add_to_FavoritesString_English);
		DisplayValueText (detailMenu_HaventDownloadBnt,downloadString_English);
		DisplayValueText (detailMenu_Havent2DBnt,streaming2DString_English);
		DisplayValueText (detailMenu_Havent3DBnt,streaming3DString_English);
		DisplayValueText (detailMenu_Downloaded2DBnt,watchin2DString_English);
		DisplayValueText (detailMenu_Downloaded3DBnt,watchin3DString_English);
		//DisplayValueText (detailMenu_InformationTitle,informationString_English);
		DisplayValueText (detailMenu_RegistrationdateTitle,registration_dateString_English);
		DisplayValueText (detailMenu_ModelTitle,modelString_English);
		DisplayValueText (detailMenu_PlaytimeTitle,playtimeString_English);
		DisplayValueText (detailMenu_GenreTitle,genreString_English);
		DisplayValueText (detailMenu_Network,networkString_English);
		DisplayValueText (detailMenu_ScreenShotTitle,screenShotString_English);
	}


	private void SetEnglishLanguage_FavoriteMenu(){
		DisplayValueText (favoriteMenu_Header,favoriteTitleString_English);
		DisplayValueText (favoriteMenu_Novideos,noVideosString_English);
		DisplayValueText (favoriteMenu_Network,networkString_English);
	}

	private void SetEnglishLanguage_DownloadMenu(){
		DisplayValueText (downloadMenu_Header,downloadTitleString_English);
		DisplayValueText (downloadMenu_Novideos,Go_to_inbox_after_downloadString_English);
		DisplayValueText (downloadMenu_Network,networkString_English);
	}

	private void SetEnglishLanguage_InboxMenu(){
		DisplayValueText (inboxMenu_Header,inboxTitleString_English);
		DisplayValueText (inboxMenu_Novideos,noVideosString_English);
		DisplayValueText (inboxMenu_Network,networkString_English);
	}

	private void SetEnglishLanguage_SettingMenu(){
		DisplayValueText (settingMenu_Header,settingsTitleString_English);
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

	private void SetEnglishLanguage_InfoMenu(){
		DisplayValueText (infoMenu_Header,howtouseTitleString_English);
	}

	private void SetEnglishLanguage_VRPlayerMenu(){
		DisplayValueText (vrPlayerMenu_Welcome,welcomeString_English);
		DisplayValueText (vrPlayerMenu_Top,experience_realistic_virtual_realityString_English);
		DisplayValueText (vrPlayerMenu_Bottom,connect_your_mobile_phone_to_your_deviceString_English);
		DisplayValueText (vrPlayerMenu_BottomBoxTitle,VR_headset_or_cardboard_is_not_readyString_English);
		DisplayValueText (vrPlayerMenu_BottomBox1,After_playing_VR_movieString_English);
		DisplayValueText (vrPlayerMenu_BottomBox2,Switch_from_2D_mode_to_VR_modeString_English);
		DisplayValueText (vrPlayerMenu_RunVRBnt,run_vr_PlayerString_English);
	}

	private void SetEnglishLanguage_AlertMenu(){
		DisplayValueText (alertMenu_Login,sign_in_completedString_English);
		DisplayValueText (alertMenu_Logout,logged_outString_English);
		DisplayValueText (alertMenu_Exit,press_the_previous_button_again_to_exitString_English);
		DisplayValueText (alertMenu_purchase,this_content_is_available_after_purchaseString_English);
	}

	private void SetEnglishLanguage_SensorMenu(){
		DisplayValueText (sensorMenu_SensorContent,calibrate_SensorString_English);
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
		DisplayValueText (vr_MainMenu_VR_DeleteAlertContent,vr_DeleteString_English);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertYesButton,vr_YesString_English);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertNoButton,vr_NoString_English);

		DisplayValueText (vr_MainMenu_VR_NetworkConnection,vr_Please_check_the_network_connectionString_English);

		DisplayValueText (vr_MainMenu_VR_Novideos,vr_NovideosString_English);

		DisplayValueText (vr_MainMenu_VR_StreamingTitle,vr_StreamingrequiredString_English);
		DisplayValueText (vr_MainMenu_VR_StreamingContent,vr_StreamingString_English);
		DisplayValueText (vr_MainMenu_VR_StreamingYesButton,vr_YesString_English);
		DisplayValueText (vr_MainMenu_VR_StreamingNoButton,vr_NoString_English);

		DisplayValueText (vr_MainMenu_VR_SensorTitle,vr_Sensor_calibrationString_English);
		DisplayValueText (vr_MainMenu_VR_SensorContent,vr_Application_will_beString_English);
		DisplayValueText (vr_MainMenu_VR_SensorYesButton,vr_YesString_English);
		DisplayValueText (vr_MainMenu_VR_SensorNoButton,vr_NoString_English);

		DisplayValueText (vr_MainMenu_VR_UsablecapacityAlert,vr_Usable_capacity_is_not_availableString_English);
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
		SetEnglishLanguage_InfoMenu ();
		SetEnglishLanguage_VRPlayerMenu ();
		SetEnglishLanguage_AlertMenu ();
		SetEnglishLanguage_SensorMenu ();
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
		DisplayValueText (accessMenu_UdonVideoname,vrUDONVideoListString_Korean);
		DisplayValueText (accessMenu_AffiliatedVideoname,affiliatedVideoListString_Korean);
		DisplayValueText (accessMenu_Favoritename,favoriteString_Korean);
		DisplayValueText (accessMenu_DownloadTitle,downloadString_Korean);
		DisplayValueText (accessMenu_DownloadName,downloadString_Korean);
		DisplayValueText (accessMenu_Inboxname,inboxString_Korean);
		DisplayValueText (accessMenu_EtcTitle,etcString_Korean);
		DisplayValueText (accessMenu_PreferencesName,preferencesString_Korean);
		DisplayValueText (accessMenu_HowtouseName,howtouseString_Korean);
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
		DisplayValueText (myVideoMenu_Header,myvideoTitleString_Korean);
		DisplayValueText (myVideoMenu_Novideos,noVideosString_Korean);
		DisplayValueText (myVideoMenu_Network,networkString_Korean);
	}

	private void SetKoreanLanguage_DetailMenu(){
		DisplayValueText (detailMenu_Header,detailPageString_Korean);
		DisplayValueText (detailMenu_UnfavoriteBnt,add_to_FavoritesString_Korean);
		DisplayValueText (detailMenu_FavoriteBnt,add_to_FavoritesString_Korean);
		DisplayValueText (detailMenu_HaventDownloadBnt,downloadString_Korean);
		DisplayValueText (detailMenu_Havent2DBnt,streaming2DString_Korean);
		DisplayValueText (detailMenu_Havent3DBnt,streaming3DString_Korean);
		DisplayValueText (detailMenu_Downloaded2DBnt,watchin2DString_Korean);
		DisplayValueText (detailMenu_Downloaded3DBnt,watchin3DString_Korean);
		//DisplayValueText (detailMenu_InformationTitle,informationString_Korean);
		DisplayValueText (detailMenu_RegistrationdateTitle,registration_dateString_Korean);
		DisplayValueText (detailMenu_ModelTitle,modelString_Korean);
		DisplayValueText (detailMenu_PlaytimeTitle,playtimeString_Korean);
		DisplayValueText (detailMenu_GenreTitle,genreString_Korean);
		DisplayValueText (detailMenu_Network,networkString_Korean);
		DisplayValueText (detailMenu_ScreenShotTitle,screenShotString_Korean);
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

	private void SetKoreanLanguage_InfoMenu(){
		DisplayValueText (infoMenu_Header,howtouseTitleString_Korean);
	}

	private void SetKoreanLanguage_VRPlayerMenu(){
		DisplayValueText (vrPlayerMenu_Welcome,welcomeString_Korean);
		DisplayValueText (vrPlayerMenu_Top,experience_realistic_virtual_realityString_Korean);
		DisplayValueText (vrPlayerMenu_Bottom,connect_your_mobile_phone_to_your_deviceString_Korean);
		DisplayValueText (vrPlayerMenu_BottomBoxTitle,VR_headset_or_cardboard_is_not_readyString_Korean);
		DisplayValueText (vrPlayerMenu_BottomBox1,After_playing_VR_movieString_Korean);
		DisplayValueText (vrPlayerMenu_BottomBox2,Switch_from_2D_mode_to_VR_modeString_Korean);
		DisplayValueText (vrPlayerMenu_RunVRBnt,run_vr_PlayerString_Korean);
	}

	private void SetKoreanLanguage_AlertMenu(){
		DisplayValueText (alertMenu_Login,sign_in_completedString_Korean);
		DisplayValueText (alertMenu_Logout,logged_outString_Korean);
		DisplayValueText (alertMenu_Exit,press_the_previous_button_again_to_exitString_Korean);
		DisplayValueText (alertMenu_purchase,this_content_is_available_after_purchaseString_Korean);
	}

	private void SetKoreanLanguage_SensorMenu(){
		DisplayValueText (sensorMenu_SensorContent,calibrate_SensorString_Korean);
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
		DisplayValueText (vr_MainMenu_VR_DeleteAlertContent,vr_DeleteString_Korean);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertYesButton,vr_YesString_Korean);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertNoButton,vr_NoString_Korean);

		DisplayValueText (vr_MainMenu_VR_NetworkConnection,vr_Please_check_the_network_connectionString_Korean);

		DisplayValueText (vr_MainMenu_VR_Novideos,vr_NovideosString_Korean);

		DisplayValueText (vr_MainMenu_VR_StreamingTitle,vr_StreamingrequiredString_Korean);
		DisplayValueText (vr_MainMenu_VR_StreamingContent,vr_StreamingString_Korean);
		DisplayValueText (vr_MainMenu_VR_StreamingYesButton,vr_YesString_Korean);
		DisplayValueText (vr_MainMenu_VR_StreamingNoButton,vr_NoString_Korean);

		DisplayValueText (vr_MainMenu_VR_SensorTitle,vr_Sensor_calibrationString_Korean);
		DisplayValueText (vr_MainMenu_VR_SensorContent,vr_Application_will_beString_Korean);
		DisplayValueText (vr_MainMenu_VR_SensorYesButton,vr_YesString_Korean);
		DisplayValueText (vr_MainMenu_VR_SensorNoButton,vr_NoString_Korean);

		DisplayValueText (vr_MainMenu_VR_UsablecapacityAlert,vr_Usable_capacity_is_not_availableString_Korean);

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
		SetKoreanLanguage_InfoMenu ();
		SetKoreanLanguage_VRPlayerMenu ();
		SetKoreanLanguage_AlertMenu ();
		SetKoreanLanguage_SensorMenu ();
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
		DisplayValueText (accessMenu_UdonVideoname,vrUDONVideoListString_Japanese);
		DisplayValueText (accessMenu_AffiliatedVideoname,affiliatedVideoListString_Japanese);
		DisplayValueText (accessMenu_Favoritename,favoriteString_Japanese);
		DisplayValueText (accessMenu_DownloadTitle,downloadString_Japanese);
		DisplayValueText (accessMenu_DownloadName,downloadString_Japanese);
		DisplayValueText (accessMenu_Inboxname,inboxString_Japanese);
		DisplayValueText (accessMenu_EtcTitle,etcString_Japanese);
		DisplayValueText (accessMenu_PreferencesName,preferencesString_Japanese);
		DisplayValueText (accessMenu_HowtouseName,howtouseString_Japanese);
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
		DisplayValueText (myVideoMenu_Header,myvideoTitleString_Japanese);
		DisplayValueText (myVideoMenu_Novideos,noVideosString_Japanese);
		DisplayValueText (myVideoMenu_Network,networkString_Japanese);
	}

	private void SetJapaneseLanguage_DetailMenu(){
		DisplayValueText (detailMenu_Header,detailPageString_Japanese);
		DisplayValueText (detailMenu_UnfavoriteBnt,add_to_FavoritesString_Japanese);
		DisplayValueText (detailMenu_FavoriteBnt,add_to_FavoritesString_Japanese);
		DisplayValueText (detailMenu_HaventDownloadBnt,downloadString_Japanese);
		DisplayValueText (detailMenu_Havent2DBnt,streaming2DString_Japanese);
		DisplayValueText (detailMenu_Havent3DBnt,streaming3DString_Japanese);
		DisplayValueText (detailMenu_Downloaded2DBnt,watchin2DString_Japanese);
		DisplayValueText (detailMenu_Downloaded3DBnt,watchin3DString_Japanese);
		//DisplayValueText (detailMenu_InformationTitle,informationString_Japanese);
		DisplayValueText (detailMenu_RegistrationdateTitle,registration_dateString_Japanese);
		DisplayValueText (detailMenu_ModelTitle,modelString_Japanese);
		DisplayValueText (detailMenu_PlaytimeTitle,playtimeString_Japanese);
		DisplayValueText (detailMenu_GenreTitle,genreString_Japanese);
		DisplayValueText (detailMenu_Network,networkString_Japanese);
		DisplayValueText (detailMenu_ScreenShotTitle,screenShotString_Japanese);
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

	private void SetJapaneseLanguage_InfoMenu(){
		DisplayValueText (infoMenu_Header,howtouseTitleString_Japanese);
	}

	private void SetJapaneseLanguage_VRPlayerMenu(){
		DisplayValueText (vrPlayerMenu_Welcome,welcomeString_Japanese);
		DisplayValueText (vrPlayerMenu_Top,experience_realistic_virtual_realityString_Japanese);
		DisplayValueText (vrPlayerMenu_Bottom,connect_your_mobile_phone_to_your_deviceString_Japanese);
		DisplayValueText (vrPlayerMenu_BottomBoxTitle,VR_headset_or_cardboard_is_not_readyString_Japanese);
		DisplayValueText (vrPlayerMenu_BottomBox1,After_playing_VR_movieString_Japanese);
		DisplayValueText (vrPlayerMenu_BottomBox2,Switch_from_2D_mode_to_VR_modeString_Japanese);
		DisplayValueText (vrPlayerMenu_RunVRBnt,run_vr_PlayerString_Japanese);
	}

	private void SetJapaneseLanguage_AlertMenu(){
		DisplayValueText (alertMenu_Login,sign_in_completedString_Japanese);
		DisplayValueText (alertMenu_Logout,keep_logged_inString_Japanese);
		DisplayValueText (alertMenu_Exit,press_the_previous_button_again_to_exitString_Japanese);
		DisplayValueText (alertMenu_purchase,this_content_is_available_after_purchaseString_Japanese);
	}

	private void SetJapaneseLanguage_SensorMenu(){
		DisplayValueText (sensorMenu_SensorContent,calibrate_SensorString_Japanese);
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
		DisplayValueText (vr_MainMenu_VR_DeleteAlertContent,vr_DeleteString_Japanese);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertYesButton,vr_YesString_Japanese);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertNoButton,vr_NoString_Japanese);

		DisplayValueText (vr_MainMenu_VR_NetworkConnection,vr_Please_check_the_network_connectionString_Japanese);

		DisplayValueText (vr_MainMenu_VR_Novideos,vr_NovideosString_Japanese);

		DisplayValueText (vr_MainMenu_VR_StreamingTitle,vr_StreamingrequiredString_Japanese);
		DisplayValueText (vr_MainMenu_VR_StreamingContent,vr_StreamingString_Japanese);
		DisplayValueText (vr_MainMenu_VR_StreamingYesButton,vr_YesString_Japanese);
		DisplayValueText (vr_MainMenu_VR_StreamingNoButton,vr_NoString_Japanese);

		DisplayValueText (vr_MainMenu_VR_SensorTitle,vr_Sensor_calibrationString_Japanese);
		DisplayValueText (vr_MainMenu_VR_SensorContent,vr_Application_will_beString_Japanese);
		DisplayValueText (vr_MainMenu_VR_SensorYesButton,vr_YesString_Japanese);
		DisplayValueText (vr_MainMenu_VR_SensorNoButton,vr_NoString_Japanese);

		DisplayValueText (vr_MainMenu_VR_UsablecapacityAlert,vr_Usable_capacity_is_not_availableString_Japanese);
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
		SetJapaneseLanguage_InfoMenu ();
		SetJapaneseLanguage_VRPlayerMenu ();
		SetJapaneseLanguage_AlertMenu ();
		SetJapaneseLanguage_SensorMenu ();
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
		DisplayValueText (accessMenu_UdonVideoname,vrUDONVideoListString_Chinese);
		DisplayValueText (accessMenu_AffiliatedVideoname,affiliatedVideoListString_Chinese);
		DisplayValueText (accessMenu_Favoritename,favoriteString_Chinese);
		DisplayValueText (accessMenu_DownloadTitle,downloadString_Chinese);
		DisplayValueText (accessMenu_DownloadName,downloadString_Chinese);
		DisplayValueText (accessMenu_Inboxname,inboxString_Chinese);
		DisplayValueText (accessMenu_EtcTitle,etcString_Chinese);
		DisplayValueText (accessMenu_PreferencesName,preferencesString_Chinese);
		DisplayValueText (accessMenu_HowtouseName,howtouseString_Chinese);
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
		DisplayValueText (myVideoMenu_Header,myvideoTitleString_Chinese);
		DisplayValueText (myVideoMenu_Novideos,noVideosString_Chinese);
		DisplayValueText (myVideoMenu_Network,networkString_Chinese);
	}

	private void SetChineseLanguage_DetailMenu(){
		DisplayValueText (detailMenu_Header,detailPageString_Chinese);
		DisplayValueText (detailMenu_UnfavoriteBnt,add_to_FavoritesString_Chinese);
		DisplayValueText (detailMenu_FavoriteBnt,add_to_FavoritesString_Chinese);
		DisplayValueText (detailMenu_HaventDownloadBnt,downloadString_Chinese);
		DisplayValueText (detailMenu_Havent2DBnt,streaming2DString_Chinese);
		DisplayValueText (detailMenu_Havent3DBnt,streaming3DString_Chinese);
		DisplayValueText (detailMenu_Downloaded2DBnt,watchin2DString_Chinese);
		DisplayValueText (detailMenu_Downloaded3DBnt,watchin3DString_Chinese);
		//DisplayValueText (detailMenu_InformationTitle,informationString_Chinese);
		DisplayValueText (detailMenu_RegistrationdateTitle,registration_dateString_Chinese);
		DisplayValueText (detailMenu_ModelTitle,modelString_Chinese);
		DisplayValueText (detailMenu_PlaytimeTitle,playtimeString_Chinese);
		DisplayValueText (detailMenu_GenreTitle,genreString_Chinese);
		DisplayValueText (detailMenu_Network,networkString_Chinese);
		DisplayValueText (detailMenu_ScreenShotTitle,screenShotString_Chinese);
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

	private void SetChineseLanguage_InfoMenu(){
		DisplayValueText (infoMenu_Header,howtouseTitleString_Chinese);
	}

	private void SetChineseLanguage_VRPlayerMenu(){
		DisplayValueText (vrPlayerMenu_Welcome,welcomeString_Chinese);
		DisplayValueText (vrPlayerMenu_Top,experience_realistic_virtual_realityString_Chinese);
		DisplayValueText (vrPlayerMenu_Bottom,connect_your_mobile_phone_to_your_deviceString_Chinese);
		DisplayValueText (vrPlayerMenu_BottomBoxTitle,VR_headset_or_cardboard_is_not_readyString_Chinese);
		DisplayValueText (vrPlayerMenu_BottomBox1,After_playing_VR_movieString_Chinese);
		DisplayValueText (vrPlayerMenu_BottomBox2,Switch_from_2D_mode_to_VR_modeString_Chinese);
		DisplayValueText (vrPlayerMenu_RunVRBnt,run_vr_PlayerString_Chinese);
	}

	private void SetChineseLanguage_AlertMenu(){
		DisplayValueText (alertMenu_Login,sign_in_completedString_Chinese);
		DisplayValueText (alertMenu_Logout,logged_outString_Chinese);
		DisplayValueText (alertMenu_Exit,press_the_previous_button_again_to_exitString_Chinese);
		DisplayValueText (alertMenu_purchase,this_content_is_available_after_purchaseString_Chinese);
	}

	private void SetChineseLanguage_SensorMenu(){
		DisplayValueText (sensorMenu_SensorContent,calibrate_SensorString_Chinese);
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
		DisplayValueText (vr_MainMenu_VR_DeleteAlertContent,vr_DeleteString_Chinese);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertYesButton,vr_YesString_Chinese);
		DisplayValueText (vr_MainMenu_VR_DeleteAlertNoButton,vr_NoString_Chinese);

		DisplayValueText (vr_MainMenu_VR_NetworkConnection,vr_Please_check_the_network_connectionString_Chinese);

		DisplayValueText (vr_MainMenu_VR_Novideos,vr_NovideosString_Chinese);

		DisplayValueText (vr_MainMenu_VR_StreamingTitle,vr_StreamingrequiredString_Chinese);
		DisplayValueText (vr_MainMenu_VR_StreamingContent,vr_StreamingString_Chinese);
		DisplayValueText (vr_MainMenu_VR_StreamingYesButton,vr_YesString_Chinese);
		DisplayValueText (vr_MainMenu_VR_StreamingNoButton,vr_NoString_Chinese);

		DisplayValueText (vr_MainMenu_VR_SensorTitle,vr_Sensor_calibrationString_Chinese);
		DisplayValueText (vr_MainMenu_VR_SensorContent,vr_Application_will_beString_Chinese);
		DisplayValueText (vr_MainMenu_VR_SensorYesButton,vr_YesString_Chinese);
		DisplayValueText (vr_MainMenu_VR_SensorNoButton,vr_NoString_Chinese);

		DisplayValueText (vr_MainMenu_VR_UsablecapacityAlert,vr_Usable_capacity_is_not_availableString_Chinese);
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
		SetChineseLanguage_InfoMenu ();
		SetChineseLanguage_VRPlayerMenu ();
		SetChineseLanguage_AlertMenu ();
		SetChineseLanguage_SensorMenu ();
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
