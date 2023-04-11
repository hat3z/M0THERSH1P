using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MSV2_DropItem : MonoBehaviour
{
    public bool GenerateItemOnEnable;
    private MSV2_ItemDatabase.DropItem Item;
    public SpriteRenderer IconImage;
    bool isItemSet;
    void OnEnable()
    {
        if(GenerateItemOnEnable)
        {
            SetRandomItem();
        }
    }

    public void SetRandomItem()
    {
        if(!isItemSet)
        {
            Item = MSV2_WorldController.instance.ItemDatabase.GetRandomItem();
            isItemSet = true;
        }
    }
}
