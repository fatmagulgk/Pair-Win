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
    
    private void Start()
    {
        ddDifficulty = gameObject.GetComponent<Dropdown>();
        AddDifficulty();
        DropdownListener();
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
        difficulty = ddDifficulty.options[index].text; ;    
    }
    
    public void DropdownListener()
    {
        ddDifficulty.GetComponent<Dropdown>().onValueChanged.AddListener(SetDiffuculty);
    }
    public void DifficultySelect(string difficulty)
    {
        switch (difficulty)
        {
            case "Easy":
                cardCount = 16;
                break;
            case "Medium":
                cardCount = 30;
                break;
            case "Hard":
                cardCount = 50;
                break;
            default:Debug.Log("Herhangi bir zorluk atanmadý");
                cardCount = 16;
                break;
        }
    }
    public void PlayGameControl()
    {
        DifficultySelect(difficulty);
    }
}

