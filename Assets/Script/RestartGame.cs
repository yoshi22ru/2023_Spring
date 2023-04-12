using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    [SerializeField] GameObject soundManagerObj;
    [SerializeField] SoundManager soundManager;
    [SerializeField] AudioClip deadSe;
    [SerializeField] AudioClip replaySe;

    // Start is called before the first frame update
    void Start()
    {
        soundManagerObj = GameObject.FindGameObjectWithTag("SoundManager");
        soundManagerObj.GetComponent<SoundManager>();
        soundManager = soundManagerObj.GetComponent<SoundManager>();
        soundManager.PlaySe(deadSe);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            soundManager.PlaySe(replaySe);
            SceneManager.LoadScene("GameStart");
        }
    }
}
