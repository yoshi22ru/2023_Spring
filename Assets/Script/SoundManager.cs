using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //前提:変数は他のクラスに公開しない
    [SerializeField] AudioSource bgmAudioSource;
    [SerializeField] AudioSource seAudioSource;

    public static SoundManager instance;
    public float span = 3f;
    private float currentTime = 0f;
    public bool isSe;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public float BgmVolume
    {
        get
        {
            return bgmAudioSource.volume;
        }
        set
        {
            bgmAudioSource.volume = Mathf.Clamp01(value);
        }
    }

    public float SeVolume
    {
        get
        {
            return seAudioSource.volume;
        }
        set
        {
            seAudioSource.volume = Mathf.Clamp01(value);
        }
    }

    void Start()
    {

        GameObject soundManager = CheckOtherSoundManager();
        bool checkResult = soundManager != null && soundManager != gameObject;

        if(checkResult)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > span)
        {
            isSe = true;
            currentTime = 0f;
        }
        isSe = false;
    }

    GameObject CheckOtherSoundManager()
    {
        return GameObject.FindGameObjectWithTag("SoundManager");
    }

    public void PlayBgm(AudioClip clip)
    {
        bgmAudioSource.clip = clip;

        if(clip==null)
        {
            return;
        }
        bgmAudioSource.Play();
    }

    public void PlaySe(AudioClip clip)
    {
        if(clip==null)
        {
            return;
        }
            seAudioSource.PlayOneShot(clip);     
    }

    public void StopBgm(AudioClip clip)
    {
        bgmAudioSource.Stop();
    }

    public void StopSe(AudioClip clip)
    {
        seAudioSource.Stop();
    }

    public void PauseBgm(AudioClip clip)
    {
        bgmAudioSource.Pause();
    }

    public void UnPauseBgm(AudioClip clip)
    {
        bgmAudioSource.UnPause();
    }

    public void SetBgmVolume(float bgmVolume)
    {
        bgmAudioSource.volume = bgmVolume;
    }

    public void SetSeVolume(float seVolume)
    {
        seAudioSource.volume = seVolume;
    }
}
