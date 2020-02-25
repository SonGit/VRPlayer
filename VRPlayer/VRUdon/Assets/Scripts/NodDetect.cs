using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodDetect : MonoBehaviour
{
    public float Threshold = 7;

    Quaternion itemRotation;
    Quaternion previousRotation;
    Vector3 angularVelocity;

    public Transform camera;

    void Update()
    {
        //Debug.Log ("FixedUpdate");
        //#if UNITY_EDITOR
        itemRotation = camera.rotation;

        Quaternion deltaRotation = itemRotation * Quaternion.Inverse(previousRotation);

        previousRotation = itemRotation;

        float angle = 0.0f;
        Vector3 axis = Vector3.up;

        deltaRotation.ToAngleAxis(out angle, out axis);

        angle *= Mathf.Deg2Rad;

        angularVelocity = axis * angle * (1.0f / Time.deltaTime);

        //print (angularVelocity.magnitude);

        if (angularVelocity.magnitude > Threshold)
        {
           // print("Recentering at angularVelocity  " + angularVelocity.magnitude);
            MessageDispatcher.SendMessageData(GameEvent.recenterEvent, angularVelocity.magnitude);
        }
        //#endif
    }

    void OnEnable()
    {
        itemRotation = camera.rotation;
        previousRotation = itemRotation;
        angularVelocity = Vector3.zero;
    }
}
