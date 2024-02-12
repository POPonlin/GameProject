using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletUI : MonoBehaviour
{
    public Text maxBulletsUI;
    public Text shellBulletsUI;
    public Text gunName;

    public GameObject ak47;
    public GameObject glock;
    public GameObject armAK;
    public GameObject armGLOCK;


    private AssaultRifle assaultRifle;
    private HandGun handGun;
    public StoreUI storeUI;
    // Start is called before the first frame update
    void Start()
    {
       // storeUI=FindObjectOfType<StoreUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ak47.activeSelf)
        {
            
            gunName.text="AK47";
            assaultRifle=armAK.GetComponent<AssaultRifle>();
            maxBulletsUI.text=assaultRifle.maxBulletUI.ToString();
            shellBulletsUI.text=assaultRifle.shellBulletUI.ToString();
            
            if(storeUI.store.activeSelf)
            {
                assaultRifle.stopMouseWork=true;                
            }
            else
            {
                assaultRifle.stopMouseWork=false;
            }
        }

        else if(glock.activeSelf)
        {
            gunName.text="Glock";
            handGun=armGLOCK.GetComponent<HandGun>();
            maxBulletsUI.text=handGun.maxBulletUI.ToString();
            shellBulletsUI.text=handGun.shellBulletUI.ToString();

            if (storeUI.store.activeSelf)
            {
                handGun.stopMouseWork=true;
            }
            else
            {
                handGun.stopMouseWork=false;
            }
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{

    //}


    public void UpBullet()
    {                
            Debug.Log("dawd");
            if (ak47.activeSelf)
            {
                assaultRifle=armAK.GetComponent<AssaultRifle>();
                assaultRifle.maxBullets+=10;
                assaultRifle.currentBullets=assaultRifle.maxBullets;
            }
            else if (glock.activeSelf)
            {
                handGun=armGLOCK.GetComponent<HandGun>();
                handGun.maxBullets+=10;
                handGun.currentBullets=handGun.maxBullets;
            }                    
    }
    


}
