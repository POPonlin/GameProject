using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IsGround : MonoBehaviour
{
    [HideInInspector]
    public bool onGround=true;

    public Transform point1;
    public Transform point2;

    // Update is called once per frame
    void Update()
    {
       onGround = Physics.Raycast(point2.transform.position,point1.position-point2.position,0.2f,LayerMask.GetMask("Ground"));
    }
}
