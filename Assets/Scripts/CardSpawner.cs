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
    public float distanceFromCamera = 50f;  // Kamera ile kartlar aras�ndaki mesafe
    public List<GameObject> spawnedCards = new List<GameObject>();

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
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1); // Unity'nin rastgele fonksiyonu
            (list[n], list[k]) = (list[k], list[n]); // Swap i�lemi
        }
    }
    public void CalculateGrid(int _totalcards)
    {
        if (_totalcards == 50)
        {
            rows = 5;
            cols = 10;
        }
        else
        {
            rows = Mathf.FloorToInt(Mathf.Sqrt(_totalcards));//Ondalikli degeri alt zemine yuvarlar.
            cols = Mathf.CeilToInt((float)_totalcards / rows);//Ust zemine yuvarlar.

            while (rows * cols < _totalcards)
            {
                cols++;
            }
        }


    }
    private void SpawnPrefabs()
    {
        Camera cam = Camera.main;//Aktif olan kamera

        //if (GameManager.Instance.gameDifficulty ==Difficulty.Hard)
        //{
        //    Debug.Log("kamera uzakl��� ayarland�");
        //    distanceFromCamera *= (int)Difficulty.Hard;
        //}


        // Kamera geni�li�ini hesapla
        float camHeight = 2f * cam.orthographicSize;
        //float camWidth = camHeight * cam.aspect;

        // Kart boyutunu belirle (ilk prefab �zerinden)
        Vector3 cardSize = cards[0].GetComponent<Renderer>().bounds.size;
        Debug.Log("Card Size :" + cardSize.x +" " +cardSize.y +" " + cardSize.z);

        //// Kartlar�n toplam geni�li�i
        //float totalWidth = cols * (cardSize.x + spacing) - spacing;
        //float totalHeight = rows * (cardSize.z + spacing) - spacing;

        // UI y�ksekli�ini hesapla (d�nya birimlerine d�n��t�r)
        float uiHeight = 167 * camHeight / Screen.height;

        // Ba�lang�� pozisyonunu hesapla (merkeze ortalama) ve kameradan uzakla�t�r
        Vector3 startPos = new Vector3(
            -((cols - 1) * (cardSize.x + spacing)) / 2f,  // X pozisyonu
            -distanceFromCamera,                         // Y pozisyonu
            ((rows - 1) * (cardSize.z + spacing)) / 2f - (uiHeight * 3.2f)  // Z pozisyonu (DAHA FAZLA NEGAT�F Z)  
        );
        Debug.Log("StartPos : " + startPos);
        int prefabIndex = 0;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (cards.Count > 0)
                {
                    // Pozisyonu hesapla (X ve Z de�i�iyor, Y sabit)
                    Vector3 spawnPosition = startPos + new Vector3(col * (cardSize.x + spacing), 0, -row * (cardSize.z + spacing));
                    Debug.Log($"({row},{col}) spawn point:{spawnPosition}");

                    // Prefab� olu�tur
                    GameObject prefab = cards[prefabIndex % cards.Count];
                    GameObject spawnedCard = Instantiate(prefab, spawnPosition, Quaternion.identity);
                    spawnedCards.Add(spawnedCard); // Kart� listeye ekledim
                    prefabIndex++;

                }
            }
        }
    }
}
