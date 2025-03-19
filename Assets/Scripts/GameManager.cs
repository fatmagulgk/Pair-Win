using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    public Difficulty gameDifficulty;
    public CardBehaviour FirstCard;
    public CardBehaviour SecondCard;
    public int HowManyTurnedCard;
    public void SetDifficulty(Difficulty _difficulty)
    {
        gameDifficulty = _difficulty;
    }
    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        FirstCard = null;
        SecondCard = null;
        HowManyTurnedCard = 0;
    }
    public void SetCard(CardBehaviour card)
    {
        if (HowManyTurnedCard==1)
        {
            FirstCard = card;
        }
        else if (HowManyTurnedCard==2)
        {
            SecondCard = card;
        }
        StartCoroutine(CardMatching());
    }
    IEnumerator CardMatching()
    {

        if (FirstCard == null || SecondCard == null)
            yield break;
        //matching
        bool isClosedCards = false ;
        if (FirstCard.name == SecondCard.name)
        {

            yield return new WaitForSeconds(0.5f);
            Destroy(FirstCard.gameObject);
            Destroy(SecondCard.gameObject);

        }
        else
        {
            yield return new WaitUntil(()=>!FirstCard.IsBusy()&&!SecondCard.IsBusy());
            CardAnimation.Instance.CloseCard(FirstCard);
            CardAnimation.Instance.CloseCard(SecondCard).OnComplete(()=>isClosedCards=true);
            yield return new WaitUntil(()=>isClosedCards);
            Debug.Log("kart kapanma iþlemi tamamlandý");
        }
        
        //matching
        HowManyTurnedCard = 0;
        FirstCard = null;
        SecondCard = null;
    }

}
