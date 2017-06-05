using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Fruit : MonoBehaviour {

    public Transform tree;

    public int count = 0;

    public bool canInteract = true;

    private bool canPlayFallFromTreeSound = true;

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

        if (GetComponent<NegativeSpaceImage> ().material.GetColor ("_Color") != BackgroundManager.Instance.currentBackgroundColor) {

            transform.DOScale (1, 0.15f);

            if (BackgroundManager.Instance.currentBackgroundColor != ColorManager.Instance.PositiveSpaceColor) {

                if (count < 2) {
                    AudioManager.Instance.Play ("Click");
                    BackgroundManager.Instance.ChangeColor (this.transform);
                }

            } else {

                if (count < 2) {
                    AudioManager.Instance.Play ("Click");
                    // StartCoroutine (IncrementCount (3f));
                    transform.DOMoveY (-4.3f, 1.5f).SetEase (Ease.OutBounce).OnComplete (ChangeColorBack);
                }

            }

            // Jump
            if (count == 2) {

                AudioManager.Instance.Play ("Click");
                StartCoroutine (Jump (0.75f));

            }

            // Show mountains and interact with the lightbulb
            if (count == 3) {

                AudioManager.Instance.Play ("Click");
                BackgroundManager.Instance.ChangeColor (this.transform);
                transform.DOScale (1, 0.15f);

                FindObjectOfType<LightBulb> ().canInteract = true;
                FindObjectOfType<LightBulb> ().PlayAnimation ();

                Mountain[] m = FindObjectsOfType<Mountain> ();

                m[0].transform.DOMoveY (-2.67f, 2.5f).SetDelay (0.5f).SetEase (Ease.OutExpo);
                m[1].transform.DOMoveY (-1.87f, 2.5f).SetDelay (0.5f).SetEase (Ease.OutExpo);

                StartCoroutine (IncrementCount (3));

            }

        }

        // Move lamp upwards and show the dot up as well
        if (count == 4) {

            AudioManager.Instance.Play ("Click");
            BackgroundManager.Instance.ChangeColor (this.transform.position, ColorManager.Instance.PositiveSpaceColor, 0);

            Transform lamp = FindObjectOfType<Lamp> ().transform;

            lamp.DOMoveY (lamp.transform.position.y + 5, 1).SetDelay (1);

            lamp.GetChild (0).DOScale (0, 1).SetDelay (1);

            FindObjectOfType<LightBulb> ().transform.DOMoveX (-6, 1).SetDelay (1);

            transform.DOScale (1, 0.15f);

            transform.DOMoveY (transform.position.y + 5, 3);

            StartCoroutine (IncrementCount (3));

        }

        // Show the sunburst and rotate it
        if (count == 5) {

            AudioManager.Instance.Play ("Click");
            GetComponent<NegativeSpaceImage> ().canPlayEvents = false;

            FindObjectOfType<LightBulb> ().GetComponent<NegativeSpaceImage> ().canPlayEvents = true;

            BackgroundManager.Instance.ChangeColor (this.transform.position, ColorManager.Instance.NegativeSpaceColor, 3.85f);

            Transform child = transform.GetChild (0);

            child.DOScale (Vector3.one * 0.5f, 3).SetEase (Ease.OutQuart);
            child.DORotate (Vector3.forward * 180, 1.5f).SetDelay (3).SetEase (Ease.InOutExpo);
            StartCoroutine (IncrementCount (4.5f));

        }

    }

    private IEnumerator Jump (float delay) {

        GetComponent<NegativeSpaceImage> ().canInteract = false;

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

        StartCoroutine (IncrementCount (0));

    }

    private void ChangeColorBack () {

        GetComponent<NegativeSpaceImage> ().canInteract = true;
        transform.DOScale (1, 0.15f);
        StopCoroutine("FallFromTree");
        canPlayFallFromTreeSound = false;
        BackgroundManager.Instance.ChangeColor (this.transform.position, ColorManager.Instance.NegativeSpaceColor, 1f);
        FindObjectOfType<LightBulb> ().transform.DOScale (1, 0.25f);
        tree.DOMoveX (-12.5f, 1.5f);
        tree.DOScale (0, 1.5f);
        count = 2;

    }

    private IEnumerator IncrementCount (float delay) {
        GetComponent<NegativeSpaceImage> ().canInteract = false;
        transform.DOScale (1, 0.15f);
        yield return new WaitForSeconds (delay);
        Debug.Log (name + " - animation #" + count + " completed");
        GetComponent<NegativeSpaceImage> ().canInteract = true;
        count++;
    }

    private IEnumerator FallFromTree () {

        AudioManager.Instance.Play ("Bounce");
        canPlayFallFromTreeSound = false;
        yield return new WaitForSeconds (0.3f);
        canPlayFallFromTreeSound = true;

    }

}