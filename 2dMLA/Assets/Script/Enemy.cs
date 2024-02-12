using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class Enemy : MonoBehaviour
{
    
    public float enemyBlood;
    public Text text;
    public  float enemyHit=1;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(enemyBlood<=0)
        {
            Destroy(gameObject);
            //int b = Player.score1+100;
            Player.score1+=100;
            //print(b);
            text.text=Player.score1.ToString();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player")&&collision.gameObject.transform.position.y>=transform.position.y)
        {
            Damage(Player.hit);
            collision.GetComponent<Player>().SmellJump();
        }
   
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().PlayerDamage(enemyHit);
            Player b= collision.gameObject.GetComponent<Player>();
            b.Hit();   
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().PlayerDamage(enemyHit);
            Player b = collision.gameObject.GetComponent<Player>();
            b.Hit2();
        }
    }
    public  void Damage(float x)
    {
        enemyBlood-=x;
    }
}
