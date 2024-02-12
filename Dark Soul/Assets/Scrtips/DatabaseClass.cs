using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defective.JSON;

public class DatabaseClass 
{
    private string weaponDatabaseFileName = "abc";

    private JSONObject weaponDatabase;
    public JSONObject WeaponDatabase { get => weaponDatabase; }

    public DatabaseClass()
    {
        TextAsset weaponContent = Resources.Load(weaponDatabaseFileName) as TextAsset;
        weaponDatabase = new JSONObject(weaponContent.text);
    }
    

}
