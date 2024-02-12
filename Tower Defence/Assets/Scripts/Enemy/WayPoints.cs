using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public static Transform[] postions;
    private void Awake()
    {
        postions=new Transform[transform.childCount];
        for(int i=0;i<postions.Length;i++)
        {
            postions[i]=transform.GetChild(i);
        }
    }

    void Update()
    {
        
    }
}
