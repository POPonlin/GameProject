using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject num;
    public Animator animator;
    public GameObject bloodUI;
    public int goldValue;
    public float blood;
    public float maxBlood;
    public EnemySprit enemySprit;

    private Animator anim;
    private AnimatorStateInfo info;
    public PointUI pointUI;
    public DataUI dataUI;

    public bool onlyBoss;
    void Start()
    {
        maxBlood=blood;
        anim=GetComponent<Animator>();
        //pointUI=FindObjectOfType<PointUI>();
       // dataUI=FindObjectOfType<DataUI>();
    }

 
    void Update()
    {
        Dead();
        if(onlyBoss==true)
        {
            if(blood<maxBlood)
            {
                bloodUI.SetActive(true);
            }
        }

        if(blood<=0&&bloodUI)
        {
            bloodUI.SetActive(false);
        }
    }

    public void Hurt(float damage)
    {
        if(blood>0)
        {
            blood-=damage;
            if(enemySprit)
            {
                enemySprit.findPlayer=true;                        
            }
            if(anim)
            {
                anim.Play("Damage01");
            }
            pointUI.ChangePointColor();
        }
    }

    public void Dead()
    {
        if(blood<=0)
        {
            if(anim)
            {
                anim.Play("Dead00");
            }
            if(bloodUI)
            {
                bloodUI.SetActive(false);
            }

            info=anim.GetCurrentAnimatorStateInfo(0);

            if(info.IsTag("Dead"))
            {
                if(info.normalizedTime>0.9)
                {
                    dataUI.Gold(goldValue);
                    Destroy(this.gameObject);
                    if(bloodUI)
                    {
                        bloodUI.SetActive(false);
                    }
                    if(animator)
                    {
                        animator.Play("DoorOpen");
                    }
                    if(num)
                    {
                        num.SetActive(true);
                    }
                }
            }
        }
    }
   
}
