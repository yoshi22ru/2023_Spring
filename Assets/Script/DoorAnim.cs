using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorAnim : MonoBehaviour
{
    [SerializeField] GameObject soundManagerObj;
    [SerializeField] SoundManager soundManager;
    [SerializeField] AudioClip openSe;
    [SerializeField] GameObject openPanel;

    public Move player;
    public bool isOpen;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        this.anim= GetComponent<Animator>();
        soundManagerObj = GameObject.FindGameObjectWithTag("SoundManager");
        soundManagerObj.GetComponent<SoundManager>();
        soundManager = soundManagerObj.GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.Coin == 5)
        {
            isOpen = true;
            openPanel.SetActive(true);
            soundManager.PlaySe(openSe);
            anim.SetTrigger("Open");
        }
            //openPanel.SetActive(false);        
    }
}
