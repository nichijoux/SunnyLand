using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 老鹰脚本
public class Eagle : Enemy
{
    /// <summary>
    /// 老鹰刚体组件
    /// </summary>
    private Rigidbody2D eagleBody;

    /// <summary>
    /// 老鹰上下移动边界的对象
    /// </summary>
    public Transform topTransform,bottomTransform;

    /// <summary>
    /// 老鹰上下移动的边界
    /// </summary>
    private float topBorder,bottomBorder;

    /// <summary>
    /// 老鹰的移动速度
    /// </summary>
    public float speed = 100f;

    /// <summary>
    /// 老鹰移动的方向,先向上移动
    /// </summary>
    private bool isUpward = true;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        eagleBody = GetComponent<Rigidbody2D>();

        if(topTransform == null || bottomTransform == null)Debug.LogError("the Eagle's border is null");

        topBorder = topTransform.position.y;
        bottomBorder = bottomTransform.position.y;

        Destroy(topTransform.gameObject);
        Destroy(bottomTransform.gameObject);
    }

    protected void FixedUpdate()
    {
        Move();   
    }

    /// <summary>
    /// 老鹰的移动函数,只需要上下移动
    /// </summary>
    private void Move(){
        if(isUpward){
            // 向上移动
            eagleBody.velocity = new Vector2(0,speed * Time.fixedDeltaTime);
            if(transform.position.y > topBorder) isUpward = false;
        }else{
            // 向下移动
            eagleBody.velocity = new Vector2(0,-speed * Time.fixedDeltaTime);
            if(transform.position.y < bottomBorder) isUpward = true;
        }
    }
}
