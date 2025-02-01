using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : Singleton<CardSpawner>
{
    public GameObject redCardPrefab;  // K�rm�z� kart prefab�
    public GameObject blueCardPrefab; // Mavi kart prefab�
    public Transform panel;           // Kartlar�n dizilece�i UI paneli
    public int redCardCount = CardController.Instance.redCardCount;      // K�rm�z� kart say�s�
    public int blueCardCount = CardController.Instance.blueCardCount;     // Mavi kart say�s�

    void Start()
    {
        SpawnCards();
    }

    void SpawnCards()
    {
        List<GameObject> cards = new List<GameObject>();

        // Belirtilen say�da k�rm�z� kart ekle
        for (int i = 0; i < redCardCount; i++)
        {
            cards.Add(redCardPrefab);
        }

        // Belirtilen say�da mavi kart ekle
        for (int i = 0; i < blueCardCount; i++)
        {
            cards.Add(blueCardPrefab);
        }

        // Kart listesini kar��t�r
        Shuffle(cards);

        // Kar���k kartlar� panele ekle
        foreach (GameObject cardPrefab in cards)
        {
            Instantiate(cardPrefab, panel);
        }
    }

    // Listeyi kar��t�ran yard�mc� fonksiyon
    void Shuffle<T>(List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[n], list[k]) = (list[k], list[n]); // Swap i�lemi
        }
    }
}
