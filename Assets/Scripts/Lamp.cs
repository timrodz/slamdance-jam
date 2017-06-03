using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Lamp : MonoBehaviour {

	public bool canInteract = false;

	public int count = 0;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start () {

		transform.DOMoveY(transform.position.y + 6, 0);

    }

	public void PlayAnimation() {

		if (!canInteract) {
			return;
		}

		if (count == 0) {

			transform.DOMoveY(transform.position.y - 6, 2).OnComplete(IncrementCount);

		}

		if (count == 1) {



		}

	}

	private void IncrementCount() {
        count++;
    }

}