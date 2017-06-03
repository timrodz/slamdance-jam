using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Fruit : MonoBehaviour {

    public Transform tree;

    public int count = 0;

    public void PlayAnimation () {

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

            transform.DOJump (new Vector3 (transform.position.x + 4, transform.position.y, transform.position.z), 3, 3, 2).SetEase (Ease.Linear).OnComplete(IncrementCount);

        }

        if (count == 3) {

            BackgroundManager.Instance.ChangeColor (this.transform);
            count++;
            FindObjectOfType<LightBulb>().canInteract = true;
            FindObjectOfType<LightBulb>().PlayAnimation();

        }

    }

    private void ChangeColorBack () {

        BackgroundManager.Instance.ChangeColor (this.transform, ColorManager.Instance.NegativeSpaceColor, 1f);
        tree.DOMoveX (-12.5f, 1.5f);
        count = 2;

    }

    private void IncrementCount() {
        count++;
    }

}