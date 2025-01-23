using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CardController : MonoBehaviour
{
    public GameObject redCardPrefab;  // Kýrmýzý kart prefabý
    public GameObject blueCardPrefab; // Mavi kart prefabý
    public RectTransform panelRectTransform; // Panelin RectTransform'u
    public int _cardCount;  // Spamlanacak kart sayýsý
    public float cardSpacing = 10f; // Kartlar arasýndaki boþluk
   

    private void Start()
    {
        _cardCount = DifficultyControl.Instance.cardCount;

    }


}
