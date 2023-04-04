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
    [SerializeField] AudioClip searchSE;
    AudioSource audio;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distance = Vector3.Distance(this.transform.position, player.transform.position);
        if (distance < 2)
        {
            animator.SetTrigger("Attack");           
        }
        if (search==true)
        {
            animator.SetBool("Run", true);
            transform.position += transform.forward * 0.04f;
            this.transform.LookAt(player.transform);
        }
        else
        {
            animator.SetBool("Run", false);
            transform.position += transform.forward * 0.03f;
        }     
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            //主人公の方向
            var playerDirection = other.transform.position - transform.position;
            //敵の前方からの主人公の方向
            var angle = Vector3.Angle(transform.forward, playerDirection);

            //サーチする角度内なら発見
            if(angle > searchAngle)
            {
                search = true;
            }
        }      
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            //主人公の方向
            var playerDirection = other.transform.position - transform.position;
            //敵の前方からの主人公の方向
            var angle = Vector3.Angle(transform.forward, playerDirection);

            if (angle>searchAngle)
            {
                audio.PlayOneShot(searchSE);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    { 
        if (other.tag == "Player")
        {
            search = false;
        }
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawSolidArc(transform.position, Vector3.up, Quaternion.Euler(0f, -searchAngle, 0f) * transform.forward, searchAngle * 2f, searchArea.radius);
    }
}
