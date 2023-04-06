using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;//BGM�X���C�_�[
    [SerializeField] private Slider seSlider;//SE�X���C�_�[

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
        //�Z�[�u
        BgmSave();
        print(a);
    }

    public void SeVolume()
    {
        float b = seSlider.value;
        SoundManager.instance.SetSeVolume(b);
        //�Z�[�u
        SeSave();
        print(b);
    }
    //�Z�[�u�p���\�b�h
    public void BgmSave()
    {
        PlayerPrefs.SetFloat("bgmSliderValue", bgmSlider.value);
    }
    public void SeSave()
    {
        PlayerPrefs.SetFloat("seSliderValue", seSlider.value);
    }

    //���[�h��AudioSource��volume�ɒl�𔽉f������
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
