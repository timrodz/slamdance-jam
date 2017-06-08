using DG.Tweening;
using UnityEngine;

public class Credits : MonoBehaviour {

    public Transform title;
    public Transform credits;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start () {

        credits.DOScale(Vector3.zero, 0);
        title.DORotate (Vector3.forward * -5, 1).SetEase (Ease.InOutQuad).SetLoops (-1, LoopType.Yoyo);

    }

    public void Hide (float duration, float delay, Ease easeType) {

        title.DOScale (0, duration).SetDelay (delay).SetEase (easeType);
		title.GetComponentInParent<UnityEngine.UI.Image>().DOFade(0, duration).SetDelay (delay).SetEase (easeType);

    }

    public void Show (float duration, float delay, Ease easeType) {

		credits.eulerAngles = new Vector3(0, 0, 5);
        credits.DORotate (Vector3.forward * -5, 1).SetEase (Ease.InOutQuad).SetLoops (-1, LoopType.Yoyo);
        credits.DOScale (1, duration).SetDelay (delay).SetEase (easeType);
		
		Camera.main.DOOrthoSize(10, 4).SetDelay(duration + delay + 3 + 2.5f);
		credits.parent.DOShakePosition(4, 25f).SetDelay(duration + delay + 3);
		credits.DOMoveY(-200, 4f).SetDelay(duration + delay + 3);

    }

}