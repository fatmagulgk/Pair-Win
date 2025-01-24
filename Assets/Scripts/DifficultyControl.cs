using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyControl : Singleton<DifficultyControl>
{
    public string difficulty;
    List<string> difficultyList = new List<string>();
    Dropdown ddDifficulty;
    public int cardCount;
    public bool clickControl = false;
    private void Awake()
    {
        base.Awake();
        ddDifficulty = gameObject.GetComponent<Dropdown>();
        ddDifficulty.onValueChanged.AddListener(SetDiffuculty);
        AddDifficulty();
        ddDifficulty.value = 0;

    }
    public void AddDifficulty()
    {
        difficultyList.Add("Easy");
        difficultyList.Add("Medium");
        difficultyList.Add("Hard");
        ddDifficulty.AddOptions(difficultyList);
    }
    public void SetDiffuculty(int index)
    {     
            GameManager.Instance.SetDifficulty((Difficulty)index);                      
    }
    public void DifficultySelect(Difficulty _df)
    {
        switch (_df)
        {
            case Difficulty.Easy:
                cardCount = 16;
                break;
            case Difficulty.Medium:
                cardCount = 30;
                break;
            case Difficulty.Hard:
                cardCount = 50;
                break;
            default:Debug.Log("Herhangi bir zorluk atanmadý");
                cardCount = 16;
                break;
        }
    }
    public void PlayGameControl()
    {
        Debug.Log(GameManager.Instance == null);
        DifficultySelect(GameManager.Instance.gameDifficulty);
    }
}
public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

