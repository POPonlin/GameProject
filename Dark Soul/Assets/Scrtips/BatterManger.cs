using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BatterManger : IActorMangerInterface
{
    private CapsuleCollider capsuleCollider;
    // Start is called before the first frame update
    void Start()
    {
        am=transform.parent.GetComponent<ActorManger>();
        capsuleCollider=GetComponent<CapsuleCollider>();
        capsuleCollider.height=2;
        capsuleCollider.radius=0.25f;
        capsuleCollider.center=new Vector3(0,1,0);
        capsuleCollider.isTrigger=true;
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    private void OnTriggerEnter(Collider other1)
    {
        WeaponControl gbWC = other1.GetComponentInParent<WeaponControl>();

        if(!gbWC)
        {
            return;
        }
            
        GameObject attacker = gbWC.wm.am.playerControl.model;
        GameObject receiver = am.playerControl.model;



        if(other1.gameObject.CompareTag("Weapon") )
        {
            if(ScopEffect(attacker, receiver, 45))
            {
                am.DoDamag(gbWC);
            }
        }
    }

    public static bool ScopEffect(GameObject attacker, GameObject receiver, float angleValue)
    {
        Vector3 attackDir = receiver.transform.position-attacker.transform.position;

        float attackValid = Vector3.Angle(attacker.transform.forward, attackDir);

        bool inAttackValid = (attackValid < angleValue);

        //Debug.Log(attackValid);
        //Debug.Log("receive:" + receiver.name);
        //Debug.Log("attacker:" + attacker.name);
        return inAttackValid;
    }

}
