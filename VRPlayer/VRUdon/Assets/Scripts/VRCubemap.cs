using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VRCubemap : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    public string eventString;
    protected void Init()
    {
        meshRenderer = this.GetComponent<MeshRenderer>();
    }

    protected void Show()
    {
        meshRenderer.enabled = true;
    }

    protected void Close()
    {
        meshRenderer.enabled = false;
    }
}
