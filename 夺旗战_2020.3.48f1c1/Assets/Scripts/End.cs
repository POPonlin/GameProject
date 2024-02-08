using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class End : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        text.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Offense")
        {
            text.enabled = true;
            
            if(text.IsActive())
            {
                Time.timeScale = 0f;
            }
        }
    }
}
