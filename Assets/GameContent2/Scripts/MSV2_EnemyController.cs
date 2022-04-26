using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MSV2_EnemyController : MonoBehaviour
{
    public enum enemyType { Tier1, Tier2, Tier3};
    [Header("Common settings")]
    public enemyType EnemyType;
    public Transform Player;
    public bool AIEnabled;
    public float XPAmountToPlayer;
    MSV2_PlayerController playerController;

    [Header("Health / Damage / Moving settings")]
    public float moveSpeed;
    public float MaxHealth;
    public float DamageToTake;

    [Header("UI")]
    public Image HealthBar;

    private float health;
    private float minDist = 1;
    private CircleCollider2D Collider;

    // Start is called before the first frame update
    void Start()
    {
        Collider = GetComponent<CircleCollider2D>();
        health = MaxHealth;
        if(Player == null)
        {
            Player = GameObject.Find("PlayerControllert").transform;
        }
        playerController = Player.gameObject.GetComponent<MSV2_PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(AIEnabled)
        {
            if (Vector2.Distance(transform.position, Player.position) > minDist)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.position, moveSpeed * Time.deltaTime);
            }
        }

        RefreshHPBar();
    }

    void OnTriggerEnter2D(Collider2D _other)
    {
        MSV2_BulletController bc;
        if(_other.gameObject.tag == "Player_WP1_Bullet")
        {
            GetDamage(playerController.GetBaseDamage);
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

    void RefreshHPBar()
    {
        HealthBar.fillAmount = health / MaxHealth;
    }

    void OnTriggerStay2D(Collider2D _other)
    {
        if(_other.gameObject.tag == "PlayerTag")
        {
            playerController.GetDamage(DamageToTake);
        }
    }
}
