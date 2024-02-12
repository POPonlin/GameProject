using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("鼠标灵敏度")]
    public float mouseSensitivity;
    [Header("主视角转动角度")]
    private Vector3 camerRotation;
    [Header("视角上下转动限制")]
    public Vector2 max_minAngle;
    [Header("")]
    [SerializeField] private Transform playerTrasform;
    private Transform cameraTransform;

    [Header("")]
    public AnimationCurve animCure;
    public Vector2 RecoilRange;

    private float currentRecoilTime;
    private Vector2 currentRecoil;

    public bool stopWork;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform=transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(stopWork)
        {
            return;
        }

        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");

        camerRotation.y+=mouseX*mouseSensitivity;
        camerRotation.x-=mouseY*mouseSensitivity;

        Recoil();

        camerRotation.x-=currentRecoil.y;

        camerRotation.x= Mathf.Clamp(camerRotation.x, max_minAngle.x, max_minAngle.y);

        cameraTransform.rotation=Quaternion.Euler(camerRotation.x, camerRotation.y, 0);
        playerTrasform.rotation=Quaternion.Euler(0, camerRotation.y, 0);
    }

    private void Recoil()
    {
        currentRecoilTime+=Time.deltaTime;
        float curveValue = animCure.Evaluate(currentRecoilTime/0.3f);
        currentRecoil=Vector2.Lerp(Vector2.zero, currentRecoil, curveValue);
    }

    public void FringForTest()
    {
        currentRecoil+=RecoilRange;
        currentRecoilTime=0;
    }
}
