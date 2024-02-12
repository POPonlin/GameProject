using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject duiHuaKuang;
    public GameObject door;
    public GameObject panel;

    private bool isCome;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)&&isCome==true)
        {
            duiHuaKuang.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.F)&&isCome==true)
        {
            if (Player.yaoShi==true)
            {
                Destroy(door);
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ZanTing();
        }
    }

    public void ChongLai()
    {
        SceneManager.LoadScene(0);
    }

    public void DuiHua()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isCome=true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isCome=false;
            duiHuaKuang.SetActive(false);
        }
    }

    public void Back()
    {
        panel.SetActive(false);
        Time.timeScale=1.0f;
    }

    public void ZanTing()
    {
        panel.SetActive(true);
        Time.timeScale=0.0f;
    }
}
