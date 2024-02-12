using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject tiShi;
    public Image loadTiao;
    public GameObject load;

    private AsyncOperation ao;
    private bool a;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(a)
        {
            loadTiao.fillAmount=ao.progress/0.89f;
        }
        if (Input.anyKeyDown&&tiShi.activeSelf)
        {
            ao.allowSceneActivation=true;
        }
    }
    public void StartGame()
    {
        load.SetActive(true);
        StartCoroutine(LoadNextScene());
    }

    public void Quit()
    {
        Application.Quit();
    }



    IEnumerator LoadNextScene()
    {
        ao=SceneManager.LoadSceneAsync(1);

        a=true;

        ao.allowSceneActivation=false;
        while(ao.progress<0.9f)
        {             
            yield return null;
        }
        
        tiShi.SetActive(true);
    }
}
