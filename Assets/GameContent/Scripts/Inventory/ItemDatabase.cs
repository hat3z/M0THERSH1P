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
    //ITEMS HERE
    public List<Item> Items = new List<Item>();

    public Item GetItemByItemName(string _itemName)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if(Items[i].itemName == _itemName)
            {
                return Items[i];
            }
            else
            {
                Debug.Log("No Item with name: " + _itemName);
                return null;
            }
        }
        return null;
    }

    public void AddNewItemToDatabase()
    {
        Item newItem = new Item();
        Items.Add(newItem);
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
}
