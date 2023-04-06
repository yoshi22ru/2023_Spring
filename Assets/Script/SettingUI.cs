using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider seSlider;
    [SerializeField] SoundManager soundManager;
    
    // Start is called before the first frame update
    void Start()
    {
        Slider bgmslider = GetComponent<Slider>();
        Slider seSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        bgmSlider.value = soundManager.BgmVolume;
        seSlider.value = soundManager.SeVolume;
    }
}
