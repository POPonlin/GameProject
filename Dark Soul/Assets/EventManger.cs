using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManger : MonoBehaviour
{
    private Animator anim;
    private PlayerControl playerControl;
    [Header("ÊÖ±ÛÐÞÕý")]
    public Vector3 vector;

    // Start is called before the first frame update
    void Awake()
    {
        anim=GetComponent<Animator>();
        playerControl=transform.parent.GetComponent<PlayerControl>();
    }

    public void ResetTrigger(string name)
    {
        anim.ResetTrigger(name);
    }

    private void OnAnimatorMove()
    {
        SendMessageUpwards("OnUpdateRM",(object)anim.deltaPosition);
    }

    //private void OnAnimatorIK()
    //{
    //    if(playerControl.canLeftShiled)
    //    {
    //        if (!anim.GetBool("Defense"))
    //        {
    //            Transform leftLowerArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
    //            leftLowerArm.localEulerAngles+=vector;
    //            anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(leftLowerArm.localEulerAngles));
    //        }
    //    }

    //}
}
