using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAi : EnemyAi
{
    private Transform playerTransform2;
    int speed2 = 1;
    // Start is called before the first frame update
    new void Start()
    {
        playerTransform2=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        //int u = 1;
        //if (playerTransform2)
        //{
        //    float dis = (playerTransform2.transform.position-transform.position).sqrMagnitude;
        //    if (u!=0)
        //    {
        //        Vector2 vec2 = new Vector2(Random.Range(transform.position.x, transform.position.x+2), Random.Range(transform.position.y, transform.position.y+2));
        //        transform.Translate(vec2*speed2*Time.deltaTime, Space.World);
        //        u=0;
        //        // transform.position=Vector2.MoveTowards(transform.position,vec2,speed2*Time.deltaTime);
        //    }
        //}
    }
}
