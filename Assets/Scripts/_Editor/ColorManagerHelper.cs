#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof (ColorManager))]
public class ColorManagerHelper : Editor {

    public override void OnInspectorGUI() {

        ColorManager colorMan = (ColorManager) target;

        // this if statement is true whenever the inspector changes its values
        if (DrawDefaultInspector()) { }

        if (GUILayout.Button("Populate Image list")) {

            colorMan.PopulateImageList();

        }

        if (GUILayout.Button("Assign Colors")) {

            colorMan.AssignColors();

        }

        if (GUILayout.Button("Invert Colors")) {

            colorMan.InvertColors();

        }

    }

}
#endif