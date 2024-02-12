using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    private Transform player;

    private SpriteRenderer self;
    private SpriteRenderer playerRend;

    private Color color;

    public float keepTime;//显示时间
    public float begionTime;//开始时间

    private float alpha;
    public float alphaSet;
    public float alphaChange;

    // Start is called before the first frame update
    private void OnEnable()
    {
        player=GameObject.FindGameObjectWithTag("Player").transform;
        self=GetComponent<SpriteRenderer>();
        playerRend=player.GetComponent<SpriteRenderer>();

        alpha=alphaSet;

        self.sprite=playerRend.sprite;

        transform.position=player.position;
        transform.localScale=player.localScale;
        transform.rotation=player.rotation;

        begionTime=Time.time;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        alpha*=alphaChange;

        color=new Color(1, 1, 1, alpha);

        self.color=color;

        if(Time.time>=begionTime+keepTime)
        {
            DuiXiangPool.instance.ReturnPool(this.gameObject);
        }
    }
}
