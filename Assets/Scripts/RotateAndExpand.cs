using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RotateAndExpand : MonoBehaviour {

    [Header ("First animation")]
    public Transform yinYang;
    public Vector3 scale = Vector3.one;
    public Ease firstEase = Ease.OutQuad;
    public float firstDuration = 3;
    public float angle = 90;

    [Header ("Second animation")]
    public Transform positiveSide;
    public Transform negativeSide;
    public float yOffset = 6;
    public Ease secondEase = Ease.InExpo;
    public float secondDuration;

    [SerializeField]
    public bool canInteract = true;
    private int count = 0;

    private void PlayFirstAnimation () {

        Camera.main.DOOrthoSize (5, 1f);
        transform.DOScale (scale, firstDuration).SetDelay(1f).SetEase (firstEase);
        transform.DORotate (Vector3.forward * angle, firstDuration).SetDelay(1f).SetEase (firstEase).OnComplete (PlaySecondAnimation);

    }

    private void PlaySecondAnimation () {

        // Negative side goes up
        negativeSide.DOMoveY (negativeSide.position.y + yOffset, secondDuration).SetEase (secondEase);

        // Positive side goes down
        positiveSide.DOMoveY (positiveSide.position.y - yOffset, secondDuration).SetEase (secondEase);

        StartCoroutine (EnableChildInteraction (secondDuration + 0.1f));

    }

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseDown () {

        if (canInteract) {

            if (count == 0) {

                canInteract = false;

                count++;

                PlayFirstAnimation ();

                // Get rid of the circle collider
                Destroy (GetComponent<CircleCollider2D> ());

            }
        }

    }

    private void IncrementCount () {
        count++;
    }

    private IEnumerator EnableChildInteraction (float waitTime) {

        yield return new WaitForSeconds (waitTime);

        for (int i = 0; i < yinYang.childCount; i++) {

            NegativeSpaceImage child = yinYang.GetChild (i).GetComponent<NegativeSpaceImage> ();

            if (child != null) {
                child.gameObject.AddComponent<CircleCollider2D> ();
                child.canInteract = true;
            }

        }

    }

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseEnter () {

        if (canInteract) {
            Camera.main.DOOrthoSize (4.5f, 0.3f);
        }

    }

    /// <summary>
    /// Called when the mouse is not any longer over the GUIElement or Collider.
    /// </summary>
    void OnMouseExit () {

        if (canInteract) {
            Camera.main.DOOrthoSize (5, 0.3f);
        }

    }

    private IEnumerator ShrinkCamera (float value) {

        for (float t = 0f; t <= 1f; t += (Time.deltaTime / 015f)) {

            yield return null;

        }

    }

}