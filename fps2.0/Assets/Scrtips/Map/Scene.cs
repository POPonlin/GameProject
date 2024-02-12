using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class Scene : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject SceneCamera;
    public GameObject UI;

    public PlayableDirector playableDirector;

    private bool a;
    private bool b;
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
        if (!a)
        {
            if (other.CompareTag("Player"))
            {
                a=true;
                Invoke("OpenSceneCamera", 2);
            }
        }
        
    }    

    private void OpenSceneCamera()
    {
        playerCamera.SetActive(false);
        SceneCamera.SetActive(true);
        UI.SetActive(false);

        playableDirector.Play();
    }
}
