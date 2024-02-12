using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodTool : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<DataUI>().BloodTool();
            
            Destroy(gameObject);
        }
    }
}
