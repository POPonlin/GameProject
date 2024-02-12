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
    [Header("临时持盾信号")]
    public bool canLeftShiled;
    [Header("是否为AI")]
    public bool isAI;
    [Header("是否为物体")]
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

        if((playerInput.mouseL||playerInput.mouseR)&&clockForward==false) //可能要改,左右手攻击
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

        if(canLeftShiled)   //持盾状态，控制举盾
        {

            if(CheckState("Ground")||CheckState("Block"))
            {
                anim.SetBool("Defense",Input.GetKey(KeyCode.P));    //mouseL类型是getkeydown，不能持续按压，信号输入脚本需要根据持盾信号选择方式，但现在还没有做持盾信号输入，暂以此代替
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
                //TODO:重攻击
                //
            //}
           // else
            //{
                //print("ccc");
                if(canLeftShiled==false)
                {
                    //TODO:左手重攻击
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
            anim.SetFloat("WalkSpeed",tempDvec.z*(playerInput.run ? 2f : 1f));  //2d混合树，x，z有正负值
            anim.SetFloat("Right", tempDvec.x*(playerInput.run ? 2f : 1f));
        }


        if(cameraControl.lockFlag==false)
        {
            if(playerInput.dmag>0.1)
            {
                model.transform.forward=Vector3.Slerp(model.transform.forward, playerInput.dvec,0.3f);  //给模型一个正确的朝向
                //model.transform.forward=playerInput.dvec;  //老方法，模型朝向改变时，中间没有过渡 
            }
            if(clockForward==false) //控制movingVec是否继续更新 
            {
                movingVec=(playerInput.run ? runSpeed:walkSpeed) *model.transform.forward*playerInput.dmag;
            }                            //因为始终朝模型自身z轴方向移动,所以要乘model.transform.forward
        }                                   //playerInput.dmag控制是否移动(范围是0到1)
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
                movingVec=playerInput.dvec*(playerInput.run ? runSpeed : walkSpeed);    //模型固定朝向目标，所以用dvec
            }
        }
                                                                       
                                                                        
    }                                                                   

    private void FixedUpdate()
    {
        rig.position+=movingVec*Time.fixedDeltaTime;    //移动距离代码
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

    public bool CheckState(string stateName,string layerName="Base Layer") //查看当前是否处在目标动画状态
    {
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsName(stateName);
    }
    public bool CheckTag(string tagName,string layername="Base Layer") //查看当前动画是否具有目标标签
    {
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layername)).IsTag(tagName);
    }

    public void EnterJump()
    {
        trackDirection=true;
        playerInput.flag=false; //锁死方向键输入
        clockForward=true;

        jumpVec=model.transform.up*jumpUpValue; //向上跳一般通过给速度加一个y轴方向的值
        rig.velocity=jumpVec;
    }

    public void ExitJump()
    {
        trackDirection=false;
        playerInput.flag=true;
        clockForward=false;
    }

    public void Enter() //锁死方向键输入，锁死模型方向
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
        rig.position+=model.transform.forward*anim.GetFloat("Attack1Value")*Time.fixedDeltaTime;    //移动距离代码
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
