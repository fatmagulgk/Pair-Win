using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardAnimation : Singleton<CardAnimation>
{
    public float rotationDuration = 0.5f; // Dönme süresi
    public float delayBetweenRotations = 0.5f; // Dönmeler arasýndaki bekleme süresi
    public float displayTime = 2f; // Kartlarýn açýk kalacaðý süre
    private Sequence rotationSequence; // Döndürme sekansý
    public bool hasTurned = false; // Kartlarýn döndürülüp döndürülmediðini kontrol eden flag

    private void Start()
    {
        // Kartlarý döndürme iþlemini baþlatýyoruz
        if (CardSpawner.Instance != null && CardSpawner.Instance.spawnedCards.Count > 0)
        {
            // Kartlarý ilk baþta kapalý konuma getiriyoruz (180 derece)
            SetInitialCardRotation();

            RotateCards();
        }
    }

    // Kartlarýn baþlangýç rotasýný 180 dereceye ayarlýyoruz
    private void SetInitialCardRotation()
    {
        foreach (var card in CardSpawner.Instance.spawnedCards)
        {
            // Baþlangýçta kartlarý kapalý konuma getirmek için Z rotasýný 180 derece yapýyoruz
            card.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
    }

    private void RotateCards()
    {
        // Ýlk döndürme iþlemi yapýlmadýysa
        if (hasTurned)
            return;

        // Önceki dönüþ iþlemi devam ediyorsa iptal et
        rotationSequence?.Kill();

        // Yeni bir döndürme sekansý oluþtur
        rotationSequence = DOTween.Sequence();

        // Ýlk döndürme (kartlarý açma)
        foreach (var card in CardSpawner.Instance.spawnedCards)
        {
            // Kartý 180 derece döndür, böylece kartlarý açýyoruz
            rotationSequence.Append(card.transform.DORotate(new Vector3(0f, 0f, 0f), rotationDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutSine)); // Kartlar açýlýyor
        }

        // Kartlar açýldýktan sonra bekleme süresi
        rotationSequence.AppendInterval(displayTime);

        // Kartlarý kapatma (ikinci dönüþ)
        foreach (var card in CardSpawner.Instance.spawnedCards)
        {
            rotationSequence.Append(card.transform.DORotate(new Vector3(0f, 0f, 180f), rotationDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutSine)); // Kartlarý tekrar kapatýyoruz
        }

        // Döndürme iþlemi tamamlandýktan sonra `hasTurned` flag'ini true yapýyoruz
        rotationSequence.AppendCallback(() =>
        {
            Debug.Log("Kartlar açýldý ve sonra kapandý.");
            hasTurned = true; // Animasyon tamamlandý, kartlar açýldý ve kapandý
        });

        // Sekansý çalýþtýr
        rotationSequence.Play();
    }


}
