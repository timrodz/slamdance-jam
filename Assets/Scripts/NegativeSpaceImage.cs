using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public enum State {
    PositiveSpace,
    NegativeSpace
}

public class NegativeSpaceImage : MonoBehaviour {

    [HideInInspector]
    public Material material;

    public State state;

    public bool canInteract = true;
    public bool shouldRepeatEvent = true;
    private bool canPlayEvents = true;
    public UnityEvent[] onMouseDown;
    private int clickCount = 0;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake () {

        material = GetComponent<Renderer> ().material;

    }

    public void AssignColor () {

        if (state == State.PositiveSpace) {

            if (ColorManager.Instance == null) {
                Debug.Log ("Negative Material");
                GetComponent<Renderer> ().material = Resources.Load ("Materials/Positive Space") as Material;
            } else {
                Debug.Log ("Negative Color");
                material.SetColor ("_Color", ColorManager.Instance.PositiveSpaceColor);
            }

        } else {

            if (ColorManager.Instance == null) {
                Debug.Log ("Positive Material");
                GetComponent<Renderer> ().material = Resources.Load ("Materials/Negative Space") as Material;
            } else {
                Debug.Log ("Positive Color");
                material.SetColor ("_Color", ColorManager.Instance.NegativeSpaceColor);
            }

        }

    }

    public void InvertColor () {

        if (state == State.PositiveSpace) {
            state = State.NegativeSpace;
        } else {
            state = State.PositiveSpace;
        }

        AssignColor ();

    }

    public void MakeTransparent () {

    }

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseDown () {

        if (!canInteract || !canPlayEvents) {
            return;
        }

        if (material.GetColor ("_Color") != BackgroundManager.Instance.currentBackgroundColor) {

            Debug.Log ("OnMouseDown " + name);

            if (onMouseDown.Length > 1) {

                onMouseDown[clickCount].Invoke ();
                clickCount++;

                if (clickCount > onMouseDown.Length - 1) {
                    canPlayEvents = false;
                    ResetClickCount ();
                }

            } else if (onMouseDown.Length == 1) {

                onMouseDown[clickCount].Invoke ();

                if (!shouldRepeatEvent) {
                    canPlayEvents = false;
                }

            }

        }

    }

    /// <summary>
    /// Called when the mouse enters the GUIElement or Collider.
    /// </summary>
    void OnMouseEnter () {

        if (!canInteract || !canPlayEvents || (material.GetColor("_Color") == BackgroundManager.Instance.currentBackgroundColor)) {
            return;
        }

        transform.DOScale (1.15f, 0.25f);

    }

    /// <summary>
    /// Called when the mouse is not any longer over the GUIElement or Collider.
    /// </summary>
    void OnMouseExit () {

        if (!canInteract || !canPlayEvents || (material.GetColor("_Color") == BackgroundManager.Instance.currentBackgroundColor)) {
            return;
        }

        transform.DOScale (1, 0.25f);

    }

    public void EnableInteraction () {

        canInteract = true;

    }

    public void DisableInteraction () {

        canInteract = false;

    }

    public void ResetClickCount () {

        Debug.Log ("Reset click counts for " + name);
        clickCount = 0;

    }

#if UNITY_EDITOR
    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    void OnValidate () {

        AssignColor ();

    }
#endif

}