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
            //ターゲットの方向への回転
            var lookAtRotation = Quaternion.LookRotation(dir, Vector3.up);
            //回転補正　
            var offsetRotation = Quaternion.FromToRotation(_forward, Vector3.forward);
            //回転補正　ターゲット方向への回転の順に、自身の向きを操作する
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
        //地点が何も設定されていない時に返す
        if(points.Length==0)
            return;

        agent.destination = points[destPoint].position;
        //配列内の次の位置を目標地点に設定し
        //必要なら出発地点に戻る
        destPoint = (destPoint + 1) % points.Length;
        agent.SetDestination(points[destPoint].position);
    }
}