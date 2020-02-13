using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageButton : MonoBehaviour
{
    public Text label;

    public int page;
    void Start()
    {
        label = this.GetComponentInChildren<Text>();
    }

    public void SetData(int page)
    {
        this.page = page;

        page++;
        label.text = page.ToString();
        GetComponent<Toggle>().enabled = true;
    }
    public void NoData()
    {
        this.page = -1;
        label.text = "";
        GetComponent<Toggle>().enabled = false;
    }

    public void Reset()
    {
        ToggleGroup group = this.GetComponentInParent<ToggleGroup>();
        group.SetAllTogglesOff();
    }
    public void Activate()
    {
        GetComponent<Toggle>().isOn = true;
    }

    public void GoToPage(bool enabled)
    {
        if(enabled)
        {
            if(page != -1)
            {
                MessageDispatcher.SendMessageData(GameEvent.goToPage, page);
            }
        }
    }
}
