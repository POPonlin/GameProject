using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttack : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem boom;

    [Header("��ɫ��Χ")]
    public GameObject red;

    [Header("�����ͷ�ʱ��")]
    public float idleTime=1.5f;

    [Header("��ⷶΧ")]
    public GameObject collider1;


    // Start is called before the first frame update
    private void OnEnable()
    {        

    }


    public void ShowRed()
    {
        StartCoroutine(WaitBoom());
    }

    IEnumerator WaitBoom()
    {
        red.SetActive(true);        
        yield return new WaitForSeconds(idleTime);

        boom.Play();
        red.SetActive(false);
        collider1.SetActive(true);
    }
}
