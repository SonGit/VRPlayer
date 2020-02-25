using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(.5f);
        MessageDispatcher.SendMessageData(GameEvent.showLocalVideoScreen, null);
    }

    void ShowLocalVideoScreen(IMessage rMessage)
    {
      
    }

    private void Update()
    {

    }

}
