using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MS_PlayerProfile
{

    public PlayerShipControl ShipControl;

    public PlayerWeaponControl WeaponControl;

    public PlayerInventory Inventory;

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
        public float bulletForce;
    }

    [System.Serializable]
    public class PlayerInventory
    {
        // List of items.
    }
}
