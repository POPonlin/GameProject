using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeapenClass : MonoBehaviour,MyWeapon
{

    public Transform worldCamera;
    [Header("是否处在换弹状态中")]
    public bool isReloding;
    [Header("是否处在瞄准状态中")]
    public bool isAiming;
    [Header("是否处在检视中")]
    public bool isInspect;

    public AudioSource gunFireAudio;
    public AudioSource reAudio;
    public AudioSource re_outAudio;

    public Transform mullze;
    public Transform shellCase;

    public ParticleSystem mullzePar;
    public ParticleSystem shellCasePar;
    public ParticleSystem JiZhong;

    public float maxBullet_Clip;    //弹匣内最大子弹数
    public float maxBullets;    //携带的总子弹数

    public float lastShootTime; //最后一次射击的时间

    public float bulletRate; //一秒内射出的子弹数

    public GameObject Bullet;

    public Camera eyesCamera;

    public float SpreadAngle;

    public float bulletSpeed;

    public float shellBulletUI => currentBullet_Clip;
    public float maxBulletUI => currentBullets;
    public float currentBullet_Clip; //当前弹匣内的子弹数
    public float currentBullets; //当前携带子弹的总数

    protected Animator gunAnim;

    protected float originFOV;  //记录原始的FOV数据
    protected IEnumerator aimFOV;
    protected CameraControl cameraControl;

    protected EnemyHealth enemyHealth;
    public ParticleSystem par;
    protected virtual void Start()
    {
        currentBullet_Clip=maxBullet_Clip;
        currentBullets=maxBullets;
        gunAnim=GetComponent<Animator>();
        originFOV=eyesCamera.fieldOfView;

        cameraControl=FindObjectOfType<CameraControl>();
    }
    public void DoAttack()
    {
        Shooting();        
    }

    /// <summary>
    /// 射击函数
    /// </summary>
    protected abstract void Shooting();

    /// <summary>
    /// 生成子弹函数，跟Shooting（）函数一起使用
    /// </summary>
    protected abstract void CreatBullet();

    /// <summary>
    /// 换弹函数
    /// </summary>
    protected abstract void Reloding();


    /// <summary>
    /// 检视函数
    /// </summary>
    protected abstract void Inspect();

    /// <summary>
    /// 判断是否可以进行下一次射击的函数
    /// </summary>
    protected bool AleadyShooting()
    {
        return Time.time-lastShootTime>1/bulletRate;
    }

    /// <summary>
    /// 返回子弹散射的范围
    /// </summary>
    /// <returns></returns>
    protected Vector3 BulletDispersal()
    {
        //var temp = SpreadAngle/eyesCamera.fieldOfView;
        return SpreadAngle*Random.insideUnitCircle;
    }

    /// <summary>
    /// 瞄准函数
    /// </summary>
    protected void Aim()
    {
        gunAnim.SetBool("Aim", isAiming);
        //----------------------
        if (aimFOV==null)
        {
            aimFOV=AimFOV();
            StartCoroutine(aimFOV);
        }
        else
        {
            StopCoroutine(aimFOV);
            aimFOV=null;
            aimFOV=AimFOV();
            StartCoroutine(aimFOV);
        }
        //----------------------
    }

    IEnumerator AimFOV()
    {
        while (true)
        {
            yield return null;

            float temp_fov = 0;
            eyesCamera.fieldOfView=Mathf.SmoothDamp(eyesCamera.fieldOfView, isAiming ? 30 : originFOV, ref temp_fov, Time.deltaTime*2);
        }
    }
}
