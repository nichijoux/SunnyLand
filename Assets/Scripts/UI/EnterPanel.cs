using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 进入下一关的面板类
/// </summary>
public class EnterPanel : PopAnimationPanel{
    /// <summary>
    /// UI面板
    /// </summary>
    public GameObject enterPanelObject;

    /// <summary>
    /// 显示对话框
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other) {
        GameObject otherObject = other.gameObject;
        
        // 如果碰撞体是玩家
        if(otherObject.CompareTag("Player")){
            enterPanelObject.SetActive(true);
            StartCoroutine(ShowPanel(enterPanelObject));
        }
    }

    /// <summary>
    /// 隐藏对话框
    /// </summary>
    private void OnTriggerExit2D(Collider2D other) {
        StartCoroutine(HidePanel(enterPanelObject));
    }
}