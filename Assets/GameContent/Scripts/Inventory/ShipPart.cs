using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipPart 
{
    public enum partType { DashEngine, Shield, SpeedBoost};
    public string ShipPartName;
    public string displayName;
    public string iconName;
    public partType PartType;
    public Item.itemTier ItemTier;
    public List<ShipPartModifier> ShipPartModifiers = new List<ShipPartModifier>();
    public int quantity;
    public int priceToSell;
    public List<string> ingredientsList = new List<string>();
    [System.Serializable]
    public class ShipPartModifier
    {
        public string modifierName;
        public float modifierValue;

        // Example: modifierName = "dashForce", modifierValue = PlayerDefaultDashForce + modifierValue.
    }

}
