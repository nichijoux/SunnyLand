using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    /// <summary>
    /// 中文菜单
    /// </summary>
    public GameObject ChineseMenu;

    /// <summary>
    /// 英文菜单
    /// </summary>
    public GameObject EnglishMenu;

    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetGame(){
        ChineseMenu.SetActive(!ChineseMenu.activeSelf);
        EnglishMenu.SetActive(!EnglishMenu.activeSelf);
    }

    public void QuitGame(){
        Application.Quit();
    }
}