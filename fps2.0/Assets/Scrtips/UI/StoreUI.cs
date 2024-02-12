using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    public GameObject store;
    public Transform worldCamera;
    public LayerMask checkLayer;
    public GameObject AK47;
    public GameObject Health;
    public GameObject Bullet;
    public GameObject a;
    public GameObject b;

    private ReminderUI reminderUI;
    public DataUI dataUI;
    private CameraControl cameraControl;
    private HandGun weapenClass;
    // Start is called before the first frame update
    void Start()
    {
        reminderUI=FindObjectOfType<ReminderUI>();
        //dataUI=FindObjectOfType<DataUI>();
        cameraControl=FindObjectOfType<CameraControl>();
        weapenClass=FindObjectOfType<HandGun>();
    }

    // Update is called once per frame
    void Update()
    {
        bool temp=Physics.Raycast(worldCamera.position, worldCamera.forward, out RaycastHit hit, 2, checkLayer);
        if(temp)
        {
            if(hit.collider.CompareTag("Store"))
            {
                reminderUI.JiaoHU_True();
                if(Input.GetKeyDown(KeyCode.F))
                {
                    store.SetActive(true);
                    cameraControl.stopWork=true;
                    Cursor.visible = true;
                    // weapenClass.stopMouseWork=true;
                }
            }
        }
        else
        {
            reminderUI.JiaoHU_False();
        }
    }

    public void Close()
    {
        store.SetActive(false);
        cameraControl.stopWork=false;
        //weapenClass.stopMouseWork=false;
        Cursor.visible = false;
    }

    public void BuyAK47()
    {
        if(dataUI.goldValue>=500)
        {
            AK47.SetActive(true);
            dataUI.goldValue-=500;
        }
    }

    public void BuyHealth()
    {
        if(dataUI.goldValue>=800)
        {
            Instantiate(Health, a.transform);
            dataUI.goldValue-=800;
        }
    }

    public void BuyBullet()
    {
        if (dataUI.goldValue>=50)
        {
            Instantiate(Bullet, b.transform);
            dataUI.goldValue-=50;
        }
    }
}
