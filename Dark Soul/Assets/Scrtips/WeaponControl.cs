using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defective.JSON;

public class WeaponControl : MonoBehaviour
{
    public WeaponManger wm;
    public WeaponData wdata;
    // Start is called before the first frame update
    void Awake()
    {
        wdata = GetComponentInChildren<WeaponData>();
    }

    private void Start()
    {
        
    }

    public float GetATK()
    {
        return wdata.ATK;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
