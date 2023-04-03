using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Zombie : MonoBehaviour
{
    private Animator animator;
    public GameObject player;
    public float distance;

    public bool search;
    [SerializeField]
    private SphereCollider searchArea;
    [SerializeField]
    private float searchAngle = 230f;
    AudioSource audiosource;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distance = Vector3.Distance(this.transform.position, player.transform.position);
        if(search==true&&distance>1.5)
        {
            animator.SetBool("Run", true);
            transform.position += transform.forward * 0.07f;
            this.transform.LookAt(player.transform);   
        }
        if (distance < 1.5)
        {
            animator.SetTrigger("Attack");
        }
        else
        {
            animator.SetBool("Run", false);
            transform.position += transform.forward * 0.05f;
        }

       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            search = false;
        }
    }

    private void OnCollidionEnter(Collision other)
    {
        if(other.gameObject.tag=="Player")
        {
           
        }
    }


    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawSolidArc(transform.position, Vector3.up, Quaternion.Euler(0f, -searchAngle, 0f) * transform.forward, searchAngle * 2f, searchArea.radius);
    }
}
