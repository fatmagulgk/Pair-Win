using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.InteropServices.WindowsRuntime;

public class CardAnimation : Singleton<CardAnimation>
{
    [SerializeField] float rotationDuration = 0.05f; // D�nme s�resi
    [SerializeField] float displayTime = 0.01f; // Kartlar�n a��k kalaca�� s�re
    private Sequence rotationSequence; // D�nd�rme sekans�
    public bool endAnimation = false; // Kartlar�n d�nd�r�l�p d�nd�r�lmedi�ini kontrol eden flag

    private void Start()
    {
        // Kartlar� d�nd�rme i�lemini ba�lat�yoruz
        if (CardSpawner.Instance != null && CardSpawner.Instance.spawnedCards.Count > 0)
        {
            // Kartlar� ilk ba�ta kapal� konuma getiriyoruz (180 derece)
            SetInitialCardRotation();

            RotateCards();
        }
    }

    // Kartlar�n ba�lang�� rotas�n� 180 dereceye ayarl�yoruz
    private void SetInitialCardRotation()
    {
        foreach (var card in CardSpawner.Instance.spawnedCards)
        {
            // Ba�lang��ta kartlar� kapal� konuma getirmek i�in Z rotas�n� 180 derece yap�yoruz
            card.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
    }

    private void RotateCards()
    {
        // �lk d�nd�rme i�lemi yap�lmad�ysa
        if (endAnimation)
            return;

        // �nceki d�n�� i�lemi devam ediyorsa iptal et
        rotationSequence?.Kill();

        // Yeni bir d�nd�rme sekans� olu�tur
        rotationSequence = DOTween.Sequence();

        // �lk d�nd�rme (kartlar� a�ma)
        foreach (var card in CardSpawner.Instance.spawnedCards)
        {
            // Kart� 180 derece d�nd�r, b�ylece kartlar� a��yoruz
            rotationSequence.Append(card.transform.DORotate(new Vector3(0f, 0f, 0f), rotationDuration*2, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutSine)); // Kartlar a��l�yor
        }

        // Kartlar a��ld�ktan sonra bekleme s�resi
        rotationSequence.AppendInterval(displayTime);

        // Kartlar� kapatma (ikinci d�n��)
        foreach (var card in CardSpawner.Instance.spawnedCards)
        {
            rotationSequence.Append(card.transform.DORotate(new Vector3(0f, 0f, 180f), rotationDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutSine)); // Kartlar� tekrar kapat�yoruz
        }

        // D�nd�rme i�lemi tamamland�ktan sonra `hasTurned` flag'ini true yap�yoruz
        rotationSequence.AppendCallback(() =>
        {
            Debug.Log("Kartlar a��ld� ve sonra kapand�.");
            endAnimation = true; // Animasyon tamamland�, kartlar a��ld� ve kapand�
        });

        // Sekans� �al��t�r
        rotationSequence.Play();
    }
    public void OpenCard(CardBehaviour card)
    {
        if (card.IsTurned()||GameManager.Instance.HowManyTurnedCard >= 2)
            return;
        card.SetBusy(true);
        card.transform.DORotate(new Vector3(0f, 0f, 0f), rotationDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutSine).OnComplete(()=>card.SetBusy(false));

        OpenCardProcess(card);

    }
    public Tween CloseCard(CardBehaviour card)
    {
        card.SetTurned(false);
        return card.transform.DORotate(new Vector3(0f, 0f, 180f), rotationDuration*2, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutSine);

        
    }
    public void OpenCardProcess(CardBehaviour card)
    {
        GameManager.Instance.HowManyTurnedCard++;
        GameManager.Instance.SetCard(card);
        card.SetTurned(true);


    }


}
