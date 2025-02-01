using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : Singleton<CardSpawner>
{
    public GameObject redCardPrefab;  // Kýrmýzý kart prefabý
    public GameObject blueCardPrefab; // Mavi kart prefabý
    public Transform panel;           // Kartlarýn dizileceði UI paneli
    public int redCardCount = CardController.Instance.redCardCount;      // Kýrmýzý kart sayýsý
    public int blueCardCount = CardController.Instance.blueCardCount;     // Mavi kart sayýsý

    void Start()
    {
        SpawnCards();
    }

    void SpawnCards()
    {
        List<GameObject> cards = new List<GameObject>();

        // Belirtilen sayýda kýrmýzý kart ekle
        for (int i = 0; i < redCardCount; i++)
        {
            cards.Add(redCardPrefab);
        }

        // Belirtilen sayýda mavi kart ekle
        for (int i = 0; i < blueCardCount; i++)
        {
            cards.Add(blueCardPrefab);
        }

        // Kart listesini karýþtýr
        Shuffle(cards);

        // Karýþýk kartlarý panele ekle
        foreach (GameObject cardPrefab in cards)
        {
            Instantiate(cardPrefab, panel);
        }
    }

    // Listeyi karýþtýran yardýmcý fonksiyon
    void Shuffle<T>(List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[n], list[k]) = (list[k], list[n]); // Swap iþlemi
        }
    }
}
