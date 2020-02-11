using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recenter : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Drag the VR cam handle here in the inspector")]
    private Transform VRCamHandle;

    [SerializeField]
    [Tooltip("Drag the VR Sphere")]
    public Transform VRSphere;

    // Start is called before the first frame update
    void Awake()
    {
        MessageDispatcher.AddListener(GameEvent.nodEvent, OnReceiveRecenterMessage);
    }

    void OnReceiveRecenterMessage(IMessage rMessage)
    {
        Do();
    }

    // Start recenter
    public void Do()
    {
        VRSphere.transform.position = VRCamHandle.transform.position;
        VRSphere.transform.eulerAngles = VRCamHandle.transform.eulerAngles;
    }

    // Reset Rotation
    public void ResetRotation()
    {
        VRSphere.transform.localPosition = Vector3.zero;
        VRCamHandle.transform.localPosition = Vector3.zero;

        VRSphere.transform.localEulerAngles = Vector3.zero;
        VRCamHandle.transform.localEulerAngles = Vector3.zero;

        Do();
    }
}
