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
    [SerializeField] GameObject soundManagerObj;
    [SerializeField] SoundManager soundManager;
    [SerializeField] AudioClip stageBgm;

    public bool paused=false;


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
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            paused = true;
            Time.timeScale = 0;
            pause_menu.SetActive(true);
            cross.SetActive(false);
            text.SetActive(false);
            soundManager.PauseBgm(stageBgm);
        }

        if (paused == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            { 
                SceneManager.LoadScene("GameStart");
                Cursor.visible = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = false;
            Time.timeScale = 1;
            pause_menu.SetActive(false);
            cross.SetActive(true);
            text.SetActive(true);
            soundManager.UnPauseBgm(stageBgm);
        }

    }
}
