using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuiXiangPool : MonoBehaviour
{
    public static DuiXiangPool instance;

    public GameObject duiXiang;

    public int duiXiangCount;

    public Queue<GameObject> queue=new Queue<GameObject>();
    // Start is called before the first frame update
    private void Awake()
    {
        instance=this;
        FillPool();
    }

    public void FillPool()
    {
        for (int i = 0; i<duiXiangCount; i++)
        {
            var newDuiXiang = Instantiate(duiXiang);
            newDuiXiang.transform.SetParent(transform);

            ReturnPool(newDuiXiang);
        }
    }

    public void ReturnPool(GameObject game)
    {
        game.SetActive(false);

        queue.Enqueue(game);
    }

    public GameObject GetPool()
    {
        if (queue.Count==0)
        {
            FillPool();
        }
        var outDuiXiang = queue.Dequeue();

        outDuiXiang.SetActive(true);

        return outDuiXiang;
    }
}
