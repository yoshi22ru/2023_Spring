using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;//BGMスライダー
    [SerializeField] private Slider seSlider;//SEスライダー

    void Start()
    {

    }
     void FixedUpdate()
    {
        BgmLoadSlider();
        SeLoadSlider();
    }



    public void BgmVolume()
    {
        float a = bgmSlider.value * 0.8f;
        SoundManager.instance.SetBgmVolume(a);
        //セーブ
        BgmSave();
        print(a);
    }

    public void SeVolume()
    {
        float b = seSlider.value;
        SoundManager.instance.SetSeVolume(b);
        //セーブ
        SeSave();
        print(b);
    }
    //セーブ用メソッド
    public void BgmSave()
    {
        PlayerPrefs.SetFloat("bgmSliderValue", bgmSlider.value);
    }
    public void SeSave()
    {
        PlayerPrefs.SetFloat("seSliderValue", seSlider.value);
    }

    //ロードしAudioSourceのvolumeに値を反映させる
    public void BgmLoadSlider()
    {
        bgmSlider.value = PlayerPrefs.GetFloat("bgmSliderValue", 1.0f);
        float a = bgmSlider.value * 0.8f;
        SoundManager.instance.SetBgmVolume(a);
        print(a);
    }

    public void SeLoadSlider()
    {
        seSlider.value = PlayerPrefs.GetFloat("seSliderValue", 1.0f);
        float b = seSlider.value;
        SoundManager.instance.SetSeVolume(b);
        print(b);
    }
}
