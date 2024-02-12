using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseEnemy:Enemy
{
    public Image image1;
    public GameObject game1;
    //Ѫ�����������
    public GameObject game;
    //Ѫ����ʾ����Ķ���
    public GameObject game2;
    public Transform player;
    //������Զ���
    public GameObject yaoShi;
    private float blood;
    //��¼�����ʼѪ��
    // Start is called before the first frame update
    protected override void Start()
    {
        blood=enemyBlood;
    }

    // Update is called once per frame
    protected override void Update()
    {

        base.Update();
        if(enemyBlood>=0)
        {
            image1.fillAmount=enemyBlood/blood;
        }
        MouseBloodUI();
        DiaoLuo();
    }
    /// <summary>
    /// �����Ѫ��UI��ʾ���ƺ���
    /// </summary>
    public void MouseBloodUI()
    {
        if (player)
        {
            if (player.position.x>=game.transform.position.x&&player.position.y<=game2.transform.position.y)
            {
                game1.SetActive(true);
            }
            else
            {
                game1.SetActive(false);
            }
            if (enemyBlood<=0)
            {
                game1.SetActive(false);
            }
        }
    }

    private void DiaoLuo()
    {
        if(enemyBlood<=0)
        {
            Instantiate(yaoShi, transform.position, Quaternion.identity);
        }
    }
}
