using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent
{
    public static string recenterEvent = "vr:recenter";
    public static string nodEvent = "vr:nod";

    public static string showScreenCursor = "vr:show_screen_cursor";
    public static string closeScreenCursor = "vr:close_screen_cursor";

    public static string showMenuCubemap = "vr:show_menu_cubemap";
    public static string closeMenuCubemap = "vr:close_menu_cubemap";

    public static string loadLocalVideos = "vr:load_local_video";

    public static string sendVideos = "vr:send_videos";

    public static string setPageNumber = "vr:set_page_number";
    public static string goToPage = "vr:go_to_page";
}
