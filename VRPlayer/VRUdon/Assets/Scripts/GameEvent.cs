using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent
{
    public static string recenterEvent = "vr:recenter";

    public static string showScreenCursor = "vr:show_screen_cursor";
    public static string closeScreenCursor = "vr:close_screen_cursor";

    public static string showMenuCubemap = "vr:show_menu_cubemap";
    public static string closeMenuCubemap = "vr:close_menu_cubemap";

    public static string loadLocalVideos = "vr:load_local_video";

    public static string sendVideos = "vr:send_videos";

    public static string setPageNumber = "vr:set_page_number";
    public static string goToPage = "vr:go_to_page";

    // Call By UI string
    public static string openLocalVideoScreen = "vr:open_local_video_screen";
    public static string openDownloadVideoScreen = "vr:open_download_video_screen";
    public static string openMyVideoScreen = "vr:open_my_video_screen";
    public static string openFavoriteVideoScreen = "vr:open_favorite_video_screen";

    //
    public static string showLocalVideoScreen = "vr:show_local_video_screen";
    public static string showDownloadVideoScreen = "vr:show_download_video_screen";
    public static string showUserVideoScreen = "vr:show_user_video_screen";
    public static string showFavoriteUserVideoScreen = "vr:show_user_favorite_video_screen";

    public static string InitScreen = "vr:init_screen";

    public static string userLoggedIn = "vr:user_logged_in";

    public static string onFavoriteVideo = "vr:favorite_video";
    public static string onDoneFavoriteVideo = "vr:done_favorite_video";

    // VR Settings
    public static string pauseVideo = "vr:pause_video";
    public static string resumeVideo = "vr:resume_video";
    public static string lockScreen = "vr:lock_screen";
    public static string unlockScreen = "vr:unlock_screen";
    public static string setBrightness = "vr:set_brightness";
    public static string setSize = "vr:set_size";

    public static string packingLR = "vr:set_packing_lr";
    public static string packingTB = "vr:set_packing_tb";
    public static string packingNone = "vr:set_packing_none";

    public static string setRatio43 = "vr:set_ratio_43";
    public static string setRatio169 = "vr:set_ratio_169";
    public static string setRatio1851 = "vr:set_ratio_1851";
    public static string setRatioOriginal = "vr:set_ratio_original";

    // VR Player
    public static string showFlatVR = "vr:show_flat_vr";
    public static string showCinemaVR = "vr:show_cinema_vr";
    public static string showStereoVR = "vr:show_stereo_vr";
    public static string showSphereVR = "vr:show_sphere_vr";
    public static string showAutoVR = "vr:show_auto_vr";

    public static string openVideoSetting = "vr:open_video_setting";
    public static string openScreenSetting = "vr:open_screen_setting";
}

