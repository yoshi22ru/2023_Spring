using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GamePause : MonoBehaviour
{
    [SerializeField] GameObject pause_menu;
    [SerializeField] GameObject cross;
    [SerializeField] GameObject text;
    public bool paused=false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
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
        }

    }
}
