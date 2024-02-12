using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAi : MonoBehaviour
{
    public float speed;
    public float detectionRadius;
    //public float stayDistance;
    protected Transform playerTrasform;
    // Start is called before the first frame update
    public void Start()
    {
        playerTrasform=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    public void Update()
    {
        if(playerTrasform)
        {
            //float distance = (playerTrasform.position-transform.position).sqrMagnitude;

            //if(distance<=detectionRadius)
            //{
            //    transform.position=Vector2.MoveTowards(transform.position, playerTrasform.position,speed*Time.deltaTime);
            //}
            if (Vector2.Distance(transform.position, playerTrasform.position)<detectionRadius)
            {
                transform.position=Vector2.MoveTowards(transform.position, playerTrasform.position, speed*Time.deltaTime);
            }
        }
        
        
    }
}
