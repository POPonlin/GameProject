using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAction : MonoBehaviour
{
    [Header("敌人检测列表")]
    public List<GameObject> enemyList = new List<GameObject>();
    [Header("攻击间隔时间")]
    public float attackWaitTime=1;

    public Transform mullzePostion;

    public GameObject bulletPrefab;

    public Transform towerHead; 

    private float timer=0;    //计时器


    void Start()
    {
        
    }

    void Update()
    {
        if(enemyList.Count>0&&enemyList[0]!=null)   //控制炮头转动
        {
            Vector3 temp=enemyList[0].transform.position;
            temp.y=towerHead.position.y;
            towerHead.LookAt(temp);
        }


        timer+=Time.deltaTime;

        if(enemyList.Count>0&&timer>=attackWaitTime)
        {
            timer=0;

            Attack();
        }
    }

    /// <summary>
    /// 炮台攻击函数
    /// </summary>
    private void Attack()
    {
        if(enemyList[0]==null)
        {
            RemoveNull(); 
        }
        if (enemyList.Count>0)
        {
            GameObject tempBullet = Instantiate(bulletPrefab, mullzePostion.position, mullzePostion.rotation);
            tempBullet.GetComponent<Bullet>().SetTarget(enemyList[0].transform);
        } 
    }

    /// <summary>
    /// 移除敌人列表内的空元素
    /// </summary>
    private void RemoveNull()
    {
        List<int> tempList = new List<int>();

        for(int i=0;i<enemyList.Count;i++)
        {
            if(enemyList[i]==null)
            {
                tempList.Add(i);
            }
        }

        for(int i=0;i<tempList.Count;i++)
        {
            enemyList.RemoveAt(tempList[i]-i);  //移除一个元素后，后边元素会向前移动。对应下标就不准确，所以是tempList[i]-i
                                                //i=0时为空，移除，下标列表内的值向前移动
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyList.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            enemyList.Remove(other.gameObject);
        }
    }
}
