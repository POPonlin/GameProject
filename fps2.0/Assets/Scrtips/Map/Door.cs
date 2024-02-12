using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{   
    private Vector3 a;
    private Vector3 originPosition;
    private Trigger cubeTrigger;
    
    void Start()
    {
        originPosition=transform.position;
        cubeTrigger=FindObjectOfType<Trigger>();
        a=new Vector3(transform.position.x, transform.position.y-12, transform.position.z);        
    }

    private void Update()
    {
        if(cubeTrigger.inCube)
        {
            transform.position=Vector3.MoveTowards(transform.position, a, 7*Time.deltaTime);            
        }

        if(transform.position==a)
        {
            StartCoroutine(CloseDoor());
        }
       
    }
    
    IEnumerator CloseDoor()
    {
        yield  return new WaitForSeconds(2);
        Close();
    }

    public void Close()
    {
        transform.position=Vector3.MoveTowards(transform.position,originPosition , 7*Time.deltaTime);
    }

    

}
