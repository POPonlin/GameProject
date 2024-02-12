using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindWay : MonoBehaviour
{    
    public bool findPlayer;
    public GameObject target;
    public Transform[] targetTransforms;
    public NavMeshAgent agent;
    private int numb=1;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.FindGameObjectWithTag("Player");
        targetTransforms=target.GetComponentsInChildren<Transform>();        
        agent=GetComponent<NavMeshAgent>();
        if(agent!=null)
        {
            agent.destination=targetTransforms[numb].position;
        }
    }

    // Update is called once per frame
    void Update()
    {        

    }

    public void FindWay1()
    {
        if(!agent.pathPending&&agent.remainingDistance<0.5f)
        {
            if (!findPlayer)
            {
                numb=(numb+1)%targetTransforms.Length;
                agent.destination=targetTransforms[numb].position;
            }
          
        }
        if(findPlayer)
        {
            agent.destination=player.transform.position;
        }
    }
}
