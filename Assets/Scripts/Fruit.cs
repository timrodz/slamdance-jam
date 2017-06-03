using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fruit : MonoBehaviour {

    public void PlayAnimation() {
		
        if (BackgroundManager.Instance.currentBackgroundColor != ColorManager.Instance.PositiveSpaceColor) {
			
			BackgroundManager.Instance.ChangeColor(this.transform);
			
		} else {
			
			transform.DOMoveY(-4.3f, 1.5f).SetEase(Ease.OutBounce).OnComplete(ChangeColorBack);
			
		}

    }
	
	private void ChangeColorBack() {
		
		Debug.Log("Changing bg color");
		BackgroundManager.Instance.ChangeColor(this.transform, ColorManager.Instance.NegativeSpaceColor, 1f);
		
	}

}