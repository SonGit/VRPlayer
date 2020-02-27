using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;


[CustomEditor(typeof(VRButton))]
public class CustomVRFlashButtonEditor : UnityEditor.UI.ButtonEditor
{
    public override void OnInspectorGUI()
    {

        VRButton component = (VRButton)target;

        base.OnInspectorGUI();

        component._text = (Graphic)EditorGUILayout.ObjectField("Text Graphic", component._text, typeof(Graphic), true);
        component._loopable = EditorGUILayout.Toggle("Loopable", component._loopable);
        component._vrSlider = (VRSlider)EditorGUILayout.ObjectField("attached VR slider", component._vrSlider, typeof(VRSlider), true);
    }
}
