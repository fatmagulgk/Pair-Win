using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{   private bool isTurned;
    private bool isBusy;
   
    private void OnMouseDown()
    {
        if (CardAnimation.Instance.endAnimation)
        {
            Debug.Log($"{name} adli karta tiklandi");
            CardAnimation.Instance.OpenCard(this);
        }
    
    }
    public bool IsTurned()=>isTurned;
    public bool IsBusy()=>isBusy;
    public void SetTurned(bool turned)
    {
        isTurned = turned;
    }
    public void SetBusy(bool busy)
    {
        isBusy = busy;
    }
}
