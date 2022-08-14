using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人基类
/// </summary>
public class Enemy : MonoBehaviour{
    /// <summary>
    /// EnemyDeath动画
    /// </summary>
    private static readonly int deathID = Animator.StringToHash("death");

    /// <summary>
    /// 敌人的动画播放器
    /// </summary>
    protected Animator animator;

    /// <summary>
    /// 敌人的死亡音乐播放器
    /// </summary>
    protected AudioSource deathAudio;

    /// <summary>
    /// 初始化代码
    /// </summary>
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 播放死亡动画
    /// </summary>
    public void PlayDeathAnimation(){
        // 锁住敌人的x,y,z轴的力，防止因为缺失Collider2D而被弹开
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        // 禁止再次碰撞
        GetComponent<Collider2D>().enabled = false;

        deathAudio.Play();
        animator.SetTrigger(deathID);
    }

    /// <summary>
    /// enemy掉落后应该被摧毁的代码
    /// </summary>
    protected void Death(){
        Destroy(this.gameObject);
    }

    /// <summary>
    /// 死亡检测
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("DeathLine")){
            Destroy(this.gameObject);
        }
    }
}