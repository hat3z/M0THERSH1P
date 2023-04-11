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
    public bool AIEnabled;
    public float XPAmountToPlayer;
    MSV2_PlayerController playerController;

    [Header("Health / Damage / Moving settings")]
    public float MoveSpeed; // The base movespeed
    public float MaxHealth;
    public float MaxShield;
    public float MeleeDamageToPlayer;

    [Header("UI")]
    public Image HealthBar;

    // AIMING TO PLAYER
    private float aimSpeed = 1f;
    private Vector3 target;

    // Shooting control
    private float fireRate;

    private float health;
    private float followDistanceRandomAddon;
    private float minDist;
    private float waitingTime;
    private bool isMoving;
    private CircleCollider2D Collider;
    // Start is called before the first frame update
    void Start()
    {

    }

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
                target = new Vector3(Player.transform.position.x, Player.transform.position.y, 0);
                Vector3 diff = target - AIMTransform.transform.position;
                diff.Normalize();
                float rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + 90;
                AIMTransform.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
            }
        }
        #endregion AIM At Player
    }



    void OnTriggerEnter2D(Collider2D _other)
    {
        MSV2_BulletController bc;
        if(_other.gameObject.tag == "Player_WP1_Bullet")
        {
            GetDamage(playerController.GetWP1BaseDamage);
            bc = _other.GetComponent<MSV2_BulletController>();
            //StartCoroutine(bc.SetToDeadCooldown());
            _other.gameObject.SetActive(false);
        }
    }

    public void GetDamage(float _damageValue)
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

    void Shoot_Tier3()
    {

    }

    void RefreshHPBar()
    {
        HealthBar.fillAmount = health / MaxHealth;
    }

    void OnTriggerStay2D(Collider2D _other)
    {
        if(_other.gameObject.tag == "PlayerTag")
        {
            playerController.GetDamage(MeleeDamageToPlayer);
        }
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
