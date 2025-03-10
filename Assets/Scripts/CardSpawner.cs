using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CardSpawner : Singleton<CardSpawner>
{
    public GameObject redCardPrefab;  // Kýrmýzý kart prefabý
    public GameObject blueCardPrefab; // Mavi kart prefabý
    public Camera mainCamera;          // Ana kamera referansý
    public int rows;
    public int cols;
    public float spacing = 1.5f;       // Prefablar arasý mesafe
    public List<GameObject> cards = new List<GameObject>();
    private void Awake()
    {
        base.Awake();
        
        
    }
    void Start()
    {
        
        cards = CardsBlender();
        CalculateGrid(CardController.Instance.totalCards);
        Debug.Log($"Satýr: {rows}, Sütun: {cols}");
        SpawnPrefabs();
    }

    public List<GameObject> CardsBlender()
    {
        List<GameObject> cards = new List<GameObject>();
        // Belirtilen sayýda kýrmýzý kart ekle
        for (int i = 0; i < CardController.Instance.redCardCount; i++)
        {
            cards.Add(redCardPrefab);
        }

        // Belirtilen sayýda mavi kart ekle
        for (int i = 0; i < CardController.Instance.blueCardCount; i++)
        {
            cards.Add(blueCardPrefab);
        }

        // Kart listesini karýþtýr
        Shuffle(cards);
        return cards;
    }
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
    public void CalculateGrid(int _totalcards)
    {

        rows = Mathf.FloorToInt(Mathf.Sqrt(_totalcards));
        cols = Mathf.CeilToInt((float)_totalcards / rows);

        while (rows * cols < _totalcards)
        {
            cols++;
        }

    }
    private void SpawnPrefabs()
    {
        if (mainCamera == null) mainCamera = Camera.main;

        // Kamera yüksekliði ve geniþliði
        float height = 2f * mainCamera.orthographicSize;
        float width = height * mainCamera.aspect;

        // Baþlangýç pozisyonu (sol üst köþe)
        Vector3 startPos = mainCamera.transform.position - new Vector3(width / 2, height / 2, 0);

        int prefabIndex = 0;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                // Pozisyonu hesapla
                float xPos = startPos.x + col * spacing;
                float yPos = startPos.y + row * spacing;
                Vector3 spawnPosition = new Vector3(xPos, yPos, 0);

                // Prefabý oluþtur
                if (cards.Count > 0)
                {
                    GameObject prefab = cards[prefabIndex % cards.Count];
                    Instantiate(prefab, spawnPosition, Quaternion.identity);
                    prefabIndex++;
                }
            }
        }
    }
}
