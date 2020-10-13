using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
[System.Serializable]
public class MS_PlayerProfile
{
    public PlayerShipControl ShipControl;

    public List<PlayerWeaponControl> WeaponControl = new List<PlayerWeaponControl>();

    public PlayerInventory Inventory;

    #region --- WEAPONS ---
    public PlayerWeaponControl GetActiveWeapon()
    {
        for (int i = 0; i < WeaponControl.Count; i++)
        {
            if(WeaponControl[i].isActive)
            {
                return WeaponControl[i];
            }
        }
        return null;
    }

    public GameObject GetWeaponActiveBulletPrefab()
    {
        return (GameObject)Resources.Load(Path.Combine("BulletPrefabs", GetActiveWeapon().bulletPrefabName));
    }

    #endregion

    #region --- ITEMS HANDLING (Add, Remove) ---

    public void AddItemToInventory(string _itemName, int _quantity)
    {
        Item itemToAdd = ItemDatabase.Instance.GetItemByItemName(_itemName);
        if (isItemInInventory(_itemName))
        {
            GetItemByNameInProfile(_itemName).quantity += _quantity;
        }
        else
        {
            itemToAdd.quantity = _quantity;
            Inventory.PlayerItemsList.Add(itemToAdd);
        }
    }

    bool isItemInInventory(string _itemName)
    {
        for (int i = 0; i < Inventory.PlayerItemsList.Count; i++)
        {
            if (Inventory.PlayerItemsList[i].itemName == _itemName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    Item GetItemByNameInProfile(string _itemName)
    {
        for (int i = 0; i < Inventory.PlayerItemsList.Count; i++)
        {
            if (Inventory.PlayerItemsList[i].itemName == _itemName)
            {
                return Inventory.PlayerItemsList[i];
            }
            else
            {
                Debug.Log("No Item with name in Inventory: " + _itemName);
                return null;
            }
        }
        return null;
    }

    public void RemoveItemFromPlayerInventory(string _itemName, int _quantity)
    {
        if(isItemInInventory(_itemName))
        {
            if(GetItemByNameInProfile(_itemName).quantity >=1)
            {
                GetItemByNameInProfile(_itemName).quantity -= _quantity;
            }
            else
            {
                Inventory.PlayerItemsList.Remove(GetItemByNameInProfile(_itemName));
            }
        }
    }

    #endregion



    [System.Serializable]
    public class PlayerShipControl
    {
        public float maxSpeed;
        public float maxRotationSpeed;
        public bool useMouseAim;
        public float verticalInputAcceleration;
        //public float horizontalInputAcceleration;
        public float velocityDrag = 1;
        public float rotationDrag = 1;

        // DASH
        public float dashForce;
        public float dashCooldown;
        bool canUseDash = true;


        public bool CanUseDash
        {
            get
            {
                return canUseDash;
            }
            set
            {
                canUseDash = value;
            }
        }


    }

    [System.Serializable]
    public class PlayerWeaponControl
    {
        public string weaponName;
        public float bulletForce;
        public string transformName;
        public string bulletPrefabName;
        public bool isActive;
    }

    [System.Serializable]
    public class PlayerInventory
    {
        public int PlayerHealth;
        public int PlayerExperience;
        public int PlayerFuel;
        public int PlayerMoney;
        public List<Item> PlayerItemsList = new List<Item>();
        public List<ShipPart> ShipPartsList = new List<ShipPart>();
    }
}
