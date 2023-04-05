using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

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
    private NavMeshAgent agent;
    [SerializeField] private Vector3 _forward=Vector3.forward;
    public Transform[] points;
    public int destPoint;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(points[0].position);

        this.agent.speed = 1.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player!=null)
        {
            distance = Vector3.Distance(this.transform.position, player.transform.position);
            if (distance < 2)
            {
                animator.SetTrigger("Attack");
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            //��l���̕���
            var playerDirection = other.transform.position - transform.position;
            //�G�̑O������̎�l���̕���
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