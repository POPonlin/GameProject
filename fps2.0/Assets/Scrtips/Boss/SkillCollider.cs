using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCollider : MonoBehaviour
{
    [Header("ººƒ‹…À∫¶÷µ")]
    public float damage;

    private bool notAttack;
    private DataUI dataUI;

    public ParticleSystem w;
    // Start is called before the first frame update
    void Start()
    {
        dataUI=GameObject.FindGameObjectWithTag("Player").GetComponent<DataUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        w.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {            
            if (!notAttack)
            {
                notAttack=true;
              
                dataUI.Hurt(damage);

                gameObject.SetActive(false);
                w.Stop();
            }
        }
        else
        {
            StartCoroutine(s());
        }
    }

    IEnumerator s()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);

        if (w)
        {
            w.Stop();
        }
    }
}
