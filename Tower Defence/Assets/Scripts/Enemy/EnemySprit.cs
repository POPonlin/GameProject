using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySprit : MonoBehaviour
{
    [Header("敌人移动速度")]
    public float moveSpeed;
    public float blood=150;
    public GameObject deedEffect;

    private int idex=0;

    void Start()
    {
        
    }


    void Update()
    {
        Move();
    }

    private void Move()
    {
        if(idex>WayPoints.postions.Length-1)
        {
            return;
        }
        transform.Translate((WayPoints.postions[idex].position-transform.position).normalized*moveSpeed*Time.deltaTime);

        if (Vector3.Distance(WayPoints.postions[idex].position, transform.position)<0.2)
        {
            idex++;
        }

        if(idex>WayPoints.postions.Length-1)    //表示到达最后一个标记点，删除自己
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        EnemySpawner.enemyAliveCount--;
    }

    public void Damage(float damage)
    {        
        if(blood>0)
        {
            blood-=damage;
        }
        else 
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject temp= Instantiate(deedEffect,transform.position,transform.rotation);
        Destroy(this.gameObject);
        Destroy(temp,1f);
    }


}
