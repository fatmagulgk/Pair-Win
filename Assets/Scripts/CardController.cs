using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;


public class CardController : Singleton<CardController>
{
    public int totalCards ;
    public int redCardCount;
    public int blueCardCount;
    public int bigCount;
    public int smallCount;
    CardColor color;
  
    private void Awake()
    {
        base.Awake();
        totalCards = DifficultyControl.Instance.cardCount;
        Debug.Log(totalCards);
        
    }
    void Start()
    {
        CardCountIdentifier();
        NumberAssigner(DeterminedColor());
    }

    public void CardCountIdentifier()
    {
        int determinedEvenNumber = Random.Range(1, totalCards);
        Debug.Log(determinedEvenNumber);
        if (!(determinedEvenNumber % 2 == 0) )
        {
            determinedEvenNumber++;
        }
        int temp = totalCards - determinedEvenNumber;
        if (temp < 0)
        {
            temp = temp * (-1);
        }
        if (temp > determinedEvenNumber|| temp == determinedEvenNumber)
        {
            bigCount = temp;
            smallCount = determinedEvenNumber;
        }else if (temp < determinedEvenNumber)
        {
            bigCount = determinedEvenNumber;
            smallCount = temp;
        }
       
    }
    
    public CardColor DeterminedColor()
    {
        CardColor determinedColor;
        int determinedNumber = Random.Range(0, 2);
        if (determinedNumber == 0)
        {
            determinedColor = CardColor.Red;
        }else
        {
            determinedColor = CardColor.Blue;
        }
        return determinedColor;
    }
    public void NumberAssigner(CardColor color)
    {

        if (color == CardColor.Red)
        {
            redCardCount = bigCount;
            blueCardCount = smallCount;
        }else
        {
            redCardCount = smallCount;
            blueCardCount = bigCount;
        }
        Debug.Log(redCardCount + " " + blueCardCount);

    }
}
public enum CardColor
{
    Red,
    Blue
}
