using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    public Difficulty gameDifficulty;
    public void SetDifficulty(Difficulty _difficulty)
    {
        gameDifficulty = _difficulty;
    }

}
