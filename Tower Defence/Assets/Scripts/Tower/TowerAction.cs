using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAction : MonoBehaviour
{
    [Header("���˼���б�")]
    public List<GameObject> enemyList = new List<GameObject>();
    [Header("�������ʱ��")]
    public float attackWaitTime=1;

    public Transform mullzePostion;

    public GameObject bulletPrefab;

    public Transform towerHead; 

    private float timer=0;    //��ʱ��


    void Start()
    {
        
    }

    void Update()
    {
        if(enemyList.Count>0&&enemyList[0]!=null)   //������ͷת��
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
    /// ��̨��������
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
    /// �Ƴ������б��ڵĿ�Ԫ��
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
            enemyList.RemoveAt(tempList[i]-i);  //�Ƴ�һ��Ԫ�غ󣬺��Ԫ�ػ���ǰ�ƶ�����Ӧ�±�Ͳ�׼ȷ��������tempList[i]-i
                                                //i=0ʱΪ�գ��Ƴ����±��б��ڵ�ֵ��ǰ�ƶ�
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
