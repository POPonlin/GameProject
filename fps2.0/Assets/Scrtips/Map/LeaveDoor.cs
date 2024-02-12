using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LeaveDoor : MonoBehaviour
{
    public PlayableDirector playable;
    public GameObject UI;
    private Vector3 a;
    public GameObject[] box;

    private bool b;
    // Start is called before the first frame update
    private void Awake()
    {
       // playable=FindObjectOfType<PlayableDirector>();
    }
    void Start()
    {
        a=new Vector3(transform.position.x, transform.position.y-12, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        if (!b)
        {
            if (!box[0]&&!box[1]&&!box[2])
            {
                transform.position=Vector3.MoveTowards(transform.position, a, 7*Time.deltaTime);
                UI.SetActive(false);
                playable.Play();

            }
        }
        if (transform.position==a)
        {            
            Invoke("Stop",3);
        }

    }
    
    private void Stop()
    {
        playable.Stop();
        UI.SetActive(true);
        b=true;
    }
}
