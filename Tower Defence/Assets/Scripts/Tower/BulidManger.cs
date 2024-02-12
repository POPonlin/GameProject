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

    public TextMeshProUGUI moneyText; //TMP���壬������ʾ��Ǯ����

    public GameObject towerWorkPanel;   //�����������
    [Header("������ť")]
    public Button upGreat;
    [Header("ɾ����ť")]
    public Button dismantle;

    private GameObject tempBuild;   //���ڼ�¼��һ���ڳ�����ѡ�е�ʵ�彨��
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
                    if (mapCube.aliveBuild==null&&currentOnCube.currentTower!=null) //ѡ�з�����û�н���
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
                            //TODO����ʾ��Ǯ����
                            //
                        }
                    }
                    else if(mapCube.aliveBuild!=null)
                    {
                        if(towerWorkPanel.activeInHierarchy&&tempBuild==mapCube.aliveBuild) //���������ʾ���ҵڶ��ε����cube�ǵ�һ�ε����cube
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

        upGreat.interactable=!false;    //����������ť�Ƿ����ʹ��
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
