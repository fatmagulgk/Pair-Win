using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UiManager : Singleton<UiManager>
{
    
    [SerializeField] private GameObject pnlMenu;
    [SerializeField] private Button btnMenu;
    [SerializeField] private Button btnRestart;
    private void Start()
    {
        btnMenu.onClick.AddListener(BackToMenu);
        btnRestart.onClick.AddListener(GameManager.Instance.Restart);
    }
    public void TogglePanel()
    {
        pnlMenu.SetActive(!pnlMenu.activeSelf);//Bu sat�r, panelin mevcut gorunurlugunu tersine cevirir.Panel ac�kken kapat�r.Kapal�yken acar.
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
