using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat: Enemy
{
    public float stayTime;
    public int damage;
    private float keepTime;
    
    new void Start()
    {
        keepTime=stayTime;
    }

    new void Update()
    {
        base.Update();  
        //调用基类Update()函数
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&collision.GetType().ToString()=="UnityEngine.CapsuleCollider2D")
        {
            collision.GetComponent<HeroMov>().Damaged(damage);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(stayTime!=0)
            {
                stayTime-=Time.deltaTime;
            }
            if(stayTime<=0)
            {
                stayTime=0;
            }
            if (stayTime<=0)
            {
                collision.GetComponent<HeroMov>().Damaged(damage);
                stayTime=keepTime;
            }
        }
    }
}
