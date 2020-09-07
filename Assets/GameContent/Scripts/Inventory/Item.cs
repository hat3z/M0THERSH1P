using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Item 
{

    public string itemName;
    public string displayName;
    public string gameObjectName;
    public string iconName;
    public enum itemTier { Normal, High, Rare};
    public itemTier ItemTier;
    public int quantity;
    public int priceToSell;
    public List<string> ingredientsList = new List<string>();
    
    public bool isCraftable()
    {
        if(ingredientsList.Count != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
