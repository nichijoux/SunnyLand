using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 青蛙脚本
public class Frog : Enemy
{
    /// <summary>
    /// FrogJump动画
    /// </summary>
    private static readonly int jumpID = Animator.StringToHash("jumping");

    /// <summary>
    /// FrogFall动画
    /// </summary>
    private static readonly int fallID = Animator.StringToHash("falling");

    /// <summary>
    /// 青蛙的刚体组件
    /// </summary>
    private Rigidbody2D frogBody;

    /// <summary>
    /// 青蛙的碰撞组件
    /// </summary>
    private Collider2D frogCollider;

    /// <summary>
    /// 青蛙移动的左右边界
    /// </summary>
    public Transform leftTransform, rightTransform;

    /// <summary>
    /// 青蛙移动左右边界的实际x坐标
    /// </summary>
    private float leftBorder, rightBorder;

    /// <summary>
    /// 青蛙的移动速度
    /// </summary>
    public float speed = 140f;

    /// <summary>
    /// 青蛙向上跳跃的力
    /// </summary>
    public float jumpFroce = 5f;

    /// <summary>
    /// 地面图层
    /// </summary>
    public LayerMask groundLayer;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        frogBody = GetComponent<Rigidbody2D>();
        frogCollider = GetComponent<Collider2D>();

        if(leftTransform == null || rightTransform == null) Debug.LogError("the frog border is null");
        leftBorder = leftTransform.position.x;
        rightBorder = rightTransform.position.x;

        Destroy(leftTransform.gameObject);
        Destroy(rightTransform.gameObject);
    }

    /// <summary>
    /// 固定帧率刷新
    /// </summary>
    protected void FixedUpdate()
    {
        SwitchAnimation();
    }

    /// <summary>
    /// 青蛙的移动
    /// </summary>
    private void Move(){
        //青蛙应该只在左右方向移动
        float direction = transform.localScale.x;
        if(direction == 1){
            // 向左移动
            if(transform.position.x < leftBorder){
                // 如果越过左边界则应该转向
                transform.localScale = new Vector3(-1,1,1);
                return;
            }
            // 在地面就应该跳起
            if(frogCollider.IsTouchingLayers(groundLayer)){
                animator.SetBool(jumpID,true);
                frogBody.velocity = new Vector2(-speed * Time.fixedDeltaTime,jumpFroce);
            }
        }else{
            // 向右移动
            if(transform.position.x > rightBorder){
                // 如果越过右边界则应该转向
                transform.localScale = new Vector3(1,1,1);
                return;
            }
            // 在地面就应该跳起
            if(frogCollider.IsTouchingLayers(groundLayer)){
                animator.SetBool(jumpID,true);
                frogBody.velocity = new Vector2(speed * Time.fixedDeltaTime,jumpFroce);
            }
        }
    }

    /// <summary>
    /// 青蛙的动画效果
    /// </summary>
    private void SwitchAnimation(){
        if(animator.GetBool(jumpID) && frogBody.velocity.y < 0){
            // 下落
            animator.SetBool(fallID,true);
            animator.SetBool(jumpID,false);
        }
        if(animator.GetBool(fallID) && frogCollider.IsTouchingLayers(groundLayer)){
            // 落到地面
            animator.SetBool(fallID,false);
        }
    }
}
