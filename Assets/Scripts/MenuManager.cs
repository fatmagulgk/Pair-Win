using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField] Button btnPlay;
    [SerializeField] Button btnExit;
    public void Start()
    {    
        ToolsListener();
    }
    public void ToolsListener()
    {
        btnExit.GetComponent<Button>().onClick.AddListener(GameExit);
        btnPlay.GetComponent<Button>().onClick.AddListener(GameSceneChange);
    } 
    public void GameSceneChange()
    {
        DifficultyControl.Instance.PlayGameControl();
        SceneManager.LoadScene(1);
    }
    public void GameExit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif

        Application.Quit();

    }
}
