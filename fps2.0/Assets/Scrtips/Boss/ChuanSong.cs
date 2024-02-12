using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChuanSong : MonoBehaviour
{
    public GameObject num;
    public Transform target;
    public int numb;

    public GameObject player;
    private bool p;
    public bool b;
    // Start is called before the first frame update
    void Start()
    {
        //player=GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(num.activeSelf)
        {
            if(!p)
            {
                StartCoroutine(www());

            }
        }

        if(p==true)
        {
            if(Input.GetKeyDown(KeyCode.P))
            {
                player.transform.position=target.transform.position;
            }
        }
    }

    IEnumerator www()
    {
        p=true;
        for(int i=numb;i>=0;i--)
        {
            yield return new WaitForSeconds(1f);
        
            num.GetComponent<Text>().text=i.ToString();
        }


        
        player.transform.position=target.transform.position;
        num.SetActive(false);

        
    }
}
