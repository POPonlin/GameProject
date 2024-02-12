using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damage;
    public DataUI dataUI;
    private bool m;
    // Start is called before the first frame update
    void Start()
    {
        //dataUI=FindObjectOfType<DataUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        dataUI.Hurt(damage);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.CompareTag("Player"))
    //    {
    //        //Debug.Log(898);
    //        dataUI.Hurt(damage);
    //    }
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if (!m)
    //    {
    //        if (other.gameObject.CompareTag("Player"))
    //        {
    //            m=true;
                
    //            dataUI.Hurt(damage);
    //            StartCoroutine(Wait());
    //        }
    //    }
    //}

    //IEnumerator Wait()
    //{
    //    yield return new WaitForSeconds(1f);
    //    dataUI.Hurt(damage);
    //    m=false;
    //}
}
