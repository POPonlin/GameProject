using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayControl : MonoBehaviour
{
    public Transform target;

    [Header("速度值")]
    public float walkSpeed;
    public float runSpeed;
    public float crouchSpeed;
    [Header("g值")]
    public float grivate;
    [Header("跳跃高度")]
    public float jumpHeight;

    public bool onlyCrouch;

    public float crouchHeight;
    private float originHeight;

    private bool isCrouch;
    private bool stopCrouch;
    private float moveSpeed;
    private float truePlayerHeight;
    private float velocity;
    private Transform playerTra;
    private Vector3 playerDir;
    private CharacterController controller;

    private WeapnManger weapnManger;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {

        playerTra=transform;
        controller=GetComponent<CharacterController>();
        originHeight=controller.height;
        //anim=GameObject.FindGameObjectWithTag("Aim").GetComponent<Animator>();
        weapnManger=FindObjectOfType<WeapnManger>();
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed=walkSpeed;
        if(controller.isGrounded)
        {
            float Horiz = Input.GetAxis("Horizontal");
            float Vert = Input.GetAxis("Vertical");
            playerDir=playerTra.TransformDirection(new Vector3(Horiz,0f,Vert));                       

            if (!isCrouch)
            {                
                   // moveSpeed =Input.GetKey(KeyCode.LeftShift)?runSpeed:walkSpeed;
                if(Input.GetKey(KeyCode.S))
                {
                    moveSpeed=walkSpeed;
                }
                else if(Input.GetKey(KeyCode.LeftShift)&&!Input.GetKey(KeyCode.S))
                {
                    //if(onlyCrouch)
                    //{
                    //    return;
                    //}
                    moveSpeed=runSpeed;
                }
                else
                {
                    moveSpeed=walkSpeed;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {

                    stopCrouch=true;
                    if (onlyCrouch)
                    {
                        stopCrouch=false;
                    }
                }
                else
                {
                    moveSpeed=crouchSpeed;
                }
            }

            if (!Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKeyDown(KeyCode.C)||stopCrouch==true)
                {
                    truePlayerHeight=isCrouch ? originHeight : crouchHeight;
                    StartCoroutine(Crouch(truePlayerHeight));
                    isCrouch=!isCrouch;
                    stopCrouch=false;
                }
            }

            

            if (Input.GetButtonDown("Jump"))
            {
                if(onlyCrouch)
                {
                    return;
                }
                if(isCrouch)
                {
                    stopCrouch=true;         
                }
                playerDir.y=jumpHeight;
            }

            var temp_Vel = controller.velocity;
            temp_Vel.y=0;
            velocity=temp_Vel.magnitude;

            if(weapnManger.mainWeapon!=null||weapnManger.secondWeapon!=null)
            {
                anim.SetFloat("Speed", velocity, 0.25f, Time.deltaTime);
            }
        }

        playerDir.y-=grivate*Time.deltaTime;
       
        controller.Move(moveSpeed*playerDir*Time.deltaTime);
        
         if(Input.GetKeyDown(KeyCode.U))
        {
            transform.position=target.position;
        }
    }

    IEnumerator Crouch(float height)
    {
        float currentVel = 0;
        while (Mathf.Abs(controller.height-height)>0.1)
        {
            yield return null;
            controller.height= Mathf.SmoothDamp(controller.height, height, ref currentVel, Time.deltaTime*5);
        }
    }
    
}
