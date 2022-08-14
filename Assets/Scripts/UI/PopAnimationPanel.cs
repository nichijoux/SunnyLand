using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopAnimationPanel : MonoBehaviour{
    /// <summary>
    /// 显示面板的动画曲线
    /// </summary>
    public AnimationCurve showCurve;

    /// <summary>
    /// 隐藏面板的动画曲线
    /// </summary>
    public AnimationCurve hideCurve;

    /// <summary>
    /// 显示动画的速率
    /// </summary>
    public float animationSpeed;

    /// <summary>
    /// 显示面板
    /// </summary>
    protected IEnumerator ShowPanel(GameObject gameObject){
        float timer = 0;
        while(timer <= 1){
            gameObject.transform.localScale = Vector3.one * showCurve.Evaluate(timer);
            timer += Time.deltaTime * animationSpeed;
            yield return null;
        }
    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    protected IEnumerator HidePanel(GameObject gameObject){
        float timer = 0;
        while(timer <= 1){
            gameObject.transform.localScale = Vector3.one * hideCurve.Evaluate(timer);
            timer += Time.deltaTime * animationSpeed;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}