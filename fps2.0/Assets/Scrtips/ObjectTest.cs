using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTest : MonoBehaviour
{
    private PlayControl playControl;
    // Start is called before the first frame update
    void Start()
    {
        playControl=FindObjectOfType<PlayControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {          
            playControl.onlyCrouch=true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {            
            playControl.onlyCrouch=false;
        }
    }
}
