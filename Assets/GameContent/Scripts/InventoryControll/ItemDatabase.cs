using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Create new Item Database")]
public class ItemDatabase : ScriptableObject {

    private static ItemDatabase _instance;
    public static ItemDatabase Instance {
        get {
            if (_instance == null) {
                _instance = (ItemDatabase)Resources.Load("ItemDatabase");
            }

            return _instance;
        }
    }

    [SerializeField]
    // ITEMS LIST
    public List<Item> Items = new List<Item>();

    // SHIP PARTS LIST
    public List<ShipPart> ShipParts = new List<ShipPart>();

    // INGREDIENTS HELP: Example: metalScrap_2 => 2 metalScrap

    public Item GetItemByItemName(string _itemName)
    {
        Item result = new Item();
        for (int i = 0; i < Items.Count; i++)
        {
            if(Items[i].itemName == _itemName)
            {
                result = Items[i];
                return result;
            }
            else
            {
                Debug.Log("No Item with name: " + _itemName);
                return null;
            }
        }
        return null;
    }

    public List<Item> GetItemListByTier(Item.itemTier _tier, MS_WorldItem.itemType _itemType)
    {
        List<Item> result = new List<Item>();
        for (int i = 0; i < Items.Count; i++)
        {
            if(Items[i].ItemTier == _tier && Items[i].ParentItemType == _itemType)
            {
                result.Add(Items[i]);
            }
        }
        return result;
    }
    public void AddNewItemToDatabase()
    {
        Item newItem = new Item();
        Items.Add(newItem);
    }

    public void AddNewShipPartToDatabase()
    {
        ShipPart newSP = new ShipPart();
        ShipParts.Add(newSP);
    }

    public Sprite GetSpriteFromPath(string _path)
    {
        Sprite result =(Sprite) Resources.Load("ItemSprites/" + _path, typeof(Sprite));
        Debug.Log("<color=green>" + _path + "</color>");
        return result;
    }

    public GameObject GetGameObjectFromPath(string _path)
    {
        GameObject result = (GameObject)Resources.Load("ItemPrefabs/" + _path, typeof(GameObject));
        return result;
    }

    //public GameObject GetGameObjectFromItemName(string _itemname)
    //{
    //    for (int i = 0; i < Items.Count; i++)
    //    {
    //        if(Items[i].itemName == _itemname)
    //        {
    //            return GetGameObjectFromPath(Items[i].gameObjectNames);
    //        }
    //    }
    //    return null;
    //}

    public GameObject GetRandomGameObjectFromItemName(string _itemname)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].itemName == _itemname)
            {
                return GetGameObjectFromPath(Items[i].gameObjectNames[Random.Range(0,Items[i].gameObjectNames.Count)]);
            }
        }
        return null;
    }

}
