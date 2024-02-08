using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Offense : MonoBehaviour
{
    public bool haveFlag = false;

    private Vector3 startPostion;
    private Quaternion startRotation;

    private void Start()
    {
        startPostion = transform.position;
        startRotation = transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Defenses")
        {
            if (haveFlag)
            {
                haveFlag = false;
                GameManger.Instance.DropFlag();
                if (transform.childCount > 0)
                {
                    Transform tmp = transform.GetChild(0);
                    tmp.GetComponent<Flag>().ower = null;
                    tmp.parent= null;
                }
                transform.position= startPostion;
                transform.rotation= startRotation;
            }
        }
    }
}


