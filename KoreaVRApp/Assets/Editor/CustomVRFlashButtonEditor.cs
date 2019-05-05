using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;


[CustomEditor(typeof(VR_FlashButton))]
public class CustomVRFlashButtonEditor : UnityEditor.UI.ButtonEditor
{
    public override void OnInspectorGUI()
    {

        VR_FlashButton component = (VR_FlashButton)target;

        base.OnInspectorGUI();

        component._text = (Graphic)EditorGUILayout.ObjectField("Text Graphic", component._text, typeof(Graphic), true);
        component._loopable = EditorGUILayout.Toggle("Loopable", component._loopable);
    }
}
