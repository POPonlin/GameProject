using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    private Vector2 mousePot;
    public GameObject bulletPre;
    public GameObject shellPre;
    private Transform muzzlePot;
    private Transform bulletPot;
    private Vector2 gunDirection;
    public  float timeJG;
    private float fileY;
    private float time;
    private Animator animator;
    private GameObject bulletNum;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
        muzzlePot=transform.Find("Muzzle");
        bulletPot=transform.Find("BulletShell");
        fileY=transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        mousePot=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(mousePot.x<transform.position.x)
        {
            transform.localScale=new Vector3(1, -fileY, 1);
        }
        else
        {
            transform.localScale=new Vector3(1, fileY, 1);
        }
        Shoot();
    }
    /// <summary>
    /// Éä»÷
    /// </summary>
    private void Shoot()
    {
        gunDirection=(mousePot-new Vector2(transform.position.x, transform.position.y)).normalized;
        transform.right=gunDirection;
        if(time!=0)
        {
            time-=Time.deltaTime;
            if(time<=0)
            {
                time=0;
            }
        }
        if(Input.GetMouseButtonDown(0))
        {
            if(time==0)
            {
                Fire();
                time=timeJG;
            }
        }
    }

    /// <summary>
    /// ·¢Éä
    /// </summary>
    private void Fire()
    {
        animator.SetTrigger("Fire");
        //int i = 1;
        //if(i<=bulletNum.TryGetComponent<HeroMov>().)
        //{

        //}
        GameObject bullet=Instantiate(bulletPre, muzzlePot.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetSpeed(gunDirection);
    }
}
