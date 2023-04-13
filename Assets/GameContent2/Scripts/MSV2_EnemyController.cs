using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class MSV2_EnemyController : MonoBehaviour
{
    public enum enemyType { Tier1, Tier2, Tier3, Tier4, Tier5};
    [Header("Common settings")]
    public enemyType EnemyType;
    Transform Player;
    public Transform AIMTransform;
    public Transform BulletStart;
    public float BulletSpeed;
    public bool AIEnabled;
    public bool ShootOnTarget; // If false, enemy shoot randomly
    public bool ShowDebugTargetLine;
    public float ViewDistance;
    public float XPAmountToPlayer;
    MSV2_PlayerController playerController;

    [Header("Health / Damage / Moving settings")]
    public float MoveSpeed; // The base movespeed
    public float MaxHealth;
    public float MaxShield;
    public float FireRate;
    public float RotateToPlayerSpeed;
    [Header("UI")]
    public Image HealthBar;

    // AIMING TO PLAYER
    private float aimSpeed = 1f;
    private Vector3 target;
    private float rotationZ;
    private float nextFire = 0f;
    // Shooting control


    private float health;
    private float followDistanceRandomAddon;
    private float minDist;
    private float waitingTime;
    private bool isMoving;
    private CircleCollider2D Collider;

    public void StartupInit()
    {
        Collider = GetComponent<CircleCollider2D>();
        health = MaxHealth;
        if (Player == null)
        {
            Player = GameObject.Find("PlayerController").transform;
        }
        playerController = Player.gameObject.GetComponent<MSV2_PlayerController>();
        waitingTime = 1f;
        switch (EnemyType)
        {
            case enemyType.Tier1:
                minDist = 1f;
                break;
            case enemyType.Tier2:
                minDist = 1f;
                break;
            case enemyType.Tier3:
                minDist = 20f;
                break;
            case enemyType.Tier4:
                minDist = 20f;
                break;
            case enemyType.Tier5:
                minDist = 20f;
                break;
            default:
                break;
        }
    }

    private void OnEnable()
    {
        isMoving = true;
        health = MaxHealth;
    }


    // Update is called once per frame
    void Update()
    {
        if (AIEnabled)
        {
            if (Vector2.Distance(transform.position, Player.position) > minDist)
            {
                if (isMoving)
                {
                    transform.position = Vector2.MoveTowards(transform.position, Player.position, MoveSpeed * Time.deltaTime);
                    Debug.Log("Move");
                }
            }
        }

        RefreshHPBar();

        #region AIM At Player
        if (EnemyType == enemyType.Tier3)
        {
            if (AIMTransform != null)
            {
                Vector2 difference = Player.position - AIMTransform.position;
                float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg + 90;
                Quaternion rot = Quaternion.Euler(0, 0, rotationZ);
                AIMTransform.rotation = Quaternion.Lerp(AIMTransform.rotation, rot, RotateToPlayerSpeed * Time.deltaTime);
                RaycastHit2D hit = Physics2D.Raycast(BulletStart.position, difference, ViewDistance);
                if (ShowDebugTargetLine)
                {
                    Debug.DrawRay(BulletStart.position, difference.normalized * ViewDistance, Color.red);
                }
                if(ShootOnTarget)
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.tag == "PlayerTag")
                        {
                            ShootProjectileOnTarget(difference);
                        }
                    }

                }
                else
                {
                    ShootProjectileStraight();
                }
            }
        }
        #endregion AIM At Player
    }

    void ShootProjectileOnTarget(Vector2 _difference)
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + FireRate;
            float distance = _difference.magnitude;
            Vector2 direction = _difference / distance;
            direction.Normalize();
            Shoot_Tier3(direction);
        }
    }

    void ShootProjectileStraight()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + FireRate;
            Vector2 direction =  BulletStart.transform.right;
            direction.Normalize();
            Shoot_Tier3(direction);
        }
    }

    void OnTriggerEnter2D(Collider2D _other)
    {
        if(_other.gameObject.tag == "Player_WP1_Bullet")
        {
            GetDamage(playerController.GetWP1BaseDamage);
            _other.gameObject.SetActive(false);
        }
    }

    void GetDamage(float _damageValue)
    {
        if (health != 0)
        {
            health -= _damageValue;
        }

        if (health <= 0)
        {
            gameObject.SetActive(false);
            // DIE
            playerController.AddToCurrentXP(XPAmountToPlayer);
        }
    }

    void Shoot_Tier3(Vector2 direction)
    {
        GameObject b = MSV2_WorldController.instance.Sys_GetTier3EnemyBullet();
        b.SetActive(true);
        b.transform.position = BulletStart.transform.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * BulletSpeed;
    }

    void RefreshHPBar()
    {
        HealthBar.fillAmount = health / MaxHealth;
    }


    public bool IsAlive()
    {
        if (health <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}
