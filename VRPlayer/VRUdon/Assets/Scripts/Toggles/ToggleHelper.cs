using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleHelper : MonoBehaviour
{
    public string message;

    public void Toggle(bool val)
    {
        if(val)
        {
            print("Sending message..." + message);
            MessageDispatcher.SendMessageData(message,null);
        }
    }
}
