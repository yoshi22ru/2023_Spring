using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using Unity.VisualScripting;

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
    Rigidbody rb;
    private NavMeshAgent agent;
    [SerializeField] 
    private Vector3 _forward=Vector3.forward;
    [SerializeField]
    SoundManager soundManager;
    [SerializeField]
    GameObject soundManagerObj;
    [SerializeField]
    AudioClip attackSe;
    [SerializeField]
    AudioClip searchSe;
    [SerializeField]
    AudioClip dangerBgm;
    [SerializeField]
    AudioClip stageBgm;

    public Transform[] points;
    public int destPoint;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        soundManagerObj = GameObject.FindGameObjectWithTag("SoundManager");
        soundManagerObj.GetComponent<SoundManager>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        soundManager = soundManagerObj.GetComponent<SoundManager>(); 
        agent.SetDestination(points[0].position);

        this.agent.speed = 1.2f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player!=null)
        {
            distance = Vector3.Distance(this.transform.position, player.transform.position);
            if (distance < 2.5)
            {
                animator.SetTrigger("Attack");       
                this.agent.speed = 0.0f;
            }
            if(distance<1)
            {
                Destroy(this.gameObject);
            }
        }
        
        if (search==true)
        {
            animator.SetBool("Run", true);   
            this.transform.LookAt(player.transform);
            var dir = player.transform.position - this.transform.position;
            //�^�[�Q�b�g�̕����ւ̉�]
            var lookAtRotation = Quaternion.LookRotation(dir, Vector3.up);
            //��]�␳�@
            var offsetRotation = Quaternion.FromToRotation(_forward, Vector3.forward);
            //��]�␳�@�^�[�Q�b�g�����ւ̉�]�̏��ɁA���g�̌����𑀍삷��
            transform.rotation = lookAtRotation * offsetRotation * Quaternion.Euler(0, 90, 0);
            animator.SetBool("Move", true);
            this.transform.LookAt(player.transform);
            agent.SetDestination(player.transform.position);
        }
        else
        {
            animator.SetBool("Run", false);
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }     
    }

     private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {        
             soundManager.PlaySe(searchSe);
             soundManager.StopBgm(stageBgm); 
             soundManager.PlayBgm(dangerBgm);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            //��l���̕���
            var playerDirection = other.transform.position - transform.position;
            //�G�̑O������̎�l���̕���
            var angle = Vector3.Angle(transform.forward, playerDirection);

            //�T�[�`����p�x���Ȃ甭��
            if(angle > searchAngle)
            {
                search = true;
            }
        }      
    }

    private void OnTriggerExit(Collider other)
    { 
        if (other.tag == "Player")
        {
            search = false;
            if(search==false)
            {
                soundManager.PauseBgm(dangerBgm);
                soundManager.PlayBgm(stageBgm);
            }
        }
    }

    void GotoNextPoint()
    {
        //�n�_�������ݒ肳��Ă��Ȃ����ɕԂ�
        if(points.Length==0)
            return;

        agent.destination = points[destPoint].position;
        //�z����̎��̈ʒu��ڕW�n�_�ɐݒ肵
        //�K�v�Ȃ�o���n�_�ɖ߂�
        destPoint = (destPoint + 1) % points.Length;
        agent.SetDestination(points[destPoint].position);
    }
}