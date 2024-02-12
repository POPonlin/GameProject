using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManger : IActorMangerInterface
{
     
    [HideInInspector] public List<EventCasterManger> iList = new List<EventCasterManger>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        EventCasterManger[] temp = other.GetComponents<EventCasterManger>();
        foreach (var item in temp)
        {
            if(!iList.Contains(item))
            {
                iList.Add(item);
            }    
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EventCasterManger[] temp = other.GetComponents<EventCasterManger>();
        foreach (var item in temp)
        {
            if(iList.Contains(item))
            {
                iList.Remove(item);
            }
        }
    }
}
