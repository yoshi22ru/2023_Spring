using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class Move : MonoBehaviour
{
    private Animator animator;

    public float stamina;
    public Slider stamina_slider;
    public GameObject enemy;
    public float distance;
    public int Coin;
    public TextMeshProUGUI coin_text;
    public bool isIdle = false;
    public bool isWalk = false;
    public bool isRun = false;
    public bool isDead = false;

    [SerializeField] AudioClip fastHeartBgm;
    [SerializeField] AudioClip semiFastHeartBgm;
    [SerializeField] AudioClip slowHeartBgm;
    [SerializeField] private SphereCollider searchArea;
    [SerializeField] SoundManager soundManager;
    [SerializeField] GameObject soundManagerObj;
    Rigidbody rb;

    void Start()
    {
        Application.targetFrameRate = 120;
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        soundManagerObj=GameObject.FindGameObjectWithTag("SoundManager");
        soundManagerObj.GetComponent<SoundManager>();
        soundManager=soundManagerObj.GetComponent<SoundManager>();      
    }

    void FixedUpdate()
    {
        //distance = Vector3.Distance(this.transform.position, enemy.transform.position);
        //Ray ray = new Ray(new Vector3(0,1,0),new Vector3(1,0,0));
        //Vector3 origin = ray.origin;
        //Vector3 direction = ray.direction;

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            animator.SetBool("Walk", true);
            if (Input.GetKey(KeyCode.Space)&&stamina>0)
            {
                transform.position += transform.forward * 0.07f; 
                stamina = stamina - (Time.deltaTime * 10);
                isWalk = false;
                isIdle = false;
                isRun = true;
                
            }
            else
            {
                transform.position += transform.forward * 0.05f;
                isWalk = true;
                isIdle = false;
                isRun = false;
            }
        }
        else
        {
            animator.SetBool("Walk", false);
            isIdle = true;
            isWalk = false;
        }

        if(Input.GetKey(KeyCode.Space)&&stamina>0)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            stamina = stamina + (Time.deltaTime * 4);
        }

        const float MIN = 0;
        const float MAX = 100;

        stamina = Mathf.Clamp(stamina, MIN, MAX);
        stamina_slider.value = stamina;
        
        if(Input.GetKey(KeyCode.DownArrow)||Input.GetKey(KeyCode.S))
        {
            animator.SetBool("Back", true);
            transform.position -= transform.forward * 0.05f;
            //isWalk = true;
        }
        else
        {
            animator.SetBool("Back",false);
            //isWalk = false;
        }

        if(Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0.0f, 2.5f,0.0f); //
            //isWalk = false;
          
        }
        if(Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0.0f, -2.5f,0.0f); //
            //isWalk = false;
        }

        //Debug.DrawRay(ray.origin ,ray.direction*30,Color.red, 5.0f);

        coin_text.text = "" + Coin + "/5";
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            soundManager.StopBgm(fastHeartBgm);
            StartCoroutine("GameOverScene");
        }
    } 
    


    IEnumerator GameOverScene()
    {
        isDead = true;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GameOver");
    }
}
