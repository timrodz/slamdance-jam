using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RotateAndExpand : MonoBehaviour {

    [Header("First animation")]
    public Transform yinYang;
    public Vector3 scale = Vector3.one;
    public Ease firstEase = Ease.OutQuad;
    public float firstDuration = 3;
    public float angle = 90;
    
    [Header("Second animation")]    
    public Transform positiveSide;
    public Transform negativeSide;
    public float yOffset = 6;
    public Ease secondEase = Ease.InExpo;
    public float secondDuration;

    [SerializeField]
    public bool canInteract = true;
    private int count = 0;

    private void PlayFirstAnimation() {

        transform.DOScale(scale, firstDuration).SetEase(firstEase);
        transform.DORotate(Vector3.forward * angle, firstDuration).SetEase(firstEase).OnComplete(IncrementCount);

    }

    private void PlaySecondAnimation() {

        // Negative side goes up
        negativeSide.DOMoveY(negativeSide.position.y + yOffset, secondDuration).SetEase(secondEase);
        
        // Positive side goes down
        positiveSide.DOMoveY(positiveSide.position.y - yOffset, secondDuration).SetEase(secondEase);

    }

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseDown() {

        if (canInteract) {

            if (count == 0) {

                PlayFirstAnimation();

            } else if (count == 1) {

                PlaySecondAnimation();
                // Get rid of the circle collider
                Destroy(GetComponent<CircleCollider2D>());

                StartCoroutine(EnableChildInteraction(secondDuration + 0.1f));
                canInteract = false;

            }

        }

    }

    private void IncrementCount() {
        count++;
    }

    private IEnumerator EnableChildInteraction(float waitTime) {

        yield return new WaitForSeconds(waitTime);

        for (int i = 0; i < yinYang.childCount; i++) {

            NegativeSpaceImage child = yinYang.GetChild(i).GetComponent<NegativeSpaceImage>();
            child.canInteract = true;

        }

    }

}