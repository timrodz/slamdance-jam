using DG.Tweening;
using UnityEngine;

public class Credits : MonoBehaviour {

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start () {

		transform.DORotate(Vector3.forward * -5, 1).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);

    }

    public void Hide (float duration, float delay, Ease easeType) {

        transform.DOScale (0, duration).SetDelay (delay).SetEase (easeType);

    }

    public void Show (float duration, float delay, Ease easeType) {

        transform.DOScale (1, duration).SetDelay (delay).SetEase (easeType);

    }

}