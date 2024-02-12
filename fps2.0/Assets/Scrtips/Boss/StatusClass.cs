using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusClass :  IState
{
    private BossFSM manger;

    private Parameter parameter;

    private float timer;
         
    public StatusClass(BossFSM _manger)
    {
        this.manger=_manger;
        this.parameter=_manger.parameter;
    }
    public void OnEnter()
    {

    }

    public void OnExit()
    {
        
    }

    public void OnUpDate()
    {
        //timer+=Time.deltaTime;

        //if (timer>=parameter.idleTime)
        //{

        //}
        if (parameter.enemyHealth.blood<=0)
        {
            manger.TransitionState(StateType.Dead);
        }
        StartBossAttack();
    }

    private void StartBossAttack()
    {
        if (!parameter.start)
        {
            if (parameter.startTra.position.z<parameter.playerTarget.position.z)
            {
                parameter.start=true;
                parameter.bloodUI.isActive=true;

                manger.TransitionState(StateType.Boom);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public class BoomClass : IState
{
    
    
    private BossFSM manger;
    private Parameter parameter;

    private float timer=0;
    private bool startTime;
    public BoomClass(BossFSM _manger)
    {
        this.manger=_manger;
        this.parameter=_manger.parameter;
    }
    public void OnEnter()
    {
   
    }

    public void OnExit()
    {
        
    }

    public void OnUpDate()
    {
        if (parameter.enemyHealth.blood<=0)
        {
            manger.TransitionState(StateType.Dead);
        }
        if (!parameter.notBoom)
        {
            parameter.notBoom=true;
            for (int i=0;i<parameter.skillAttack.Length;i++)
            {
                parameter.skillAttack[i].ShowRed();
                if(i+1==parameter.skillAttack.Length)
                {                  
                    startTime=true;
                }
            }
        }

        if(startTime)
        {
            timer+=Time.deltaTime;

            if(timer>=3f)
            {
                manger.TransitionState(StateType.Laser);
            }
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
public class StaightClass : IState
{
    private BossFSM manger;
    private Parameter parameter;

    public StaightClass(BossFSM _manger)
    {
        this.manger=_manger;
        this.parameter=_manger.parameter;
    }
    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }

    public void OnUpDate()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
public class SunkensClass : IState
{
    private BossFSM manger;
    private Parameter parameter;
    private bool b;

    private float timer;
    public SunkensClass(BossFSM _manger)
    {
        this.manger=_manger;
        this.parameter=_manger.parameter;
    }
    public void OnEnter()
    {
        Debug.Log("地刺");

        timer=+Time.deltaTime;
    }

    public void OnExit()
    {
        Debug.Log("结束地刺状态");
    }

    public void OnUpDate()
    {
        if(parameter.enemyHealth.blood<=0)
        {
            manger.TransitionState(StateType.Dead);
        }

        timer+=Time.deltaTime;
        if(timer>=3f)
        {               
            timer=0;
            manger.TransitionState(StateType.Laser);
        }

        if (!b)
        {
            b=true;

            manger.StartSunken();           
        }


    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
public class SummonClass : IState
{
    private BossFSM manger;
    private Parameter parameter;
    private bool a;
    public SummonClass(BossFSM _manger)
    {
        this.manger=_manger;
        this.parameter=_manger.parameter;
    }
    public void OnEnter()
    {
       
    }

    public void OnExit()
    {

    }

    public void OnUpDate()
    {
        if (parameter.enemyHealth.blood<=0)
        {
            manger.TransitionState(StateType.Dead);
        }

        if (!a)
        {
            a=true;
            for (int i = 0; i<parameter.enemys.Length; i++)
            {
               // parameter.enemys[i].transform.position=new Vector3(Random.Range(-9, -19), -0.959f, Random.Range(51, 82));
                parameter.enemys[i].SetActive(true);
            }
        }

        if (parameter.SummonEnemys.transform.childCount==0)
        {
            manger.TransitionState(StateType.Sunkens);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
public class LaserClass : IState
{
    private BossFSM manger;
    private Parameter parameter;
    private float timer;
    private bool notTime;

    public LaserClass(BossFSM _manger)
    {
        this.manger=_manger;
        this.parameter=_manger.parameter;        
    }
    public void OnEnter()
    {
        Debug.Log("进入激光状态");
        notTime=false;
        timer=0;
    }

    public void OnExit()
    {
        timer=0;
        notTime=true;
        parameter.ray_Red.SetActive(false);
    }

    public void OnUpDate()
    {
        if (parameter.enemyHealth.blood<=0)
        {
            manger.TransitionState(StateType.Dead);
        }
        if (!notTime)
        {
            timer+=Time.deltaTime;
        } 

        if(timer>=3)
        {                           
            parameter.ray_Red.SetActive(true);
        }
       
       if(timer>=13)
        {
            manger.TransitionState(StateType.Summon);
        }

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
public class DeadClass : IState
{
    private BossFSM manger;
    private Parameter parameter;

    public DeadClass(BossFSM _manger)
    {
        this.manger=_manger;
        this.parameter=_manger.parameter;
    }
    public void OnEnter()
    {
      
        
    }

    public void OnExit()
    {

    }

    public void OnUpDate()
    {

        Cursor.visible = true;
        
        parameter.end.SetActive(true);        
     
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
