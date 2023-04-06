using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
