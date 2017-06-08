using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LightBulb : NegativeSpaceImage {

    private Fruit fruit;

    public Transform lamp;
    public Transform Light;

    private NegativeSpaceImage image;
    private Color imageColor;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start () {

        image = GetComponent<NegativeSpaceImage>();
        imageColor = image.material.GetColor ("_Color");
        fruit = FindObjectOfType<Fruit>();

    }

    public void PlayAnimation () {

        if (imageColor != BackgroundManager.Instance.currentBackgroundColor) {

            if (count == 0) {

                AudioManager.Instance.Play ("Click");
                BackgroundManager.Instance.ChangeColor (this.transform);
                fruit.AllowInteraction(0.05f);
                return;

            }

            // Lamp appears
            if (count == 1) {

                AudioManager.Instance.Play ("Click");

                transform.DOScale (1, 0.25f);

                lamp.DOMoveY (lamp.position.y - 5, 2);

                IncrementCount();
                AllowInteraction(2f);

            }

            // Light shows mountains and the dot
            if (count == 2) {

                AudioManager.Instance.Play ("Click");

                Light.DOScaleX (1.5f, 2.5f).SetEase (Ease.OutExpo);
                Light.DOScaleY (0.7f, 2.5f).SetEase (Ease.OutExpo);
                Light.DOLocalMoveY (-4.6f, 2.5f).SetEase (Ease.OutExpo);

                IncrementCount();
                fruit.AllowInteraction(0.15f);

            }

            // Hand appears
            if (count == 3) {

                AudioManager.Instance.Play ("Click");
                IncrementCount();

                // Remove mountains and dot with sun
                Transform fruit = FindObjectOfType<Fruit> ().transform;
                fruit.transform.DOMoveY (20, 2.5f).SetEase (Ease.OutQuad);
                fruit.transform.DOMoveX (4, 2.5f).SetEase (Ease.OutQuad);

                Mountain[] m = FindObjectsOfType<Mountain> ();

                m[0].transform.DOMoveY (-35, 2.5f).SetDelay (0.5f).SetEase (Ease.OutExpo);
                m[1].transform.DOMoveY (-35, 2.5f).SetDelay (0.5f).SetEase (Ease.OutExpo);

                // HAND
                Transform hand_1 = FindObjectOfType<Hand_1> ().transform;
                hand_1.DOMoveX (-8.4f, 2).SetDelay (1);

                // Take the dot with the hand
                hand_1.DOMoveX (-12f, 2).SetDelay (3);
                transform.DOMoveX (transform.position.x - 3.75f, 2).SetDelay (3);

                // HAND 2
                // Takes the dot from the first hand
                Transform hand_2 = FindObjectOfType<Hand_2> ().transform;
                hand_2.DOMoveX (-6.7f, 3).SetDelay (5);

                hand_2.DOMoveX (4.5f, 3).SetDelay (8);
                transform.DOMoveX (transform.position.x + 7.8f, 3).SetDelay (8);

                // Hide hand_2
                hand_2.DOMoveX (12.5f, 3).SetDelay (11);

                // show dot in middle
                AllowInteraction(13f);
                fruit.GetChild (0).DOScale (0, 0).SetDelay (11);
                fruit.DOMove (new Vector3 (-1.75f, 0, 0), 0).SetDelay (11);

            }

            // Finish the game
            if (count == 4) {

                AudioManager.Instance.Play ("Click");

                Material m = Resources.Load ("Materials/Tree") as Material;
                BackgroundManager.Instance.ChangeColor (Vector3.zero, m.GetColor ("_Color"), 0);

                image.canInteract = false;
                transform.DOScale (1, 0.15f);

                FindObjectOfType<RotateAndExpand> ().PlayFirstAnimationInverse ();

            }

        }

    }

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseDown () {

        transform.DOScale (1, 0.15f);
        PlayAnimation();
        canInteract = false;

    }

}