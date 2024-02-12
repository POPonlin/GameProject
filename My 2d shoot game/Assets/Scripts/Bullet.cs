using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public GameObject explosionPre;
    public float damage;
    private Rigidbody2D rd;
    // Start is called before the first frame update
    void Awake()
    {
        rd=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �ӵ��ٶ�
    /// </summary>
    /// <param name="dir"></param>
    public void SetSpeed(Vector2 dir)
    {
        rd.velocity=dir*speed;
    }
    
    /// <summary>
    /// �������
    /// </summary>
    /// <param name="collision">��������</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().Damage(damage);
        }
        Instantiate(explosionPre, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
