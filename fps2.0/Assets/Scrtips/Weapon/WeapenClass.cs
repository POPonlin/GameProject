using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeapenClass : MonoBehaviour,MyWeapon
{

    public Transform worldCamera;
    [Header("�Ƿ��ڻ���״̬��")]
    public bool isReloding;
    [Header("�Ƿ�����׼״̬��")]
    public bool isAiming;
    [Header("�Ƿ��ڼ�����")]
    public bool isInspect;

    public AudioSource gunFireAudio;
    public AudioSource reAudio;
    public AudioSource re_outAudio;

    public Transform mullze;
    public Transform shellCase;

    public ParticleSystem mullzePar;
    public ParticleSystem shellCasePar;
    public ParticleSystem JiZhong;

    public float maxBullet_Clip;    //��ϻ������ӵ���
    public float maxBullets;    //Я�������ӵ���

    public float lastShootTime; //���һ�������ʱ��

    public float bulletRate; //һ����������ӵ���

    public GameObject Bullet;

    public Camera eyesCamera;

    public float SpreadAngle;

    public float bulletSpeed;

    public float shellBulletUI => currentBullet_Clip;
    public float maxBulletUI => currentBullets;
    public float currentBullet_Clip; //��ǰ��ϻ�ڵ��ӵ���
    public float currentBullets; //��ǰЯ���ӵ�������

    protected Animator gunAnim;

    protected float originFOV;  //��¼ԭʼ��FOV����
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
    /// �������
    /// </summary>
    protected abstract void Shooting();

    /// <summary>
    /// �����ӵ���������Shooting��������һ��ʹ��
    /// </summary>
    protected abstract void CreatBullet();

    /// <summary>
    /// ��������
    /// </summary>
    protected abstract void Reloding();


    /// <summary>
    /// ���Ӻ���
    /// </summary>
    protected abstract void Inspect();

    /// <summary>
    /// �ж��Ƿ���Խ�����һ������ĺ���
    /// </summary>
    protected bool AleadyShooting()
    {
        return Time.time-lastShootTime>1/bulletRate;
    }

    /// <summary>
    /// �����ӵ�ɢ��ķ�Χ
    /// </summary>
    /// <returns></returns>
    protected Vector3 BulletDispersal()
    {
        //var temp = SpreadAngle/eyesCamera.fieldOfView;
        return SpreadAngle*Random.insideUnitCircle;
    }

    /// <summary>
    /// ��׼����
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
