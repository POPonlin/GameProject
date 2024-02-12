using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private float x;
    private float z;
    private float y;

    [Header("左右移动速度")]
    public float speed1=50;
    [Header("上下移动速度")]
    public float speed2=800;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        z = Input.GetAxis("Horizontal");
        x = (Input.GetAxis("Vertical"))*(-1);
        y = Input.GetAxisRaw("Mouse ScrollWheel");

        transform.Translate(new Vector3(x*speed1,y*speed2,z*speed1)*Time.deltaTime,Space.World);
    }
}
