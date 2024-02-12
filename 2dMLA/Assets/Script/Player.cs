using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject siWang;
    public float playerBlood;
    public Text zuanText;
    public Text xingText;
    public Text scoreText;
    public Text bloodText;
    public float moveSpeed;
    public float junpSpeed;
    private float x;
    public GameObject p;
   // public static Player instance { get; private set; }

    public static float hit=1;
    public BoxCollider2D box2;
    private BoxCollider2D box;
    public static Animator animator;
    private Rigidbody2D rid;
    private int zuan1=0;
    private int xing1=0;
    public static int score1 = 0;
    private int t = 0;//控制分数是否增加的开关

    //------------------------------
    private bool k;//控制是否开启加速的开关

    public float dashTime;//加速时间
    private float dashTimeLeft;//加速剩余时间
    private float lastDash=-10f;//上一次加速的时间点
    public float dashCollDown;//冷却时间
    public float dashSpeed;//加速的速度大小

    public bool isDashing;//判断是否处于加速
    // Start is called before the first frame updat
  
    public static bool yaoShi;

    public GameObject winUI;
    void Start()
    {
        box=GetComponent<BoxCollider2D>();
        animator=GetComponent<Animator>();
        rid=GetComponent<Rigidbody2D>();
       // winUI=GameObject.FindGameObjectWithTag("WinUI");
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
        x = Input.GetAxisRaw("Horizontal");
        if (k)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (Time.time>=lastDash+dashCollDown)
                {
                    ReadyToDash();
                }
            }
        }
        Dash();
        
        Move();
        PlayerJump();
        Jump1();
        if(score1>9999)
        {
            t=1;
            scoreText.text="9999(MAX)";
        }
        if(playerBlood<=0)
        {
            bloodText.text="x"+0;
            Destroy(gameObject);
            siWang.SetActive(true);
        }
        if(transform.position.y<=p.transform.position.y)
        {
            Destroy(gameObject);
            bloodText.text="x"+0;
            siWang.SetActive(true);
        }

        if(xing1==1&&score1>=2800)
        {
            //Debug.Log(666);
            winUI.SetActive(true);
        }
        
    }
    
    public void PlayerDamage(float n)
    {
        
        if(playerBlood>0)
        {
            playerBlood-=n;
           // animator.SetBool("Hurt", true);
           
            for(int i=0;i<2;i++)
            {
                if(i>2)
                {
                    animator.SetBool("Hurt", false);
                }
            }
            bloodText.text="x"+playerBlood;
            
        }
    }
    public void Hit()
    {
        animator.SetBool("Hurt", true);
    }
    public void Hit2()
    {
        animator.SetBool("Hurt", false);
    }
   
    private void PlayerJump()
    {
        
        
            if (Input.GetButtonDown("Jump"))
            {
            
            if (box.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                //rid.velocity=new Vector2(rid.velocity.x, junpSpeed*Time.deltaTime);
                Vector2 vector2 = new Vector2(0.0f, junpSpeed);
                rid.velocity=Vector2.up*vector2;
                animator.SetBool("Idle", false);
                animator.SetBool("Jump", true);

            }
            }
        
    }
    private void Jump1()
    {
       
          if(animator.GetBool("Jump")==true)
          {
            

            if(rid.velocity.y<0.0f)
                {
                        animator.SetBool("Idle", false);
                        animator.SetBool("Jump", false);
                        animator.SetBool("Down", true);
                }



          }
        if (animator.GetBool("Down")==true)
        {
            if (box.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                animator.SetBool("Idle", true);
                animator.SetBool("Down", false);
            }
            //else
            //{
            //    animator.SetBool("Down", false);
            //}
            else if(animator.GetBool("Idle"))
            {
                animator.SetBool("Idle", false);
            }
        }
    
     }
    public  void SmellJump()
    {
        Vector2 vec2 = new Vector2(0.0f, 6.0f);
        rid.velocity=Vector2.up*vec2;
    }
    private void Move()
    {

        transform.Translate(Vector2.right*moveSpeed*x*Time.deltaTime, Space.World);
        //rid.velocity=new Vector2(x*moveSpeed*Time.deltaTime, rid.velocity.y);
         
            if(x!=0)
            {
                transform.localScale=new Vector3(x, 1, 1);
            }
            if(x!=0)
            {
                animator.SetBool("Run",true);
                animator.SetBool("Idle", false);
            }
            else
            {
                animator.SetBool("Idle", true);
                animator.SetBool("Run", false);
            }
    }
    private void ReadyToDash()
    {
        isDashing=true;
        dashTimeLeft=dashTime;
        lastDash=Time.time;
    }
    private void Dash()
    {
        if(dashTimeLeft<=0)
        {
            isDashing=false;
        }
        if(isDashing)
        {
            if(dashTimeLeft>0)
            {
                rid.velocity=new Vector2(dashSpeed*x, rid.velocity.y);
                
                dashTimeLeft-=Time.deltaTime;
                DuiXiangPool.instance.GetPool();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("ZuanShi"))
        {
            Destroy(collision.gameObject);
            zuan1+=1;
            zuanText.text="x"+zuan1;
            if (t==0)
            {
                score1+=300;
                scoreText.text=score1.ToString();
            }
        }
        else if(collision.gameObject.CompareTag("XingXing"))
        {
            Destroy(collision.gameObject);
            xing1+=1;
            xingText.text="x"+xing1;
            if (t==0)
            {
                score1+=1000;
                scoreText.text=score1.ToString();
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("YingTao"))
        {
            Destroy(collision.gameObject);
            playerBlood+=1;
            bloodText.text="x"+playerBlood;
        }
        if(collision.gameObject.CompareTag("YaoShi"))
        {
            Destroy(collision.gameObject);
            yaoShi=true;
        }
        if (collision.gameObject.CompareTag("JiaSu"))
        {
            Destroy(collision.gameObject);
            k=true;
        }
    }

    
}
