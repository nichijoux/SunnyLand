using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// 设置按钮
public class SettingControl : MonoBehaviour
{
    /// <summary>
    /// 暂停按钮
    /// </summary>
    [SerializeField]
    private Button pauseButton;

    /// <summary>
    /// 设置面板
    /// </summary>
    [SerializeField]
    private GameObject SettingPanel;

    /// <summary>
    /// 音量条
    /// </summary>
    private Slider backgroundSoundSlider;

    /// <summary>
    /// 背景音乐音源
    /// </summary>
    [SerializeField]
    private AudioSource backgroundAudioSource;

    private void Start() {
        // backgroundSoundSlider 应该靠SettingPanel自动获取
        Transform[] children = SettingPanel.GetComponentsInChildren<Transform>();
        foreach (Transform child in children){
            if(child.name == "BackgroundSoundSlider"){
                backgroundSoundSlider = child.gameObject.GetComponent<Slider>();
                backgroundSoundSlider.value = backgroundAudioSource.volume;
                return;
            }
        }
    }

    /// <summary>
    /// 打开设置面板
    /// </summary>
    public void OpenSetPanel(){
        // 先暂停游戏
        Time.timeScale = 0;
        // 再将面板设置为可活动
        SettingPanel.SetActive(true);
    }

    /// <summary>
    /// 继续游戏
    /// </summary>
    public void ResumeGame(){
        SettingPanel.SetActive(false);
        Time.timeScale = 1;
    }

    /// <summary>
    /// 音量调节
    /// </summary>
    public void VolumeChange(){
        backgroundAudioSource.volume = backgroundSoundSlider.value;
    }

    /// <summary>
    /// 返回主菜单
    /// </summary>
    public void ReturnMainMenu(){
        // 要先将游戏恢复正常,否则再次进入游戏会卡住
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    public void QuitGame(){
        Application.Quit();
    }
}
