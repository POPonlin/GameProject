using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataUI : MonoBehaviour
{
    public Text gold;
    public Image blood;
    public Image blue;

    public int goldValue;
    public float currentBloodValue;
    public float maxBlood;

    public GameObject ui;
    // Start is called before the first frame update
    void Start()
    {
        currentBloodValue=maxBlood;
    }

    // Update is called once per frame
    void Update()
    {
        gold.text=goldValue.ToString();

        blood.fillAmount=currentBloodValue/maxBlood;

        if(currentBloodValue<=0f)
        {
            Time.timeScale=0;
            ui.SetActive(true);
            Cursor.visible = true;
        }
    }

    public void BloodTool()
    {
        Debug.Log(515445745);
        currentBloodValue=maxBlood;       
    }

    public void Gold(int value)
    {
        goldValue+=value;
        gold.text=goldValue.ToString();
    }



    public void Hurt(float damage)
    {
        if(currentBloodValue>=0)
        {
            currentBloodValue-=damage;
        }
        else
        {
            currentBloodValue=0f;
        }
    }
}
