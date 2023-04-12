using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject guidePanel;
    [SerializeField] GameObject loadPanel;
    [SerializeField] GameObject soundManagerObj;
    [SerializeField] SoundManager soundManager;
    [SerializeField] AudioClip titleBgm;
    [SerializeField] AudioClip startGame;

    public static GameStart instance;
    public bool isGameStart = false;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        Cursor.visible = true;
        soundManagerObj = GameObject.FindGameObjectWithTag("SoundManager");
        soundManagerObj.GetComponent<SoundManager>();
        soundManager = soundManagerObj.GetComponent<SoundManager>();
        soundManager.PlayBgm(titleBgm);
    }
    public void StartGame()
    {

        StartCoroutine(OnLoad());
        //soundManager.StopBgm(titleBgm);
        //soundManager.PlayeSe(startGame);
    }

    public IEnumerator OnLoad()
    {
        soundManager.StopBgm(titleBgm);
        soundManager.PlaySe(startGame);
        loadPanel.SetActive(true);

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("SampleScene");
    }


    public void Setting()
    {
        settingPanel.SetActive(true);
    }

    public void Guiding()
    {
        guidePanel.SetActive(true);
    }

    public void Close()
    {
        settingPanel.SetActive(false);
        guidePanel.SetActive(false);
    }



}
