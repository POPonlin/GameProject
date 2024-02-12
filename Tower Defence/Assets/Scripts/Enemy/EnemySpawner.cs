using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static int enemyAliveCount=0;
    public WaveData[] waves;

    [Header("��ʼ������")]
    public Transform startPostion;

    [Header("��һ���������ɵļ��ʱ��")]
    public float waitTime;
    void Start()
    {
        startPostion=GameObject.FindGameObjectWithTag("Start").GetComponent<Transform>();

        StartCoroutine(CreatEnemy());
    }

    
    void Update()
    {
        
    }

    IEnumerator CreatEnemy()
    {
        foreach(WaveData wave in waves)
        {
            for(int i=0;i<wave.Count;i++)
            {
                Instantiate(wave.enemyPrefab,startPostion.position,Quaternion.identity);
                enemyAliveCount++;

                if(i!=wave.Count-1)
                {
                    yield return new WaitForSeconds(wave.refreshTime);
                }
            }

            while(enemyAliveCount>0)
            {
                yield return 0;
            }

            yield return new WaitForSeconds(waitTime);
        }
    }
}
