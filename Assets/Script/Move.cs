using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class Move : MonoBehaviour
{
    private Animator animator;

    public float stamina;
    public Slider stamina_slider;
    public GameObject enemy;
    public float distance;
    [SerializeField] AudioClip heartSE;
    AudioSource audio;

    void Start()
    {
        Application.targetFrameRate = 120;
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        distance = Vector3.Distance(this.transform.position, enemy.transform.position);
        Ray ray = new Ray(new Vector3(0,1,0),new Vector3(1,0,0));
        Vector3 origin = ray.origin;
        Vector3 direction = ray.direction;

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            animator.SetBool("Walk", true);

            if (Input.GetKey(KeyCode.Space)&&stamina>0)
            {
                transform.position += transform.forward * 0.08f;
                stamina = stamina - (Time.deltaTime * 10);
            }
            else
            {
                transform.position +=transform.forward* 0.05f;
            }
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        if(Input.GetKey(KeyCode.Space))
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
        }
        else
        {
            animator.SetBool("Back",false);
        }

        if(Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0.0f, 2.0f,0.0f);
        }
        if(Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0.0f, -2.0f,0.0f);
        }

        Debug.DrawRay(ray.origin ,ray.direction*30,Color.red, 5.0f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            StartCoroutine("GameOverScene");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag=="Enemy")
        {
            audio.PlayOneShot(heartSE);
        }
    }

    IEnumerator GameOverScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameOver");
    }
}
