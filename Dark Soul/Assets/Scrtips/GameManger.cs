using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    private static GameManger instance;
    private DatabaseClass weaponDB;
    // Start is called before the first frame update

    private void Awake()
    {
        CheckSingle();
        CheckSelfTag();
    }
    private void Start()
    {
        InitWeaponDB();
        //print(weaponDB.WeaponDatabase["Sword"]);
    }
   
    private void InitWeaponDB()
    {
        weaponDB = new DatabaseClass();
    }

    private void CheckSingle()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(this);
    }

    private void CheckSelfTag()
    {
        if (tag == "GM")
        {
            return;
        }
        Destroy(this);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
