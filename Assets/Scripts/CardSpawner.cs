using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CardSpawner : Singleton<CardSpawner>
{
    public GameObject redCardPrefab;  // K�rm�z� kart prefab�
    public GameObject blueCardPrefab; // Mavi kart prefab�
    public Camera mainCamera;          // Ana kamera referans�
    public int rows;
    public int cols;
    public float spacing = 1.5f;       // Prefablar aras� mesafe
    public List<GameObject> cards = new List<GameObject>();
    private void Awake()
    {
        base.Awake();
        
        
    }
    void Start()
    {
        
        cards = CardsBlender();
        CalculateGrid(CardController.Instance.totalCards);
        Debug.Log($"Sat�r: {rows}, S�tun: {cols}");
        SpawnPrefabs();
    }

    public List<GameObject> CardsBlender()
    {
        List<GameObject> cards = new List<GameObject>();
        // Belirtilen say�da k�rm�z� kart ekle
        for (int i = 0; i < CardController.Instance.redCardCount; i++)
        {
            cards.Add(redCardPrefab);
        }

        // Belirtilen say�da mavi kart ekle
        for (int i = 0; i < CardController.Instance.blueCardCount; i++)
        {
            cards.Add(blueCardPrefab);
        }

        // Kart listesini kar��t�r
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
            (list[n], list[k]) = (list[k], list[n]); // Swap i�lemi
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

        // Kamera y�ksekli�i ve geni�li�i
        float height = 2f * mainCamera.orthographicSize;
        float width = height * mainCamera.aspect;

        // Ba�lang�� pozisyonu (sol �st k��e)
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

                // Prefab� olu�tur
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
