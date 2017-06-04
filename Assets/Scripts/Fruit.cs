using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Fruit : MonoBehaviour {

    public Transform tree;

    public int count = 0;

    public bool canInteract = true;

    public void PlayAnimation () {

        if (GetComponent<NegativeSpaceImage> ().material.GetColor ("_Color") != BackgroundManager.Instance.currentBackgroundColor) {

            transform.DOScale (1, 0.15f);

            if (BackgroundManager.Instance.currentBackgroundColor != ColorManager.Instance.PositiveSpaceColor) {

                if (count < 2) {
                    BackgroundManager.Instance.ChangeColor (this.transform);
                    count++;
                }

            } else {

                if (count < 2) {
                    transform.DOMoveY (-4.3f, 1.5f).SetEase (Ease.OutBounce).OnComplete (ChangeColorBack);
                }

            }

            if (count == 2) {

                StartCoroutine (Jump (0.75f));

            }

            if (count == 3) {

                BackgroundManager.Instance.ChangeColor (this.transform);
                count++;
                
                FindObjectOfType<LightBulb> ().canInteract = true;
                FindObjectOfType<LightBulb> ().PlayAnimation ();

                Mountain[] m = FindObjectsOfType<Mountain> ();

                m[0].transform.DOMoveY (-1.87f, 2.5f).SetDelay(0.5f).SetEase (Ease.OutExpo);
                m[0].transform.DOMoveY (-2.67f, 2.5f).SetDelay(0.5f).SetEase (Ease.OutExpo);

            }

        }

        if (count == 4) {

            transform.DOScale (1, 0.15f);

        }

    }

    private IEnumerator Jump (float delay) {

        GetComponent<NegativeSpaceImage> ().canInteract = false;

        // First jump
        Vector3 pos = transform.position;
        pos.x += 1.33f;
        transform.DOJump (pos, 2, 1, delay).SetEase (Ease.Linear);

        yield return new WaitForSeconds (delay);

        delay -= 0.1f;

        // Second jump
        pos = transform.position;
        pos.x += 1.33f;
        transform.DOJump (pos, 1.5f, 1, delay).SetEase (Ease.Linear);

        yield return new WaitForSeconds (delay);

        delay -= 0.1f;

        // Third jump
        pos = transform.position;
        pos.x += 1.33f;
        transform.DOJump (pos, 1, 1, delay).SetEase (Ease.Linear);

        yield return new WaitForSeconds (delay);

        IncrementCount ();

    }

    private void ChangeColorBack () {

        BackgroundManager.Instance.ChangeColor (this.transform, ColorManager.Instance.NegativeSpaceColor, 1f);
        FindObjectOfType<LightBulb> ().transform.DOScale (1, 0.25f);
        tree.DOMoveX (-12.5f, 1.5f);
        tree.DOScale (0, 1.5f);
        count = 2;

    }

    private void IncrementCount () {
        GetComponent<NegativeSpaceImage> ().canInteract = true;
        count++;
    }

}