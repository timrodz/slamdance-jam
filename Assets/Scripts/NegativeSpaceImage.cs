using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

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
    public bool canPlayEvents = false;

    public int count = 0;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake () {

        material = GetComponent<Renderer> ().material;

    }

    public void AssignColor () {

        if (state == State.PositiveSpace) {

            if (ColorManager.Instance == null) {
                // Debug.Log ("Negative Material");
                GetComponent<Renderer> ().material = Resources.Load ("Materials/Positive Space") as Material;
            } else {
                // Debug.Log ("Negative Color");
                material.SetColor ("_Color", ColorManager.Instance.PositiveSpaceColor);
            }

        } else {

            if (ColorManager.Instance == null) {
                // Debug.Log ("Positive Material");
                GetComponent<Renderer> ().material = Resources.Load ("Materials/Negative Space") as Material;
            } else {
                // Debug.Log ("Positive Color");
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

    /// <summary>
    /// Called when the mouse enters the GUIElement or Collider.
    /// </summary>
    void OnMouseEnter () {

        if (!canPlayEvents) {
            return;
        }

        if (!canInteract) {
            return;
        }

        transform.DOScale (1.15f, 0.25f);

    }

    /// <summary>
    /// Called when the mouse is not any longer over the GUIElement or Collider.
    /// </summary>
    void OnMouseExit () {

        if (!canPlayEvents) {
            return;
        }

        if (!canInteract) {
            return;
        }

        transform.DOScale (1, 0.25f);

    }

    public void IncrementCount() {
        
        StartCoroutine(IncrementCountHelper(0.05f));

    }

    private IEnumerator IncrementCountHelper (float delay) {

        Debug.Log (name + " - animation #" + count);
        transform.DOScale (1, 0.15f);

        yield return new WaitForSeconds (delay);

        count++;

        Debug.Log (name + " - animation #" + (count - 1) + " completed");
    
    }

    public void AllowInteraction (float delay) {

        StartCoroutine(AllowInteractionHelper(delay));

    }

    private IEnumerator AllowInteractionHelper(float delay) {

        transform.DOScale (1, 0.15f);
        yield return new WaitForSeconds (delay);
        canInteract = true;

    }

    public void AllowEvents(float delay) {

        transform.DOScale (1, 0.15f);
        StartCoroutine(AllowEventsHelper(delay));
        canPlayEvents = true;

    }

    private IEnumerator AllowEventsHelper (float delay) {

        yield return new WaitForSeconds (delay);
        canPlayEvents = true;

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