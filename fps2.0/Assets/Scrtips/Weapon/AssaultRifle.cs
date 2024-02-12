using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : WeapenClass
{
    public bool stopMouseWork;    
    protected override void Inspect()
    {
        gunAnim.Play("inspect_weapon@assault_rifle_01", 0);
    }

    //protected override void Aim()
    //{
    //    gunAnim.SetBool("Aim", isAiming);
    //    //----------------------
    //    if (aimFOV==null)
    //    {
    //        aimFOV=AimFOV();
    //        StartCoroutine(aimFOV);
    //    }
    //    else
    //    {
    //        StopCoroutine(aimFOV);
    //        aimFOV=null;
    //        aimFOV=AimFOV();
    //        StartCoroutine(aimFOV);
    //    }
    //    //----------------------
    //}

    /// <summary>
    /// 换弹动画结束时，触发的帧事件
    /// </summary>
    public void Relod_InAnim()
    {       
        float needBulletCount = maxBullet_Clip-currentBullet_Clip;  //弹匣中缺少的子弹数
        float leaveBulletCount = currentBullets-needBulletCount;    //总子弹数与缺少子弹数的差值，
                                                                    //用来判断装弹后，是否还有剩余弹药


        if (leaveBulletCount>0)
        {
            currentBullet_Clip=maxBullet_Clip;
            currentBullets-=needBulletCount;

        }
        else
        {
            currentBullet_Clip=currentBullets;
            currentBullets=0;
        }

        isReloding=false;   //换弹结束
    }

    protected override void Reloding()
    {        
        if(currentBullet_Clip==maxBullet_Clip)
        {
            return;
        }

        if(isReloding==true)
        {
            return;
        }

        if(currentBullets<=0)
        {
            return;
        }

        gunAnim.SetLayerWeight(2,1);    //设置换弹层权值为1，进行播放动画

        isReloding=true;    //换弹开始

        if(currentBullet_Clip>0)
        {
            reAudio.Play();
        }
        else
        {
            re_outAudio.Play();
        }

        gunAnim.SetTrigger(currentBullet_Clip>0? "Relode_left" : "Relode_out_of");
    }

    protected override void Shooting()
    {
        //Debug.Log("666");
        if(isReloding==true)
        {
            return;
        }
        if (currentBullet_Clip<=0)
        {
            return;
        }
        if (!AleadyShooting())
        {
            return;  
        }

        currentBullet_Clip-=1;

        gunAnim.Play(isAiming? "aim_fire@assault_rifle_01 0" : "fire@assault_rifle_01" ,isAiming?1:0);
        gunFireAudio.Play();
        mullzePar.Play();      
        CreatBullet();
        shellCasePar.Play();
        
        cameraControl.FringForTest();

        lastShootTime=Time.time;
    }

    protected override void CreatBullet()
    {
        //tempBullet = Instantiate(ShadowPool.instance.OutPool(), mullze.position, mullze.rotation);
        var tempBullet= ShadowPool.instance.OutPool();

        if(!isAiming)
        {
            tempBullet.transform.eulerAngles+=BulletDispersal();
        }

        var temp_BRig = tempBullet.gameObject.GetComponent<Rigidbody>();
        temp_BRig.velocity=tempBullet.transform.forward*bulletSpeed;

        //bool t = Physics.Raycast(mullze.position, tempBullet.transform.forward, out RaycastHit hit);
        bool t = Physics.Raycast(worldCamera.position, tempBullet.transform.forward, out RaycastHit hit);

        StartCoroutine(FireRay(t, hit, tempBullet));
        StartCoroutine(BulletLife(tempBullet)); //防止什么都没打到，子弹不能返回对象池
    }

    IEnumerator FireRay(bool t, RaycastHit hit, GameObject tempBullet)
    {
        yield return null;
        while (true)
        {
            if (t)
            {
                Debug.Log(9);
                if (hit.collider.CompareTag("Enemy"))
                {
                    enemyHealth=hit.collider.gameObject.GetComponent<EnemyHealth>();
                    enemyHealth.Hurt(20);
                    Instantiate(par, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation);
                    ShadowPool.instance.ReturnPool(tempBullet);
                }
                yield break;
            }
        }
    }
    IEnumerator BulletLife(GameObject bullet)
    {
        yield return new WaitForSeconds(3f);

        ShadowPool.instance.ReturnPool(bullet);
    }
    //IEnumerator AimFOV()
    //{
    //    while (true)
    //    {
    //        yield return null;

    //        float temp_fov = 0;
    //        eyesCamera.fieldOfView=Mathf.SmoothDamp(eyesCamera.fieldOfView,isAiming?50:originFOV,ref temp_fov, Time.deltaTime*2);
    //    }
    //}

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if(!stopMouseWork)
            {
                DoAttack();
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Reloding();
        }

        if(Input.GetMouseButtonDown(1))
        {
            if(!stopMouseWork)
            {
                isAiming=true;
                Aim();
            }
        }

        if(Input.GetMouseButtonUp(1))
        {
            isAiming=false;
            Aim();
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            Inspect();
        }
    }

    
}
