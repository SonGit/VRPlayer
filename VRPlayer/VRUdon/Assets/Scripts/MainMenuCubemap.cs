using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCubemap : VRCubemap
{
    // Start is called before the first frame update
    void Awake()
    {
        Init();
        MessageDispatcher.AddListener(GameEvent.showMenuCubemap, OnShow);
        MessageDispatcher.AddListener(GameEvent.closeMenuCubemap, OnClose);
    }

    void OnShow(IMessage rMessage)
    {
        Show();
    }

    void OnClose(IMessage rMessage)
    {
        Close();
    }
}
