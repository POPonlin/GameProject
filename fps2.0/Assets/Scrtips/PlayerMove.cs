using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Transform playerTrasform;
    private Rigidbody playerRigidbody;
    private CapsuleCollider capCollider;
    private Animator aimAnim;
    [Header("速度值")]
    public float runSpeed;
    public float walkSpeed;
    public float crouchSpeed;

    private bool isCrouch;
    private bool isGround;
    private bool isJump;
    private bool stopCrouch;
    [Header("g值")]
    public float grivate; 
    [Header("跳跃高度")]
    public float jumpHeight;
    [Header("角色下蹲高度")]
    public float crouchHeight;
    private float originHeight;

    private Vector3 currentVelocity;    //当前速度
    private float moveSpeed;
    private float truePlayerHeight;

    private float temp_Hor;
    private float temp_Ver;
    // Start is called before the first frame update
    void Start()
    {

        playerTrasform=transform;
        playerRigidbody=GetComponent<Rigidbody>();
        capCollider=GetComponent<CapsuleCollider>();
        originHeight=capCollider.height;

        aimAnim=GameObject.FindGameObjectWithTag("Aim").GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGround)
        {

             temp_Hor = Input.GetAxis("Horizontal");
             temp_Ver = Input.GetAxis("Vertical");

            if (!isCrouch)
            {
                moveSpeed=Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;   //按下shift加速奔跑
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    stopCrouch=true;
                }
                else
                {
                    moveSpeed=crouchSpeed;
                }
            }

            var temp_Dir = new Vector3(temp_Hor, 0, temp_Ver);
            temp_Dir=playerTrasform.TransformDirection(temp_Dir);
            temp_Dir*=moveSpeed;

            currentVelocity = playerRigidbody.velocity;
            var temp_VelocityCange = temp_Dir-currentVelocity;
            temp_VelocityCange.y=0;

            playerRigidbody.AddForce(temp_VelocityCange, ForceMode.VelocityChange);


        }

        playerRigidbody.AddForce(0, -grivate*playerRigidbody.mass, 0);    //模拟重力
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump")&&isGround==true)
        {
            //isJump=true;
            playerRigidbody.velocity=new Vector3(currentVelocity.x, Mathf.Sqrt(2*grivate*jumpHeight), currentVelocity.z);
            if(isCrouch)
            {
                stopCrouch=true;
            }
        }

        if (Input.GetKeyDown(KeyCode.C)||stopCrouch)
        {
            truePlayerHeight=isCrouch ? originHeight : crouchHeight;
            StartCoroutine(Crouch(truePlayerHeight));
            isCrouch=!isCrouch;
            stopCrouch=false;
        }
        
        SwitchAinm();
    }

    private IEnumerator Crouch(float height)
    {
        float currentVel = 0;
        while (Mathf.Abs(capCollider.height-height)>0.1)
        {
            yield return null;
            capCollider.height= Mathf.SmoothDamp(capCollider.height, height, ref currentVel, Time.deltaTime*5);
        }
    }

    private void SwitchAinm()
    {    
        //aimAnim.SetFloat("Speed", Mathf.Abs(playerRigidbody.velocity.x));
        if(temp_Hor!=0||temp_Ver!=0)
        {
            aimAnim.SetBool("Walk", true);
        }
        else
        {
            aimAnim.SetBool("Walk", false);
        }
        
        if(moveSpeed==runSpeed)
        {
            aimAnim.SetBool("Run", true);

        }
        else
        {
            aimAnim.SetBool("Run", false);
        }

        if(Mathf.Abs(playerRigidbody.velocity.y)>0.1)
        {
            aimAnim.SetBool("Jump", true);
        }
        else
        {
            aimAnim.SetBool("Jump", false);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        isGround=true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGround=false;
    }
}
