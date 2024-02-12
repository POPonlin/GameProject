using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("�ƶ������Զ���")]
    public string keyUp="w";
    public string keyDown="s";
    public string keyLeft="a";
    public string keyRight="d";

    public string keyRun;
    public string keyJump;

    public string keyDirUp;
    public string keyDirDown;
    public string keyDirRight;
    public string keyDirLeft;
    [Header("���������Զ���")]
    public string keyMouse0;
    public string keyMouse1;

    public string keyStab;
    [Header("�ع����򷴻���")]
    public string keyCounterBack;
    [Header("���������Զ���")]
    public string keyLockOn;
    
    [Header("�ź����ģ��")]
    public float dup;   
    public float dright;

    public float dDirUp;
    public float dDirRight;

    public bool jump;
    public bool run;
    public bool mouseL;
    public bool mouseR;
    public bool lockOn;
    public bool counterBack;
    public bool stab;
    public float dmag;  //������ֱ������ˮƽ�����ƽ����ֵ
    public Vector3 dvec;  //�����������ȷ����

    [Header("�������")]
    public bool flag;
    

    private float targetDup;    //��ֱ��������Ŀ��ֵ
    private float targetDright; //ˮƽ��������Ŀ��ֵ
    private float vectoryUp;
    private float vectoryRight;

    private float targetDirDup;
    private float targetDirRight;
    private float vectoryDirUp;
    private float vectoryDirRight;
    
    void Start()
    {
        flag=true;
    }

    // Update is called once per frame
    void Update()
    {
        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0f)-(Input.GetKey(keyDown) ? 1.0f : 0f);
        targetDright = (Input.GetKey(keyRight)?1.0f:0f)-(Input.GetKey(keyLeft) ? 1.0f : 0f);
        targetDirDup=(Input.GetKey(keyDirUp) ? 1.0f : 0f)-(Input.GetKey(keyDirDown) ? 1.0f : 0f);
        targetDirRight=(Input.GetKey(keyDirRight) ? 1.0f : 0f)-(Input.GetKey(keyDirLeft) ? 1.0f : 0f);

        run=Input.GetKey(keyRun);
        jump=Input.GetKeyDown(keyJump);
        mouseL=Input.GetKeyDown(keyMouse0);
        mouseR=Input.GetKeyDown(keyMouse1);
        lockOn=Input.GetKeyDown(keyLockOn);
        counterBack=Input.GetKeyDown(keyCounterBack);
        stab=Input.GetKeyDown(keyStab);

        if(flag==false)
        {
            targetDup=0;
            targetDright=0;
        }

        dup = Mathf.SmoothDamp(dup,targetDup,ref vectoryUp,0.1f);
        dright = Mathf.SmoothDamp(dright,targetDright,ref vectoryRight,0.1f);
        dDirUp=Mathf.SmoothDamp(dDirUp,targetDirDup,ref vectoryDirUp,0.1f);
        dDirRight=Mathf.SmoothDamp(dDirRight,targetDirRight,ref vectoryDirRight,0.1f);

        dmag=Mathf.Sqrt(dup*dup+dright*dright);
        if(dmag>1)  //��ֹб����ֵ��Ϊ1.414
        {
            dmag=1;
        }

        dvec=transform.forward*dup+transform.right*dright;

    }
}
