using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour {

    private Material material;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {

        material = GetComponent<Renderer>().material;

    }
	
	public void MakeTransparent() {
		
		Color c = material.GetColor("_Color");
		c.a = 0;
		
		material.SetColor("_Color", c);
		
	}

    public void ChangeColor(string hex) {

        material.SetColor("_Color", HexToColor(hex));

    }

    Color HexToColor(string hex) {
		
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
		
    }

}