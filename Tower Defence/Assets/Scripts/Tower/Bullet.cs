using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage=50;

    public float speed=20;

    public GameObject bulletBoomEffect;

    private float distance=0.3f;

    private Transform target;
    public void SetTarget(Transform _target)
    {
        this.target=_target;
    }

    void Start()
    {
        
    }


    void Update()
    {
        if(target==null)
        {
            Destroy(this.gameObject);
            return;
        }
        transform.LookAt(target.position);
        transform.Translate(Vector3.forward*Time.deltaTime*speed);

        WorkDistance();
    }

    private void  WorkDistance()
    {
        Vector3 dir = target.position-transform.position;
        if(dir.magnitude<distance)
        {
            target.gameObject.GetComponent<EnemySprit>().Damage(damage);

            GameObject temp = Instantiate(bulletBoomEffect, transform.position, transform.rotation);

            Destroy(this.gameObject);
            Destroy(temp, 1);
        }
    }


}
