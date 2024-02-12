using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManger : MonoBehaviour
{
    public BatterManger bm;
    public WeaponManger wm;
    public StateManger sm;
    public DirectorManger dm;
    public InteractionManger im;
    public EventCasterManger em;
    public PlayerControl playerControl;

    [SerializeField] private GameObject caster; //耦合性过高，以后要想办法改
    // Start is called before the first frame update
    void Start()
    {
        playerControl=GetComponent<PlayerControl>();
        var sensor = transform.Find("Sensor").gameObject;
        bm=Bind<BatterManger>(sensor);
        wm=Bind<WeaponManger>(transform.Find("ybot").gameObject);
        sm=Bind<StateManger>(gameObject);
        dm=Bind<DirectorManger>(gameObject);
        im=Bind<InteractionManger>(sensor);
        em=Bind<EventCasterManger>(caster);
    }
    
    private T Bind<T>(GameObject gb) where T:IActorMangerInterface
    {
        T tempReturnValue;
        tempReturnValue=gb.GetComponent<T>();
        if(tempReturnValue==null)
        {
            tempReturnValue=gb.AddComponent<T>();
        }
        tempReturnValue.am=this;

        return tempReturnValue;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            DoAction();
        }
    }

    public void DoAction()
    {
        if(im.iList.Count != 0)
        {           
            if(im.iList[0].isActive == true)    //判断是否执行相关事件
            {
                if (im.iList[0].casterName == "StabFront")
                {          
                    dm.FrontStab("StabFront", this, im.iList[0].am);
                }
                else if(im.iList[0].casterName == "BoxOpen")
                {
                    if(BatterManger.ScopEffect(im.iList[0].gameObject, playerControl.model, 35f))
                    {
                        //im.iList[0].isActive = false;
                        transform.position = im.iList[0].am.transform.position + im.iList[0].am.transform.TransformVector(im.iList[0].offest);
                        //设置角色与物体的相对位置。iList中成员的物体位置为局部坐标系，所以需要先转换成全局坐标系
                        playerControl.model.transform.LookAt(im.iList[0].am.transform, Vector3.up);

                        dm.BoxOpen("OpenBox", this, im.iList[0].am);
                    }
                }
            }

        }
    }

    public void  DoDamag(WeaponControl otherWC)
    {
        if(sm.isSuccessBack)
        {         
            otherWC.wm.am.Stunned();
        }
        else if(sm.isFailueBack)
        {
            Stunned();
            HitOrDie(otherWC, false);
        }
        else if(sm.isInvincible)
        {
            
        }
        else if(sm.isDefense)
        {
            playerControl.ChangeState("Block");
        }
        else
        {
            if(sm.hP<=0)
            {

            }
            else
            {
                HitOrDie(otherWC, true);
            }
        }
    }
    public void Stunned()
    {
        playerControl.ChangeState("Stunned");
    }

    /// <summary>
    /// 切换至受伤动画
    /// </summary>
    public void Hit()
    {
        playerControl.ChangeState("Hit");
    }

    /// <summary>
    /// 切换至死亡动画
    /// </summary>
    public void Die()
    {
        playerControl.ChangeState("Die");
    }

    public void HitOrDie(WeaponControl targetWc, bool isOn)
    {
        sm.AddHP(-1 * (targetWc.GetATK() + sm.ATK));
        if (sm.hP>0)
        {
            if(isOn)
            {
                Hit();
            }
        }
        else
        {
            Die();
        }
    }

    public void LockALLState(bool isOn = false,string name="Lock")
    {
        playerControl.ChangeAnimBool(name,isOn);
    }
}
