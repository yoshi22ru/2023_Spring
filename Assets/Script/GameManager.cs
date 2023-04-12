using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject pause_menu;
    [SerializeField] GameObject cross;
    [SerializeField] GameObject text;
    //[SerializeField] GameObject player;
    [SerializeField] GameObject soundManagerObj;
    [SerializeField] SoundManager soundManager;
    [SerializeField] AudioClip stageBgm;
    [SerializeField] AudioClip dangerBgm;
    [SerializeField] AudioClip pauseSe;
    [SerializeField] AudioClip restartSe;
    [SerializeField] AudioClip retrySe;

    public bool paused=false;
    public Move move;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
        soundManagerObj = GameObject.FindGameObjectWithTag("SoundManager");
        soundManagerObj.GetComponent<SoundManager>();
        soundManager= soundManagerObj.GetComponent<SoundManager>();
        soundManager.PlayBgm(stageBgm);
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Tab)&&paused==false)
        {
            paused = true;
            Time.timeScale = 0;
            pause_menu.SetActive(true);
            cross.SetActive(false);
            text.SetActive(false);
            soundManager.PauseBgm(stageBgm);
            soundManager.PauseBgm(dangerBgm) ;
            soundManager.PlaySe(pauseSe);
        }

        if (paused == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                paused = false;
                Time.timeScale = 1;
                soundManager.StopBgm(stageBgm);
                soundManager.StopBgm(dangerBgm);
                soundManager.PlaySe(retrySe);
                SceneManager.LoadScene("GameStart");
                Cursor.visible = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)&&paused==true)
        {
            paused = false;
            Time.timeScale = 1;
            pause_menu.SetActive(false);
            cross.SetActive(true);
            text.SetActive(true);
            soundManager.UnPauseBgm(dangerBgm);
            soundManager.UnPauseBgm(stageBgm);
            soundManager.PlaySe(restartSe);
        }

        if(move.isDead==true)
        {
            soundManager.StopBgm(stageBgm);
            Cursor.visible = true;
        }
    }
}
