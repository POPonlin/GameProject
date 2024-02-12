using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PointUI : MonoBehaviour
{
    public Image point1;
    public Image point2;
    public Image point3;
    public Image point4;

    private Color origionColor;
    // Start is called before the first frame update
    void Start()
    {
        origionColor=point1.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePointColor()
    {
        point1.color=Color.red;
        point2.color=Color.red;
        point3.color=Color.red;
        point4.color=Color.red;

        StartCoroutine(BackBase());
    }

    IEnumerator BackBase()
    {
        yield return new WaitForSeconds(0.5f);

        point1.color=origionColor;
        point2.color=origionColor;
        point3.color=origionColor;
        point4.color=origionColor;
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}
