using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LightBulb : MonoBehaviour {

    public Transform lamp;
	public Transform Light;

    public bool canInteract = false;

	public int count = 0;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start () {

		lamp.DOMoveY(lamp.position.y + 6, 0);

    }

	public void PlayAnimation() {

		if (!canInteract) {
            BackgroundManager.Instance.ChangeColor(this.transform);
			return;
		}

		if (count == 0) {

			GetComponent<NegativeSpaceImage>().canInteract = false;
			transform.DOScale (1, 0.25f);
			lamp.DOMoveY(lamp.position.y - 6, 2).OnComplete(IncrementCount);

		}

		if (count == 1) {

			ScaleLightDownwards();

		}

	}

	public void ScaleLightDownwards() {

		Light.DOScaleX(1.5f, 3);
		Light.DOScaleY(0.7f, 3);
		Light.DOLocalMoveY(-4.6f, 3).OnComplete(IncrementCount);

	}

	private void IncrementCount() {
		GetComponent<NegativeSpaceImage>().canInteract = true;
        count++;
    }

}