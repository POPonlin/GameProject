using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreate : MonoBehaviour
{
    public GameObject enemyPre;
    public int enemyNumber;
    private GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyNumber>0)
        {
            --enemyNumber;
            EnemyCreate1();
        }
    }

    /// <summary>
    /// Éú³ÉµÐÈË
    /// </summary>
    private void EnemyCreate1()
    {

        obj = (GameObject)Instantiate(enemyPre);
        obj.transform.position=new Vector3(Random.Range(-5, 5), Random.Range(-2, 2), 0);

    }
}
