using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 收集物的显示
/// </summary>
public class CollectionControl : MonoBehaviour{
    /// <summary>
    /// 显示Cherry数字的UI组件
    /// </summary>
    [SerializeField]
    private TMP_Text cherryNumber;

    /// <summary>
    /// 显示Gem数字的UI组件
    /// </summary>
    [SerializeField]
    private TMP_Text gemNumber;

    /// <summary>
    /// cherry数量增加
    /// </summary>
    public void AddCherry(){
        int number = 0;
        int.TryParse(cherryNumber.text,out number);
        number++;
        cherryNumber.text = number.ToString();
    }

    /// <summary>
    /// gem数量增加
    /// </summary>
    public void AddGem(){
        int number = 0;
        int.TryParse(gemNumber.text,out number);
        number++;
        gemNumber.text = number.ToString();
    }
}