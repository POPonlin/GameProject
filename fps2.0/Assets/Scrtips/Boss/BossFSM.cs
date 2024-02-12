using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum StateType
{
    Idle,Boom,Staight,Sunkens,Summon, Laser,Dead,
}

[Serializable]
public class Parameter
{
    public int Health;

    public float idleTime;    

    public SkillAttack[] skillAttack;

    public EnemyHealth enemyHealth;

    public SkillAttack skillAttack2;
    [Header("玩家位置")]
    public Transform playerTarget;

    [Header("起始目标点")]
    public Transform startTra;

    public GameObject ray_Red;

    [Header("怪物数组")]
    public GameObject[] enemys;

    public GameObject SummonEnemys;

    public BloodUI bloodUI;

    [Header("地刺攻击间隔")]
    public float jianGe;

    [Header("地刺攻击次数")]
    public float numb;

    [Header("地刺预制体")]
    public GameObject sunken;

    public GameObject bloodui;

    public GameObject end;

    [HideInInspector]
    public bool start;

    [HideInInspector]
    public bool notBoom;
}


public class BossFSM : MonoBehaviour
{
    public Parameter parameter;

    private IState currentState;

    private Dictionary<StateType,IState> states=new Dictionary<StateType,IState>();
    
    void Start()
    {
        states.Add(StateType.Idle,new StatusClass(this));
        states.Add(StateType.Boom, new BoomClass(this));
        states.Add(StateType.Staight, new StaightClass(this));
        states.Add(StateType.Sunkens, new SunkensClass(this));
        states.Add(StateType.Summon, new SummonClass(this));
        states.Add(StateType.Laser, new LaserClass(this));
        states.Add(StateType.Dead,new DeadClass(this));

        TransitionState(StateType.Idle);
    }

    
    void Update()
    {
        currentState.OnUpDate();
    }

    public void TransitionState(StateType type)
    {
        if(currentState!=null)
        {
            currentState.OnExit();
        }
        currentState=states[type];
        currentState.OnEnter();
    }

    public void StartSunken()
    {
        StartCoroutine(Wait(parameter.jianGe));
    }

    IEnumerator Wait(float num)
    {
        for (int i = 0; i<parameter.numb; i++)
        {
            //parameter.sunken.transform.position=parameter.playerTarget.position;
            parameter.sunken.GetComponent<SkillAttack>().ShowRed();

            yield return new WaitForSeconds(num);

        }
    }
}
