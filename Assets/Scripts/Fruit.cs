using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Fruit : NegativeSpaceImage {

    private LightBulb lightBulb;

    public Transform tree;

    private bool canPlayFallFromTreeSound = true;

    private NegativeSpaceImage image;
    private Color imageColor;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start () {

        image = GetComponent<NegativeSpaceImage> ();
        imageColor = image.material.GetColor ("_Color");

        lightBulb = FindObjectOfType<LightBulb> ();

    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update () {

        if (count < 2) {

            if (transform.position.y < -4.15f && canPlayFallFromTreeSound) {
                StartCoroutine (FallFromTree ());
            }

        }

    }

    public void PlayAnimation () {

        if (imageColor != BackgroundManager.Instance.currentBackgroundColor) {

            transform.DOScale (1, 0.15f);

            if (BackgroundManager.Instance.currentBackgroundColor != ColorManager.Instance.PositiveSpaceColor) {

                if (count < 2) {
                    AudioManager.Instance.Play ("Click");
                    BackgroundManager.Instance.ChangeColor (this.transform);
                }

            } else {

                if (count < 2) {
                    AudioManager.Instance.Play ("Click");
                    transform.DOMoveY (-4.3f, 1.5f).SetEase (Ease.OutBounce).OnComplete (ChangeColorBack);
                }

            }

            // Jump
            if (count == 2) {

                AudioManager.Instance.Play ("Click");
                StartCoroutine (Jump (0.75f));

            }

        }

        // Show mountains and interact with the lightbulb
        if (count == 3) {

            AudioManager.Instance.Play ("Click");
            BackgroundManager.Instance.ChangeColor (this.transform);
            transform.DOScale (1, 0.15f);

            lightBulb.PlayAnimation ();

            Mountain[] m = FindObjectsOfType<Mountain> ();

            m[0].transform.DOMoveY (-2.67f, 2.5f).SetDelay (0.5f).SetEase (Ease.OutExpo);
            m[1].transform.DOMoveY (-1.87f, 2.5f).SetDelay (0.5f).SetEase (Ease.OutExpo);

            IncrementCount ();

        }

        // Move lamp upwards and show the dot up as well
        if (count == 4) {

            AudioManager.Instance.Play ("Click");
            BackgroundManager.Instance.ChangeColor (this.transform.position, ColorManager.Instance.PositiveSpaceColor, 0);

            Transform lamp = FindObjectOfType<Lamp> ().transform;

            lamp.DOMoveY (lamp.transform.position.y + 5, 1).SetDelay (1);

            lamp.GetChild (0).DOScale (0, 1).SetDelay (1);

            lightBulb.transform.DOMoveX (-6, 1).SetDelay (1);

            transform.DOScale (1, 0.15f);

            transform.DOMoveY (transform.position.y + 5, 3);

            IncrementCount ();
            AllowInteraction (3f);

        }

        // Show the sunburst and rotate it
        if (count == 5) {

            AudioManager.Instance.Play ("Click");

            image.canPlayEvents = false;

            BackgroundManager.Instance.ChangeColor (this.transform.position, ColorManager.Instance.NegativeSpaceColor, 3.85f);

            Transform child = transform.GetChild (0);

            child.DOScale (Vector3.one * 0.5f, 3).SetEase (Ease.OutQuart);
            child.DORotate (Vector3.forward * 180, 1.5f).SetDelay (3).SetEase (Ease.InOutExpo);

            IncrementCount ();

            lightBulb.AllowInteraction (3.85f);

        }

    }

    private IEnumerator Jump (float delay) {

        image.canInteract = false;

        AudioManager.Instance.Play ("Bounce");

        // First jump
        Vector3 pos = transform.position;
        pos.x += 1.33f;
        transform.DOJump (pos, 2, 1, delay).SetEase (Ease.Linear);

        yield return new WaitForSeconds (delay);

        AudioManager.Instance.Play ("Bounce");

        delay -= 0.1f;

        // Second jump
        pos = transform.position;
        pos.x += 1.33f;
        transform.DOJump (pos, 1.5f, 1, delay).SetEase (Ease.Linear);

        yield return new WaitForSeconds (delay);

        AudioManager.Instance.Play ("Bounce");

        delay -= 0.1f;

        // Third jump
        pos = transform.position;
        pos.x += 1.33f;
        transform.DOJump (pos, 1, 1, delay).SetEase (Ease.Linear);

        yield return new WaitForSeconds (delay);

        AudioManager.Instance.Play ("Bounce");

        lightBulb.IncrementCount ();

        IncrementCount ();

        AllowInteraction (0);

    }

    private void ChangeColorBack () {

        transform.DOScale (1, 0.15f);

        StopCoroutine ("FallFromTree");

        canPlayFallFromTreeSound = false;
        BackgroundManager.Instance.ChangeColor (this.transform.position, ColorManager.Instance.NegativeSpaceColor, 1f);

        lightBulb.transform.DOScale (1, 0.25f);

        tree.DOMoveX (-12.5f, 1.5f);
        tree.DOScale (0, 1.5f);
        count = 2;

        lightBulb.AllowInteraction (0.25f);
        // lightBulb.IncrementCount (2f);

    }

    private IEnumerator FallFromTree () {

        AudioManager.Instance.Play ("Bounce");
        canPlayFallFromTreeSound = false;

        yield return new WaitForSeconds (0.3f);
        canPlayFallFromTreeSound = true;

    }

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseDown () {

        if (!canInteract) {
            return;
        }

        transform.DOScale (1, 0.15f);
        PlayAnimation ();
        canInteract = false;

    }

}