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
    public bool isCounterBack;  //����Ƿ��ڶܷ�״̬
    public bool isCounterBackEnable;    //����Ƿ��ڶܷ�״̬�е���Ч����
    [Space]
    public bool isAllowDefense; //����ٶ�״̬�Ŀ�������
    public bool isDefense;  //����Ƿ��ھٶ�״̬
    public bool isInvincible;   //�޵�״̬
    public bool isSuccessBack;  //�ɹ��ܷ�
    public bool isFailueBack;   //ʧ�ܷܶ�
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
