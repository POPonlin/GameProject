using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySprit : MonoBehaviour
{
    [Header("ÑªÌõ")]public GameObject bloodTiao;
    public LayerMask layer;
    public float detctionRadius;
    public float originSpeed;
    public float runSpeed;
    [Header("")]
    public float tempFindTime;
    public float currentFindTIme;
    public float keepChaseTime;

    public bool canAttack=true;    

    private Animator anim;
    private AnimatorStateInfo info;
    //private float velocity;


    public bool findPlayer;
    public GameObject target;
    public Transform[] targetTransforms;
    public NavMeshAgent agent;
    private int numb = 1;
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.FindGameObjectWithTag("Player");
        targetTransforms=target.GetComponentsInChildren<Transform>();   
        agent=GetComponent<NavMeshAgent>();
        if (agent!=null)
        {
            agent.destination=targetTransforms[numb].position;
        }

        anim=GetComponent<Animator>();
        originSpeed=4;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed",agent.velocity.magnitude, 0.25f, Time.deltaTime);

        FindWay1();

        TryToAttackPlayer();

        TimeWork();

        anim.SetBool("Run", findPlayer );
        
        if(findPlayer)
        {
            agent.speed=runSpeed;
            if(bloodTiao)
            {
                bloodTiao.SetActive(true);
            }
        }
        else
        {
            agent.speed=originSpeed;
        }
        
    }

    private void TryToAttackPlayer()
    {
        Collider[] collider=Physics.OverlapSphere(transform.position, detctionRadius, layer);
        foreach(var thing in collider)
        {
            if(thing.CompareTag("Player"))
            {                
                if(findPlayer)
                {
                    agent.velocity=new Vector3(0,0,0);
                    agent.speed=0;
                    anim.SetBool("Run", false);
                    
                    anim.SetBool("Attack", true);                   
                }

            }
            else
            {
                anim.SetBool("Attack", false);
            }
        }
    }

    //IEnumerator CanAttack()
    //{
    //    yield return null;                
    //    //canAttack=false;
    //    //anim.SetBool("Attack", true);

    //    while (true)
    //    {
    //        yield return null;
    //        if(info.normalizedTime>0.95)
    //        {
    //            //canAttack=true;
    //            yield break;
    //        }
    //    }

    //}

   private void TimeWork()
    {
        if(findPlayer)
        {
            tempFindTime+=Time.deltaTime;
        }
        
        if(tempFindTime>=currentFindTIme+keepChaseTime)
        {
            findPlayer=false;
        }
    }

    public void FindWay1()
    {
        if (!agent.pathPending&&agent.remainingDistance<0.5f)
        {
            if (!findPlayer)
            {
                numb=(numb+1)%targetTransforms.Length;
                agent.destination=targetTransforms[numb].position;
            }

        }
        if (findPlayer)
        {
            agent.destination=player.transform.position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(transform.position, detctionRadius);
    }

    
}
