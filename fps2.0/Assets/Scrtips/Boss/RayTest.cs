using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{
    [Header("伤害值")]
    public float hurtDamage;
    [Header("激光转动速度")]
    public float turnSpeed;
    [Header("射线长度")]
    public float rayLine;
    [Header("射线检测层")]
    public LayerMask layer;
    public bool yes;
    public Transform target;

    private DataUI dataUI;
    private GameObject player;
    private bool a;
    // Start is called before the first frame update
    void Start()
    {
        dataUI=GameObject.FindGameObjectWithTag("Player").GetComponent<DataUI>();
        player=GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf)
        {               
            var q = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation= Quaternion.Slerp(transform.rotation,q,turnSpeed*Time.deltaTime);

            yes=Physics.Raycast(transform.position, target.position- transform.position, out RaycastHit hit,rayLine,layer);
            if (yes)
            {       
                if(!a)
                {
                    a=true;
                    StartCoroutine(Wait());                               
                }
            }

            Debug.DrawLine(transform.position,target.position- transform.forward,Color.blue);
        }
    }

    IEnumerator Wait()
    {        
        dataUI.Hurt(hurtDamage);       
        yield return new WaitForSeconds(0.5f);
        a=false;
    }
}
