using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BloodUI : MonoBehaviour
{
    //public GameObject bloodUI;
    public GameObject ui;
    public bool isActive;
    public Text enemyNameText;

    public string enemyName;
    public EnemyHealth enemyHealth;

    public Image blood1;
    public Image blood2;

    private float enemyBloodValue;
    private bool b;
    // Start is called before the first frame update
    private void OnEnable()
    {
        enemyNameText.text=enemyName;
        enemyBloodValue=enemyHealth.maxBlood;
    }

    private void OnDisable()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {      

       
        if(enemyHealth.blood!=enemyHealth.maxBlood)
        {
            BloodShow();
        }
    }

    private void BloodShow()
    {        
        blood1.fillAmount=enemyHealth.blood/enemyHealth.maxBlood;
        enemyBloodValue=enemyHealth.blood;
     
        
          
            StartCoroutine(WaitBloodShow(enemyBloodValue));
        
    }

    IEnumerator WaitBloodShow(float value)
    {
        yield return new WaitForSeconds(1);
        blood2.fillAmount=value/enemyHealth.maxBlood;
              
    }
}
