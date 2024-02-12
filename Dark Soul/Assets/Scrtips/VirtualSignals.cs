using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualSignals : PlayerInput
{
    // Start is called before the first frame update
    void Start()
    {
        dup=0;
        dright=0;
        dDirRight=0;
        dDirUp=0;
        mouseL=false;
        mouseR=true;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
