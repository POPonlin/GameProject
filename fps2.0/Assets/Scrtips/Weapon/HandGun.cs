using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : WeapenClass
{
    public bool stopMouseWork;
    public bool isOut_of_ammo_slide;  
    
    public void Relod_InAnim()
    {
        float needBulletCount = maxBullet_Clip-currentBullet_Clip;  //弹匣中缺少的子弹数
        //float leaveBulletCount = maxBullets-needBulletCount;    //总子弹数与缺少子弹数的差值，
        float leaveBulletCount = currentBullets-needBulletCount;     //用来判断装弹后，是否还有剩余弹药


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

        isOut_of_ammo_slide=false;
        isReloding=false;   //换弹结束
    }

    protected override void CreatBullet()
    {
        var tempBullet = ShadowPool.instance.OutPool();

        if (!isAiming)
        {
            tempBullet.transform.eulerAngles+=BulletDispersal();
        }

        var temp_BRig = tempBullet.gameObject.GetComponent<Rigidbody>();
        temp_BRig.velocity=tempBullet.transform.forward*bulletSpeed;

        //bool t=Physics.Raycast(mullze.position, tempBullet.transform.forward, out RaycastHit hit);
        bool t = Physics.Raycast(worldCamera.position, tempBullet.transform.forward, out RaycastHit hit);

        StartCoroutine(FireRay(t,hit, tempBullet));
        StartCoroutine(BulletLife(tempBullet));

    }

    IEnumerator FireRay(bool t,RaycastHit hit,GameObject tempBullet)
    {
        yield return null;
        while(true)
        {
            if (t)
            {
                Debug.Log(9);
                if(hit.collider.CompareTag("Enemy"))
                {
                    enemyHealth=hit.collider.gameObject.GetComponent<EnemyHealth>();
                    enemyHealth.Hurt(35);
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

    protected override void Inspect()
    {
        gunAnim.Play("inspect_weapon@assault_rifle_01");
    }

    protected override void Reloding()
    {
        if (currentBullet_Clip==maxBullet_Clip)
        {
            return;
        }

        if (isReloding==true)
        {
            return;
        }

        if (currentBullets<=0)
        {
            return;
        }

        gunAnim.SetLayerWeight(2, 1);    //设置换弹层权值为1，进行播放动画

        isReloding=true;    //换弹开始

        if (currentBullet_Clip>0)
        {
            reAudio.Play();
        }
        else
        {
            re_outAudio.Play();
        }

        gunAnim.SetTrigger(currentBullet_Clip>0 ? "Relode_left" : "Relode_out_of");
    }

    protected override void Shooting()
    {
        if (isReloding==true)
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

        gunAnim.Play(isAiming ? "aim_fire@assault_rifle_01 0" : "fire@handgun_01", isAiming ? 1 : 0);
        mullzePar.Play();
        CreatBullet();
        gunFireAudio.Play();
        shellCasePar.Play();
        cameraControl.FringForTest();

        lastShootTime=Time.time;
    }

    private void NullOfTheShell()
    {
        if(currentBullet_Clip==0f)
        {
            isOut_of_ammo_slide=true;
        }
    }
    
    void Update()
    {       
        NullOfTheShell();

        if (Input.GetMouseButton(0))
        {
            
            if (!stopMouseWork)
            {
                if(isOut_of_ammo_slide)
                {
                    gunAnim.Play("out_of_ammo_slider@handgun_01");
                }
                DoAttack();
            }            
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reloding();
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (!stopMouseWork)
            {
                isAiming=true;
                Aim();
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            isAiming=false;
            Aim();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Inspect();
        }
    }
}
