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

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.W))
        {
            animator.SetBool("Walk", true);

            if (Input.GetKey(KeyCode.Space))
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

    }
}
