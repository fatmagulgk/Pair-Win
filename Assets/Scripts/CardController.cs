using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CardController : MonoBehaviour
{
    public GameObject redCardPrefab;  // K�rm�z� kart prefab�
    public GameObject blueCardPrefab; // Mavi kart prefab�
    public RectTransform panelRectTransform; // Panelin RectTransform'u
    public int _cardCount;  // Spamlanacak kart say�s�
    public float cardSpacing = 10f; // Kartlar aras�ndaki bo�luk
   

    private void Start()
    {
        _cardCount = DifficultyControl.Instance.cardCount;

    }


}
