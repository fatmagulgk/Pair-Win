using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : Singleton<CountdownTimer>
{
    public float countdownTime = 90f; // 1dk 30sn
    public Text timerText; // UI Text bileþeni

    private float currentTime;
    private bool isCountingDown = false;

    void Start()
    {
        currentTime = countdownTime;
        isCountingDown = true;
    }

    void Update()
    {
        if (isCountingDown)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                UpdateTimerText();
            }
            else
            {
                currentTime = 0;
                isCountingDown = false;
                // Geri sayým bittiðinde yapýlacaklar
                Debug.Log("Süre doldu!");
            }
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
