using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
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
        // List of items.
    }
}
