using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethod
{
    public static Transform DeepFind(this Transform parent,string targetName)
    {
        Transform temp = null;
        foreach (Transform item in parent)
        {
            if(item.name==targetName)
            {
                return item;
            }
            else
            {
                temp=DeepFind(item,targetName);
                if(temp!=null)
                {
                    return temp;
                }
            }
        }
        return null;
    }
}
