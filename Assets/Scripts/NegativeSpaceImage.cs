using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeSpaceImage : MonoBehaviour {

    [HideInInspector]
    public MeshRenderer ren;

    public State state;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {

        ren = GetComponent<MeshRenderer>();

    }
    
    public void AssignColor() {
        
        switch (state) {

            case State.PositiveSpace:
                {
                    GetComponent<Renderer>().material = Resources.Load("Materials/Positive Space") as Material;
                }
                break;
            case State.NegativeSpace:
                {
                    GetComponent<Renderer>().material = Resources.Load("Materials/Negative Space") as Material;
                }
                break;

        }
        
    }
    
    public void InvertColor() {
        
        switch (state) {

            case State.PositiveSpace:
                {
                    state = State.NegativeSpace;
                }
                break;
            case State.NegativeSpace:
                {
                    state = State.PositiveSpace;
                }
                break;

        }
        
        AssignColor();
        
    }

#if UNITY_EDITOR
    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    void OnValidate() {

        switch (state) {

            case State.PositiveSpace:
                {
                    GetComponent<Renderer>().material = Resources.Load("Materials/Positive Space") as Material;
                }
                break;
            case State.NegativeSpace:
                {
                    GetComponent<Renderer>().material = Resources.Load("Materials/Negative Space") as Material;
                }
                break;

        }

    }
#endif

}

[System.Serializable]
public enum State {
    PositiveSpace,
    NegativeSpace
}