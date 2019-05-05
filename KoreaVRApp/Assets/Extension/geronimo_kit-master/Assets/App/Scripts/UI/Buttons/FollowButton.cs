using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowButton : MonoBehaviour
{
    public string URL;

    private void Start()
    {
        var button = gameObject.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() =>
            {
                Application.OpenURL(URL);
            });
        }
    }
}