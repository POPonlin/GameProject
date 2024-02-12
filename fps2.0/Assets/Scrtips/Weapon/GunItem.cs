using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunItem : BaseItem
{
    public enum FirearmsType
    {
        AK47,
        Glock,
    }

    public FirearmsType currentFirearmsType;
    public string armsName;

}
