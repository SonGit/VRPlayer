using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        //yield return new WaitForEndOfFrame();
       // MessageDispatcher.SendMessageData(GameEvent.showScreenCursor, EnumMessageDelay.IMMEDIATE);
      //  MessageDispatcher.SendMessageData(GameEvent.closeMenuCubemap, EnumMessageDelay.IMMEDIATE);
        yield return new WaitForSeconds(1);
        MessageDispatcher.SendMessageData(GameEvent.loadLocalVideos, EnumMessageDelay.IMMEDIATE);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
