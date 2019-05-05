using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SplashPanel : BasicPanel
{
    [Header("Components")]
    [SerializeField] private Button BtnTap;
    [SerializeField] private Text TxtTap;
    [SerializeField] private Image ImgIcon;
    [SerializeField] private Image[] Line;

    private int _countLine = 0;

    protected override void Start()
    {
        base.Start();

        BtnTap.onClick.AddListener(() =>
        {
            TxtTap.gameObject.SetActive(false);
            ImgIcon.gameObject.SetActive(false);
            StartCoroutine(AnimStartAsync());
        });

        SetActive(true);
    }

    private IEnumerator AnimStartAsync()
    {
        foreach (var line in Line)
        {
            yield return new WaitForSeconds(0.05f);
            StartCoroutine(AnimAsync(line));
        }
    }

    private IEnumerator AnimAsync(Image line)
    {
        while (Math.Abs(line.fillAmount - 1) > 0.0001)
        {
            yield return new WaitForSeconds(0.01f * Time.deltaTime);
            line.fillAmount += 0.075f;
        }

        yield return new WaitForSeconds(0.50f);
        line.fillOrigin = 1;

        while (Math.Abs(line.fillAmount) > 0.0001)
        {
            yield return new WaitForSeconds(0.01f * Time.deltaTime);
            line.fillAmount -= 0.075f;
        }

        _countLine++;

        if (_countLine == Line.Length)
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        ScenesManager.Instance.LoadScene("Main");
    }
}