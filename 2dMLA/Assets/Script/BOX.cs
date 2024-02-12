using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOX : MonoBehaviour
{
    public Transform transform1;
    public GameObject yingTao;
    private GameObject yTStand;
    private Animator animator;
    private bool b;
    //private bool d;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (yTStand)
        {
            if (yTStand.gameObject.transform.position.x<transform1.position.x)
            {
                //print(666);
                yTStand.transform.position=Vector2.MoveTowards(yTStand.transform.position, transform1.position, Time.deltaTime*5.0f);

            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&transform.position.y>=collision.gameObject.transform.position.y)
        {
            animator.SetTrigger("Tan");
            if (b==false)
            {
                b=true;
                //Debug.Log(111);
                GameObject stand = Instantiate(yingTao, transform.position, Quaternion.identity);
                yTStand=stand;
                Vector2 vec2 = new Vector2(stand.transform.position.x, 6.0f);
                stand.gameObject.GetComponent<Rigidbody2D>().velocity=Vector2.up*vec2;
                
          
            }
        }
        
    }
}
