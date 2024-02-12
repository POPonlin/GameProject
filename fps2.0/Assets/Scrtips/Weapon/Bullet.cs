using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform Mullze;  
    public ParticleSystem par;

    private EnemyHealth enemyHealth;  
    private void OnEnable()
    {
        Mullze=GameObject.FindGameObjectWithTag("Mullze").transform;
        transform.position=Mullze.position;
        transform.rotation=Mullze.rotation;       
    }   
    //private void RayTest()
    //{        
    //        Debug.DrawRay(transform.position, transform.forward,Color.red,2f);

    //    if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 1f,TestLayer))
    //    {
    //        if(hit.collider.CompareTag("Enemy"))
    //        {
    //            enemyHealth=hit.collider.gameObject.GetComponent<EnemyHealth>();
    //            enemyHealth.Hurt(20);
    //        }
    //        Instantiate(par, transform.position, transform.rotation);
    //        //par.transform.position=transform.position;
    //        //par.Play();
    //        ShadowPool.instance.ReturnPool(game);


    //    }
    //}
}
