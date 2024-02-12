using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeapnManger : MonoBehaviour
{
    //public GameObject[] gun;
    public Animator glockAnim;
    public Animator ak47Anim;

    public AnimatorStateInfo stateInfo;
    public AnimatorStateInfo stateInfo1;

    public Transform worldCamera;
    public LayerMask checkLayer;

    public List<GameObject> Arms = new List<GameObject>();

    public GameObject mainWeapon;
    public GameObject secondWeapon;

    public GameObject ak47Object;
    public GameObject glockObject;


    private PlayControl playControl;
    public ReminderUI reminderUI;
    
    public GameObject currentWeapon;
    // Start is called before the first frame update
    
    void Start()
    {        
        //mainWeapon=gun[0];
        //secondWeapon=gun[1];
        //currentWeapon=mainWeapon;
        //currentWeapon=secondWeapon;
        playControl=FindObjectOfType<PlayControl>();
        //reminderUI=FindObjectOfType<ReminderUI>();
        
    }

    // Update is called once per frame
    void Update()
    {        
        if(mainWeapon)
        {
            ak47Object.SetActive(false);
        }
        if(secondWeapon)
        {
            glockObject.SetActive(false);
        }

        if (!mainWeapon||!secondWeapon)
        {
            CheckItem();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (mainWeapon)
            {
                if (currentWeapon==mainWeapon)
                {
                    return;
                }

                glockAnim.Play("holster_weapon@handgun_01");

                StartCoroutine(GlockHolster());
            }
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (secondWeapon)
            {
                if (currentWeapon==secondWeapon)
                {
                    return;
                }

                ak47Anim.Play("holster_weapon@assault_rifle_01");

                StartCoroutine(AKHolster());
            }
        }
    }

    //private void BulletsUI()
    //{
    //    if(currentWeapon)
    //    {
    //        if(currentWeapon==mainWeapon)
    //        {
    //            //bulletUI.BulletsUI(assaultRifle.shellBulletUI, assaultRifle.maxBulletUI);
    //            //Debug.Log(assaultRifl);
    //        }
    //        else if(currentWeapon==secondWeapon)
    //        {
    //            //bulletUI.BulletsUI(handGun.shellBulletUI, handGun.maxBulletUI);
    //        }    
    //    }
    //}

    private void CheckItem()
    {
        bool tempCheck= Physics.Raycast(worldCamera.position,worldCamera.forward,out RaycastHit hit,2,checkLayer);
        if(tempCheck)
        {

            if(!mainWeapon||!secondWeapon)
            {
                reminderUI.TiShiUI_True();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                bool temp_BaseItem = hit.collider.TryGetComponent(out BaseItem baseItem);
                if(temp_BaseItem)
                {                    
                    if(baseItem is GunItem gunItem)
                    {
                        foreach(GameObject gun in Arms)
                        {
                            if(gunItem.armsName.CompareTo(gun.name)==0)
                            {
                                switch(gunItem.currentFirearmsType)
                                {
                                    case GunItem.FirearmsType.AK47:
                                        mainWeapon=gun;
                                        playControl.anim=ak47Anim;
                                        //currentWeapon=gun;
                                        break;
                                    case GunItem.FirearmsType.Glock:
                                        secondWeapon=gun;
                                        playControl.anim=glockAnim;
                                        //currentWeapon=gun;
                                        break;
                                }

                                CurrentWeapon(gun);
                            }
                        }
                    }
                }
            }
        }
        else
        {
            if(reminderUI)
            { 
                reminderUI.TiShiUI_False();
            }
        }
    }

    private void CurrentWeapon(GameObject gameObject)
    {
        if(currentWeapon==gameObject)
        {
            return;
        }
        if(currentWeapon!=null)
        {
            currentWeapon.SetActive(false);
        }
        currentWeapon=gameObject;
        currentWeapon.SetActive(true);

        reminderUI.TiShiUI_False();
    }

    IEnumerator GlockHolster()
    {
        while(true)
        {
            stateInfo=glockAnim.GetCurrentAnimatorStateInfo(0);
            yield return null;

            if(stateInfo.IsTag("GlockHolster"))
            {
                if(stateInfo.normalizedTime>=0.9f)
                {                    
                    currentWeapon.SetActive(false);
                    currentWeapon=mainWeapon;
                    currentWeapon.SetActive(true);

                    yield return null;  //这里要暂停一帧的原因是，执行完SetActive(true)以后的下一帧，
                                        //才会变成active.确保active为true后再调用动画播放
                                        //不然会报Animator is not playing an AnimatorController的警告

                    playControl.anim=ak47Anim;
                    yield break;
                }
            }            
        }
        
    }

    IEnumerator AKHolster()
    {
        while(true)
        {
            stateInfo1=ak47Anim.GetCurrentAnimatorStateInfo(0);
            yield return null;
            if(stateInfo1.IsTag("AK47Holster"))
            {
                if (stateInfo1.normalizedTime>=0.9f)
                {
                    currentWeapon.SetActive(false);
                    currentWeapon=secondWeapon;
                    currentWeapon.SetActive(true);

                    yield return null;

                    playControl.anim=glockAnim;
                    yield break;
                }
            }
        }
    }
}
