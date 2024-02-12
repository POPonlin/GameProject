using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QingWaAi : MonoBehaviour
{
    public float moveSpeed;
    //移动速度
    public Transform leftObject,rightObject;
    //左右移动范围
    private float leftTag, rightTag;
    private bool door;
    private Rigidbody2D rid;
    private SpriteRenderer ren;
    // Start is called before the first frame update
    void Start()
    {
        door=true;
        ren=GetComponent<SpriteRenderer>();
        rid=GetComponent<Rigidbody2D>();
        transform.DetachChildren();
        leftTag=leftObject.transform.position.x;
        rightTag=rightObject.transform.position.x;
        Destroy(leftObject.gameObject);
        Destroy(rightObject.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        QingWaMove();
    }

    private void QingWaMove()
    {
        if(door)
        {

            //Vector2 vec = new Vector2(moveSpeed,0.0f);
            //rid.velocity=vec*(-1)*Time.deltaTime;
            rid.velocity=new Vector2(-moveSpeed, 0.0f);
            if(transform.position.x<=leftTag)
            {
                door=false;
                ren.flipX=true;
            }
        }
        else
        {
            //Vector2 vec = new Vector2(moveSpeed, 0.0f);
            //.velocity=vec*Time.deltaTime;
            rid.velocity=new Vector2(moveSpeed, 0.0f);
            if (transform.position.x>=rightTag)
            {
                door=true;
                ren.flipX=false;
            }
        }
    }
}
