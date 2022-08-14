using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 摄像机移动代码
/// </summary>
public class CameraController : MonoBehaviour
{
    /// <summary>
    /// 人物位置
    /// </summary>
    public Transform player;

    void Start(){
        if(player == null) Debug.LogError("the camera's player is null");
    }

    void Update()
    {
        if(player != null)
            this.transform.position = new Vector3(player.position.x, player.position.y, -10);
    }
}
