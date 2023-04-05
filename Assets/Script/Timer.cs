using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    private float second;
    private float minute;
    private float hour;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        second += Time.deltaTime;
        if(minute>60)
        {
            hour++;
            minute = 0;
        }
        if(second>60f)
        {
            minute += 1;
            second = 0;
        }
        timerText.text = hour.ToString() + ":" + minute.ToString("00") + ":" + second.ToString("f2");
    }

    
}
