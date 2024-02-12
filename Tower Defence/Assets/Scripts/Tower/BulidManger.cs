using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class BulidManger : MonoBehaviour
{
    public CubeData laserBuild;
    public CubeData standBuild;
    public CubeData boomBuild;

    public CubeData currentOnCube;

    public float money=1000;

    public TextMeshProUGUI moneyText; //TMP字体，控制显示金钱数量

    public GameObject towerWorkPanel;   //建筑操作面板
    [Header("升级按钮")]
    public Button upGreat;
    [Header("删除按钮")]
    public Button dismantle;

    private GameObject tempBuild;   //用于记录第一次在场景中选中的实体建筑
    private void ChangeMoney(float change)
    {
        money+=change;
        moneyText.text=money.ToString();
    }

    void Start()
    {
        moneyText.text=money.ToString();
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(EventSystem.current.IsPointerOverGameObject()==false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                bool isCollider= Physics.Raycast(ray,out RaycastHit hit,1000,LayerMask.GetMask("CubeMask"));
                if(isCollider)
                {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    if (mapCube.aliveBuild==null&&currentOnCube.currentTower!=null) //选中方格上没有建筑
                    {
                        if (money>=currentOnCube.originCost)
                        {
                            mapCube.GoToCreatBuild(currentOnCube.currentTower);
                            //money-=currentOnCube.originCost;

                            ChangeMoney(-currentOnCube.originCost);
                        }
                        else
                        {
                            //
                            //TODO：提示金钱不足
                            //
                        }
                    }
                    else if(mapCube.aliveBuild!=null)
                    {
                        if(towerWorkPanel.activeInHierarchy&&tempBuild==mapCube.aliveBuild) //操作面板显示，且第二次点击的cube是第一次点击的cube
                        {
                            HideTowerWorkPanel();
                        }

                        else
                        {
                            ShowTowerWorkPanel(mapCube.transform, mapCube.isUpgreaded);
                        }

                        tempBuild=mapCube.aliveBuild;
                    }
                }
            }
        }
    }

    public void ClickUILaser(bool isON)
    {
        if(isON)
        {
            currentOnCube=laserBuild;
        }
    }

    public void ClickUIBoom(bool isON)
    {
        if(isON)
        {
            currentOnCube=boomBuild;
        }
    }

    public void ClickUIStand(bool isON)
    {
        if (isON)
        {
            currentOnCube=standBuild;
        }
    }

    public void ShowTowerWorkPanel(Transform pos,bool isON)
    {
        towerWorkPanel.SetActive(false);
        towerWorkPanel.transform.position=new Vector3(pos.position.x, 3.64f, pos.position.z);
        towerWorkPanel.SetActive(true);

        upGreat.interactable=!false;    //设置升级按钮是否可以使用
    }

    public void HideTowerWorkPanel()
    {
        towerWorkPanel.SetActive(false);        
    }

    public void UpgreatButton()
    {

    }

    public void DismantleButton()
    {

    }
}
