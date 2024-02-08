using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public Offense ower;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Offense")
        {
            GameManger.Instance.TakeFlag();
            if (ower != null)
            {
                other.GetComponent<Offense>().haveFlag = false;
            }

            ower = other.GetComponent<Offense>();
            other.GetComponent <Offense>().haveFlag = true;  
            transform.parent = other.transform;
        }
    }
}
