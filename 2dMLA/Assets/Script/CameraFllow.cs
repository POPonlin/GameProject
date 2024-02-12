using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFllow : MonoBehaviour
{
    // Start is called before the first frame update
    public float time;
    public Transform player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player)
        {
            if(transform.position!=player.position)
            {
                transform.position=Vector3.Lerp(transform.position, player.position, time);
            }    
        }
    }
}
