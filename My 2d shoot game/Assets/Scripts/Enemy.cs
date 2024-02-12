using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Transform muzzlePot2;
    public float  enemyBlood;
    public float flishTime;
    private SpriteRenderer sprite;
    private Color color;
    public void Awake()
    {
        sprite=GetComponent<SpriteRenderer>();
        color=sprite.color;
    }
    // Start is called before the first frame update
    public void Start()
    {
        
      
    }


    // Update is called once per frame
    public void Update()
    {
        if(enemyBlood<=0)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ø€—™
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(float damage)
    {
        enemyBlood-=damage;
        FlishColor(flishTime);
    }
    
    /// <summary>
    ///  ‹…À…¡∫Ï
    /// </summary>
    /// <param name="time"></param>
    private void FlishColor(float time)
    {
        sprite.color=Color.red;
        Invoke("OrColor", time);
    }

    /// <summary>
    /// «–ªªªÿ‘≠…´
    /// </summary>
    private void OrColor()
    {
        sprite.color=color;
    }
}
