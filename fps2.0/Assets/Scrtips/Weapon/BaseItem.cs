using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    public enum ItemType
    {
        Firearms,
        others
    }

    public ItemType currentItemType;
    public int itemId;
}
