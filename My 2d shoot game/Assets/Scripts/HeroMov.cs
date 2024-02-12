using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroMov : MonoBehaviour
{
    private Rigidbody2D rd2;
    private Animator animator;
    public int speed;
    private Vector2 mousePos;
    private float px=0;
    private float py=0;
    public GameObject[] Guns;
    private int gunNum;
    public  Image jindu;
    public Text Text;

    public GameObject dieUI;

    private SpriteRenderer rend;

    private Color color;

    public float time;
    //���˺�������
   // public GameObject hero;

    public float heroBlood;
    //����Ѫ��
   // public int heroBullets;
    //�����ӵ���
    private float blood;
    // Start is called before the first frame update
    void Start()
    {
        rend=GetComponent<SpriteRenderer>();
        rd2=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();

        //dieUI=GameObject.Find("DeadUI");
        //dieUI.SetActive(false);
        blood=heroBlood;
        color=rend.color;
    }

    // Update is called once per frame
    void Update()
    {
         SwitchGun();
         mousePos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //��ȡ���λ��
         GunRoation(mousePos);
        HeroBlood();
    }
    private void FixedUpdate()
    {
        px = Input.GetAxis("Horizontal");
        py = Input.GetAxis("Vertical");

        PlayerMove(px, py);

        if (px!=0||py!=0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        //Shoot();

      
    }
    //private void Shoot()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        animator.SetTrigger("Fire");
    //        return;
    //    }
    //}

    /// <summary>
    /// ����ƶ�����
    /// </summary>
    /// <param name="x">x����</param>
    /// <param name="y">y����</param>
    private void PlayerMove(float x,float y)
    {
        transform.Translate(Vector2.right*px*speed*Time.fixedDeltaTime,Space.World);
        transform.Translate(Vector2.up*py*speed*Time.fixedDeltaTime, Space.World);
    }
    /// <summary>
    /// ǹе��ת
    /// </summary>
    /// <param name="vec"></param>
    private void GunRoation(Vector2 vec)
    {
        if(mousePos.x>transform.position.x)
        {
            transform.rotation=Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else
        {
            transform.rotation=Quaternion.Euler(new Vector3(0,180,0));
        }
    }
    /// <summary>
    /// �л�ǹе
    /// </summary>
    private void SwitchGun()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Guns[gunNum].SetActive(false);
            gunNum--;
            if(gunNum<0)
            {
                gunNum=Guns.Length-1;
            }
            Guns[gunNum].SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            Guns[gunNum].SetActive(false);
            gunNum++;
            if(gunNum>Guns.Length-1)
            {
                gunNum=0;
            }
            Guns[gunNum].SetActive(true);
        }
    }

    /// <summary>
    ///Ѫ��Ϊ0���� 
    /// </summary>
    private void HeroBlood()
    {
        if(heroBlood<=0)
        {
            Destroy(gameObject);
            
           
        }
    }

    /// <summary>
    /// Ӣ���ܵ����˺�
    /// </summary>
    /// <param name="damage">���˵��˺�ֵ</param>
    public void Damaged(int damage)
    {
        heroBlood-=damage;
        jindu.fillAmount=heroBlood/blood;
        
        Text.text=heroBlood.ToString();
        if(heroBlood<=0)
        {
            Text.text="0";
            dieUI.SetActive(true);
        }

        rend.color=Color.green;
        Invoke("FTime",time);

    }

    private void FTime()
    {
        rend.color=color;
    }

    //public void BulletNums(int num)
    //{
    //    heroBullets-=num;
    //}
}
