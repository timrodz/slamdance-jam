using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMat : MonoBehaviour {
	
	// private Renderer renderer;
	
    public Material Mat1;
    public Material Mat2;
    void OnGUI() {
        if (GUILayout.Button("Mat1")) {
            GetComponent<Renderer>().sharedMaterial.color = Color.red;
            // GetComponent<Renderer>().sharedMaterial.shader = Mat1.shader;
            // GetComponent<Renderer>().sharedMaterial.CopyPropertiesFromMaterial(Mat1);
        }
        if (GUILayout.Button("Mat2")) {
            GetComponent<Renderer>().sharedMaterial.color = Color.blue;
            // GetComponent<Renderer>().sharedMaterial.shader = Mat2.shader;
            // GetComponent<Renderer>().sharedMaterial.CopyPropertiesFromMaterial(Mat2);
        }
    }
}