using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPool : MonoBehaviour
{
    [Header("¶ÔÏó³Øµ¥Àý")]
    public static ShadowPool instance;
    public int ShadowCount;
    public GameObject bulletPrefab;

    private Queue<GameObject> dui = new Queue<GameObject>();

    private void Awake()
    {
        instance=this;

        FillPool();
    }

    public void FillPool()
    {
        for(int i=0;i<ShadowCount;i++)
        {
            var newPrefab = Instantiate(bulletPrefab);
            newPrefab.transform.SetParent(transform);            

            ReturnPool(newPrefab);
        }
    }

    public void ReturnPool(GameObject gameObject)
    {
        var temp = gameObject.GetComponent<Rigidbody>();
        temp.velocity=new Vector3(0,0,0);

        gameObject.SetActive(false);

        dui.Enqueue(gameObject);
    }

    public GameObject OutPool()
    {
        if(dui.Count==0)
        {
            FillPool();
        }
        var tempOut = dui.Dequeue();

        tempOut.SetActive(true);

        return tempOut;
    }
}
