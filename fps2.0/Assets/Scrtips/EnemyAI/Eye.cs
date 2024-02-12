using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    [Header("视线长度")]
    public float rayDistance;

    public Transform tra;

    private RaycastHit hit;
    private Ray ray;

    public EnemySprit enemySprit;
    void Start()
    {
        ray=new Ray(transform.position, tra.forward);
        //enemySprit=FindObjectOfType<EnemySprit>();      
    }

    // Update is called once per frame
    void Update()
    {
        ray=new Ray(transform.position, tra.forward);

        Debug.DrawRay(transform.position, tra.forward*rayDistance,Color.green);

        if(Physics.Raycast(ray, out hit, rayDistance))
        {
            if(hit.collider.CompareTag("Player"))
            {
                enemySprit.findPlayer=true;

                enemySprit.currentFindTIme=Time.deltaTime;
                enemySprit.tempFindTime=enemySprit.currentFindTIme;
            }
        }
    }
}
