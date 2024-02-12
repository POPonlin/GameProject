using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControl : MonoBehaviour
{
    public GameObject model;
    public float walkSpeed=1.4f;
    public float runSpeed=3f;
    public float jumpUpValue=3f;
    //public float rollUpValue = 0.3f;
    [Header("��ʱ�ֶ��ź�")]
    public bool canLeftShiled;
    [Header("�Ƿ�ΪAI")]
    public bool isAI;
    [Header("�Ƿ�Ϊ����")]
    public bool isObject;

    private bool trackDirection=false;
    [HideInInspector]
    public Animator anim;
    private Rigidbody rig;

    private PlayerInput playerInput;
    private IsGround isGround;
    private CameraControl cameraControl;

    private Vector3 movingVec;
    private Vector3 jumpVec;
    
    private bool clockForward;

    // Start is called before the first frame update
    void Awake()
    {
        rig=GetComponent<Rigidbody>();
        anim=model.GetComponent<Animator>();
        playerInput=GetComponent<PlayerInput>();
        isGround=GetComponent<IsGround>();
        cameraControl=GetComponentInChildren<CameraControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isObject)
        {
            return;
        }

        OnGround();

        if(playerInput.lockOn)
        {
            cameraControl.LockOnTarget();
        }

        if(playerInput.jump)
        {
            anim.SetTrigger("Jump");
        }

        if((playerInput.mouseL||playerInput.mouseR)&&clockForward==false) //����Ҫ��,�����ֹ���
        {
            if(playerInput.mouseL&&!canLeftShiled)
            {
                anim.SetBool("R0L1",true);
                anim.SetTrigger("Attack");
            }
            else if(playerInput.mouseR)
            {
                anim.SetBool("R0L1",false);
                anim.SetTrigger("Attack");
            }
        }

        if(canLeftShiled)   //�ֶ�״̬�����ƾٶ�
        {

            if(CheckState("Ground")||CheckState("Block"))
            {
                anim.SetBool("Defense",Input.GetKey(KeyCode.P));    //mouseL������getkeydown�����ܳ�����ѹ���ź�����ű���Ҫ���ݳֶ��ź�ѡ��ʽ�������ڻ�û�����ֶ��ź����룬���Դ˴���
                anim.SetLayerWeight(anim.GetLayerIndex("Defense Layer"),1f);
            }
            else
            {
                anim.SetBool("Defense",false);
                anim.SetLayerWeight(anim.GetLayerIndex("Defense Layer"), 0f);
            }
        }
        else
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Defense Layer"),0f);
        }
        //anim.SetBool("Defense",playerInput.mouseR);
        
        if(playerInput.counterBack&&(CheckState("Ground")||CheckTag("AttackL")||CheckTag("AttackR")))
       // if(playerInput.counterBack&&CheckState("Ground"))
        {
            //print("tick!");
            //if(playerInput.counterBack)
            //{
                //
                //TODO:�ع���
                //
            //}
           // else
            //{
                //print("ccc");
                if(canLeftShiled==false)
                {
                    //TODO:�����ع���
                }
                else
                {
                    anim.SetTrigger("CounterBack");
                }
            //}
        }

        if(cameraControl.lockTarget==null)
        {
            float tempValue = playerInput.run ? 2f : 1f;
            anim.SetFloat("WalkSpeed", playerInput.dmag*(Mathf.Lerp(anim.GetFloat("WalkSpeed"), tempValue, 0.2f)));
            anim.SetFloat("Right",0);
        }
        else
        {
            var tempDvec = transform.InverseTransformVector(playerInput.dvec);
            anim.SetFloat("WalkSpeed",tempDvec.z*(playerInput.run ? 2f : 1f));  //2d�������x��z������ֵ
            anim.SetFloat("Right", tempDvec.x*(playerInput.run ? 2f : 1f));
        }


        if(cameraControl.lockFlag==false)
        {
            if(playerInput.dmag>0.1)
            {
                model.transform.forward=Vector3.Slerp(model.transform.forward, playerInput.dvec,0.3f);  //��ģ��һ����ȷ�ĳ���
                //model.transform.forward=playerInput.dvec;  //�Ϸ�����ģ�ͳ���ı�ʱ���м�û�й��� 
            }
            if(clockForward==false) //����movingVec�Ƿ�������� 
            {
                movingVec=(playerInput.run ? runSpeed:walkSpeed) *model.transform.forward*playerInput.dmag;
            }                            //��Ϊʼ�ճ�ģ������z�᷽���ƶ�,����Ҫ��model.transform.forward
        }                                   //playerInput.dmag�����Ƿ��ƶ�(��Χ��0��1)
        else
        {
            if(trackDirection==false)
            {
                model.transform.forward=transform.forward;
            }
            else
            {
                model.transform.forward=movingVec.normalized;
            }

            if(clockForward==false)
            {
                movingVec=playerInput.dvec*(playerInput.run ? runSpeed : walkSpeed);    //ģ�͹̶�����Ŀ�꣬������dvec
            }
        }
                                                                       
                                                                        
    }                                                                   

    private void FixedUpdate()
    {
        rig.position+=movingVec*Time.fixedDeltaTime;    //�ƶ��������
        //rig.velocity=jabVec;
        jumpVec=Vector3.zero;
    }

    private void OnGround()
    {
        if(isGround.onGround)
        {
            anim.SetBool("IsGround",true);
        }
        else
        {
            anim.SetBool("IsGround",false);
        }
    }

    public bool CheckState(string stateName,string layerName="Base Layer") //�鿴��ǰ�Ƿ���Ŀ�궯��״̬
    {
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsName(stateName);
    }
    public bool CheckTag(string tagName,string layername="Base Layer") //�鿴��ǰ�����Ƿ����Ŀ���ǩ
    {
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layername)).IsTag(tagName);
    }

    public void EnterJump()
    {
        trackDirection=true;
        playerInput.flag=false; //�������������
        clockForward=true;

        jumpVec=model.transform.up*jumpUpValue; //������һ��ͨ�����ٶȼ�һ��y�᷽���ֵ
        rig.velocity=jumpVec;
    }

    public void ExitJump()
    {
        trackDirection=false;
        playerInput.flag=true;
        clockForward=false;
    }

    public void Enter() //������������룬����ģ�ͷ���
    {
        playerInput.flag=false;
        clockForward=true;
    }

    public void EnterRoll()
    {
        Enter();
        trackDirection=true;
    }

    public void UpdateJab()
    {
        rig.position+=model.transform.forward*anim.GetFloat("JabValue")*Time.deltaTime;
    }

    public void AttackEnter()
    {
        //anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"),1.0f);
        playerInput.flag=false;
    }

    public void AttackExit()
    {
        //anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0f);
        playerInput.flag=true;
    }

    public void AttackUpdate()
    {
        //var currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("Attack Layer"));
        //anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"),Mathf.Lerp(currentWeight, 1.0f, 0.3f)) ;
        rig.position+=model.transform.forward*anim.GetFloat("Attack1Value")*Time.fixedDeltaTime;    //�ƶ��������
    }

    //public void AttackUpdateIdle()
    //{
    //    var currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("Attack Layer"));
    //    anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), Mathf.Lerp(currentWeight, 0f, 0.3f));
    //}

    public void OnUpdateRM(object _deltaPos)
    {
        if(CheckState("Attack_C"))
        {
            rig.position+=(Vector3)_deltaPos;
        }
    }

    public void HitEnter()
    {
        playerInput.flag=false;
    }

    public void ChangeState(string stateName)
    {
        anim.SetTrigger(stateName);
    }

    public void ChangeAnimBool(string name,bool isOn)
    {
        anim.SetBool(name,isOn);
    }

    public void DieEnter()
    {
        playerInput.flag=false;
    }

    public void CounterBackEnter()
    {
        playerInput.flag=false;
    }

}
