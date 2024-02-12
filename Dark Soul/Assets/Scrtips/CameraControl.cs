using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
    public Image lockImage;

    public float horizontalSpeed=100f;
    public float verticalSpeed=100f;

    public PlayerInput playerInput;

    public LockTarget lockTarget;   //存储标记到敌人，同时也可以拿来做判断

    public bool lockFlag;

    [Header("是否为AI")]
    public bool isAI;

    private GameObject cameraHandle;    //控制上下旋转的物体
    private GameObject activeBox;   //控制水平旋转的物体
    private GameObject model;   //人物模型

    private float tempRX;
    private Vector3 tempRot;

    private GameObject camera1;

    // Start is called before the first frame update
    void Awake()
    {
        cameraHandle=transform.parent.gameObject;
        activeBox=cameraHandle.transform.parent.gameObject;
        model=activeBox.GetComponent<PlayerControl>().model;
        camera1=Camera.main.gameObject;
        playerInput=activeBox.gameObject.GetComponent<PlayerInput>();

        Cursor.lockState=CursorLockMode.Locked;
    }

    private void Update()
    {
        if(lockTarget!=null)
        {
            //print(lockTarget.height);
            lockImage.rectTransform.position=Camera.main.WorldToScreenPoint(lockTarget.gam.transform.position+new Vector3(0,lockTarget.height,0));
            //注意是转换lockTarget.gam的坐标

            if(Vector3.Distance(model.transform.position,lockTarget.gam.transform.position)>10f)
            {
                lockFlag=false;
                lockTarget=null;
                LockImage(lockFlag);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tempRot=model.transform.eulerAngles;
        if(lockTarget==null)
        {
            if(isAI==true)
            {
                return;
            }
            activeBox.transform.Rotate(Vector3.up,playerInput.dDirRight*horizontalSpeed*Time.fixedDeltaTime);   //绕y轴进行旋转

            tempRX-=playerInput.dDirUp*verticalSpeed*Time.fixedDeltaTime;
            tempRX=Mathf.Clamp(tempRX,-30,40);  //限定转动角度
            cameraHandle.transform.localEulerAngles=new Vector3(tempRX,0,0);    //通过new新的向量值实现旋转

            model.transform.eulerAngles=tempRot;


        }
        else
        {
            Vector3 temp = lockTarget.gam.transform.position-model.transform.position;
            temp.y=0;
            activeBox.transform.forward=temp;   //输入信号的坐标轴是以activebox的局部坐标轴为基础的

            //cameraHandle.transform.LookAt(lockTarget.gameObject.transform);   //方法一
            Vector3 dir = lockTarget.gam.transform.position-cameraHandle.transform.position;    //方法二
            cameraHandle.transform.forward=Vector3.Lerp(cameraHandle.transform.forward, dir.normalized,0.2f);            
        }

        if(isAI==false)
        {
            camera1.transform.position=Vector3.Lerp(camera1.transform.position,transform.position,0.5f);
            //camera1.transform.eulerAngles=transform.eulerAngles;
            camera1.transform.LookAt(cameraHandle.transform);        
        }
    }

    private void LockImage(bool isOn)
    {
        lockImage.enabled=isOn;
    }

    public void LockOnTarget()
    {
        Vector3 point1 = model.transform.position+new Vector3(0f,1f,0f);
        Vector3 point2 = point1+model.transform.forward*5f; //生成在模型前方
        Collider[] temp= Physics.OverlapBox(point2,new Vector3(0.5f,0.5f,5f),model.transform.rotation,LayerMask.GetMask(isAI?"Player":"Enemy"));
        if(temp.Length==0)
        {
            lockTarget=null;
            lockFlag=false;
        }
        else
        {
            foreach (var item in temp)
            {
                if(lockTarget!=null&&lockTarget.gam==item.gameObject)
                {
                    lockTarget=null;
                    lockFlag=false;
                    break;
                }
                lockTarget=new LockTarget(item.gameObject,item.bounds.extents.y);
                lockFlag=true;
                break;
            }
        }

        LockImage(lockFlag);
    }

    public class LockTarget
    {
        public GameObject gam;
        public float height;
        public LockTarget(GameObject _lockTarget,float _height)
        {
            this.gam=_lockTarget;
            this.height=_height;
        }
    }

}
