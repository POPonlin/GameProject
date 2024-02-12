using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKChange : MonoBehaviour
{
    private Animator anim;
    private Vector3 leftFootIk, rightFootIk;
    private Vector3 leftFootPosition, rightFootPosition;
    private Quaternion leftFootRotation, rightFootRotation;

    [SerializeField]private LayerMask ikLayer;
    [SerializeField][Range(0,0.2f)]private float rayHitOffset;
    [SerializeField] private float rayCastDistance;

    [SerializeField] private bool enableIK;
    [SerializeField] private float ikSphereRadius = 0.05f;
    [SerializeField] private float positionSphereRadius = 0.05f;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        //leftFootIk = anim.GetIKPosition(AvatarIKGoal.LeftFoot);
        //rightFootIk = anim.GetIKPosition(AvatarIKGoal.RightFoot);
    }
    private void OnAnimatorIK()
    {
        leftFootIk = anim.GetIKPosition(AvatarIKGoal.LeftFoot);
        rightFootIk = anim.GetIKPosition(AvatarIKGoal.RightFoot);

        if(!enableIK)
        {
            return;
        }

        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot,1);
        anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot,1);

        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot,1);
        anim.SetIKRotationWeight(AvatarIKGoal.RightFoot,1);

        anim.SetIKPosition(AvatarIKGoal.LeftFoot,leftFootPosition);
        anim.SetIKRotation(AvatarIKGoal.LeftFoot,leftFootRotation);

        anim.SetIKPosition(AvatarIKGoal.RightFoot,rightFootPosition);
        anim.SetIKRotation(AvatarIKGoal.RightFoot,rightFootRotation);
    }

    private void FixedUpdate()
    {
        Debug.DrawLine(leftFootIk+(Vector3.up*0.5f),leftFootIk+Vector3.down*rayCastDistance,Color.blue,Time.fixedDeltaTime);
        Debug.DrawLine(rightFootIk+(Vector3.up*0.5f), rightFootIk+Vector3.down*rayCastDistance, Color.blue, Time.fixedDeltaTime);
        if (Physics.Raycast(leftFootIk+(Vector3.up*0.5f),Vector3.down,out RaycastHit hit,rayCastDistance+1,ikLayer))
        {
            Debug.DrawRay(hit.point,hit.normal,Color.red,Time.fixedDeltaTime);

            leftFootPosition=hit.point+Vector3.up*rayHitOffset;
            leftFootRotation=Quaternion.FromToRotation(Vector3.up,hit.normal)* transform.rotation;
       
        }

        if (Physics.Raycast(rightFootIk+(Vector3.up*0.5f), Vector3.down, out RaycastHit hit1, rayCastDistance+1, ikLayer))
        {
            Debug.DrawRay(hit1.point, hit1.normal, Color.red, Time.fixedDeltaTime);

            rightFootPosition=hit1.point+Vector3.up*rayHitOffset;
            rightFootRotation=Quaternion.FromToRotation(Vector3.up, hit1.normal)* transform.rotation;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color=Color.green;
        Gizmos.DrawSphere(leftFootIk,ikSphereRadius);
        Gizmos.DrawSphere(rightFootIk,ikSphereRadius);
        Gizmos.color=Color.cyan;
        Gizmos.DrawSphere(leftFootPosition,positionSphereRadius);
        Gizmos.DrawSphere(rightFootPosition,positionSphereRadius);
    }
}
