using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum State {
    PositiveSpace,
    NegativeSpace
}

public class NegativeSpaceImage : MonoBehaviour {

    private MeshRenderer mr;
    private Material material;

    public State state;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {

        mr = GetComponent<MeshRenderer>();
        material = mr.material;

    }

    public void AssignColor() {

        if (state == State.PositiveSpace) {

            if (ColorManager.Instance == null) {
                Debug.Log("Negative Material");
                GetComponent<Renderer>().material = Resources.Load("Materials/Positive Space") as Material;
            } else {
                Debug.Log("Negative Color");
                material.SetColor("_Color", ColorManager.Instance.PositiveSpaceColor);
            }

        } else {

            if (ColorManager.Instance == null) {
                Debug.Log("Positive Material");
                GetComponent<Renderer>().material = Resources.Load("Materials/Negative Space") as Material;
            } else {
                Debug.Log("Positive Color");
                material.SetColor("_Color", ColorManager.Instance.NegativeSpaceColor);
            }

        }

    }

    public void InvertColor() {

        if (state == State.PositiveSpace) {
            state = State.NegativeSpace;
        } else {
            state = State.PositiveSpace;
        }

        AssignColor();

    }

    public void MakeTransparent() {

    }

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseDown() {

    }

#if UNITY_EDITOR
    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    void OnValidate() {

        // AssignColor();

    }
#endif

}