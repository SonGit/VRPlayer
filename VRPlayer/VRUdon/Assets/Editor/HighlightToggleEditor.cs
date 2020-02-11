using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

namespace AVR.ADVEngine
{
    [CustomEditor(typeof(HighlightToggle))]
    public class HighlightToggleEditor : ToggleEditor
{
        public override void OnInspectorGUI()
        {
            HighlightToggle targetToggle = (HighlightToggle)target;


            targetToggle._message = EditorGUILayout.TextField("Message to broadcast", targetToggle._message);
            targetToggle._messageOff = EditorGUILayout.TextField("Message to broadcast when toggle off", targetToggle._messageOff);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("On Sprite");
            targetToggle._onSprite = (Sprite)EditorGUILayout.ObjectField(targetToggle._onSprite, typeof(Sprite), allowSceneObjects: true);
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Off Sprite");
            targetToggle._offSprite = (Sprite)EditorGUILayout.ObjectField(targetToggle._offSprite, typeof(Sprite), allowSceneObjects: true);
            EditorGUILayout.EndHorizontal();

            targetToggle._text = (Graphic)EditorGUILayout.ObjectField("Text Graphic", targetToggle._text, typeof(Graphic), true);
            targetToggle._loopable = EditorGUILayout.Toggle("Loopable", targetToggle._loopable);

            base.OnInspectorGUI();
        }
    }
}
