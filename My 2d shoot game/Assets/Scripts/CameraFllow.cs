using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFllow : MonoBehaviour
{
    public Transform player;
    public float smoTime;
    // Start is called before the first frame update
    void Start()
    {
        //player=transform.Find("Hero");
    }

    // Update is called once per frame
    void Update()
    {
        if(player!=null)
        {
            if (transform.position!=player.position)
            {
                transform.position=Vector3.Lerp(transform.position, player.position, smoTime);
            }
        }    
    }
}
