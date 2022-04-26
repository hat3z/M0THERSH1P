using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSV2_PlayerController : MonoBehaviour
{

    [Header("Character health")]
    public float maxHealth;
    private float health;
    public float currentXP;
    private float nextXPAmount = 50; // Step in xp system.
    public int currentLevel = 1;


    [Header("Character movement")]
    public float moveSpeed = 20.0f;

    public enum facing { Left, Right };
    [Header("Character AIM")]
    public facing Facing;
    public GameObject Crosshair;
    public GameObject PlayerHandsAIM;
    [Header("Weapons")]
    public GameObject WP1_BulletPrefab;
    public GameObject bulletStart;
    public float bulletSpeed = 80f;

    public PlayerModifiers PlayerModifiers;

    // Moving
    private Vector3 target;
    float acceleration = 0.9f;
    float speed = 0; 
    Rigidbody2D body;
    float horizontal;
    float vertical;

    // WP1
    public float fireRate = .18f;
    float nextFire = 0f;
    public float WP1_BaseDamage;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Cursor.visible = false;
        health = maxHealth;
    }

    void Update()
    {
        // AIM
        target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        Crosshair.transform.position = new Vector2(target.x, target.y);
        Vector3 diff = target - PlayerHandsAIM.transform.position;
        diff.Normalize();
        float rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90;
        PlayerHandsAIM.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        if(PlayerHandsAIM.transform.localEulerAngles.z < 180f)
        {
            Facing = facing.Left;
        }
        else
        {
            Facing = facing.Right;
        }

        // SHOOTING
        if (Input.GetMouseButton(0) && Time.time >nextFire)
        {
            nextFire = Time.time + (fireRate + PlayerModifiers.WP1_fireRateMod);
            float distance = diff.magnitude;
            Vector2 direction = diff / distance;
            direction.Normalize();
            Shoot_WP1(direction, rotationZ);
        }

        // MOVING
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if(vertical != 0 || horizontal != 0)
        {
            if (speed < (moveSpeed + PlayerModifiers.moveSpeedMod))
            {
                speed += acceleration * Time.deltaTime;
            }
        }
        else
        {
            speed = 0;
        }

        // XP Bar refresh
        MSV2_UIController.instance.XpLoadingBar.fillAmount = currentXP / nextXPAmount;
    }

    private void FixedUpdate()
    {
        body.AddForce(new Vector2(horizontal * moveSpeed, vertical * moveSpeed), ForceMode2D.Force);
    }
    void Shoot_WP1(Vector2 direction, float rotationZ)
    {
        GameObject b = MSV2_WorldController.instance.Sys_GetPlayerBaseBullet();
        b.SetActive(true);
        b.transform.position = bulletStart.transform.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }

    #region HEALTH MANAGEMENT

    public void GetDamage(float _damageValue)
    {
        if(health != 0)
        {
            if(health >= _damageValue)
            {
                health -= _damageValue;
            }
        }
    }

    public void AddToCurrentXP(float _amount)
    {
        currentXP += _amount;

        if(currentXP >= nextXPAmount)
        {
            currentXP = currentXP - nextXPAmount;
            currentLevel++;
            nextXPAmount += 100;
        }

    }

    public float GetBaseDamage
    {
        get
        {
            return WP1_BaseDamage + PlayerModifiers.WP1_damageMod;
        }
    }

    #endregion HEALTH MANAGEMENT

}
[System.Serializable]
public class PlayerModifiers
{
    public float healthAddMod;
    public float healthRegenMod;
    public float WP1_fireRateMod;
    public float WP1_damageMod;
    public float WP2_fireRateMod;
    public float moveSpeedMod;
    public float armorAreaMod;
}