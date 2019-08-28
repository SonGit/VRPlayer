using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RenderHeads.Media.AVProVideo.Demos;

public class VR_VideoFinishMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _root;

    private VCR vcrMenu;
    private SceneVR sceneVR;
    private bool _isVideoFinish;


    public GameObject root
    {
        get
        {
            return _root;
        }

        set
        {
            _root = value;
        }
    }

    public bool isVideoFinish
    {
        get
        {
            return _isVideoFinish;
        }

        set
        {
            _isVideoFinish = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        vcrMenu = UnityEngine.Object.FindObjectOfType<VCR>();
        sceneVR = UnityEngine.Object.FindObjectOfType<SceneVR>();

        DisableVideoFinishUI();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsVideoFinish();
    }


    public void VR_VideoFinishMenu_OnBack()
    {
        DisableVideoFinishUI();

        if (vcrMenu != null)
        {
            vcrMenu.LoadingPlayer.Control.Rewind();
        }

        if (sceneVR != null)
        {
            sceneVR.ShowCurrentVR_Menu();
        }

        StartCoroutine(WaitIsVideoFinish());
    }

    public void VR_VideoFinishMenu_ReplayVideo()
    {
        DisableVideoFinishUI();

        if (vcrMenu != null)
        {
            vcrMenu.LoadingPlayer.Control.Rewind();
        }

        if (sceneVR != null)
        {
            sceneVR.HideProgressBar();
        }

        StartCoroutine(WaitIsVideoFinish());
    }

    private void EnableVideoFinishUI()
    {
        if (root != null)
        {
            if (!root.activeSelf)
            {
                root.SetActive(true);
            }
        }
    }

    public void DisableVideoFinishUI()
    {
        if (root != null)
        {
            if (root.activeSelf)
            {
                root.SetActive(false);
            }
        }
    }

    private void CheckIsVideoFinish() {
        if (vcrMenu != null && vcrMenu.LoadingPlayer.Control.IsFinished() && sceneVR != null && !sceneVR.vrSetting.isShowSetting && !isVideoFinish)
        {
            isVideoFinish = true;
            EnableVideoFinishUI();

            if (sceneVR != null)
            {
                sceneVR.ShowProgressBar();
            }
        }
    }

    IEnumerator WaitIsVideoFinish()
    {
        yield return new WaitForSeconds(0.4f);
        isVideoFinish = false;
    }

}
