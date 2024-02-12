using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTool : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<BulletUI>().UpBullet();
            Destroy(gameObject);
        }
    }
}
