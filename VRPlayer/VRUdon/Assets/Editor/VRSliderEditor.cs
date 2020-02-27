using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;


[CustomEditor(typeof(VRSlider))]
public class VRSliderEditor : UnityEditor.UI.SliderEditor
{
    public override void OnInspectorGUI()
    {

        VRSlider component = (VRSlider)target;

        base.OnInspectorGUI();


        component.seekSlider = EditorGUILayout.Toggle("Is Seeker Slider?", component.seekSlider);

    }
}
