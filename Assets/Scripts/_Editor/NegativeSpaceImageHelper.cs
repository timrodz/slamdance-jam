using UnityEditor;
using UnityEngine;

[CustomEditor(typeof (NegativeSpaceImage))]
public class NegativeSpaceImageHelper : Editor {

    public override void OnInspectorGUI() {

        NegativeSpaceImage Image = (NegativeSpaceImage) target;

        // this if statement is true whenever the inspector changes its values
        if (DrawDefaultInspector()) {}
		
		if (GUILayout.Button("Invert Color")) {
			
			Image.InvertColor();
			
		}

    }

}