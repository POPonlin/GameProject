using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReminderUI : MonoBehaviour
{
    public GameObject tiShi;
    public GameObject jiaoHU;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TiShiUI_True()
    {
        tiShi.SetActive(true);
    }

    public void TiShiUI_False()
    {
        tiShi.SetActive(false);
    }

    public void JiaoHU_True()
    {
        jiaoHU.SetActive(true);
    }

    public void JiaoHU_False()
    {
        jiaoHU.SetActive(false);
    }
}
