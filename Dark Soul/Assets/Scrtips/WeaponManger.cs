using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManger : IActorMangerInterface
{
    public GameObject weaponHandL;
    public GameObject weaponHandR;
    public Collider colliderL;
    public Collider colliderR;

    public WeaponControl wcL;
    public WeaponControl wcR;

    // Start is called before the first frame update
    void Start()
    {
        weaponHandL=transform.DeepFind("WeaponHandL").gameObject;
        weaponHandR=transform.DeepFind("WeaponHandR").gameObject;
        colliderL=weaponHandL.GetComponentInChildren<Collider>();
        colliderR=weaponHandR.GetComponentInChildren<Collider>();
        am=transform.parent.GetComponent<ActorManger>();
        wcL=BindWeaponControl(weaponHandL);
        wcR=BindWeaponControl(weaponHandR);        
    }

    public WeaponControl BindWeaponControl(GameObject gb)
    {
        WeaponControl temp;
        temp=gb.GetComponent<WeaponControl>();
        if(temp==null)
        {
            temp=gb.AddComponent<WeaponControl>();
        }
        temp.wm=this;
        return temp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableWeaponCollider()
    {        
        colliderL.enabled=false;
        colliderR.enabled=false;
    }

    public void OpenWeaponCollider()
    {
        if (am.playerControl.CheckTag("AttackL"))
        {
            colliderL.enabled=true;
        }
        else if(am.playerControl.CheckTag("AttackR"))
        {
            colliderR.enabled=true;
        }
    }

    public void DisableCounterBackEffect()
    {
        am.sm.isCounterBackEnable=false;
    }

    public void OpenCounterBackEffect()
    {
        am.sm.isCounterBackEnable=true;
    }
}
