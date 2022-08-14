using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 人物控制代码
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// run动画ID
    /// </summary>
    private static readonly int runID = Animator.StringToHash("running");

    /// <summary>
    /// jump动画ID
    /// </summary>
    private static readonly int jumpID = Animator.StringToHash("jumping");

    /// <summary>
    /// fall动画ID
    /// </summary>
    private static readonly int fallID = Animator.StringToHash("falling");

    /// <summary>
    /// climb动画ID
    /// </summary>
    private static readonly int climbID = Animator.StringToHash("climbing");

    /// <summary>
    /// crouch动画ID
    /// </summary>
    private static readonly int crouchID = Animator.StringToHash("crouching");

    /// <summary>
    /// hurt动画ID
    /// </summary>
    private static readonly int hurtID = Animator.StringToHash("hurting");

    /// <summary>
    /// 人物移动速度
    /// </summary>
    [Range(400f,1000f)]
    public float moveSpeed = 500f;

    /// <summary>
    /// 跳跃力
    /// </summary>
    [Range(10f,100f)]
    public float jumpForce = 10f;

    /// <summary>
    /// 人物刚体组件
    /// </summary>
    private Rigidbody2D playerBody;

    /// <summary>
    /// 人物动画组件
    /// </summary>
    private Animator animator;

    /// <summary>
    /// 人物头部的可关闭的碰撞盒组件
    /// </summary>
    [SerializeField]
    private Collider2D headCollider;

    /// <summary>
    /// 人物移动用的碰撞盒组件
    /// </summary>
    [SerializeField]
    private Collider2D moveCollider;

    /// <summary>
    /// 人物头部点
    /// </summary>
    [SerializeField]
    private Transform headPoint;

    /// <summary>
    /// 跳跃音乐
    /// </summary>
    public AudioSource jumpAudio;

    /// <summary>
    /// 获取收集物的音乐
    /// </summary>
    public AudioSource collectAudio;

    /// <summary>
    /// 受伤的音乐
    /// </summary>
    public AudioSource hurtAudio;

    /// <summary>
    /// 地面图层
    /// </summary>
    public LayerMask groundLayer;

    /// <summary>
    /// UI控制器
    /// </summary>
    [SerializeField]
    private CollectionControl collectionControl;

    /// <summary>
    /// 初始化代码
    /// </summary>
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if(headCollider == null || moveCollider == null)
            Debug.LogError("the player's collider is null");
        if(collectionControl == null)
            Debug.LogError("the collectionControl is null");
    }

    // Update is called once per frame
    private void Update()
    {
        // 如果没受伤才可以移动
        if(!animator.GetBool(hurtID)){
            // 跳跃
            Jump();
            // 爬行
            Crouch();
        }
        // 动画切换
        SwitchAnimation();
    }

    private void FixedUpdate() {
        // 如果没受伤才可以移动
        if(!animator.GetBool(hurtID)){
            Move();
        }
    }

    /// <summary>
    /// 人物移动代码
    /// </summary>
    private void Move(){
        float moveX = Input.GetAxis("Horizontal");
        float direction = Input.GetAxisRaw("Horizontal");
        if(moveX != 0){
            playerBody.velocity = new Vector2(moveX * moveSpeed * Time.fixedDeltaTime, playerBody.velocity.y);
            animator.SetFloat(runID,Mathf.Abs(direction));
        }
        // 设置角色朝向
        if(direction != 0) transform.localScale = new Vector3(direction,1,1);
    }

    /// <summary>
    /// 人物跳跃代码
    /// </summary>
    private void Jump(){
        if(moveCollider.IsTouchingLayers(groundLayer) && !animator.GetBool(crouchID) && Input.GetButtonDown("Jump")){
            playerBody.velocity = new Vector2(playerBody.velocity.x,jumpForce);
            animator.SetBool(jumpID,true);
            jumpAudio.Play();
        }
    }

    /// <summary>
    /// 人物爬行代码
    /// </summary>
    private void Crouch(){
        if(moveCollider.IsTouchingLayers(groundLayer)){
            if(Input.GetButton("Crouch")){
                animator.SetBool(crouchID,true);
                headCollider.enabled = false;
            }else if(!Physics2D.OverlapCircle(headPoint.position,0.2f,groundLayer)){
                animator.SetBool(crouchID,false);
                headCollider.enabled = true;
            }
        }
    }

    /// <summary>
    /// 切换动画效果
    /// </summary>
    private void SwitchAnimation(){
        if(!moveCollider.IsTouchingLayers(groundLayer) && playerBody.velocity.y < 0){
            // 速度小于0并且人物不在地面上说明在下落
            animator.SetBool(jumpID,false);
            animator.SetBool(fallID,true);
        }else if(animator.GetBool(hurtID)){
            // 如果已经受伤了,则在人物冲击速度小于某个值的时候停止
            if(Mathf.Abs(playerBody.velocity.x) < 0.05f){
                animator.SetBool(hurtID,false);
            }
        }else if(animator.GetBool(fallID) && moveCollider.IsTouchingLayers(groundLayer)){
            // 下落到地面
            animator.SetBool(fallID,false);
        }
    }

    /// <summary>
    /// 碰撞检测
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("CollectionCherry")){
            Destroy(other.gameObject);
            collectionControl.AddCherry();
            collectAudio.Play();
        }else if(other.CompareTag("CollectionGem")){
            Destroy(other.gameObject);
            collectionControl.AddGem();
            collectAudio.Play();
        }else if(other.CompareTag("DeathLine")){
            //死亡
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 消灭敌人
    /// <summary>
    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject enemyObject = other.gameObject;
        // 如果碰撞到敌人
        if(enemyObject.CompareTag("Enemy")){
            if(animator.GetBool(fallID) && (transform.position.y > enemyObject.transform.position.y + 1)){
                // 如果人物在下落并且是在怪物上方,则消灭敌人
                Enemy enemy = enemyObject.GetComponent<Enemy>();
                enemy.PlayDeathAnimation();
                // 自己跳跃
                playerBody.velocity = new Vector2(playerBody.velocity.x,jumpForce);
                animator.SetBool(jumpID,true);
            }else{
                int direction = (transform.position.x - enemyObject.transform.position.x) > 0 ? 1 : -1;
                playerBody.velocity = new Vector2(8 * direction,playerBody.velocity.y);
                // 受伤动画
                hurtAudio.Play();
                animator.SetFloat(runID,0f);
                animator.SetBool(hurtID,true);
            }
        }
    }
}
