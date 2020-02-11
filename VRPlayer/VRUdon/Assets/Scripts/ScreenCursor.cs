using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCursor : MonoBehaviour
{
    // Start is called before the first frame update
    CanvasGroup canvasGroup;
    void Awake()
    {
        MessageDispatcher.AddListener(GameEvent.showScreenCursor, OnShowScreenCursor);
        MessageDispatcher.AddListener(GameEvent.closeScreenCursor, OnCloseScreenCursor);

        canvasGroup = this.GetComponent<CanvasGroup>();
    }

    void OnShowScreenCursor(IMessage rMessage)
    {
        Show();
    }

    void OnCloseScreenCursor(IMessage rMessage)
    {
        Close();
    }

    public void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void Close()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        MessageDispatcher.SendMessageData(GameEvent.showMenuCubemap, "");
    }
}
