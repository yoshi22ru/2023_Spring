using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GamePause : MonoBehaviour
{
    [SerializeField] GameObject pause_menu;
    [SerializeField] GameObject cross;
    [SerializeField] GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            Time.timeScale = 0;
            pause_menu.SetActive(true);
            cross.SetActive(false);
            text.SetActive(false);
        }
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            pause_menu.SetActive(false);
            cross.SetActive(true);
            text.SetActive(true);
        }

    }
}
