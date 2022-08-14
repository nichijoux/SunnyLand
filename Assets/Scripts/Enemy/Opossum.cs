using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 鼹鼠脚本
public class Opossum : Enemy
{
    /// <summary>
    /// 鼹鼠刚体组件
    /// </summary>
    private Rigidbody2D opossumBody;

    /// <summary>
    /// 鼹鼠左右边界
    /// </summary>
    public Transform leftTransform,rightTransform;

    /// <summary>
    /// 鼹鼠移动左右边界的实际x坐标
    /// </summary>
    private float leftBorder, rightBorder;

    /// <summary>
    /// 鼹鼠的移动速度
    /// </summary>
    public float speed = 150f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        opossumBody = GetComponent<Rigidbody2D>();

        if(leftTransform == null || rightTransform == null) Debug.LogError("the frog border is null");
        leftBorder = leftTransform.position.x;
        rightBorder = rightTransform.position.x;

        Destroy(leftTransform.gameObject);
        Destroy(rightTransform.gameObject);
    }

    protected void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// 鼹鼠的移动
    /// </summary>
    private void Move(){
        //鼹鼠应该只在左右方向移动
        float direction = transform.localScale.x;
        if(direction == 1){
            // 向左移动
            if(transform.position.x < leftBorder){
                // 如果越过左边界则应该转向
                transform.localScale = new Vector3(-1,1,1);
            }
            opossumBody.velocity = new Vector2(-speed * Time.fixedDeltaTime,0);
        }else{
            // 向右移动
            if(transform.position.x > rightBorder){
                // 如果越过右边界则应该转向
                transform.localScale = new Vector3(1,1,1);
            }
            opossumBody.velocity = new Vector2(speed * Time.fixedDeltaTime,0);
        }
    }
}
