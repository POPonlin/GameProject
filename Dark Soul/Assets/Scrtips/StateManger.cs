using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManger : IActorMangerInterface
{
    public float hP=10f;
    public float maxHP=10f;
    public float ATK = 10f;
    [Space]
    public bool isGround;
    public bool isJump;
    public bool isJab;
    public bool isFall;
    public bool isRoll;
    public bool isHit;
    public bool isDie;
    public bool isAttack;
    public bool isBlock;
    public bool isCounterBack;  //检测是否在盾反状态
    public bool isCounterBackEnable;    //检测是否在盾反状态中的生效部分
    [Space]
    public bool isAllowDefense; //进入举盾状态的控制条件
    public bool isDefense;  //检测是否在举盾状态
    public bool isInvincible;   //无敌状态
    public bool isSuccessBack;  //成功盾反
    public bool isFailueBack;   //失败盾反
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGround=am.playerControl.CheckState("Ground");
        isJump=am.playerControl.CheckState("Jump");
        isJab=am.playerControl.CheckState("Jab");
        isFall=am.playerControl.CheckState("Fail");
        isRoll=am.playerControl.CheckState("Roll");
        isHit=am.playerControl.CheckState("Hit");
        isDie=am.playerControl.CheckState("Die");
        isAttack=am.playerControl.CheckTag("AttackR")||am.playerControl.CheckTag("AttackL");
        isBlock=am.playerControl.CheckState("Block");

        isAllowDefense=isGround||isBlock;
        isDefense=isAllowDefense&&am.playerControl.CheckState("Defense", "Defense Layer");
        isInvincible=isJab||isRoll;
        isCounterBack=am.playerControl.CheckState("CounterBack");

        isSuccessBack=isCounterBackEnable;
        isFailueBack=isCounterBack&&!isCounterBackEnable;
    }

    public void AddHP(float change)
    {
        if(hP>0)
        {
            hP+=change;
            hP=Mathf.Clamp(hP,0,maxHP);
            //am.Hit();
            //if(hP<=0)
            //{
            //    am.Die();
            //}
        }
    }
}
