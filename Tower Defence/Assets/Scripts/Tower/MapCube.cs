using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour
{
    [HideInInspector]
    public GameObject aliveBuild;

    public GameObject effect;

    [HideInInspector]
    public bool isUpgreaded = false;    //ÊÇ·ñÉý¼¶¹ý

    private Renderer renderers;

    private void Start()
    {
        renderers=gameObject.GetComponent<Renderer>();
    }

    public void GoToCreatBuild(GameObject prefab)
    {
        isUpgreaded=false;

        aliveBuild=GameObject.Instantiate(prefab,transform.position,Quaternion.identity);

        GameObject temp=GameObject.Instantiate(effect, transform.position, Quaternion.identity);

        Destroy(temp,1f);
    }

    private void OnMouseEnter()
    {
        if(aliveBuild==null&&EventSystem.current.IsPointerOverGameObject()==false)
        {
            renderers.material.color=Color.red;
        }
    }
    
    private void OnMouseExit()
    {
        renderers.material.color=Color.white;
    }

}
